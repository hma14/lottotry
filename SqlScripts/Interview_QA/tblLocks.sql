
--CREATE DATABASE homework;
USE homework;

SET NOCOUNT ON

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='tblLocks' AND TYPE='u') DROP TABLE tblLocks; 

CREATE TABLE tblLocks (col1 INT)
	GO
INSERT INTO tblLocks VALUES (1)
INSERT INTO tblLocks VALUES (3)
INSERT INTO tblLocks VALUES (5)
	GO
	
	
	
-- Read Uncommitted (Dirty Read)
-- Lets other applications read and update uncommitted data.

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='spUpdateTblLocks_DirtyRead' AND TYPE='p') 
	DROP PROCEDURE spUpdateTblLocks_DirtyRead; 
GO

CREATE PROCEDURE spUpdateTblLocks_DirtyRead AS
	SET TRANSACTION ISOLATION LEVEL
	READ UNCOMMITTED		--permits dirty reads
	BEGIN TRANSACTION 
		UPDATE tblLocks
		SET    col1 = 7 
		WHERE  col1 = 5

	IF @@ERROR <> 0
		BEGIN
			ROLLBACK	--undo transaction
			RETURN
		END
	ELSE
		BEGIN
			COMMIT	--commit transaction
		END
GO
	
	
--Read Committed (Non-repeatable reads)
--•	Is the default isolation level in SQL Server. 
--•	Lets other applications read and update committed data.
	
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='spUpdateTblLocks_ReadCommitted' AND TYPE='p') 
	DROP PROCEDURE spUpdateTblLocks_ReadCommitted; 
GO

CREATE PROCEDURE spUpdateTblLocks_ReadCommitted AS
	SET TRANSACTION ISOLATION LEVEL
	READ COMMITTED		--permits reads & updates to committed data
	BEGIN TRANSACTION 
		UPDATE tblLocks
		SET    col1 = 7 
		WHERE  col1 = 5

	IF @@ERROR <> 0
		BEGIN
			ROLLBACK	--undo transaction
			RETURN
		END
	ELSE
		BEGIN
			COMMIT	--commit transaction
		END
GO

--Repeatable Read
--•	Prevents other applications from updating data.

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='spUpdateTblLocks_RepeatableRead' AND TYPE='p') 
	DROP PROCEDURE spUpdateTblLocks_RepeatableRead; 
GO

CREATE PROCEDURE spUpdateTblLocks_RepeatableRead AS
	SET TRANSACTION ISOLATION LEVEL
	REPEATABLE READ		--allows reads but not updates to uncommitted data
	BEGIN TRANSACTION 
		UPDATE tblLocks
		SET    col1 = 7 
		WHERE  col1 = 5

	IF @@ERROR <> 0
		BEGIN
			ROLLBACK	--undo transaction
			RETURN
		END
	ELSE
		BEGIN
			COMMIT	--commit transaction
		END
GO



--Repeatable Read
--•	Prevents other applications from updating data.

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='spUpdateTblLocks_ReadOnly' AND TYPE='p') 
	DROP PROCEDURE spUpdateTblLocks_ReadOnly; 
GO

CREATE PROCEDURE spUpdateTblLocks_ReadOnly AS
	SET TRANSACTION ISOLATION LEVEL
	REPEATABLE READ		--allows reads but not updates to uncommitted data
	BEGIN TRANSACTION 
		UPDATE tblLocks
		SET    col1 = 7 
		WHERE  col1 = 5

	IF @@ERROR <> 0
		BEGIN
			ROLLBACK	--undo transaction
			RETURN
		END
	ELSE
		BEGIN
			COMMIT	--commit transaction
		END
GO



--Serializable
--•	Prevents others from updating or inserting rows in the data set till the transaction is complete.

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME='spUpdateTblLocks_Serializable' AND TYPE='p') 
	DROP PROCEDURE spUpdateTblLocks_Serializable; 
GO

CREATE PROCEDURE spUpdateTblLocks_Serializable AS
	SET TRANSACTION ISOLATION LEVEL
	SERIALIZABLE--allows reads of committed data only
			--permits no updates 
			--other transactions can't insert rows where 
			--key values that would fall in the range of keys
			--read by any statements in the current transaction
	BEGIN TRANSACTION 
		UPDATE tblLocks
		SET    col1 = 7 
		WHERE  col1 = 5

	IF @@ERROR <> 0
		BEGIN
			ROLLBACK	--undo transaction
			RETURN
		END
	ELSE
		BEGIN
			COMMIT	--commit transaction
		END
GO

SELECT * FROM tblLocks;
EXEC spUpdateTblLocks_DirtyRead;
EXEC spUpdateTblLocks_ReadCommitted;
EXEC spUpdateTblLocks_RepeatableRead
EXEC spUpdateTblLocks_ReadOnly;
SELECT * FROM tblLocks;
	


SET NOCOUNT OFF
