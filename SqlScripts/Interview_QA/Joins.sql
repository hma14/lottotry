use tempdb

CREATE TABLE tblFruit 

(
 FruitNum INTEGER, 
 Fruit VARCHAR(20) 
); 

 

INSERT INTO tblFruit VALUES(1, 'Apples'); 

INSERT INTO tblFruit VALUES(2, 'Oranges'); 

INSERT INTO tblFruit VALUES(4, 'Bananas'); 

 

CREATE TABLE tblFruitVendor 

( 
 VendorName VARCHAR(20), 
 FruitNum INTEGER, 
); 

INSERT INTO tblFruitVendor VALUES('Johnny Appleseed', 1); 

INSERT INTO tblFruitVendor VALUES('Bobs Produce', 1); 

INSERT INTO tblFruitVendor VALUES('Orangatang Oranges', 2); 

INSERT INTO tblFruitVendor VALUES('Bobs Produce', 2); 

INSERT INTO tblFruitVendor VALUES('Marys Grapes', 3);

-- inner join
SELECT f.FruitNum, f.Fruit, fv.VendorName FROM tblFruit f, tblFruitVendor fv WHERE f.FruitNum = fv.FruitNum;

--- left outer join
SELECT f.FruitNum, f.Fruit, fv.VendorName FROM tblFruit f
left join tblFruitVendor fv
on f.FruitNum = fv.FruitNum


--- right outer join
SELECT f.FruitNum, f.Fruit, fv.VendorName FROM tblFruit f
right join tblFruitVendor fv
on f.FruitNum = fv.FruitNum

--- full outer join
SELECT f.FruitNum, f.Fruit, fv.VendorName FROM tblFruit f
full join tblFruitVendor fv
on f.FruitNum = fv.FruitNum

-- sub-select
SELECT tblFruit.Fruit FROM tblFruit WHERE tblFruit.FruitNum = 
(SELECT tblFruitVendor.FruitNum FROM tblFruitVendor WHERE VendorName = 'Johnny Appleseed' );

SELECT ProdName, UnitPrice FROM tblInventory WHERE UnitPrice > (SELECT AVG(UnitPrice) FROM tblInventory);


select * from tblFruit
select * from tblFruitVendor


-- UNION
if object_id ('tblConvenienceStore', 'U') is not null
	drop table tblConvenienceStore

CREATE TABLE tblConvenienceStore(ProdName VARCHAR(30), ProdID INTEGER, ProdQty INTEGER); 

INSERT INTO tblConvenienceStore VALUES('Oranges', 1, 33); 
INSERT INTO tblConvenienceStore VALUES('Apples', 2, 43); 
INSERT INTO tblConvenienceStore VALUES('Lemons', 3, 42); 

 
if object_id('tblGrocer', 'U') is not null
	drop table tblGrocer

CREATE TABLE tblGrocer(ProdName VARCHAR(30), ProdID INTEGER, ProdQty INTEGER, Branch INTEGER); 

INSERT INTO tblGrocer VALUES('Grapes', 1, 31, 1); 
INSERT INTO tblGrocer VALUES('Apples', 2, 41, 1); 
INSERT INTO tblGrocer VALUES('Cantalope', 3, 32, 1); 

 
-- union all
SELECT ProdName FROM tblConvenienceStore 
UNION ALL SELECT ProdName FROM tblGrocer;

-- union 
SELECT ProdName FROM tblConvenienceStore 
UNION SELECT ProdName FROM tblGrocer;


select * from tblConvenienceStore
select * from tblGrocer