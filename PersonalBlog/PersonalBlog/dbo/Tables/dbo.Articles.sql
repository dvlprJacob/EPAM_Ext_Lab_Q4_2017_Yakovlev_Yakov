CREATE TABLE [dbo].[Articles](
	[ArticleID] [int] IDENTITY(1,1) NOT NULL,
	[BlogID] [int] NOT NULL,
	[ThemeID] [int] NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[UpdateDate] [date] NULL,
	[Content] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Blogs] FOREIGN KEY([BlogID])
REFERENCES [dbo].[Blogs] ([BlogID])
GO

ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Blogs]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Themes1] FOREIGN KEY([ThemeID])
REFERENCES [dbo].[Themes] ([ThemeID])
GO

ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Themes1]