use [PersonalBlog]
--select * from Articles

---- comment tree for admin article with id = 1
--SELECT * FROM [dbo].[Comments] WHERE IsDeleted = 0 AND ArticleID = 1 AND Parent IS NULL
--SELECT * FROM [dbo].[Comments] WHERE Parent = 1 AND IsDeleted = 0

--select * from TagLinks WHERE TagID in (1,2,3,12,22,21,20)
--select IDENT_CURRENT('Articles')

--SELECT TagID, (SELECT TagName FROM Tags t WHERE t.TagID = tl.TagID) as 'TagName' FROM TagLinks tl WHERE tl.ArticleID = 1
--select * from Users
--SELECT ArticleID, Title FROM [dbo].[Articles] WHERE IsDeleted = 0 AND BlogID = 1 ORDER BY CreateDate DESC
--select * from Comments
--SELECT TOP (3) ArticleID, (SELECT Title FROM Articles a WHERE a.ArticleID = c.ArticleID) AS Title FROM Comments c GROUP BY c.ArticleID ORDER BY COUNT(c.ArticleID) DESC
--select * from Users
--declare @password nvarchar(max)
--set @password = 'admin'
--SELECT * FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = 'admin' AND Password = LOWER(CONVERT(NVARCHAR(MAX), HASHBYTES('md5', @password), 2))
--select LOWER(CONVERT(NVARCHAR(MAX), HASHBYTES('md5', @password), 2))
--select Password from Users where Password = LOWER(CONVERT(NVARCHAR(MAX), HASHBYTES('md5', 'admin'), 2))
--SELECT * FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = 'admin' AND Password = LOWER(CONVERT(NVARCHAR(MAX), HASHBYTES('md5', @password), 2))
--SELECT UserID, Password from Users
--SELECT TOP (1) UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND Login = 'admin' AND Password = @password
--SELECT UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND BlogID = (SELECT BlogID FROM [dbo].[Articles] WHERE ArticleID = 1)
select * from Users
SELECT UserID FROM [dbo].[Users] WHERE IsDeleted = 0 AND UserID = 5 AND BlogID IS NULL
SELECT * FROM [dbo].[Comments] WHERE ArticleID = 1 AND IsDeleted = 0  AND Parent = 1
select * from Comments where ArticleID = 1 order BY CreateDate DESC
select * from Users
SELECT * FROM [dbo].[Blogs] 