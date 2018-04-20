CREATE TABLE [dbo].[TagLinks](
	[TagID] [int] NOT NULL,
	[ArticleID] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TagLinks]  WITH CHECK ADD  CONSTRAINT [FK_TagLinks_Articles1] FOREIGN KEY([ArticleID])
REFERENCES [dbo].[Articles] ([ArticleID])
GO

ALTER TABLE [dbo].[TagLinks] CHECK CONSTRAINT [FK_TagLinks_Articles1]
GO
ALTER TABLE [dbo].[TagLinks]  WITH CHECK ADD  CONSTRAINT [FK_TagLinks_Tags1] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tags] ([TagID])
GO

ALTER TABLE [dbo].[TagLinks] CHECK CONSTRAINT [FK_TagLinks_Tags1]