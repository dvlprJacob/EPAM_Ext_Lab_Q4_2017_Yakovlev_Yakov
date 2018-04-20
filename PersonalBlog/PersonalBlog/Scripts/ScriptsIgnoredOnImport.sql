﻿
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

/****** Object:  Table [dbo].[Blogs]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Comments]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Roles]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[TagLinks]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Tags]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Themes]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[Users]    Script Date: 10.04.2018 15:55:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

USE [master]
GO

ALTER DATABASE [PersonalBlog] SET  READ_WRITE
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;

GO

--Syntax Error: Incorrect syntax near IDENTITY_CACHE.
--ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;



GO