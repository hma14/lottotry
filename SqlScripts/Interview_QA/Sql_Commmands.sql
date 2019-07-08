-- List of All Tables in All Databases

if object_ID('TempDB..#AllTables','U') IS NOT NULL drop table #AllTables
CREATE TABLE #AllTables ([DB Name] sysname, [Schema Name] nvarchar(128) NULL, [Table Name] sysname, create_date datetime, modify_date datetime)
 
DECLARE @SQL NVARCHAR(MAX)
 
SELECT @SQL = COALESCE(@SQL,'') + 'USE ' + quotename(name) + '
insert into #AllTables
select ' + QUOTENAME(name,'''') + ' as [DB Name], schema_name(schema_id) as [Table Schema], [Name] as [Table Name], Create_Date, Modify_Date
from ' +
QUOTENAME(Name) + '.sys.Tables;' FROM sys.databases
ORDER BY name
--print @SQL
EXECUTE(@SQL)
select * from #AllTables order by [DB Name]


-- List of all Stored Procedures in All Databases

if object_ID('TempDB..#SPList', 'U') IS NOT NULL drop table #SPList
create table #SPList ([DB Name] sysname, [SP Name] sysname, create_date datetime, modify_date datetime)
 
declare @SQL nvarchar(max)
set @SQL = ''
select @SQL = @SQL + ' insert into #SPList select ' + QUOTENAME(name, '''') + ', name, create_date, modify_date
from ' + QUOTENAME(name) + '.sys.procedures' from sys.databases
 
print @SQL
execute (@SQL)
 
select * from #SPList order by [DB Name], [SP Name]


--Indexes in all databases with their usage

declare @SQL nvarchar(max)
SET @SQL = ''
if object_id('tempdb..#Result','U') IS not NULL
 drop table #Result
create table #Result (DBName sysname, TableName Sysname, IndexName sysname, Usage bigint)
 
select @SQL = coalesce(@SQL,'') + CHAR(13) + CHAR(10) + ' use ' + QUOTENAME([Name]) + ';
insert into #Result select ' + quotename([Name],'''') + ' as DbName,
object_name(i.object_id) as tablename,  i.name as indexname,
s.user_seeks + s.user_scans + s.user_lookups + s.user_updates as usage
from sys.indexes i  
inner join sys.dm_db_index_usage_stats s        
on s.object_id = i.object_id                    
and s.index_id = i.index_id              
and s.database_id = db_id()
where objectproperty(i.object_id, ''IsUserTable'') = 1  
and i.index_id > 0
order by usage;' from sys.databases
print (@SQL)
execute (@SQL)
select * from #Result order by [DbName],[Usage]
drop table #Result

--Indexes in all databases with their physical stats

declare @SQL nvarchar(max)
set @SQL = ''
select @SQL = @SQL +
'Select ' + quotename(name,'''') + ' as [DB Name],
object_Name(PS.Object_ID,' + convert(varchar(10),database_id) + ') as [Object],
I.Name as [Index Name], PS.Partition_Number, PS.Index_Type_Desc,
PS.alloc_unit_type_desc,    PS.index_depth, PS.index_level,
PS.avg_fragmentation_in_percent,    PS.fragment_count,  PS.avg_fragment_size_in_pages,
PS.page_count,  PS.avg_page_space_used_in_percent,  PS.record_count,   
PS.ghost_record_count,  PS.version_ghost_record_count,
PS.min_record_size_in_bytes,    PS.max_record_size_in_bytes,    PS.avg_record_size_in_bytes,
PS.forwarded_record_count,  PS.compressed_page_count
from ' + quotename(name) + '.sys.dm_db_index_physical_stats(' +
convert(varchar(10),database_id) + ', NULL, NULL, NULL, NULL) PS
INNER JOIN ' + quotename(name) +
'.sys.Indexes I on PS.Object_ID = I.Object_ID and PS.Index_ID = I.Index_ID '
+ CHAR(13)
 
 from sys.databases where state_desc = 'ONLINE'
 
print @SQL
execute(@SQL)


--Backup All Databases with Compression (SQL 2008)

--This script will backup all databases (using compression):

Declare @ToExecute VarChar(8000)
 
Select @ToExecute = Coalesce(@ToExecute + 'Backup Database ' + quotename([Name]) +
' To Disk = ''K:\lottotry_stuff\AllDatabaseBackup\' + [Name] + '.bak''
WITH NOFORMAT, NOINIT,  
SKIP, NOREWIND, NOUNLOAD, STATS = 10' + char(13),'')
 
From sys.databases
 
Where [Name] Not In ('tempdb') and databasepropertyex ([Name],'Status') = 'online'
 
Execute (@ToExecute)
 
print @ToExecute

--Database Files Sizes in All Databases and used space

--Note, that this script assumes that database files have the same name as the database itself. 
--If this is not true, this script will not return correct result.

if object_ID('tempdb..#Test', 'U') IS NOT NULL 
	drop table #Test
create table #Test (DbName sysname, TotalSize decimal(20,2), Used decimal(20,2), [free space percentage] decimal(20,2))
 
declare @SQL nvarchar(max)
select @SQL = coalesce(@SQL,'') +
'USE ' + QUOTENAME(Name) + '

insert into #Test
select DB.name, ssf.size*8 as total,
FILEPROPERTY (AF.name, ''spaceused'')*8 as used,
((ssf.size*8) - (FILEPROPERTY (AF.name, ''spaceused'')*8))*100/(ssf.size*8) as [free space percentage]
from sys.sysALTfiles AF
inner join sys.sysfiles ssf on ssf.name=AF.name COLLATE SQL_Latin1_General_CP1_CI_AS
INNER JOIN sys.databases DB ON AF.dbid=DB.database_id
where ssf.groupid<>1' from sys.databases
 
print @SQL
execute(@SQL)
 
select * from #Test order by DbName


--Record Count in every table in a database

use lottotry
DECLARE  @DynamicSQL NVARCHAR(MAX)
 
SELECT   @DynamicSQL = COALESCE(@DynamicSQL + CHAR(13) + ' UNION ALL ' + CHAR(13),
                                '') +
                                'SELECT ' + quotename(table_schema,'''') + ' as [Schema Name], ' +
                                QUOTENAME(TABLE_NAME,'''') +
                                ' as [Table Name], COUNT(*) AS [Records Count] FROM ' +
                                quotename(Table_schema) + '.' + QUOTENAME(TABLE_NAME)
FROM     INFORMATION_SCHEMA.TABLES
ORDER BY TABLE_NAME
 
print (@DynamicSQL) -- we may want to use PRINT to debug the SQL
EXEC( @DynamicSQL)