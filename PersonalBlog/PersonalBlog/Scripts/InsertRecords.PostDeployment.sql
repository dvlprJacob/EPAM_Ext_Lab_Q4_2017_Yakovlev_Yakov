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
USE [PersonalBlog]
GO

INSERT INTO [dbo].[Roles]
           ([RoleName])
     VALUES
           ('Administrator'),
		   ('User'),
		   ('Guest')
GO

USE [PersonalBlog]
GO

INSERT INTO [dbo].[Themes]
           ([Theme])
     VALUES
           ('IT'),
		   ('Politics'),
		   ('Medicine'),
		   ('Education'),
		   ('VR/AR'),
		   ('Music'),
		   ('Movie'),
		   ('Space'),
		   ('Machine Learning'),
		   ('Phisics'),
		   ('Common')
GO

USE [PersonalBlog]
GO

INSERT INTO [dbo].[Blogs]
           ([Title]
		   ,[IsDeleted]
		   ,[CreatorID])
     VALUES
           ('admin test BLOG', 0, NULL),
		   ('user test BLOG', 0, NULL),
		   ('Jack space advantures', 0, NULL)
GO

USE [PersonalBlog]
GO

declare @password1 nvarchar(20)
declare @password2 nvarchar(20)
declare @password3 nvarchar(20)
declare @password4 nvarchar(20)
set @password1 = 'admin'
set @password2 = 'user'
set @password3 = 'user2'
set @password4 = 'Jack123'

INSERT INTO [dbo].[Users]
           ([Login]
           ,[Password]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[Phone]
           ,[RoleID]
           ,[BlogID]
           ,[RegistrationDate]
           ,[IsDeleted])
     VALUES 
           ('admin', LOWER(CONVERT(NVARCHAR(MAX),HASHBYTES('md5', @password1),2)), 'admin', NULL, 'admin@my.com', '+14 788 564 21 55', 1, 1, '10.4.2018', 0),
		   ('user', LOWER(CONVERT(NVARCHAR(MAX),HASHBYTES('md5',@password2),2)), 'user', 'Tester', 'user@my.com', NULL, 2, 2, '10.4.2018', 0),
		   ('user2', LOWER(CONVERT(NVARCHAR(MAX),HASHBYTES('md5',@password3),2)), 'user2',  NULL, 'user2@my.com', '+7 (3412) 365 95 41', 2, NULL, '10.4.2018', 0),
		   ('jackSpaceRanger', LOWER(CONVERT(NVARCHAR(MAX),HASHBYTES('md5',@password4),2)), 'Jack123',  'Niewt', 'spaceX@my.com', '+7 (963) 365 95 41', 2, 3, '7.4.2018', 0)
GO

USE [PersonalBlog]
GO

INSERT INTO [dbo].[Articles]
           ([BlogID]
           ,[ThemeID]
           ,[Title]
           ,[CreateDate]
           ,[UpdateDate]
           ,[Content]
           ,[IsDeleted])
     VALUES
           (1, 1, 'Test: first admin IT theme article', '10.4.2018', NULL, 'Content : ... ... ... ... .... ...... ...... .... ..... ........', 0),
		   (2, 4, 'Test: first user Education theme article', '10.4.2018', NULL, 'Content : ... ... ... ... .... ...... ...... text/image ........', 0),
		   (3, 8, 'Are you ready ?', '10.4.2018', NULL, 'One : Today, there are only two vehicles that can carry humans into space.  One is the Russian Soyuz, the other is the Chinese Shenzhou (see below).

Only Chinese professional astronauts (or taikonauts) are allowed to fly on the Shenzhou, but the Russian Soyuz has flown private and professional astronauts from all over the world.

The Soyuz currently flies four times per year to the International Space Station, each time carrying three astronauts.  It has an exceptional track record for reliability, having flown over 120 times consecutively without loss of life (last was Soyuz 11 in 1971).  So while it may not be the most attractive or “bad ass” looking spacecraft when compared to the newly designed Boeing CST-100 Starliner or SpaceX Dragon V2 spacecraft (see below), it has a track record of safety that will be tough to match in the coming decades.
The Soyuz has also been continually upgraded since the day it went into service.  The most recent round of upgrades went into effect in July 2016, with the launch of Soyuz-MS-01 which featured upgrades to reduce the mass of the docking system, a smaller on-board computer and upgraded solar panels.  And have launched successfully eight times since the upgrade, the most recent flight being Soyuz-MS-08 on March 21, 2018.  So while it maintains largely the same look from outside as the first day it flew, the spacecraft is definitely state of the art.
The Soyuz consists of three parts (as shown in the picture below):

The Orbital module at the top – which provides living space for the crew while in orbit (for example during approach to the ISS).
The Descent module in the middle – which is where the crew sit during launch and re-entry, and is the only part of the spacecraft to return to Earth.
The Instrument module at the bottom – which is home to fuel-tanks, maneuvering engines, solar panels and other instruments.

The Soyuz can accommodate up to three crew members, and has enough life support for 30 person days (or 10 days for a crew of three).

During launch the spacecraft, with its solar panels folded in, is housed inside the faring in the top of a Soyuz rocket.  See image below:

The Soyuz may be based on cold war technology, but its continual program of updates have helped it see off far more complex, expensive craft such as the space shuttle.  Its success led to its basic form being copied by the Chinese Shenzhou, and all manned orbital class spacecraft now in development are capsules rather than winged vehicles.  While its long term future may be in doubt as the Russian authorities contemplate a replacement, the Soyuz is sure to be around for a while yet.', 0),
		   (3, 8, 'Incredible start', '11.4.2018', NULL, 'Two : Positioned on the Earth facing side of the International Space Station (ISS) the Cupola provides extraordinary views of the Earth.  The Cupola juts out from the main structure of the ISS and has six side windows and one large center window, which allow astronauts to look both along the ISS to the horizon, and directly down onto Earth below.  The ISS is in orbit approximately 250 miles above Earth, and travels around the Earth in 90 minutes, so the Cupola provides a fantastic vantage point to watch the Earth passing by below, but also a tremendous view of the ever changing horizon and frequent sunrises and sunsets.
The cupola was conceived as a way for astronauts to see and control the station arm, for example when grappling arriving spacecraft, or to watch astronauts during a spacewalk. One of the fringe benefits was providing a tremendous vantage point for watching the Earth, as the photos below show.
NASA Astronaut Tracy Caldwell Dyson enjoying the view of Earth from the Cupola aboard the ISS.
A view from the Cupola of the Aurora Borealis over the United Kingdom. Astronaut seen inside the Cupola attached to the International Space Station.

An astronaut in the Cupola aboard the ISS, looking down (or sideways) on Earth. Cairo and the Nile delta photographed at night from the Cupola onboard the ISS.

NASA spacewalker hanging on to the outside of the cupola while floating above Earth.', 0),
		   (3, 8, 'Unexpected ending', '12.4.2018', NULL, 'Three : Space Adventures’ vision is to open the space frontier to private citizens.
Space Adventures wants as many people as possible to experience what it is like to live in space, to circle the Earth, or travel beyond Earth orbit. In the next ten years, our clients will have a choice as to what vehicle to fly to space on, and will be able to choose from multiple different destinations. By providing newly available space experiences and improving existing ones, Space Adventures will continue to lead the private spaceflight industry that it began in 2001 with the flight of the world’s first space tourist, our client Dennis Tito.

If you have any further questions please contact us, or continue to read about our Experiences.', 0),
           (3, 8, 'The future will be better tomorrow', '14.4.2018', NULL, 'Frenic has brought together his love of hip-hop and emotive soulful music to create a wonderfully rich throwback sound, 
combining moving funky samples, head-nodding drums and slashing turntable cuts.', 0),
(3, 5, 'Bonus', '14.4.2018', NULL, 'empty content', 0)
GO

USE [PersonalBlog]
GO

INSERT INTO [dbo].[Tags]
           ([TagName],
		   [IsDeleted])
     VALUES
           ('asp .net', 0),
		   ('asp .net core', 0),
		   ('asp .net mvc', 0),
		   ('c#', 0),
		   ('c++', 0),
		   ('mvc', 0),
		   ('space', 0),
		   ('visual studio', 0),
		   ('t-sql', 0),
		   ('orm', 0),
		   ('nhibernate', 0),
		   ('entity', 0),
		   ('database', 0),
		   ('model', 0),
		   ('view model', 0),
		   ('controller', 0),
		   ('view', 0),
		   ('route', 0),
		   ('phisics', 0),
		   ('ii', 0),
		   ('ии', 0)
GO

USE [PersonalBlog]
GO

INSERT INTO [dbo].[TagLinks]
           ([TagID]
           ,[ArticleID])
     VALUES
           (1, 1),
		   (2, 1),
		   (3, 1),
		   (12, 1),
		   (13, 1),
		   (14, 1),
		   (15, 1),
		   (16, 1),
		   (17, 1),
		   (8, 2),
		   (9, 2),
		   (10, 2),
		   (11, 2),
		   (7, 3),
		   (7, 4),
		   (7, 5),
		   (10, 5),
		   (7, 6),
		   (1, 7),
		   (11, 7),
		   (12, 7),
		   (13, 7),
		   (15, 7),
		   (19, 7)
GO

USE [PersonalBlog]
GO

INSERT INTO [dbo].[Comments]
           ([ArticleID]
           ,[UserID]
           ,[Content]
           ,[CreateDate]
           ,[Parent]
		   ,[IsDeleted])
     VALUES
           (1, 1, 'admin fghfgh fgfhfghg fhgfhblog', '10.4.2018', NULL, 0),
		   (1, 2, 'admin blog, child subcomment', '10.4.2018', 1, 0),
		   (1, 1, 'adminfdsgfdsgdfsgdf fdsghfsdyhfadhsdfh gjhgshfdshfdshb sghgsgsgsfdshfdshfdshgfsdgiufhds fdsghiufdshygn fsdghdisfughdn dfiusgufds uifdshg blog, child', '10.4.2018', 1, 0),
		   (1, 2, 'admin fdg dfgsdfgsdf blog, child : third level', '10.4.2018', 3, 0),
		   (2, 1, 'admin commgfhfghent', '10.4.2018', NULL, 0),
		   (2, 2, 'comment fdsgsdfgfdsg', '11.4.2018', NULL, 0),
		   (3, 2, 'admin cxhgfdhblog', '12.4.2018', NULL, 0),
		   (3, 1, 'admin bgfhfghfg fghfgh flog', '12.4.2018', NULL, 0),
		   (3, 3, 'admin blgfhfghfghfgfgfgfgfgfgfgfgfgfgfgfgfgfgfgfghfghgfhhhhhhhhhhhhhhhhhhf hfghfg hgfhgfhog', '12.4.2018', 8, 0),
		   (3, 2, 'admin bgfhgfhlog', '14.4.2018', 8, 0),

		   (4, 3, 'jack', '14.4.2018', NULL, 0),
		   (4, 1, 'admin fdgfdsgfdsyhsghjfghgfdsjfniusdfb', '14.4.2018', 11, 0),
		   (4, 2, 'user dfgdfg', '14.4.2018', 11, 0),
		   (1, 2, 'dfgsdffgshsfh gfhgfh gfhsfhstrhstrhgs gshgh', '14.4.2018', NULL, 0),
		   (6, 1, 'fsdhdf', '14.4.2018', NULL, 0),
		   (6, 2, 'yes', '14.4.2018', NULL, 0),
		   (6, 3, 'dont right', '14.4.2018', NULL, 0),
		   (6, 2, 'dzgdsgfdghfdsyhfdh', '14.4.2018', NULL, 0),
		   (6, 1, 'sdaggfhg gfhg hhg fghgf ffghfgh gfdsg', '14.4.2018', NULL, 0),
		   (6, 3, 'dasgdasggfhgf hgfhgfhgfhfghfghf gfhfgds', '14.4.2018', NULL, 0),
		   (4, 3, 'user, you right', '14.4.2018', 13, 0),
		   (7, 2, 'first comment', '14.4.2018', NULL, 0)
GO

