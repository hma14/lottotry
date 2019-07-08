
use Interview;

set nocount on

if exists (select name from sys.objects where name='tblGreaterNumber' and type='u') 
	drop table tblGreaterNumber;
go

create table tblGreaterNumber (
	num	int
)


go

insert into tblGreaterNumber values(5);
insert into tblGreaterNumber values(23);
insert into tblGreaterNumber values(-6);
insert into tblGreaterNumber values(7);

go

set nocount off


select * from tblGreaterNumber

--Solution 1
select num from tblGreaterNumber where num not in (select S.num from tblGreaterNumber S, tblGreaterNumber  L where L.num > S.num)

--Solution 2 (better one)
select M.num from tblGreaterNumber M where 1 = (select count(*) from tblGreaterNumber A where A.num >= M.num)



select count(*) from tblGreaterNumber A where A.num >= 7