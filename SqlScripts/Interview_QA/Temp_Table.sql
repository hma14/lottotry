use tempdb

if object_id('tblStationary', 'U') is not null
	drop table tblStationary

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


--select data into temp table 

SELECT OrderNum, ProductName, QTY, Price, Price * Qty AS Total 
 INTO #temptable 
 FROM tblStationary, tblOrderDetail 
 WHERE tblStationary.ProdNum = tblOrderDetail.ProdNum 

 

--add column to temp table 

ALTER TABLE #temptable ADD afterTax MONEY; 

 

--insert values into temp table 
UPDATE #temptable SET afterTax = Total * 1.07 

 

--return results from temp table 
SELECT * FROM #temptable 

 

--remove the temp table and all data in it 
DROP TABLE #temptable 

GO

SELECT od.OrderNum, SUM(s.Price*QTY) AS OrderTotal 
INTO #temptable 
FROM tblStationary s, tblOrderDetail od 
WHERE od.ProdNum = s.ProdNum GROUP BY od.OrderNum 

ALTER TABLE #temptable ADD ProductType VARCHAR(25); 

UPDATE #temptable SET ProductType = 'School Products'; 
SELECT * FROM #temptable; 

 

DROP TABLE #temptable;

select * from tblOrderDetail


-- View example

CREATE VIEW ExpensiveProducts AS 
 SELECT OrderNum, ProductName, QTY, Price, Price * Qty AS Total 
 FROM tblStationary, tblOrderDetail 
 WHERE tblStationary.ProdNum = tblOrderDetail.ProdNum AND Price >= 4.00 

 GO --end view 

 --retrieving data from a view is just like retrieving data from a table 
SELECT * FROM ExpensiveProducts
 

-- Stored Procedure that uses Temp Table
drop table #temptable

CREATE PROCEDURE spTempTable AS 

 --store results in temp table 
 SELECT * INTO #temptable FROM tblStationary 

 --add column to temp table 
 ALTER TABLE #temptable ADD afterTax MONEY; 

 --insert values into temp table 
 UPDATE #temptable SET afterTax = Price * 1.07 

 --retrieve results from temp table 
 SELECT * FROM #temptable 

 --delete temp table 
 DROP TABLE #temptable 

GO 

--run stored proc 
EXEC spTempTable 

GO

-- CAST
CREATE TABLE tblText(myText TEXT); 

INSERT INTO tblText VALUES('Bah bah blacksheep have you any wool? Yes sir yes sir three bags full.'); 

-- long text got trunked after cast to varchar(19) to myText
SELECT CAST(myText AS VARCHAR(19)) FROM tblText;

-- CASE Example

SELECT City, Region, ttlPoliceStations = 
 CASE City 
	WHEN 'Vancouver' THEN 43 

	WHEN 'Prince George' THEN 8 

	ELSE 
		5 

	END 

FROM tblGeoInfo; 

GO

-- While Loops

/* Declare and initialize variables */ 

DECLARE @year INT; 
DECLARE @counter INT; 
DECLARE @population INT; 

SET @year = 2009; 
SET @counter = 0; 
SET @population = (SELECT SUM(tblGeoInfo.TtlPopulation) FROM tblGeoInfo); 
SELECT @population AS CurrentPopulation, @year AS Year 

WHILE @year <= 2012 
 BEGIN 
	 SET @population = (@population + @counter*0.01*@population) 
	 SET @counter = @counter + 1; 
	 SET @year = @year + 1; 
 END 

SELECT @population AS ProjectedPopulation, @year AS Year