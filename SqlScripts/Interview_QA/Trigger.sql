
use Interview

if object_id('tblStationary', 'U') is not null
	DROP TABLE tblStationary; 

if object_id('tblOrderDetail', 'U') is not null
	DROP TABLE tblOrderDetail; 

CREATE TABLE tblStationary 
( 
 ProductName CHAR(20), 
 ProdNum INT, 
 PackSize CHAR(20), 
 Price MONEY 
); 

CREATE TABLE tblOrderDetail 
( 
 OrderNum INT, 
 ProdNum INT,
 QTY INT, 
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

GO
--Create trigger to print message for every update or insert 
select * from tblStationary
select * from tblOrderDetail

GO

CREATE TRIGGER triggerOrder 
 ON tblOrderDetail 
 FOR INSERT, UPDATE, DELETE 
 AS 
 PRINT 'tblOrderDetail has been modified or the data has changed' 
 GO --END TRIGGER 


--Insert data into the table 

INSERT INTO tblOrderDetail VALUES(101, 1, 4); 

GO 

 

--Retreive the data 
SELECT * FROM tblOrderDetail 

GO

--create trigger to check if other tables use a certain product 
--when deleting the product from the tblStationary 

CREATE TRIGGER deleteProduct 
 ON tblStationary 
 FOR DELETE 
 AS 

 IF (SELECT COUNT(*) FROM tblOrderDetail, deleted 
	 WHERE tblOrderDetail.ProdNum = deleted.ProdNum) > 0 

	 BEGIN 
		ROLLBACK TRANSACTION 
		PRINT 'You cannot delete a product with sales.' 
	 END 
 ELSE 
	PRINT 'Deletion successful. No sales for this item.' 

 GO -- end trigger 

 select * from tblOrderDetail
 select * from tblStationary

--try to delete item number 2 
DELETE FROM tblStationary WHERE ProdNum = 4

GO 

--create trigger to prevent products with negative 
--product numbers from being created 

CREATE TRIGGER ProductNumberCheck 
 ON tblStationary 
 FOR INSERT 
 AS 

 --if product number is negative undo transaction 
 IF (SELECT ProdNum FROM INSERTED) < 0 

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

 

INSERT INTO tblStationary VALUES('Printer cartridge', -5, 'Lexmark 1 unit', 82.39); 

--create a trigger to prevent db users from changing the price 
GO


--Drop Trigger first
IF EXISTS (SELECT name FROM sys.objects WHERE name = 'NoPriceChange' AND type = 'TR')
	DROP TRIGGER NoPriceChange
GO

CREATE TRIGGER NoPriceChange 
 ON tblStationary 
 AFTER UPDATE 
 AS 

 --if a price change is detected notify user who to speak with for approval 
 IF UPDATE (Price) 
	 BEGIN 
		 SELECT * FROM Deleted; --Get data before the UPDATE 
		 SELECT * FROM Inserted; --Get data after the INSERT
 
		 PRINT 'Contact marketing if you need to change the price.' 
		 ROLLBACK TRANSACTION 
		
	 END 

 GO 


UPDATE tblStationary SET Price = Price * 1.10 

 GO

SELECT * FROM tblStationary

-- Index

--Creating a Non-Clustered Index. If we were to create an index, and select from the table 

EXEC sp_spaceused tblStationary

drop index idx_ProductName on tblStationary
CREATE INDEX idx_ProductName ON tblStationary (ProductName) 
--CREATE INDEX idx_ProductName ON tblStationary (Price) 

SELECT * FROM tblStationary

EXEC sp_spaceused tblStationary

SELECT * FROM tblStationary
GO

--Creating a Clustered Index

DROP INDEX idx_Price ON tblStationary

CREATE CLUSTERED INDEX idx_Price ON tblStationary(ProductName) 

GO 

exec sp_spaceused tblStationary

SELECT * FROM tblStationary


-- display indexes
EXEC sp_helpindex tblStationary

--drop existing indexes (assuming they exist) 
DROP INDEX idx_Price ON tblStationary; 
DROP INDEX idx_ProductName ON tblStationary; 

GO 

--insert some data to help demonstrate a composite clustered index 
INSERT INTO tblStationary VALUES('Bic Blue Pen',6,'15 pack',3.89); 
INSERT INTO tblStationary VALUES('Bic Blue Pen',7,'12 pack',3.5); 

GO 

CREATE INDEX idx_NameSize 

 ON tblStationary (ProductName, PackSize) 

GO 

exec sp_helpindex tblStationary


SELECT * from tblStationary 

exec sp_spaceused tblStationary
GO

drop index idx_NameSize on tblStationary
GO
CREATE CLUSTERED INDEX idx_NameSize ON tblStationary (ProductName, ProdNum)
GO

EXEC sp_helpindex tblStationary


-- Creating a Composite Non-Clustered Indexes
DROP INDEX idx_NameSize ON tblStationary 

--create new non-clustered index 
CREATE INDEX idx_NameSize ON tblStationary (ProductName, PackSize) 

GO