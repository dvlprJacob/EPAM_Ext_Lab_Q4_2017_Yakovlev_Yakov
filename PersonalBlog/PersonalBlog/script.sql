USE [master]
GO
/****** Object:  Database [PersonalBlog]    Script Date: 10.04.2018 15:55:04 ******/
CREATE DATABASE [PersonalBlog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PersonalBlog', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PersonalBlog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PersonalBlog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PersonalBlog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PersonalBlog] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PersonalBlog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PersonalBlog] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PersonalBlog] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PersonalBlog] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PersonalBlog] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PersonalBlog] SET ARITHABORT OFF 
GO
ALTER DATABASE [PersonalBlog] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PersonalBlog] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PersonalBlog] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PersonalBlog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PersonalBlog] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PersonalBlog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PersonalBlog] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PersonalBlog] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PersonalBlog] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PersonalBlog] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PersonalBlog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PersonalBlog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PersonalBlog] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PersonalBlog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PersonalBlog] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PersonalBlog] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PersonalBlog] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PersonalBlog] SET RECOVERY FULL 
GO
ALTER DATABASE [PersonalBlog] SET  MULTI_USER 
GO
ALTER DATABASE [PersonalBlog] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PersonalBlog] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PersonalBlog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PersonalBlog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PersonalBlog] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PersonalBlog', N'ON'
GO
ALTER DATABASE [PersonalBlog] SET QUERY_STORE = OFF
GO
USE [PersonalBlog]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [PersonalBlog]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 10.04.2018 15:55:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
/****** Object:  Table [dbo].[Blogs]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[BlogID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[BlogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[ArticleID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Content] [nvarchar](700) NOT NULL,
	[CreateDate] [date] NOT NULL,
	[Parent] [int] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nchar](15) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TagLinks]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagLinks](
	[TagID] [int] NOT NULL,
	[ArticleID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Themes](
	[ThemeID] [int] NOT NULL,
	[Theme] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[ThemeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](60) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[RoleID] [int] NOT NULL,
	[BlogID] [int] NULL,
	[RegistrationDate] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
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
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Articles] FOREIGN KEY([ArticleID])
REFERENCES [dbo].[Articles] ([ArticleID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Articles]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Comments] FOREIGN KEY([Parent])
REFERENCES [dbo].[Comments] ([CommentID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Comments]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
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
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Blogs] FOREIGN KEY([BlogID])
REFERENCES [dbo].[Blogs] ([BlogID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Blogs]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles1] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles1]
GO
USE [master]
GO
ALTER DATABASE [PersonalBlog] SET  READ_WRITE 
GO
