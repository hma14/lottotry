
USE Interview

IF EXISTS (SELECT name  FROM sys.objects WHERE name='tblInventory' AND type='U') 
	DROP TABLE tblInventory
	GO


CREATE TABLE tblInventory 

( 
 ProdNum INTEGER, ProdName VARCHAR(25), 
 Vendor VARCHAR(25), 
 Qty INTEGER, 
 UnitPrice MONEY, 
 StorageDate datetime 
); 

INSERT INTO tblInventory VALUES(3, 'Tree', 'Treetop Inc.', 33, 3.33, '10/03/2007'); 
INSERT INTO tblInventory VALUES(1, 'Wonder Bar', 'Cadbury', 11, 1.11, '10/01/2007'); 
INSERT INTO tblInventory VALUES(2, 'Cloth', 'Toucan Inc.', 22, 2.22, '10/02/2007'); 
INSERT INTO tblInventory VALUES(4, 'Fork', 'Silverware Inc.', 44, 4.44, '10/04/2007'); 
INSERT INTO tblInventory VALUES(4, 'Fork', 'Waterford Corp', 34, 5.31, '10/04/2007'); 
INSERT INTO tblInventory VALUES(5, 'Futon', 'Sleep Country',2,4.32, '10/05/2007'); 
INSERT INTO tblInventory VALUES(6, 'Coffee Crisp', 'Cadbury', 9, 1.32,'10/06/2007');

SELECT  AVG(UnitPrice) AS AvgPrice, COUNT(Qty) AS TotalProducts, -- NULLS are not counted 
	MAX(UnitPrice) AS MaxPrice,  MIN(UnitPrice) AS MinPrice,  SUM(UnitPrice) AS SumPrices 
FROM tblInventory;

SELECT Vendor, COUNT(ProdName) AS TotalProductsStocked FROM tblInventory GROUP BY Vendor;
SELECT ProdName, COUNT(ProdName) AS ProdCount, SUM(Qty) AS ProdQty FROM tblInventory GROUP BY ProdName;
SELECT Vendor, COUNT(ProdName) AS TotalProdName, SUM(QTY)AS Qty FROM tblInventory GROUP BY Vendor;

SELECT Vendor, MIN(UnitPrice) AS MinPrice FROM tblInventory GROUP BY Vendor HAVING COUNT(*) > 1 OR Vendor = 'Waterford Corp' OR Vendor = 'Silverware Inc.';

SELECT Vendor, COUNT(ProdName) TotalProductTypes, SUM(Qty) AS TotalItemsStocked FROM tblInventory GROUP BY Vendor HAVING SUM(Qty) > 20

SELECT Vendor, MIN(UnitPrice) AS MinPrice FROM tblInventory GROUP BY Vendor HAVING COUNT(*) > 1 OR Vendor = 'Waterford Corp' OR Vendor = 'Silverware Inc.';

SELECT * FROM tblInventory
