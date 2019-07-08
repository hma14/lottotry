
use homework;

--select * from tblGreaterNumber 

--select Less.num as Less, Greater.num as Greater from tblGreaterNumber as Greater, tblGreaterNumber as Less
--where Greater.num > Less.num 
--where Less.num < Greater.num  

select distinct num from tblGreaterNumber 
where num not in (select Less.num from tblGreaterNumber as Greater, tblGreaterNumber as Less
where Greater.num > Less.num)


use homework;

select * from tblEmployee 

select m.employeeName as managerName, e.employeeName  
from tblEmployee as m, tblEmployee as e
where m.employeeID <> m.managerID 
and m.employeeID in (select managerID from tblEmployee) 
and e.employeeID not in (select managerID from tblEmployee)



use homework;

select B.num from tblGreaterNumber B 
where 1 = (select count(*) from tblGreaterNumber S 
where B.num >= S.num)
