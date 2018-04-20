/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
drop table dbo.TagLinks
drop table dbo.Tags
drop table dbo.Comments
drop table dbo.Articles
drop table dbo.Themes
drop table dbo.Users
drop table dbo.Roles
drop table dbo.Blogs