
use homework;
SET NOCOUNT ON

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='tblGeoInfo' AND type='u') DROP TABLE tblGeoInfo;
GO

CREATE TABLE tblGeoInfo
(
	City				VARCHAR(25),
	Region			VARCHAR(2),
	TtlPopulation		INT
);
INSERT INTO tblGeoInfo VALUES('Winnipeg', 'MB', 800000);
INSERT INTO tblGeoInfo VALUES('Vancouver', 'BC', 1000500);
INSERT INTO tblGeoInfo VALUES('Prince George', 'BC',380000);
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='tblBike' AND type='u') DROP TABLE tblBike;
GO

CREATE table tblBike(ProdNum INT, ProdName CHAR(20), BrandName CHAR(20))
GO
INSERT INTO tblBike VALUES(3, '10 speed bicycle', 'Sekine');
INSERT INTO tblBike VALUES(2, '5 speed bicycle',  'Sekine');
INSERT INTO tblBike VALUES(1, '5 speed bicycle',  'CCM');
INSERT INTO tblBike VALUES(6, 'Mountain',         'Killawhaia');
INSERT INTO tblBike VALUES(4, 'Mountain',         'MTax');
INSERT INTO tblBike VALUES(7, 'Elk',              'MTax');
INSERT INTO tblBike VALUES(5, '10 speed bicycle', 'Freespirit');
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='tblStationary' AND type='u') DROP TABLE tblStationary;
GO
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='tblOrderDetail' AND type='u') DROP TABLE tblOrderDetail;
GO


CREATE TABLE tblStationary
(
	ProductName		CHAR(20),
	ProdNum		INT,
	PackSize		CHAR(20),
	Price			MONEY
);
CREATE TABLE tblOrderDetail
(
	OrderNum		INT,
	ProdNum		INT,
	QTY			INT,
);
GO

INSERT INTO tblStationary VALUES('8x11 Paper 24lb',1,'500 sheets', 4.58);
INSERT INTO tblStationary VALUES('Sticky notes', 2, '12 pack', 6.01);
INSERT INTO tblStationary VALUES('Bic Blue Pen', 3, '10 pack', 3.21);
INSERT INTO tblStationary VALUES('Aylmer glue', 4, '70 ML', 2.79);
INSERT INTO tblOrderDetail VALUES(100, 1, 14);
INSERT INTO tblOrderDetail VALUES(100, 2, 1);
INSERT INTO tblOrderDetail VALUES(101, 1, 2);
INSERT INTO tblOrderDetail VALUES(101, 3, 5);



SELECT * FROM tblBike WHERE ProdName IN 
        (SELECT ProdName FROM tblBike WHERE BrandName = 'Sekine') AND (BrandName!='Sekine');

DECLARE	@message VARCHAR(25);				--declare
SET		@message =  'Welcome to Middleton!';	--assign	
SELECT	@message AS Greeting;				--view

DECLARE @TotalSeniors	FLOAT;
SET		@TotalSeniors	= 0;

SET		@TotalSeniors	=	@TotalSeniors + 0.2 *
		(SELECT SUM(TtlPopulation) FROM tblGeoInfo
		 WHERE Region	= 'BC');
SELECT @TotalSeniors AS SeniorPopulation;

DECLARE @AveragePopulation FLOAT;
SELECT  @AveragePopulation = AVG(TtlPopulation)  FROM tblGeoInfo;
SELECT  @AveragePopulation AS 'Average Population'

--DECLARE @AveragePopulation FLOAT;
SELECT  @AveragePopulation = AVG(TtlPopulation)  FROM tblGeoInfo;
	
IF @AveragePopulation < 50000 
	BEGIN
		SELECT 'Tax base is 20%' AS TaxBase, AVG(TtlPopulation) 
		AS AveragePopulation FROM tblGeoInfo;
	END
ELSE IF @AveragePopulation <1000000
	BEGIN
		SELECT 'Tax base is 15%' AS TaxBase, AVG(TtlPopulation) 
		AS AveragePopulation FROM tblGeoInfo;
	END
ELSE
	BEGIN
		SELECT 'Tax base is 10%' AS TaxBase, AVG(TtlPopulation) 
		AS AveragePopulation FROM tblGeoInfo;
	END



DECLARE @PopulationBC FLOAT;
DECLARE @PopulationMB FLOAT;
SELECT @PopulationBC = SUM(TtlPopulation) FROM tblGeoInfo WHERE Region='BC';
SELECT @PopulationMB = SUM(TtlPopulation) FROM tblGeoInfo WHERE Region='MB';

IF @PopulationMB > @PopulationBC
	BEGIN
		SELECT @PopulationMB AS 'Totle Population in MB';
	END
ELSE
	BEGIN
		SELECT @PopulationBC AS 'Totle Population in BC';;
	END

SELECT City, Region, ttlPoliceStations  = 
	CASE City
		WHEN 'Vancouver'  THEN			
			43
		WHEN 'Prince George' THEN
			8
		ELSE 
			5
	END
	FROM tblGeoInfo;
GO


/* Declare and initialize variables */
DECLARE		@year			INT;
DECLARE 	      @counter		INT;
DECLARE		@population		INT;

SET			@year			= 2009;
SET			@counter		= 0;

SET @population = (SELECT SUM(tblGeoInfo.TtlPopulation) FROM 
                              tblGeoInfo);
SELECT  @population AS CurrentPopulation, @year AS Year

WHILE @year <= 2012
	BEGIN	
		SET @population = (@population + @counter*0.01*@population) 
		SET @counter    =  @counter + 1; 
		SET @year	    =  @year + 1;
	END		

SELECT  @population AS ProjectedPopulation,
	  @year AS Year
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='triggerOrder' AND type='tr') DROP TRIGGER triggerOrder
GO

CREATE TRIGGER triggerOrder
	ON tblOrderDetail
	FOR INSERT, UPDATE, DELETE
	AS
	PRINT 'tblOrderDetail has been modified or the data has changed'
	GO --END TRIGGER
	
INSERT INTO tblOrderDetail	VALUES(101, 1, 4);
GO

SELECT * FROM tblOrderDetail
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='deleteProduct' AND type='tr') DROP TRIGGER deleteProduct
GO


CREATE TRIGGER deleteProduct
	ON tblStationary
	FOR DELETE
	AS
	IF 	(SELECT COUNT(*) FROM tblOrderDetail, deleted
			WHERE tblOrderDetail.ProdNum = deleted.ProdNum) > 0
		BEGIN
			ROLLBACK TRANSACTION
			PRINT 'You cannot delete a product with sales.'
		END
	ELSE
		PRINT 'Deletion successful. No sales for this item.'
	GO -- end trigger

--try to delete item number 2
--DELETE FROM tblStationary WHERE ProdNum = 2
--GO
--SELECT * FROM tblStationary;
	
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='ProductNumberCheck' AND type='tr') DROP TRIGGER ProductNumberCheck
GO

--create trigger to prevent products with negative 
--product numbers from being created
CREATE TRIGGER ProductNumberCheck
	ON tblStationary
	FOR INSERT
	AS
	--if product number is negative undo transaction
	IF (SELECT ProdNum FROM INSERTED)  < 0
		BEGIN
			PRINT 'Cannot have a product number of less than 0.'
			ROLLBACK TRANSACTION
		END
	--otherwise number is positive so permit entry
	ELSE
		BEGIN
			PRINT 'You have been successful.'
		END
	GO --end trigger

--INSERT INTO tblStationary VALUES('Printer cartridge', -5, 'Lexmark 1 unit', 82.39);
--SELECT * FROM tblStationary

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE name='NoPriceChange' AND type='tr') DROP TRIGGER NoPriceChange
GO

--create a trigger to prevent db users from changing the price
CREATE TRIGGER NoPriceChange
	ON tblStationary
	FOR UPDATE
	AS
	--if a price change is detected notify user who to speak with 
	--for approval
	IF UPDATE (Price)
		BEGIN
		SELECT * FROM Deleted;	--Get data before the UPDATE
		SELECT * FROM Inserted;	--Get data after the INSERT
		
		PRINT 'Contact marketing if you need to change the price.'
		ROLLBACK TRANSACTION
	END
	GO



--UPDATE tblStationary
--	SET Price = Price * 1.10
--	GO
	
--SELECT * FROM tblStationary
--	GO


EXEC sp_spaceused tblStationary;
--DROP INDEX idx_ProductName ON tblStationary;
--GO

-- Create a non-Clustered index
CREATE INDEX idx_ProductName ON tblStationary (ProductName)
SELECT * FROM tblStationary
GO

EXEC sp_spaceused tblStationary;

--DROP INDEX idx_Price ON tblStationary;
--GO

-- Create a clustered index
CREATE CLUSTERED INDEX idx_Price ON tblStationary(Price)
GO
SELECT * FROM tblStationary
GO

EXEC sp_spaceused tblStationary;

EXEC sp_helpindex tblStationary



--find out which indexes exist for the current table
EXEC sp_helpindex tblStationary

--drop existing indexes (assuming they exist)
DROP INDEX idx_Price ON tblStationary;
DROP INDEX idx_ProductName ON tblStationary;
GO

--insert some data to help demonstrate a composite clustered index
INSERT INTO tblStationary VALUES('Bic Blue Pen',6,'15 pack',3.89);
INSERT INTO tblStationary VALUES('Bic Blue Pen',7,'12 pack',3.5);
GO
CREATE CLUSTERED INDEX idx_NameSize
     ON tblStationary (ProductName, PackSize) 
GO
SELECT * from tblStationary
GO

EXEC sp_helpindex tblStationary;

DROP INDEX idx_NameSize ON tblStationary;
GO
CREATE CLUSTERED INDEX idx_NameSize 
     ON tblStationary (ProductName, ProdNum) 
GO

SELECT * FROM tblStationary;
GO

EXEC sp_helpindex tblStationary;

--drop composite clustered index if it exists
DROP INDEX idx_NameSize ON tblStationary

--create new non-clustered index
CREATE INDEX idx_NameSize 
     ON tblStationary (ProductName, PackSize) 
GO

SELECT * FROM tblStationary;
GO

EXEC sp_helpindex tblStationary;






SET NOCOUNT OFF