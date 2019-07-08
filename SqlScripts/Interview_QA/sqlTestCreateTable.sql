


use homework;

set nocount on

if exists (select * from sysobjects where name='tblEmployee' and type='u') drop table tblEmployee;
go

create table tblEmployee (
	employeeID	int,
	employeeName varchar(25),
	managerID	int
)


go

insert into tblEmployee values(61, 'Henry', 61);
insert into tblEmployee values(62, 'Smith', 61);
insert into tblEmployee values(63, 'John', 62);
insert into tblEmployee values(64, 'Shelley', 62);
insert into tblEmployee values(65, 'Stella', 62);

go

select * from tblEmployee 

select m.employeeName as 'Manager Name', e.employeeName as 'Employee Name'  from tblEmployee as m, tblEmployee as e
where 



m.employeeID <> m.managerID and m.employeeID in (select managerID from tblEmployee)
and e.employeeID not in (select managerID from tblEmployee)

set nocount off


