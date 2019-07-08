USE PUBS 

--login for SQL Server but not database. 
CREATE LOGIN richard WITH PASSWORD = '123456' 

--associate user with login. User is granted access to database. 
CREATE USER hr_user FOR LOGIN richard 

--create role to store a set of permissions 
CREATE ROLE hr_role 

GO 

--set permissions for the role 
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON employee TO hr_role 
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON jobs TO hr_role

--bind role with user 
EXEC sp_addrolemember 'hr_role', 'hr_user' 

GO 

 


