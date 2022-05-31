USE [master]
GO
/****** Object:  Database [KUSYS-Demo]    Script Date: 1.06.2022 02:22:51 ******/
CREATE DATABASE [KUSYS-Demo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KUSYS-Demo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\KUSYS-Demo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KUSYS-Demo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\KUSYS-Demo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [KUSYS-Demo] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KUSYS-Demo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KUSYS-Demo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET ARITHABORT OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KUSYS-Demo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KUSYS-Demo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KUSYS-Demo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KUSYS-Demo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET RECOVERY FULL 
GO
ALTER DATABASE [KUSYS-Demo] SET  MULTI_USER 
GO
ALTER DATABASE [KUSYS-Demo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KUSYS-Demo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KUSYS-Demo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KUSYS-Demo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [KUSYS-Demo] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'KUSYS-Demo', N'ON'
GO
ALTER DATABASE [KUSYS-Demo] SET QUERY_STORE = OFF
GO
USE [KUSYS-Demo]
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
USE [KUSYS-Demo]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 1.06.2022 02:22:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [nvarchar](50) NOT NULL,
	[CourseName] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1.06.2022 02:22:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](150) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 1.06.2022 02:22:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [nvarchar](50) NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1.06.2022 02:22:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[RoleId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Course] ([CourseId], [CourseName]) VALUES (N'CSI101', N'Introduction to Computer Science')
INSERT [dbo].[Course] ([CourseId], [CourseName]) VALUES (N'CSI102', N'Algorithms')
INSERT [dbo].[Course] ([CourseId], [CourseName]) VALUES (N'MAT101', N'Calculus')
INSERT [dbo].[Course] ([CourseId], [CourseName]) VALUES (N'PHY101', N'Physics')
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Role]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (2, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StudentId], [CourseId], [FirstName], [LastName], [BirthDate]) VALUES (2, N'MAT101', N'test1', N'test2', CAST(N'2000-04-01T22:30:00.000' AS DateTime))
INSERT [dbo].[Student] ([StudentId], [CourseId], [FirstName], [LastName], [BirthDate]) VALUES (3, N'PHY101', N'test', N'test2', CAST(N'1995-02-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Student] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [StudentId], [RoleId], [Name], [Password]) VALUES (2, NULL, 1, N'Admin', N'123')
INSERT [dbo].[Users] ([Id], [StudentId], [RoleId], [Name], [Password]) VALUES (3, 2, 2, N'User', N'1234')
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Course]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Student]
GO
USE [master]
GO
ALTER DATABASE [KUSYS-Demo] SET  READ_WRITE 
GO
