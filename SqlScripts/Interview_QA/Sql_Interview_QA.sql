
--create database Interview
GO

use Interview

if object_id('tblEmployee', 'U') is not null
drop table tblEmployee


create table tblEmployee (
EmpName  varchar(50),
Salary	 INT
)

GO

insert into tblEmployee values('Henry', 100)
insert into tblEmployee values('Henry2', 300)
insert into tblEmployee values('Henry3', 200)
insert into tblEmployee values('Henry4', 150)
insert into tblEmployee values('Stella', 150)
insert into tblEmployee values('Masha', 200)
insert into tblEmployee values('Shelley', 300)

GO

select * from tblEmployee

--Solution 1 (better one)
select EmpName, Salary from tblEmployee M where 1 = (select count(*) from tblEmployee A where M.Salary <=  A.Salary)

--Solution 2
select EmpName, Salary from tblEmployee where Salary not in (select s.Salary from tblEmployee s, tblEmployee b where b.Salary > s.Salary)


-- select each employee and get how many other employees' salary more than he/she earns

select a.EmpName, count(b.EmpName) as 'Number of Employees who earn more than' from tblEmployee a
left join tblEmployee b 
on a.Salary < b.Salary 
group by a.EmpName


-- select employees who's salary more than average

select EmpName, salary from tblEmployee where Salary >= (select avg(salary) from tblEmployee) 

select avg(Salary) from tblEmployee


select * from tblEmployee a
where 3 >= (select COUNT(*) from tblEmployee b
where b.Salary >=  a.Salary)


select a.Salary, count(a.EmpName) as 'Number of Employees' from tblEmployee a
where 3 >= (select COUNT(*) from tblEmployee b
where b.Salary >=  a.Salary)
group by a.Salary

select top 3 a.Salary, count(a.EmpName) as 'Employees'  from tblEmployee a
group by a.Salary
order by a.Salary DESC



-------------------------
use AdventureWorksDW2012

select (FirstName + ' ' + LastName) as Name, BaseRate from DimEmployee where FirstName = 'Kevin' and LastName = 'Brown'

-- Find those BaseRate great than that of Kevin Brows's
select (a.FirstName + ' ' + a.LastName) as Name, a.BaseRate from DimEmployee a
where a.BaseRate > (select b.BaseRate from DimEmployee b where b.FirstName = 'Kevin' and b.LastName = 'Brown')
order by BaseRate ASC


-- Find max BaseRate in DB
select (a.FirstName + ' ' + a.LastName) as Name, a.BaseRate from DimEmployee a
where 1 = (select count(*) from DimEmployee b where b.BaseRate >= a.BaseRate)

-- Find top 5 BaseRate in DB
select (a.FirstName + ' ' + a.LastName) as Name, a.BaseRate from DimEmployee a
where 5 >= (select count(*) from DimEmployee b where b.BaseRate >= a.BaseRate) 
order by a.BaseRate desc

-- Find top 5 BaseRate in DB including duplicated BaseRates
select top 15 a.BaseRate, count(a.EmployeeKey) as 'Employees with same BaseRate' from DimEmployee a
group by a.BaseRate
order by a.BaseRate DESC


select (a.FirstName + ' ' + a.LastName) as Name, a.BaseRate from DimEmployee a
order by a.BaseRate desc
 

-- Find minmum BaseRate in DB
select (a.FirstName + ' ' + a.LastName) as Name, a.DepartmentName, a.BaseRate from DimEmployee a
where 0 = (select count(*) from DimEmployee b where b.BaseRate < a.BaseRate)

-- Max BaseRate Grouped by Departments
select DepartmentName, MAX(BaseRate) as 'Maxmum BaseRate' from DimEmployee 
group by DepartmentName

-- Min BaseRate Grouped by Departments
select DepartmentName, MIN(BaseRate) as 'Minimum BaseRate' from DimEmployee 
group by DepartmentName






-----

use Interview

if object_id('tblSales', 'U') is not null
drop table tblSales


create table tblSales (

CustomerName varchar(50),
ProductName  varchar(100),
Amount      Money,
VendorName   varchar(50)
)

GO



insert into tblSales values('Henry', 'Books', 119.0, 'Gosoftsolution')
insert into tblSales values('Henry', 'Pens', 302.0, 'Gosoftsolution')
insert into tblSales values('Stella', 'Perfume', 102.0, 'Gosoftsolution')
insert into tblSales values('Shelley', 'Bag', 203.45, 'Lottotry')
insert into tblSales values('Masha', 'Shoes', 100.24, 'Lottotry')

GO

select * from tblSales

GO

select	ROW_NUMBER() over (order by CustomerName) as OrderNumber,
	ROW_NUMBER() over (partition by VendorName order by VendorName) as VendorNumber,
	RANK() over (order by CustomerName) as CustomerNumber,
	DENSE_RANK() over (order by CustomerName) as CustomerNumber2,
	CustomerName,
	ProductName,
	Amount,
	VendorName
from 
	tblSales

	GO

-------

use Interview

if object_id('tblCutomer', 'U') is not null
drop table tblCustomer

GO

create table tblCustomer (
CustomerID	INT Identity(1, 1),
CustomerName varchar(50)
)

GO

use Interview
if object_id('tblAudit', 'U') is not null
drop table tblAudit

create table tblAudit (
LastUpdatedTime datetime,
OldCustomerName varchar(50),
NewCustomerName varchar(50)

)

GO

insert into tblCustomer values('Henry')
insert into tblCustomer values('Stella')
insert into tblCustomer values('Masha')

update tblCustomer set CustomerName='Shelley' where CustomerName='Masha'

delete from tblCustomer where CustomerName='Henry'


select * from tblCustomer
----

use Interview

if OBJECT_ID('Employee', 'U') is not null
drop table Employee

create table Employee (
EmployeeID	int Identity(1,1),
Name        varchar(50),
ManagerID   int 
)

insert into Employee values('Mike', 3)
insert into Employee values('Rob', 1)
insert into Employee values('Todd', NULL)
insert into Employee values('Ben', 1)
insert into Employee values('Sam', 1)

select * from Employee


-- Interview Question --

--SELECT E.Name AS Employee, ISNULL (M.Name, 'No Manager') AS Manager
--SELECT E.Name AS Employee, COALESCE (M.Name, 'No Manager') AS Manager
SELECT E.Name AS Employee, 
CASE
WHEN M.Name IS NULL THEN 'No Manager' ELSE M.Name END AS Manager
FROM Employee E
LEFT JOIN Employee M
ON E.ManagerID = M.EmployeeID

SELECT E.Name AS Employee, M.Name AS Manager
FROM Employee E
INNER JOIN Employee M
ON E.ManagerID = M.EmployeeID


SELECT E.Name AS Employee, M.Name AS Manager
FROM Employee E
CROSS JOIN Employee M



-- excercis

select * from Employee

select E.Name as Employee, 
coalesce(M.Name, 'No Manager') as Manager
from Employee E 
left join Employee M
on E.ManagerID = M.EmployeeID





-- Stuff() function
-- can be used to stuff a string into another string. 
-- It inserts the string at a given position, and deletes the number of characters 
-- specified from the original string

DECLARE @string1 VARCHAR(20) = 'Microsoft Server'
DECLARE @string2 VARCHAR(20) = 'SQL Server 2005'

SELECT      @string1 + ' -> ' + STUFF(@string1, 11, 0, 'SQL ')

        AS 'String 1',

        @string2 + ' -> ' + STUFF(@string2, 15, 1, '8 R2')

        AS 'String 2'

		
-- Replace() function: replaces all the specified characters with new characters.
DECLARE @string3 VARCHAR(35) = 'sql 2005, sql 2008, sql 2008 r2'
 
SELECT @string3, REPLACE(@string3,'sql','SQL')




-- DiffDate() and getdate()

SELECT DATEDIFF(day,'1958-01-04', getdate()) AS DiffDate

SELECT DATEDIFF(month,'1958-01-04', getdate()) AS DiffMonth

select getdate() - 10

-- add 1 year from now
select dateadd(yyyy, 1, getdate())


-- MONTH() function

select month(getdate())


declare @dat  datetime
declare @month int
select  @dat = convert(datetime, '1-4-1958', 101)
select month(@dat)

select year(@dat)
select day(@dat)

set @month = month(@dat)
select @month

--------------------------------------
use AdventureWorksDW2012

select (FirstName + ' ' + LastName) as Name, BaseRate from DimEmployee where FirstName = 'Kevin' and LastName = 'Brown'

-- Find those BaseRate great than that of Kevin Brows's
select (a.FirstName + ' ' + a.LastName) as Name, a.BaseRate from DimEmployee a
where a.BaseRate > (select b.BaseRate from DimEmployee b where b.FirstName = 'Kevin' and b.LastName = 'Brown')
order by BaseRate ASC


-- Find max BaseRate in DB
select (a.FirstName + ' ' + a.LastName) as Name, a.BaseRate from DimEmployee a
where 1 = (select count(*) from DimEmployee b where b.BaseRate >= a.BaseRate)

-- Find minmum BaseRate in DB
select (a.FirstName + ' ' + a.LastName) as Name, a.DepartmentName, a.BaseRate from DimEmployee a
where 0 = (select count(*) from DimEmployee b where b.BaseRate < a.BaseRate)

-- Max BaseRate Grouped by Departments
select DepartmentName, MAX(BaseRate) as 'Maxmum BaseRate' from DimEmployee 
group by DepartmentName

-- Min BaseRate Grouped by Departments
select DepartmentName, MIN(BaseRate) as 'Minimum BaseRate' from DimEmployee 
group by DepartmentName

-- day(), month(), year() functions
select (FirstName + ' ' + LastName) as Name, BirthDate from DimEmployee where month(BirthDate) > 7 and month(BirthDate) < 10

select (FirstName + ' ' + LastName) as Name, BirthDate from DimEmployee where day(BirthDate) > 7 and day(BirthDate) < 10

select (FirstName + ' ' + LastName) as Name, BirthDate from DimEmployee where year(BirthDate) > 1950 and year(BirthDate) < 1960