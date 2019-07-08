use lottotry

SET NOCOUNT ON
GO

--SET DATEFORMAT mdy

--USE master

--declare @dttm varchar(55)
--select  @dttm=convert(varchar,getdate(),113)
--raiserror('Beginning Create_Lotto_Table.sql at %s ....',1,1,@dttm) with nowait

--GO

--if exists (select * from sysdatabases where name='Lotto')
--begin
--  raiserror('Dropping existing Lotto database ....',0,1)
--  DROP database Lotto
--end
--GO

--CHECKPOINT
--go

--raiserror('Creating Lotto database....',0,1)
--go
--/*
--   Use default size with autogrow
--*/

--CREATE DATABASE Lotto
--GO

--CREATE DATABASE lottotry
USE lottotry


-- Drop Procedures
IF OBJECT_ID('GetLastRow','P') IS NOT NULL
	DROP PROCEDURE GetLastRow
	
IF OBJECT_ID('GetLastDrawDate','P') IS NOT NULL
	DROP PROCEDURE GetLastDrawDate

IF OBJECT_ID('SelectAll','P') IS NOT NULL
	DROP PROCEDURE SelectAll
	
IF OBJECT_ID('SelectAllOnRangeOfDrawNo','P') IS NOT NULL
	DROP PROCEDURE SelectAllOnRangeOfDrawNo

IF OBJECT_ID('LoginAuth','P') IS NOT NULL
	DROP PROCEDURE LoginAuth
IF OBJECT_ID('GetTargetDraw','P') IS NOT NULL
	DROP PROCEDURE GetTargetDraw

IF OBJECT_ID('spSelectAllProvinceState','P') IS NOT NULL
	DROP PROCEDURE spSelectAllProvinceState
IF OBJECT_ID('spSelectAllCountry','P') IS NOT NULL
	DROP PROCEDURE spSelectAllCountry
	
IF OBJECT_ID('spResetUserExpiryDate','P') IS NOT NULL
	DROP PROCEDURE spResetUserExpiryDate
	
IF OBJECT_ID('spAllDrawNumbers','P') IS NOT NULL
	DROP PROCEDURE spAllDrawNumbers

IF OBJECT_ID('spLoginAuth','P') IS NOT NULL
	DROP PROCEDURE spLoginAuth
	
IF OBJECT_ID('spGetClientCloseExpired','P') IS NOT NULL
	DROP PROCEDURE spGetClientCloseExpired
	
IF OBJECT_ID('spUpdateClientStatus','P') IS NOT NULL
	DROP PROCEDURE spUpdateClientStatus

IF OBJECT_ID('spGetClientExpired','P') IS NOT NULL
	DROP PROCEDURE spGetClientExpired

IF OBJECT_ID('spIsClientExpired','P') IS NOT NULL
	DROP PROCEDURE spIsClientExpired
	
IF OBJECT_ID('spRemoveExpiredClient','P') IS NOT NULL
	DROP PROCEDURE spRemoveExpiredClient
	
IF OBJECT_ID('spRemoveClient','P') IS NOT NULL
	DROP PROCEDURE spRemoveClient
	
IF OBJECT_ID('spGetUserRole','P') IS NOT NULL
	DROP PROCEDURE spGetUserRole
	
IF OBJECT_ID('spFindPassword','P') IS NOT NULL
	DROP PROCEDURE spFindPassword
	
IF OBJECT_ID('spUpdatePassword','P') IS NOT NULL
	DROP PROCEDURE spUpdatePassword
	
IF OBJECT_ID('spRetrieveUserProfile','P') IS NOT NULL
	DROP PROCEDURE spRetrieveUserProfile
	
IF OBJECT_ID('spGetUserFullName','P') IS NOT NULL
	DROP PROCEDURE spGetUserFullName
	
IF OBJECT_ID('spRegisterUser','P') IS NOT NULL
	DROP PROCEDURE spRegisterUser
	
	
IF OBJECT_ID('spUpdateUser','P') IS NOT NULL
	DROP PROCEDURE spUpdateUser
	
IF OBJECT_ID('spUpdateUserInfo','P') IS NOT NULL
	DROP PROCEDURE spUpdateUserInfo	
	
IF OBJECT_ID('spCountUsers','P') IS NOT NULL
	DROP PROCEDURE spCountUsers
	
IF OBJECT_ID('spGetData','P') IS NOT NULL
	DROP PROCEDURE spGetData
	
IF OBJECT_ID('spPurchaseTransaction','P') IS NOT NULL
	DROP PROCEDURE spPurchaseTransaction
	
IF OBJECT_ID('spRefundTransaction','P') IS NOT NULL
	DROP PROCEDURE spRefundTransaction
	
IF OBJECT_ID('spGetAmount','P') IS NOT NULL
	DROP PROCEDURE spGetAmount
	
IF OBJECT_ID('spPurchaseResponse','P') IS NOT NULL
	DROP PROCEDURE spPurchaseResponse
	
IF OBJECT_ID('spGetPurchaseResponse','P') IS NOT NULL
	DROP PROCEDURE spGetPurchaseResponse
	
IF OBJECT_ID('spViewAlltblUsers','P') IS NOT NULL
	DROP PROCEDURE spViewAlltblUsers
	
IF OBJECT_ID('spGetUserPwHash','P') IS NOT NULL
	DROP PROCEDURE spGetUserPwHash
	
IF OBJECT_ID('spDoesUserExist','P') IS NOT NULL
	DROP PROCEDURE spDoesUserExist
	
IF OBJECT_ID('spIsUserExist','P') IS NOT NULL
	DROP PROCEDURE spIsUserExist
	
IF OBJECT_ID('spGetUserGroup','P') IS NOT NULL
	DROP PROCEDURE spGetUserGroup
	
IF OBJECT_ID('spPublishAboutContent','P') IS NOT NULL
	DROP PROCEDURE spPublishAboutContent
	
IF OBJECT_ID('spGetAboutContent','P') IS NOT NULL
	DROP PROCEDURE spGetAboutContent
	
IF OBJECT_ID('spPublishTermsContent','P') IS NOT NULL
	DROP PROCEDURE spPublishTermsContent
	
IF OBJECT_ID('spGetTermsContent','P') IS NOT NULL
	DROP PROCEDURE spGetTermsContent
	
IF OBJECT_ID('spGetAllMemberInfo','P') IS NOT NULL
	DROP PROCEDURE spGetAllMemberInfo
	
IF OBJECT_ID('spGetAllEmail','P') IS NOT NULL
	DROP PROCEDURE spGetAllEmail
	
IF OBJECT_ID('spGetMemberInfoByCity','P') IS NOT NULL
	DROP PROCEDURE spGetMemberInfoByCity

IF OBJECT_ID('spShowCityList','P') IS NOT NULL
	DROP PROCEDURE spShowCityList
	
IF OBJECT_ID('spRegistAsAdmin','P') IS NOT NULL
	DROP PROCEDURE spRegistAsAdmin
	
IF OBJECT_ID('spGetLottoName','P') IS NOT NULL
	DROP PROCEDURE spGetLottoName
	
IF OBJECT_ID('spIsLoggedIn','P') IS NOT NULL
	DROP PROCEDURE spIsLoggedIn
	
IF OBJECT_ID('spLoggedIn','P') IS NOT NULL
	DROP PROCEDURE spLoggedIn

IF OBJECT_ID('spIsSameSession','P') IS NOT NULL
	DROP PROCEDURE spIsSameSession
	
IF OBJECT_ID('spSaveSession','P') IS NOT NULL
	DROP PROCEDURE spSaveSession
	
IF OBJECT_ID('spClearSession','P') IS NOT NULL
	DROP PROCEDURE spClearSession
	
IF OBJECT_ID('spStoreReceipt','P') IS NOT NULL
	DROP PROCEDURE spStoreReceipt
	
IF OBJECT_ID('spRemoveReceipt','P') IS NOT NULL
	DROP PROCEDURE spRemoveReceipt
	
IF OBJECT_ID('spRemoveAllReceipts','P') IS NOT NULL
	DROP PROCEDURE spRemoveAllReceipts
	
IF OBJECT_ID('spGetReceipt','P') IS NOT NULL
	DROP PROCEDURE spGetReceipt
	
IF OBJECT_ID('spGetAllReceipt','P') IS NOT NULL
	DROP PROCEDURE spGetAllReceipt
	
IF OBJECT_ID('spGetTransactionID','P') IS NOT NULL
	DROP PROCEDURE spGetTransactionID	
	
IF OBJECT_ID('spGetProvinceID','P') IS NOT NULL
	DROP PROCEDURE spGetProvinceID
	
IF OBJECT_ID('spGetCountryID','P') IS NOT NULL
	DROP PROCEDURE spGetCountryID
	
IF OBJECT_ID('spBlackList','P') IS NOT NULL
	DROP PROCEDURE spBlackList

IF OBJECT_ID('spShowBlackList','P') IS NOT NULL
	DROP PROCEDURE spShowBlackList

	
--Other way
--IF EXISTS (SELECT * FROM sysobjects WHERE name='GetLastRow' AND type='u') DROP PROCEDURE GetLastRow

GO

-- Drop Login

DROP USER LOTTO_admin;
DROP LOGIN gosoftso_henry;
DROP ROLE admin_role;

GO

-- Drop Tables
IF OBJECT_ID('Lottery', 'U') IS NOT NULL
	DROP TABLE Lottery
	
IF OBJECT_ID('BC49', 'U') IS NOT NULL
	DROP TABLE BC49
	
IF OBJECT_ID('LottoMax', 'U') IS NOT NULL
	DROP TABLE LottoMax
	
IF OBJECT_ID('FloridaLotto', 'U') IS NOT NULL
	DROP TABLE FloridaLotto
	
IF OBJECT_ID('NYLotto', 'U') IS NOT NULL
	DROP TABLE NYLotto
	
IF OBJECT_ID('PowerBall', 'U') IS NOT NULL
	DROP TABLE PowerBall
	
IF OBJECT_ID('PowerBall_PowerBall', 'U') IS NOT NULL
	DROP TABLE PowerBall_PowerBall
	
IF OBJECT_ID('MegaMillions', 'U') IS NOT NULL
	DROP TABLE MegaMillions
	
IF OBJECT_ID('MegaMillions_MegaBall', 'U') IS NOT NULL
	DROP TABLE MegaMillions_MegaBall
	
IF OBJECT_ID('EuroMillions', 'U') IS NOT NULL
	DROP TABLE EuroMillions
	
IF OBJECT_ID('EuroMillions_LuckyStars', 'U') IS NOT NULL
	DROP TABLE EuroMillions_LuckyStars
		
IF OBJECT_ID('OZLottoTue', 'U') IS NOT NULL
	DROP TABLE OZLottoTue

IF OBJECT_ID('OZLottoMon', 'U') IS NOT NULL
	DROP TABLE OZLottoMon
	
IF OBJECT_ID('OZLottoWed', 'U') IS NOT NULL
	DROP TABLE OZLottoWed
		
IF OBJECT_ID('OZLottoSat', 'U') IS NOT NULL
	DROP TABLE OZLottoSat
		
IF OBJECT_ID('SSQ', 'U') IS NOT NULL
	DROP TABLE SSQ
	
IF OBJECT_ID('SSQ_Blue', 'U') IS NOT NULL
	DROP TABLE SSQ_Blue
	
IF OBJECT_ID('SevenLotto', 'U') IS NOT NULL
	DROP TABLE SevenLotto
	
IF OBJECT_ID('SuperLotto', 'U') IS NOT NULL
	DROP TABLE SuperLotto
	
IF OBJECT_ID('SuperLotto_Rear', 'U') IS NOT NULL
	DROP TABLE SuperLotto_Rear
		
IF OBJECT_ID('NYSweetMillion', 'U') IS NOT NULL
	DROP TABLE NYSweetMillion
	
IF OBJECT_ID('ColoradoLotto', 'U') IS NOT NULL
	DROP TABLE ColoradoLotto	
		
IF OBJECT_ID('FloridaLucky', 'U') IS NOT NULL
	DROP TABLE FloridaLucky	
		
IF OBJECT_ID('EuroJackpot', 'U') IS NOT NULL
	DROP TABLE EuroJackpot	
	
IF OBJECT_ID('EuroJackpot_Euros', 'U') IS NOT NULL
	DROP TABLE EuroJackpot_Euros	
	
IF OBJECT_ID('GermanLotto', 'U') IS NOT NULL
	DROP TABLE GermanLotto	
		
IF OBJECT_ID('BritishLotto', 'U') IS NOT NULL
	DROP TABLE BritishLotto	
		
IF OBJECT_ID('FloridaFantasy5', 'U') IS NOT NULL
	DROP TABLE FloridaFantasy5	
		
IF OBJECT_ID('ConnecticutLotto', 'U') IS NOT NULL
	DROP TABLE ConnecticutLotto	
		

IF OBJECT_ID('CaSuperlottoPlus', 'U') IS NOT NULL
	DROP TABLE CaSuperlottoPlus

IF OBJECT_ID('NewJerseyPick6Lotto', 'U') IS NOT NULL
	DROP TABLE NewJerseyPick6Lotto
	
IF OBJECT_ID('OregonMegabucks', 'U') IS NOT NULL
	DROP TABLE OregonMegabucks	
	

		
IF OBJECT_ID('Lottos', 'U') IS NOT NULL
	DROP TABLE Lottos
	
IF OBJECT_ID('LottoName', 'U') IS NOT NULL
	DROP TABLE LottoName

	
--IF OBJECT_ID('Login', 'U') IS NOT NULL
--	DROP TABLE Login
	
IF OBJECT_ID('tblTransactions', 'U') IS NOT NULL
	DROP TABLE tblTransactions
	
IF OBJECT_ID('tblPurchaseResponse', 'U') IS NOT NULL
	DROP TABLE tblPurchaseResponse
	
	
IF OBJECT_ID('tblAboutPageContent', 'U') IS NOT NULL
	DROP TABLE tblAboutPageContent
	
IF OBJECT_ID('tblTermsPageContent', 'U') IS NOT NULL
	DROP TABLE tblTermsPageContent
	
IF OBJECT_ID('tblCityList', 'U') IS NOT NULL
	DROP TABLE tblCityList
	
IF OBJECT_ID('tblUsers', 'U') IS NOT NULL
	DROP TABLE tblUsers
	
IF OBJECT_ID('tblCountry', 'U') IS NOT NULL
	DROP TABLE tblCountry
	
IF OBJECT_ID('tblProvinceState', 'U') IS NOT NULL
	DROP TABLE tblProvinceState
	

IF OBJECT_ID('tblSession', 'U') IS NOT NULL
	DROP TABLE tblSession
	
IF OBJECT_ID('tblReceipt', 'U') IS NOT NULL
	DROP TABLE tblReceipt

IF OBJECT_ID('tblBlackList', 'U') IS NOT NULL
	DROP TABLE tblBlackList
	
	
	
	
	
	
	
--IF EXISTS (SELECT * FROM sysobjects WHERE name='Lottery' AND type='u') DROP TABLE Lottery
--IF EXISTS (SELECT * FROM sysobjects WHERE name='LottoMax' AND type='u') DROP TABLE LottoMax
--IF EXISTS (SELECT * FROM sysobjects WHERE name='Login' AND type='u') DROP TABLE Login
GO

CREATE TABLE tblBlackList(
	ListID	int Identity(1, 1) primary key clustered,
	UserID	NVARCHAR(25) Not NULL,
	Email   NVARCHAR(50) NOT NULL
	);

	GO

CREATE TABLE tblSession (
	userName	VARCHAR(20),
	sessionID	VARCHAR(32)
);
GO

CREATE TABLE tblReceipt ( 
	userName	  VARCHAR(20),
	TransactionID VARCHAR(25),
	CCType   	  VARCHAR(20),
	CCNumber	  VARCHAR(20),
	CCExpiryDate  VARCHAR(12),
	FullName      VARCHAR(20),
	MemberPlan    VARCHAR(25),
	StartDate     VARCHAR(12),
	ExpiredDate   VARCHAR(12)
);
GO

CREATE TABLE Lottery (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Bonus      int
);
GO

CREATE TABLE BC49 (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Bonus      int
);
GO



CREATE TABLE LottoMax (
	DrawNumber int PRIMARY KEY CLUSTERED ,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Number7    int,
	Bonus      int
);

GO

CREATE TABLE FloridaLotto (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Xtra	   int
);
GO

CREATE TABLE NYLotto (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Bonus	   int
);
GO

CREATE TABLE PowerBall (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int
);
GO

CREATE TABLE PowerBall_PowerBall (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	PowerBall  int
);
GO

CREATE TABLE MegaMillions (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int
);
GO

CREATE TABLE MegaMillions_MegaBall (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	MegaBall   int
);
GO

CREATE TABLE EuroMillions (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int
);
GO


CREATE TABLE EuroMillions_LuckyStars (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Star1      int,
	Star2      int
);
GO

CREATE TABLE EuroJackpot (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int
);
GO


CREATE TABLE EuroJackpot_Euros (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Euro1      int,
	Euro2      int
);
GO



CREATE TABLE ColoradoLotto (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int
);
GO

CREATE TABLE NYSweetMillion (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int
);
GO


CREATE TABLE OZLottoTue (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Number7    int,
	Supp1      int,
	Supp2      int
);
GO



CREATE TABLE OZLottoMon (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Supp1      int,
	Supp2      int
);
GO

CREATE TABLE OZLottoWed (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Supp1      int,
	Supp2      int
);
GO

CREATE TABLE OZLottoSat (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Supp1      int,
	Supp2      int
);
GO


CREATE TABLE FloridaLucky (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Lb         int
);
GO





--双色球 - 红色球

CREATE TABLE SSQ (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int
);
GO

--双色球 - 蓝色球

CREATE TABLE SSQ_Blue (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Blue    int
);
GO

--七乐彩

CREATE TABLE SevenLotto (
	DrawNumber int PRIMARY KEY CLUSTERED,
	DrawDate   VARCHAR(25),
	Number1    int,
	Number2    int,
	Number3    int,
	Number4    int,
	Number5    int,
	Number6    int,
	Number7    int,
	Special    int
);

GO

--大乐透 - 前区号码

CREATE TABLE SuperLotto (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int
);
GO


--大乐透 - 后区号码

GO
CREATE TABLE SuperLotto_Rear (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	RearNumber1		int,
	RearNumber2		int
);
GO


CREATE TABLE GermanLotto (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int,
	Number6			int,
	Bonus			int
);
GO

CREATE TABLE BritishLotto (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int,
	Number6			int,
	Bonus			int
);
GO

CREATE TABLE FloridaFantasy5 (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int
);
GO

CREATE TABLE ConnecticutLotto (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int,
	Mega			int
);
GO

CREATE TABLE NewJerseyPick6Lotto (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int,
	Number6			int
);
GO

CREATE TABLE OregonMegabucks (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int,
	Number6			int
);
GO


CREATE TABLE CaSuperlottoPlus (
	DrawNumber		int PRIMARY KEY CLUSTERED,
	DrawDate		VARCHAR(25),
	Number1			int,
	Number2			int,
	Number3			int,
	Number4			int,
	Number5			int,
	Number6			int
);
GO






CREATE TABLE Lottos (
	id		INT IDENTITY(0,1),
	name	NVARCHAR(100),
	links   NVARCHAR(100)
);

GO

ALTER TABLE Lottos ALTER COLUMN name NVARCHAR(50) COLLATE Chinese_PRC_Stroke_CS_AI
GO

INSERT INTO Lottos VALUES('Lotto 6/49, Canada', '/LottoSites/Lotto649.aspx')
INSERT INTO Lottos VALUES('Lotto Max, Canada', '/LottoSites/LottoMax.aspx')
INSERT INTO Lottos VALUES('BC/49, British Columbia, Canada', '/LottoSites/BC49.aspx')
INSERT INTO Lottos VALUES('Florida Lotto, Florida, USA', '/LottoSites/LottoFlorida.aspx')
INSERT INTO Lottos VALUES('Mega Millions, USA', '/LottoSites/MegaMillions.aspx')
INSERT INTO Lottos VALUES('Mega Millions - MegaBall, USA', '/LottoSites/MegaMillions.aspx')
INSERT INTO Lottos VALUES('Power Ball, USA', '/LottoSites/PowerBall.aspx')
INSERT INTO Lottos VALUES('Power Ball - PowerBall, USA', '/LottoSites/PowerBall.aspx')
INSERT INTO Lottos VALUES('New York Lotto, New York, USA', '/LottoSites/NYLotto.aspx')
INSERT INTO Lottos VALUES('Euro Millions, Europe', '/LottoSites/EuroMillions.aspx')
INSERT INTO Lottos VALUES('Euro Millions - LuckyStars, Europe', '/LottoSites/EuroMillions.aspx')
INSERT INTO Lottos VALUES('OZLotto Tuesday - Australia', '/LottoSites/OZLotto.aspx')
INSERT INTO Lottos VALUES(N'双色球 - 红色球, China', '/LottoSites/SSQ.aspx')
INSERT INTO Lottos VALUES(N'双色球 - 蓝色球, China', '/LottoSites/SSQ.aspx')
INSERT INTO Lottos VALUES(N'七乐彩, China Seven Lotto', '/LottoSites/SevenLotto.aspx')
INSERT INTO Lottos VALUES(N'超级大乐透 - 前区号码, China', '/LottoSites/SuperLotto.aspx')
INSERT INTO Lottos VALUES(N'超级大乐透 - 后区号码, China', '/LottoSites/SuperLotto.aspx')
INSERT INTO Lottos VALUES('New York Sweet Million, New York, USA', '/LottoSites/NYSweetMillion.aspx')
INSERT INTO Lottos VALUES('Colorado Lotto, Colorado, USA', '/LottoSites/ColoradoLotto.aspx')
INSERT INTO Lottos VALUES('Florida Lucky Money, Florida, USA', '/LottoSites/LottoFloridaLucky.aspx')
INSERT INTO Lottos VALUES('EuroJackpot, Europe', '/LottoSites/EuroJackpot.aspx')
INSERT INTO Lottos VALUES('EuroJackpot_Euros - Euros, Europe', '/LottoSites/EuroJackpot.aspx')
INSERT INTO Lottos VALUES('Germany Lotto 64/9 + B - Germany', '/LottoSites/GermanLotto.aspx')
INSERT INTO Lottos VALUES('British Lotto - UK', '/LottoSites/BritishLotto.aspx')
INSERT INTO Lottos VALUES('OZLotto Saturday Lotto - Australia', '/LottoSites/OZLottoSat.aspx')
INSERT INTO Lottos VALUES('Florida Fantasy 5 Lotto - Florida, USA', '/LottoSites/FloridaFantasy5.aspx')
INSERT INTO Lottos VALUES('OZLotto Monday Lotto - Australia', '/LottoSites/OZLottoMon.aspx')
INSERT INTO Lottos VALUES('OZLotto Wednesday Lotto - Australia', '/LottoSites/OZLottoWed.aspx')
INSERT INTO Lottos VALUES('Connecticut 6/44 Lotto - Connecticut, USA', '/LottoSites/ConnecticutLotto.aspx')
INSERT INTO Lottos VALUES('California Super Lotto Plus - CA, USA', '/LottoSites/CaSuperlottoPlus.aspx')
INSERT INTO Lottos VALUES(N'California Super Lotto Plus - Mega, CA, USA', '/LottoSites/CaSuperlottoPlus.aspx')
INSERT INTO Lottos VALUES('New Jersey Pick 6 Lotto - New Jersey, USA', '/LottoSites/NewJerseyPick6Lotto.aspx')
INSERT INTO Lottos VALUES('Oregon Megabucks - Oregon, USA', '/LottoSites/OregonMegabucks.aspx')
INSERT INTO Lottos VALUES('New York Take 5 - New York, USA', '/LottoSites/NewYorkTake5.aspx')
INSERT INTO Lottos VALUES('Texas Cash Five - Texas, USA', '/LottoSites/TexasCashFive.aspx')



GO

CREATE TABLE LottoName (
	id		INT IDENTITY(0,1),
	name	NVARCHAR(25)
);

GO

INSERT INTO LottoName VALUES('Lottery')
INSERT INTO LottoName VALUES('LottoMax')
INSERT INTO LottoName VALUES('BC49')
INSERT INTO LottoName VALUES('FloridaLotto')
INSERT INTO LottoName VALUES('MegaMillions')
INSERT INTO LottoName VALUES('MegaMillions_MegaBall')
INSERT INTO LottoName VALUES('PowerBall')
INSERT INTO LottoName VALUES('PowerBall_PowerBall')
INSERT INTO LottoName VALUES('NYLotto')
INSERT INTO LottoName VALUES('EuroMillions')
INSERT INTO LottoName VALUES('EuroMillions_LuckyStars')
INSERT INTO LottoName VALUES('OZLottoTue')
INSERT INTO LottoName VALUES('SSQ')
INSERT INTO LottoName VALUES('SSQ_Blue')
INSERT INTO LottoName VALUES('SevenLotto')
INSERT INTO LottoName VALUES('SuperLotto')
INSERT INTO LottoName VALUES('SuperLotto_Rear')
INSERT INTO LottoName VALUES('NYSweetMillion')
INSERT INTO LottoName VALUES('ColoradoLotto')
INSERT INTO LottoName VALUES('FloridaLucky')
INSERT INTO LottoName VALUES('EuroJackpot')
INSERT INTO LottoName VALUES('EuroJackpot_Euros')
INSERT INTO LottoName VALUES('GermanLotto')
INSERT INTO LottoName VALUES('BritishLotto')
INSERT INTO LottoName VALUES('OZLottoSat')
INSERT INTO LottoName VALUES('FloridaFantasy5')
INSERT INTO LottoName VALUES('OZLottoMon')
INSERT INTO LottoName VALUES('OZLottoWed')
INSERT INTO LottoName VALUES('ConnecticutLotto')
INSERT INTO LottoName VALUES('CaSuperlottoPlus')
INSERT INTO LottoName VALUES('CaSuperlottoPlus_Mega')
INSERT INTO LottoName VALUES('NewJerseyPick6Lotto')
INSERT INTO LottoName VALUES('OregonMegabucks')
INSERT INTO LottoName VALUES('NewYorkTake5')
INSERT INTO LottoName VALUES('TexasCashFive')


GO



CREATE TABLE tblCountry
(
	ID		INT IDENTITY(1,1),
	Name	VARCHAR(50),
	CONSTRAINT PK_tblCountry PRIMARY KEY (Name)
);

GO

--INSERT INTO tblCountry VALUES('Canada')
--INSERT INTO tblCountry VALUES('United States')
--INSERT INTO tblCountry VALUES('Australia')
--INSERT INTO tblCountry VALUES('United Kingdom')
--GO

CREATE TABLE tblProvinceState
(
	ID		INT IDENTITY(1,1),
	Name	VARCHAR(50) NOT NULL,
	--CONSTRAINT PK_tblProvinceState PRIMARY KEY (Name)
);

GO

CREATE TABLE tblUsers (
  UserName		VARCHAR(20) NOT NULL,
  PasswordHash	VARCHAR(200) NOT NULL,
  userFName		VARCHAR(50) NOT NULL,
  userLName		VARCHAR(50) NOT NULL,
  userEmail		VARCHAR(50) NOT NULL,
  userRole		VARCHAR(20) DEFAULT 'Member',
  signupDate	DATETIME,
  expiryDate	DATETIME,
  isLoggedIn	INTEGER DEFAULT 0,
  CONSTRAINT PK_tblUsers PRIMARY KEY CLUSTERED (UserName),
  --CONSTRAINT FK_userProvince FOREIGN KEY (userProvince) REFERENCES tblProvinceState(Name),
  --CONSTRAINT FK_userCountry FOREIGN KEY (userCountry) REFERENCES tblCountry(Name),
  CONSTRAINT signup_check CHECK(signupDate < expiryDate)
)
GO

--CREATE TABLE Login (
--	uname VARCHAR(12),
--	passwd VARCHAR(25)
--);

--GO


--About Page

CREATE TABLE tblAboutPageContent
(
	aboutContent TEXT,
	aboutRowIndex INTEGER
);

INSERT INTO tblAboutPageContent
VALUES ( '', '1' );
GO

--Terms Page

CREATE TABLE tblTermsPageContent
(
	termsContent TEXT,
	termsRowIndex INTEGER
);

INSERT INTO tblTermsPageContent
VALUES ( '', '1' );
GO





-- For PayPal
CREATE TABLE tblTransactions
(
	transEntry		INT		IDENTITY(1,1),
	transactionID	VARCHAR(25),
	amount			MONEY,
	customerName	VARCHAR(25),
	transType		VARCHAR(25)
)
GO

CREATE TABLE tblPurchaseResponse 
(
	payerEmail		VARCHAR(50),
	paymentStatus	VARCHAR(25),
	txnType			VARCHAR(25)
);
GO


CREATE TABLE tblCityList(
	cityName	VARCHAR(25) NOT NULL UNIQUE
	)
GO



--INSERT INTO Login VALUES('henrydb', 'Hma@1985')
--GO


--Procedures for Lottos

CREATE PROCEDURE spBlackList(@uid VARCHAR(20), @email VARCHAR(50))
	AS
	INSERT INTO tblBlackList VALUES(@uid, @email)
	GO


CREATE PROCEDURE spShowBlackList
	AS
	SELECT UserID, Email FROM tblBlackList
	GO



CREATE PROCEDURE spIsSameSession(@uid	VARCHAR(20), @ses VARCHAR(32))
	AS
	SELECT COUNT(*) FROM tblSession 
	WHERE userName=@uid AND sessionID=@ses
	GO
	
CREATE PROCEDURE spSaveSession(@uid	VARCHAR(20), @ses VARCHAR(32))
	AS
	IF (SELECT COUNT(*) FROM tblSession WHERE userName=@uid) = 0
		INSERT INTO tblSession VALUES(@uid, @ses)
	ELSE
		UPDATE tblSession SET sessionID=@ses WHERE userName=@uid
		
	GO
	
CREATE PROCEDURE spClearSession(@uid	VARCHAR(20))
	AS
		DELETE FROM tblSession WHERE userName=@uid
		
	GO
	
CREATE PROCEDURE spStoreReceipt(@uid			VARCHAR(20), 
								@TransactionID  VARCHAR(25),
								@CCType 		VARCHAR(20),
								@CCNumber		VARCHAR(20),
								@CCExpiryDate	VARCHAR(20),
								@FullName       VARCHAR(20),
								@MemberPlan     VARCHAR(25),
								@StartDate      VARCHAR(12),
								@ExpiredDate    VARCHAR(12)
								)
	AS
		INSERT INTO tblReceipt VALUES(@uid, @TransactionID, 
									  @CCType, @CCNumber,@CCExpiryDate,
									  @FullName, @MemberPlan, 
									  @StartDate, @ExpiredDate);
		
	GO
	
CREATE PROCEDURE spGetReceipt(@uid VARCHAR(20), @TransactionID VARCHAR(25))
	AS
		SELECT * FROM tblReceipt WHERE userName=@uid AND TransactionID=@TransactionID
	GO
	
CREATE PROCEDURE spGetAllReceipt(@uid VARCHAR(20))
	AS
		SELECT * FROM tblReceipt WHERE userName=@uid
	GO
	
CREATE PROCEDURE spRemoveReceipt(@uid VARCHAR(20), @transactionID VARCHAR(25))
	AS
		DELETE FROM tblReceipt WHERE userName=@uid AND TransactionID=@transactionID 
		
	GO

CREATE PROCEDURE spRemoveAllReceipts(@uid VARCHAR(20))
	AS
		DELETE FROM tblReceipt WHERE userName=@uid
		
	GO	

CREATE PROCEDURE spGetTransactionID(@uid VARCHAR(20))
	AS
		SELECT  TransactionID FROM tblReceipt WHERE userName=@uid
		
	GO

	

CREATE PROCEDURE spSelectAllProvinceState
	AS
	SELECT ID, Name FROM tblProvinceState ORDER BY ID
	GO
CREATE PROCEDURE spSelectAllCountry
	AS
	SELECT * FROM tblCountry  ORDER BY ID
	GO
	
CREATE PROCEDURE spGetProvinceID (@name  VARCHAR(50))
	AS
	SELECT ID FROM tblProvinceState
	WHERE Name=@name 
	GO
	
CREATE PROCEDURE spGetCountryID (@name  VARCHAR(50))
	AS
	SELECT ID FROM tblCountry 
	WHERE Name=@name 
	GO	
	
	
CREATE PROCEDURE spAllDrawNumbers(@db int, @dnum VARCHAR(5)) 
	AS
	DECLARE @dbtable NVARCHAR(25), @sql  VARCHAR(500)
	SET @dbtable = (SELECT DISTINCT name from LottoName where id=@db)
	SET @sql = 'SELECT DrawNumber FROM ' + @dbtable +
			' WHERE CAST(DrawNumber AS VARCHAR(5)) like ' + '''' + @dnum + '%' + '''' + 
			' ORDER BY DrawNumber DESC'
	
	EXEC(@sql)
	GO
	

CREATE PROCEDURE GetLastRow(@db int) 
	AS
	DECLARE @dbtable NVARCHAR(25), @sql  VARCHAR(500)
	SET @dbtable = (SELECT DISTINCT name from LottoName where id=@db)	
	SET @sql = 'SELECT MAX(DrawNumber) FROM ' + @dbtable	
	EXEC(@sql)
	GO


CREATE PROCEDURE GetLastDrawDate(@db int) 
	AS
	DECLARE @dbtable NVARCHAR(25), @sql  VARCHAR(500)
	SET @dbtable = (SELECT DISTINCT name from LottoName where id=@db)	
	SET @sql = 'SELECT DrawDate FROM ' + @dbtable + 
		' WHERE DrawNumber = (SELECT MAX(DrawNumber) FROM ' + @dbtable + ')'
	EXEC(@sql)
	GO

CREATE PROCEDURE SelectAll(@db int) 
	AS
	DECLARE @dbtable NVARCHAR(25), @sql  VARCHAR(500)
	SET @dbtable = (SELECT DISTINCT name from LottoName where id=@db)	
	SET @sql = 'SELECT * FROM ' + @dbtable	
	EXEC(@sql)
	GO

CREATE PROCEDURE SelectAllOnRangeOfDrawNo(@db int, @start int, @end int) 
	AS
	DECLARE @dbtable NVARCHAR(25), @sql  VARCHAR(500)
	SET @dbtable = (SELECT DISTINCT name from LottoName where id=@db)	
	SET @sql = 'SELECT * FROM ' + @dbtable + 
			' WHERE DrawNumber >= ' + str(@start) + ' AND DrawNumber <= ' + str(@end)	
	EXEC(@sql)
	GO
	


CREATE PROCEDURE GetTargetDraw(@db int, @drawNum int)
	AS
	DECLARE @dbtable NVARCHAR(25), @sql  VARCHAR(500)
	SET @dbtable = (SELECT DISTINCT name from LottoName where id=@db)	
	SET @sql = 'SELECT * FROM ' + @dbtable + 
			' WHERE DrawNumber  = ' + str(@drawNum)
	print @sql
	EXEC(@sql)
	GO






--Tables and Procedures for Membership Management



--ALTER TABLE tblUsers
--DROP CONSTRAINT PK_tblUsers 
--GO

--ALTER TABLE tblUsers
--DROP CONSTRAINT FK_userProvince 
--GO


--ALTER TABLE tblUsers
--DROP CONSTRAINT FK_userCountry 
--GO


--ALTER TABLE tblUsers
--DROP CONSTRAINT signup_check 
--GO






-- Stored Procedures

CREATE PROCEDURE spRegistAsAdmin(@uid VARCHAR(20)) 
	AS
		UPDATE tblUsers SET userRole='Admin' WHERE UserName=@uid
	GO


CREATE PROCEDURE spLoginAuth(@uid VARCHAR(20), @pwd VARCHAR(200)) 
	AS
	SELECT COUNT(UserName) 
	FROM tblUsers
	WHERE @uid=UserName AND @pwd=PasswordHash AND userRole!='Expired'
	GO
	
CREATE PROCEDURE spGetClientCloseExpired
	AS
	SELECT UserName, userFName + ' ' + userLName AS 'Client Name', userEmail AS Email, CONVERT(CHAR, expiryDate, 101) AS 'Expiry Date'
	FROM tblUsers
	WHERE (DATEDIFF(day, expiryDate, GETDATE() + 30) >= 0) AND 
		  (DATEDIFF(day, expiryDate, GETDATE()) < 0) AND
		  (userRole != 'Admin')
	GO


CREATE PROCEDURE spUpdateClientStatus
	AS
	UPDATE tblUsers SET userRole = 'Expired'
	WHERE (DATEDIFF(day, expiryDate, GETDATE()) >= 0) AND 
	  (userRole != 'Admin')	
	GO

	
CREATE PROCEDURE spGetClientExpired
	AS
	UPDATE tblUsers SET userRole = 'Expired'
	WHERE (DATEDIFF(day, expiryDate, GETDATE()) >= 0) AND 
	  (userRole != 'Admin')	

	SELECT UserName, userFName + ' ' + userLName AS 'Client Name', userEmail AS Email, userRole AS Membership, CONVERT(CHAR, expiryDate, 101) AS 'Expiry Date'
	FROM tblUsers
	WHERE (userRole = 'Expired')
	GO

CREATE PROCEDURE spIsClientExpired (@uid	VARCHAR(20), @email VARCHAR(50))
	AS	
	SELECT COUNT(*) 
	FROM tblUsers
	WHERE UserName=@uid AND userEmail=@email AND userRole = 'Expired'	
	GO

	
CREATE PROCEDURE spRemoveClient(@uid VARCHAR(20))
	AS
	DELETE FROM tblUsers
	WHERE UserName = @uid
	GO
		
CREATE PROCEDURE spRemoveExpiredClient
	AS
	DELETE FROM tblUsers
	WHERE userRole = 'Expired'
	GO	
	
	
CREATE PROCEDURE spGetUserRole(@uid VARCHAR(20))
	AS
	SELECT userRole
	FROM tblUsers
	WHERE @uid=UserName
	GO
	
CREATE PROCEDURE spFindPassword(@email VARCHAR(50))
	AS
	SELECT passwordHash
	FROM tblUsers
	WHERE @email=userEmail
	GO


CREATE PROCEDURE spUpdatePassword(@email VARCHAR(50), @oldPassword VARCHAR(200))
	AS
	UPDATE tblUsers 
	SET passwordHash = @oldPassword
	WHERE @email=userEmail
	GO
	
CREATE PROCEDURE spRetrieveUserProfile(@uid VARCHAR(20))
	AS
	SELECT  * FROM tblUsers
	WHERE UserName = @uid
	
	GO
	
CREATE PROCEDURE spGetUserFullName(@uid VARCHAR(20))	
	AS
	SELECT userFName + ' ' + userLName 
	FROM tblUsers
	WHERE @uid = UserName
	GO
	
CREATE PROCEDURE spRegisterUser (
	@userName		VARCHAR(20),
	@passwordHash	VARCHAR(200),
	@userFName		VARCHAR(50),
	@userLName		VARCHAR(50),
	@userEmail		VARCHAR(50),
	@userCity		VARCHAR(50),
	@userCountry	VARCHAR(50),
	@userRole		VARCHAR(20),
	@signupDate     DATETIME,
	@expiryDate     DATETIME,
	@isLoggedIn     INTEGER
	 )
AS
	INSERT INTO tblUsers VALUES(
		@userName,
		@passwordHash,
		@userFName,
		@userLName,
		@userEmail,	
		@userRole,
		CONVERT(char,@signupDate,101),
		CONVERT(char,@expiryDate,101),
		--@signupDate,
		--@expiryDate,
		@isLoggedIn,
		@userCity,
		@userCountry
	)
	GO
	
CREATE PROCEDURE spUpdateUser (
	@userName		VARCHAR(20),
	@passwordHash	VARCHAR(200),
	@userFName		VARCHAR(50),
	@userLName		VARCHAR(50),
	@userEmail		VARCHAR(50),
	@userCity		VARCHAR(50),
	@userCountry	VARCHAR(50),
	@userRole       VARCHAR(20),
	@signupDate     DATETIME,
	@expiryDate     DATETIME,
	@isLoggedIn     INTEGER

	)          
AS
	UPDATE tblUsers 
		SET passwordHash = @passwordHash,
		userFName = @userFName,
		userLName = @userLName, 
		userEmail = @userEmail,		
		userRole = @userRole,
		signupDate = @signupDate ,
		expiryDate = @expiryDate,
		isLoggedIn = @isLoggedIn,
		userCity = @userCity,
		userCountry = @userCountry
		
		WHERE userName = @userName
GO	

CREATE PROCEDURE spUpdateUserInfo (
	@userName		VARCHAR(20),
	@passwordHash	VARCHAR(200),
	@userFName		VARCHAR(50),
	@userLName		VARCHAR(50),
	@userEmail		VARCHAR(50)
	)          
AS
	UPDATE tblUsers 
		SET passwordHash = @passwordHash,
		userFName = @userFName,
		userLName = @userLName, 
		userEmail = @userEmail
		WHERE userName = @userName
GO	
	
CREATE PROCEDURE spCountUsers 
	AS
	SELECT COUNT(UserName) 
	FROM tblUsers
	WHERE userRole = 'Member'
	GO

CREATE PROCEDURE spGetData AS
	SELECT transEntry AS 'Transaction Entry', transactionID AS 'Transaction ID',
		CONVERT(DECIMAL(6, 2), ROUND(amount, 2)) AS Amount, customerName As 'Customer Name', transType AS 'Transaction Type'
	FROM tblTransactions
	ORDER BY transEntry
GO

CREATE PROCEDURE spPurchaseTransaction (@transID VARCHAR(25), @amount MONEY, @name VARCHAR(25)) AS
	INSERT INTO tblTransactions VALUES (@transID, @amount, @name, 'Purchase')
GO

CREATE PROCEDURE spRefundTransaction (@transID VARCHAR(25), @amount MONEY) AS
	UPDATE tblTransactions 
	SET amount = amount - @amount, transType = 'Refund'
	WHERE transactionID = @transID
GO

CREATE PROCEDURE spGetAmount(@transID VARCHAR(25)) AS
	SELECT CONVERT(DECIMAL(6, 2), ROUND(amount, 2)) AS Amount 
	FROM tblTransactions
	WHERE transactionID=@transID
	GO

CREATE PROCEDURE spPurchaseResponse (@payerEmail VARCHAR(25), @paymentStatus VARCHAR(25), @txnType VARCHAR(25)) AS
	INSERT INTO tblPurchaseResponse VALUES(@payerEmail, @paymentStatus, @txnType)
	GO

CREATE PROCEDURE spGetPurchaseResponse AS
	SELECT payerEmail AS 'Payer Email', paymentStatus AS 'Payment Status', txnType AS 'Transaction Type'
	FROM tblPurchaseResponse
	GO

CREATE PROCEDURE spViewAlltblUsers
AS
SELECT (userFName + ' ' + userLName) AS 'Full Name', 
	userEmail AS 'Email', 
	userRole AS 'Role', 
	CONVERT(DATE, signupDate, 103) as 'Signup Date',
	CONVERT(DATE, expiryDate, 103) as 'Expiry Date',
	isLoggedIn AS 'IsLoggedIn', 
	userCity AS 'City',
	userCountry AS 'Country'
	FROM tblUsers
	ORDER BY expiryDate DESC
GO

-- create stored procedure to retrieve user details
CREATE PROCEDURE spGetUserPwHash
	@userName varchar(20)
AS
	SELECT PasswordHash 
	FROM tblUsers
	WHERE UserName = @userName
GO


CREATE PROCEDURE spDoesUserExist
	@userName varchar(20)
AS
	IF EXISTS(
		SELECT * FROM tblUsers
		WHERE UserName = @userName)
		
		SELECT 'TRUE'
	ELSE
		SELECT 'FALSE'
GO

CREATE PROCEDURE spIsUserExist
	@userName varchar(20)
AS
	SELECT COUNT(UserName) FROM tblUsers WHERE UserName = @userName
GO

CREATE PROCEDURE spGetUserGroup
	@userName varchar(20)
AS
	SELECT userRole FROM tblUsers
	WHERE UserName = @userName
GO


CREATE PROCEDURE spPublishAboutContent
	@content TEXT
AS
	UPDATE tblAboutPageContent
	SET aboutContent = @content
	WHERE aboutRowIndex = '1'
GO

CREATE PROCEDURE spGetAboutContent
AS
	SELECT aboutContent 
	FROM tblAboutPageContent
	WHERE aboutRowIndex = '1'
GO


CREATE PROCEDURE spPublishTermsContent
	@content TEXT
AS
	UPDATE tblTermsPageContent
	SET termsContent = @content
	WHERE termsRowIndex = '1'
GO

CREATE PROCEDURE spGetTermsContent
AS
	SELECT * FROM tblTermsPageContent;
GO

CREATE PROCEDURE spGetAllMemberInfo
AS
	SELECT  userFName,userLName,userEmail,isLoggedIn
	FROM tblUsers
GO


CREATE PROCEDURE spGetAllEmail 
	AS
		SELECT userEmail FROM tblUsers
GO

--CREATE PROCEDURE spGetMemberInfoByCity(@cityName VARCHAR(20)) 
--	AS
--	SELECT userFName,userLName,userEmail,isLoggedIn, userPhone, userStreet, userStreet2, userCity,
--	userProvince,userPostalCode,userCountry
--	FROM tblUsers
--	WHERE @cityName=userCity
--GO
--CREATE PROCEDURE spShowCityList
--	AS
--		SELECT * FROM tblCityList ORDER BY cityName
--GO


CREATE PROCEDURE spGetLottoName
	AS
		SELECT * from Lottos ORDER BY name ASC
GO

CREATE PROCEDURE spIsLoggedIn(@uid VARCHAR(20))
	AS
		SELECT isLoggedIn from tblUsers 
		WHERE @uid = UserName
GO

CREATE PROCEDURE spLoggedIn(@uid VARCHAR(20), @flag INTEGER)
	AS
		UPDATE tblUsers 
		SET isLoggedIn = @flag
		WHERE @uid = UserName
GO





CREATE PROCEDURE spResetUserExpiryDate(@userName VARCHAR(20), @days INT)
	AS
		update tblUsers 
		set expiryDate = (select DATEADD(day, @days, CURRENT_TIMESTAMP)),
		userRole='Member'
		WHERE UserName=@userName
		
	GO

--Create admin login
/****
CREATE LOGIN gosoftso_henry WITH PASSWORD = 'Hma@1985'
CREATE USER LOTTO_admin FOR LOGIN gosoftso_henry
CREATE ROLE admin_role
GO

GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON Lottery TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON BC49 TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON LottoMax TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON FloridaLotto TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON NYLotto TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON MegaMillions TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON MegaMillions_MegaBall TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON PowerBall TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON PowerBall_PowerBall TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON EuroMillions TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON EuroMillions_LuckyStars TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON OZLotto TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON SSQ TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON SSQ_Blue TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON SevenLotto TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON SuperLotto TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON SuperLotto_Rear TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON NYSweetMillion TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON ColoradoLotto TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON FloridaLucky TO admin_role
--GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON Login TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblTransactions TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblPurchaseResponse TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblAboutPageContent TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblTermsPageContent TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblCityList TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblCountry TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblProvinceState TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblSession TO admin_role
GRANT ALTER, DELETE, INSERT, SELECT, UPDATE ON tblReceipt TO admin_role


GO
GRANT EXEC ON GetLastRow TO admin_role
GRANT EXEC ON GetLastDrawDate TO admin_role
GRANT EXEC ON SelectAll TO admin_role
GRANT EXEC ON SelectAllOnRangeOfDrawNo TO admin_role
GRANT EXEC ON GetTargetDraw TO admin_role
GRANT EXEC ON spSelectAllProvinceState TO admin_role
GRANT EXEC ON spSelectAllCountry TO admin_role
GRANT EXEC ON spLoginAuth TO admin_role
GRANT EXEC ON spGetClientCloseExpired TO admin_role
GRANT EXEC ON spGetClientExpired TO admin_role
GRANT EXEC ON spRemoveExpiredClient TO admin_role
GRANT EXEC ON spRemoveClient TO admin_role
GRANT EXEC ON spGetUserRole TO admin_role
GRANT EXEC ON spFindPassword TO admin_role
GRANT EXEC ON spUpdatePassword TO admin_role
GRANT EXEC ON spRetrieveUserProfile TO admin_role
GRANT EXEC ON spGetUserFullName TO admin_role
GRANT EXEC ON spRegisterUser TO admin_role
GRANT EXEC ON spUpdateUser TO admin_role
GRANT EXEC ON spCountUsers TO admin_role
GRANT EXEC ON spGetData TO admin_role
GRANT EXEC ON spPurchaseTransaction TO admin_role
GRANT EXEC ON spRefundTransaction TO admin_role
GRANT EXEC ON spGetAmount TO admin_role
GRANT EXEC ON spPurchaseResponse TO admin_role
GRANT EXEC ON spGetPurchaseResponse TO admin_role
GRANT EXEC ON spViewAlltblUsers TO admin_role
GRANT EXEC ON spGetUserPwHash TO admin_role
GRANT EXEC ON spDoesUserExist TO admin_role
GRANT EXEC ON spIsUserExist TO admin_role
GRANT EXEC ON spGetUserGroup TO admin_role
GRANT EXEC ON spPublishAboutContent TO admin_role
GRANT EXEC ON spGetAboutContent TO admin_role
GRANT EXEC ON spPublishTermsContent TO admin_role
GRANT EXEC ON spGetTermsContent TO admin_role
GRANT EXEC ON spGetAllMemberInfo TO admin_role
GRANT EXEC ON spGetAllEmail TO admin_role
GRANT EXEC ON spRegistAsAdmin TO admin_role
GRANT EXEC ON spGetLottoName TO admin_role
GRANT EXEC ON spIsLoggedIn TO admin_role
GRANT EXEC ON spLoggedIn TO admin_role
GRANT EXEC ON spSaveSession TO admin_role
GRANT EXEC ON spIsSameSession TO admin_role
GRANT EXEC ON spClearSession TO admin_role
GRANT EXEC ON spStoreReceipt TO admin_role
GRANT EXEC ON spRemoveReceipt TO admin_role
GRANT EXEC ON spGetReceipt TO admin_role
GRANT EXEC ON spGetAllReceipt TO admin_role
GRANT EXEC ON spSelectAllProvinceState TO admin_role
GRANT EXEC ON spGetProvinceID TO admin_role
GRANT EXEC ON spGetCountryID TO admin_role
GRANT EXEC ON spResetUserExpiryDate TO admin_role



--EXEC sp_addrolemember 'aspnet_Membership_FullAccess', LOTTO_admin
--GO
EXEC sp_addrolemember 'admin_role', LOTTO_admin
GO

***/


SET NOCOUNT OFF



	



