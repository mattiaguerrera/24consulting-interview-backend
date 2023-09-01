USE [AuditDB]
GO
/****** Object:  StoredProcedure [dbo].[Usp_InsertAuditLogs]    Script Date: 05-04-2021 11.20.04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usp_InsertAuditLogs] 
    @UrlReferrer VARCHAR(500)
	,@ActionName VARCHAR(50)
	,@Area VARCHAR(50)
	,@ControllerName VARCHAR(50)
	,@LoginStatus VARCHAR(1)
	,@LoggedInAt VARCHAR(50)
	,@LoggedOutAt VARCHAR(50)
	,@PageAccessed VARCHAR(500)
	,@IPAddress VARCHAR(50)
	,@SessionID VARCHAR(50)
	,@UserID VARCHAR(10)
	,@RoleId VARCHAR(2)
	,@LangId VARCHAR(2)
	,@IsFirstLogin VARCHAR(2)

AS
BEGIN
	DECLARE @table VARCHAR(15)
		,@sql NVARCHAR(MAX)
		,@sqlcreate NVARCHAR(MAX)
		,@newtable VARCHAR(30)
		,@currentdate VARCHAR(23);

	SET @currentdate = (CONVERT(VARCHAR, getdate(), 20))
	SET @table = (
			SELECT REPLACE(CONVERT(VARCHAR(11), getdate(), 106), ' ', '_')
			)
	SET @newtable = 'Audit_' + @table

	SELECT @newtable

	IF (
			EXISTS (
				SELECT *
				FROM INFORMATION_SCHEMA.TABLES
				WHERE TABLE_NAME = @newtable
				)
			)
	BEGIN
		SET @sql = CONCAT (
				'INSERT INTO ['
				,@newtable
				,'] (Area,ControllerName,ActionName,LoginStatus,LoggedInAt,LoggedOutAt,PageAccessed,IPAddress,SessionID,UserID,RoleId,LangId,IsFirstLogin,CurrentDatetime) '
				,'VALUES ('''
				,@Area
				,''','''
				,@ControllerName
				,''','''
				,@ActionName
				,''','''
				,@LoginStatus
				,''','''
				,@LoggedInAt
				,''','''
				,@LoggedOutAt
				,''','''
				,@PageAccessed
				,''','''
				,@IPAddress
				,''','''
				,@SessionID
				,''','''
				,@UserID
				,''','''
				,@RoleId
				,''','''
				,@LangId
				,''','''
				,@IsFirstLogin
				,''','''
				,@currentdate
				
			
				,''')'
				);

		EXEC (@sql);
	END
	ELSE
	BEGIN
		SET @sqlcreate = 'CREATE TABLE ' + '[' + @newtable + ']' + '(
 [AuditId] [bigint] IDENTITY(1,1) NOT NULL,
 [Area] [varchar](50) NULL,
 [ControllerName] [varchar](50) NULL,
 [ActionName] [varchar](50) NULL,
 [LoginStatus] [varchar](1) NULL,
 [LoggedInAt] [varchar](23) NULL,
 [LoggedOutAt] [varchar](23) NULL,
 [PageAccessed] [varchar](500) NULL,
 [IPAddress] [varchar](50) NULL,
 [SessionID] [varchar](50) NULL,
 [UserID] [varchar](50) NULL,
 [RoleId] [varchar](2) NULL,
 [LangId] [varchar](2) NULL,
 [IsFirstLogin] [varchar](2) NULL,
 [CurrentDatetime] [varchar](23) NULL
 )'

		EXEC sp_executesql @sqlcreate;

		SET @sql = CONCAT (
				'INSERT INTO ['
				,@newtable
				,'] (Area,ControllerName,ActionName,LoginStatus,LoggedInAt,LoggedOutAt,PageAccessed,IPAddress,SessionID,UserID,RoleId,LangId,IsFirstLogin,CurrentDatetime) '
				,'VALUES ('''
				,@Area
				,''','''
				,@ControllerName
				,''','''
				,@ActionName
				,''','''
				,@LoginStatus
				,''','''
				,@LoggedInAt
				,''','''
				,@LoggedOutAt
				,''','''
				,@PageAccessed
				,''','''
				,@IPAddress
				,''','''
				,@SessionID
				,''','''
				,@UserID
				,''','''
				,@RoleId
				,''','''
				,@LangId
				,''','''
				,@IsFirstLogin
				,''','''
				,@currentdate
				
				,''')'
				);

		EXEC (@sql);
	END
END
GO
