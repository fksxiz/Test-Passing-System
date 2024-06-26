USE [master]
GO
/****** Object:  Database [mi_TestPassingSystem]    Script Date: 23.10.2023 14:11:22 ******/
CREATE DATABASE [mi_TestPassingSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'mi_TestPassingSystem', FILENAME = N'F:\312\databases\mi_TestPassingSystem.mdf' , SIZE = 38208KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'mi_TestPassingSystem_log', FILENAME = N'F:\312\server\MSSQL16.MSSQLSERVER\MSSQL\DATA\mi_TestPassingSystem.mdf' , SIZE = 76736KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [mi_TestPassingSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mi_TestPassingSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mi_TestPassingSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [mi_TestPassingSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [mi_TestPassingSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [mi_TestPassingSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [mi_TestPassingSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [mi_TestPassingSystem] SET  MULTI_USER 
GO
ALTER DATABASE [mi_TestPassingSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [mi_TestPassingSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [mi_TestPassingSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [mi_TestPassingSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [mi_TestPassingSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [mi_TestPassingSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'mi_TestPassingSystem', N'ON'
GO
ALTER DATABASE [mi_TestPassingSystem] SET QUERY_STORE = OFF
GO
USE [mi_TestPassingSystem]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 23.10.2023 14:11:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Answer] [nvarchar](500) NULL,
	[Correct] [bit] NULL,
	[QuestId] [int] NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 23.10.2023 14:11:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Question] [nvarchar](500) NULL,
	[Type] [nvarchar](150) NULL,
	[TestId] [int] NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Results]    Script Date: 23.10.2023 14:11:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[UserId] [int] NULL,
	[TestId] [int] NULL,
	[Score] [int] NULL,
	[Time] [time](7) NULL,
 CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 23.10.2023 14:11:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](1500) NULL,
	[Time] [time](7) NOT NULL,
	[MinScore] [int] NULL,
	[Image] [varbinary](max) NULL,
	[CreatorId] [int] NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Themes]    Script Date: 23.10.2023 14:11:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Themes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_Themes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.10.2023 14:11:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Login] [nvarchar](30) NULL,
	[Password] [nvarchar](40) NULL,
	[Email] [nvarchar](100) NULL,
	[Role] [nvarchar](50) NULL,
	[Name] [nvarchar](150) NULL,
	[Middlename] [nvarchar](150) NULL,
	[Surname] [nvarchar](150) NULL,
	[Birthday] [date] NULL,
	[Gender] [bit] NULL,
	[Avatar] [varbinary](max) NULL,
	[ThemeId] [int] NULL,
	[Verified] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ThemeId]  DEFAULT ((0)) FOR [ThemeId]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Verified]  DEFAULT ((0)) FOR [Verified]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Questions] FOREIGN KEY([QuestId])
REFERENCES [dbo].[Questions] ([Id])
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Questions]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([Id])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Tests]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([Id])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Tests]
GO
ALTER TABLE [dbo].[Results]  WITH CHECK ADD  CONSTRAINT [FK_Results_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Results] CHECK CONSTRAINT [FK_Results_Users]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Users] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Themes] FOREIGN KEY([ThemeId])
REFERENCES [dbo].[Themes] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Themes]
GO
USE [master]
GO
ALTER DATABASE [mi_TestPassingSystem] SET  READ_WRITE 
GO
