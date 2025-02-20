USE [master]
GO
/****** Object:  Database [AhChaFortunate]    Script Date: 2024/4/28 0:24:25 ******/
CREATE DATABASE [AhChaFortunate]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AhChaFortunate', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AhChaFortunate.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AhChaFortunate_log', FILENAME = N'D:\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\AhChaFortunate_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AhChaFortunate] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AhChaFortunate].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AhChaFortunate] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AhChaFortunate] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AhChaFortunate] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AhChaFortunate] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AhChaFortunate] SET ARITHABORT OFF 
GO
ALTER DATABASE [AhChaFortunate] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AhChaFortunate] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AhChaFortunate] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AhChaFortunate] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AhChaFortunate] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AhChaFortunate] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AhChaFortunate] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AhChaFortunate] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AhChaFortunate] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AhChaFortunate] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AhChaFortunate] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AhChaFortunate] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AhChaFortunate] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AhChaFortunate] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AhChaFortunate] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AhChaFortunate] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AhChaFortunate] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AhChaFortunate] SET RECOVERY FULL 
GO
ALTER DATABASE [AhChaFortunate] SET  MULTI_USER 
GO
ALTER DATABASE [AhChaFortunate] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AhChaFortunate] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AhChaFortunate] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AhChaFortunate] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AhChaFortunate] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AhChaFortunate] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'AhChaFortunate', N'ON'
GO
ALTER DATABASE [AhChaFortunate] SET QUERY_STORE = OFF
GO
USE [AhChaFortunate]
GO
/****** Object:  Table [dbo].[SysDept]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysDept](
	[Id] [bigint] NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Desc] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysDictData]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysDictData](
	[Id] [bigint] NOT NULL,
	[TypeId] [bigint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Value1] [nvarchar](200) NULL,
	[Value2] [nvarchar](200) NULL,
	[Value3] [nvarchar](200) NULL,
	[Status] [bit] NULL,
	[Sort] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
 CONSTRAINT [PK_SysDictData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysDictType]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysDictType](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Desc] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
 CONSTRAINT [PK_SysDictType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysLoginLog]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysLoginLog](
	[Id] [bigint] NOT NULL,
	[DeptId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[IP] [nvarchar](20) NULL,
	[UAStr] [nvarchar](150) NULL,
	[Browser] [nvarchar](50) NULL,
	[OS] [nvarchar](20) NULL,
	[Device] [nvarchar](30) NULL,
	[LoginTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysMenu]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysMenu](
	[Id] [bigint] NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Icon] [nvarchar](20) NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Path] [nvarchar](100) NOT NULL,
	[Component] [nvarchar](100) NOT NULL,
	[IsHide] [bit] NOT NULL,
	[IsKeepAlive] [bit] NOT NULL,
	[IsIframe] [bit] NOT NULL,
	[IsAffix] [bit] NOT NULL,
	[IsLink] [bit] NOT NULL,
	[Redirect] [nvarchar](200) NULL,
	[Url] [nvarchar](200) NULL,
	[Type] [nvarchar](10) NOT NULL,
	[Sort] [int] NULL,
	[ApiTag] [nvarchar](20) NULL,
	[Permission] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysNotice]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysNotice](
	[Id] [bigint] NOT NULL,
	[Title] [nvarchar](60) NOT NULL,
	[Content] [nvarchar](500) NULL,
	[Type] [nvarchar](2) NOT NULL,
	[Status] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRole]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRole](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Desc] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[DataRang] [nvarchar](20) NULL,
	[Permission] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysRoleMenu]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysRoleMenu](
	[Id] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[MenuId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Account] [nvarchar](100) NULL,
	[Password] [nvarchar](100) NULL,
	[Salt] [nvarchar](100) NULL,
	[Phone] [nvarchar](50) NULL,
	[IdCard] [nvarchar](20) NULL,
	[Address] [nvarchar](255) NULL,
	[Remark] [nvarchar](100) NULL,
	[UserState] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserId] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateUserId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SysUserRole]    Script Date: 2024/4/28 0:24:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUserRole](
	[Id] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_SysUserRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[SysDept] ([Id], [ParentId], [Name], [Desc], [Status], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (1, 0, N'System', N'System', 1, CAST(N'2024-04-09T09:31:21.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[SysDept] ([Id], [ParentId], [Name], [Desc], [Status], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (2, 0, N'数据部', N'数据部', 1, CAST(N'2024-04-09T09:31:24.000' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[SysDept] ([Id], [ParentId], [Name], [Desc], [Status], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (534374912098373, 0, N'测试12', N'吃撒12', 1, CAST(N'2024-04-09T10:40:45.000' AS DateTime), NULL, CAST(N'2024-04-09T10:40:22.660' AS DateTime), 530124946407493)
GO
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537573113954373, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T10:50:38.033' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537573439402053, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T10:51:57.490' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537576049295429, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T11:02:34.670' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537579605143621, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T11:17:02.793' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537580310503493, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T11:19:55.003' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537582084755525, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T11:27:08.170' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537583806275653, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T11:34:08.463' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537619144077381, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T13:57:55.857' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537631181799493, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T14:46:54.753' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537634281148485, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T14:59:31.440' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537635021004869, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T15:02:32.070' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537636631486533, 0, 1780854114666811392, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T15:09:05.240' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537636655370309, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T15:09:11.087' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537674962387013, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T17:45:03.373' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537680402788421, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-18T18:07:11.597' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537680609730629, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (iPhone; CPU iPhone OS 16_6 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/16.6 Mobile/15E148 Safari/604.1', N'Mobile Safari', N'iOS', N'iPhone', CAST(N'2024-04-18T18:08:02.130' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537916553453637, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-19T10:08:05.560' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537935648768069, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-19T11:25:47.503' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537967840559173, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-19T13:36:46.827' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (537997271273541, 0, 530124946407493, N'192.168.2.5', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-19T15:36:32.060' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (538964315451461, 0, 530124946407493, N'192.168.2.5', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-22T09:11:26.813' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (538964552785989, 0, 530124946407493, N'192.168.2.5', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-22T09:12:24.790' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (539347608326213, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-23T11:11:04.193' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (539348448239685, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-23T11:14:29.247' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540113619345477, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:07:58.603' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540114484981829, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:11:29.957' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540117897457733, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:25:23.060' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540121295179845, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:39:12.600' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540122954346565, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:45:57.653' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540123054555205, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:46:22.137' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540123097612357, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:46:32.650' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540123250266181, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:47:09.917' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540123434274885, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:47:54.840' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540123521310789, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:48:16.090' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540123657355333, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:48:49.303' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540124060024901, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T15:50:27.613' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540144039661637, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-25T17:11:45.437' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540403796766789, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T10:48:42.713' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540410837684293, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T11:17:21.670' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540504026021957, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:36:32.747' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540504054829125, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:36:39.780' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540504436166725, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:38:12.880' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540504842764357, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:39:52.147' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540504983515205, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:40:26.510' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540505202421829, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:41:19.957' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540505557811269, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:42:46.720' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540505907265605, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:44:12.037' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540506334982213, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:45:56.460' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540506938351685, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:48:23.767' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540507045019717, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T17:48:49.807' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540512300531781, 0, 530124946407493, N'192.168.2.4', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T18:10:12.873' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540531682091077, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T19:29:04.700' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540535524773957, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T19:44:42.873' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540537170739269, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T19:51:24.700' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540542254600261, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-26T20:12:05.897' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540827199914053, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-27T15:31:32.600' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540830106296389, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-27T15:43:22.170' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540831717552197, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-27T15:49:55.543' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540832145862725, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-27T15:51:40.130' AS DateTime))
INSERT [dbo].[SysLoginLog] ([Id], [DeptId], [UserId], [IP], [UAStr], [Browser], [OS], [Device], [LoginTime]) VALUES (540932160294981, 0, 530124946407493, N'127.0.0.1', N'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36', N'Chrome', N'Windows', N'Other', CAST(N'2024-04-27T22:38:37.697' AS DateTime))
GO
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (1, 0, N'系统管理', N'ele-Setting', N'system', N'/system', N'layout/routerView/parent', 0, 1, 0, 0, 0, N'/system/menu', N'', N'Menu', 0, N'', N'Query,', NULL, NULL, CAST(N'2024-04-18T15:05:15.603' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (2, 0, N'用户管理', N'ele-UserFilled', N'user', N'/user', N'layout/routerView/parent', 0, 1, 0, 0, 0, N'/system/user', N'', N'Menu', 1, N'SysUser', N'Query,', NULL, NULL, CAST(N'2024-04-26T12:03:43.743' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (3, 0, N'通知管理', N'ele-BellFilled', N'notice', N'/notice', N'layout/routerView/parent', 0, 1, 0, 0, 0, N'/system/notice', N'', N'Menu', 2, N'SysNotice', N'Query,', NULL, NULL, CAST(N'2024-04-26T10:56:56.680' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (10, 1, N'菜单管理', N'ele-Menu', N'system/menu', N'/system/menu', N'system/menu/index', 0, 1, 0, 0, 0, N'', N'', N'Menu', 1, N'SysMenu', N'Query,', NULL, NULL, CAST(N'2024-04-26T11:18:06.320' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (11, 1, N'角色管理', N'ele-Collection', N'system/role', N'/system/role', N'system/role/index', 0, 1, 0, 0, 0, N'', N'', N'Menu', 2, N'SysRole', N'Query,', NULL, NULL, CAST(N'2024-04-26T11:18:02.463' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (12, 1, N'部门管理', N'ele-Briefcase', N'system/dept', N'/system/dept', N'system/dept/index', 0, 1, 0, 0, 0, N'', N'', N'Menu', 3, N'SysDept', N'Query,', NULL, NULL, CAST(N'2024-04-26T11:17:59.407' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (13, 1, N'字典管理', N'ele-Reading', N'system/dic', N'/system/dic', N'system/dic/index', 0, 1, 0, 0, 0, N'', N'', N'Menu', 4, N'SysDict', N'Query,', NULL, NULL, CAST(N'2024-04-26T11:17:43.700' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (14, 1, N'服务监控', N'ele-Platform', N'system/server', N'/system/server', N'system/server/index', 0, 1, 0, 0, 0, N'', N'', N'Menu', 5, N'', N'Query,', NULL, NULL, CAST(N'2024-04-26T11:17:40.230' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (20, 2, N'用户管理', N'ele-UserFilled', N'user/user', N'/user/user', N'system/user/index', 0, 1, 0, 0, 0, N'', N'', N'Menu', 0, N'SysUser', N'Query,', NULL, NULL, CAST(N'2024-04-09T13:57:55.323' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (30, 3, N'通知公告', N'ele-BellFilled', N'notice/notice', N'/notice/notice', N'system/notice/index', 0, 1, 0, 0, 0, N'', N'', N'Menu', 0, N'SysNotice', N'Query,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (100, 10, N'新增', N'', N'Add', N'/system/menu', N'system/menu/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysMenu', N'Add,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (101, 10, N'删除', N'', N'Delete', N'/system/menu', N'system/menu/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysMenu', N'Delete,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (102, 10, N'编辑', N'', N'Put', N'/system/menu', N'system/menu/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysMenu', N'Put,', NULL, NULL, CAST(N'2024-04-18T15:03:25.807' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (110, 11, N'新增', N'', N'Add', N'/system/role', N'system/role/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysRole', N'Add,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (111, 11, N'编辑', N'', N'Put', N'/system/role', N'system/role/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysRole', N'Put,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (112, 11, N'删除', N'', N'Delete', N'/system/role', N'system/role/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysRole', N'Delete,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (113, 11, N'设置权限', N'', N'Audit', N'/system/role', N'system/role/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysRole', N'System,', NULL, NULL, CAST(N'2024-04-18T15:10:35.970' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (120, 12, N'新增', N'', N'Add', N'/system/dept', N'system/dept/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysDept', N'Add,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (121, 12, N'编辑', N'', N'Put', N'/system/dept', N'system/dept/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysDept', N'Put,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (122, 12, N'删除', N'', N'Delete', N'/system/dept', N'system/dept/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysDept', N'Delete,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (130, 13, N'新增', N'', N'Add', N'/system/dic', N'system/dic/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysDict', N'Add,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (131, 13, N'编辑', N'', N'Put', N'/system/dic', N'system/dic/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysDict', N'Put,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (132, 13, N'删除', N'', N'Delete', N'/system/dic', N'system/dic/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysDict', N'Delete,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (200, 20, N'新增', N'', N'Add', N'/user/user', N'system/user/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysUser', N'Add,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (201, 20, N'编辑', N'', N'Put', N'/user/user', N'system/user/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysUser', N'Put,', NULL, NULL, CAST(N'2024-04-18T15:02:54.637' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (202, 20, N'删除', N'', N'Delete', N'/user/user', N'system/user/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysUser', N'Delete,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (203, 20, N'重置密码', N'', N'System', N'/user/user', N'system/user/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysUser', N'System,', NULL, NULL, CAST(N'2024-04-18T14:10:14.737' AS DateTime), 530124946407493)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (300, 30, N'新增', N'', N'Add', N'/notice/notice', N'system/notice/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysNotice', N'Add,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (301, 30, N'编辑', N'', N'Put', N'/notice/notice', N'system/notice/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysNotice', N'Put,', NULL, NULL, NULL, NULL)
INSERT [dbo].[SysMenu] ([Id], [ParentId], [Title], [Icon], [Name], [Path], [Component], [IsHide], [IsKeepAlive], [IsIframe], [IsAffix], [IsLink], [Redirect], [Url], [Type], [Sort], [ApiTag], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (302, 30, N'删除', N'', N'Delete', N'/notice/notice', N'system/notice/index', 0, 0, 0, 0, 0, N'', N'', N'Button', 0, N'SysNotice', N'Delete,', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[SysRole] ([Id], [Name], [Desc], [Status], [DataRang], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (1, N'超级管理员', N'超级管理员', 1, N'全部', N'', CAST(N'2019-11-22T00:00:00.000' AS DateTime), NULL, CAST(N'2024-04-08T17:18:25.900' AS DateTime), 530124946407493)
INSERT [dbo].[SysRole] ([Id], [Name], [Desc], [Status], [DataRang], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (2, N'管理员', N'管理员', 1, N'全部', N'', CAST(N'2024-04-01T13:58:54.660' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[SysRole] ([Id], [Name], [Desc], [Status], [DataRang], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (3, N'总经理', N'总经理', 1, N'本部门及以下', N'', CAST(N'2024-04-01T13:58:54.660' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[SysRole] ([Id], [Name], [Desc], [Status], [DataRang], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (4, N'部门经理', N'部门经理', 1, N'本部门', N'', CAST(N'2024-04-01T13:58:54.660' AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[SysRole] ([Id], [Name], [Desc], [Status], [DataRang], [Permission], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (5, N'员工', N'员工', 1, N'仅本人', N'', CAST(N'2024-04-08T17:19:07.000' AS DateTime), NULL, CAST(N'2024-04-08T16:58:03.343' AS DateTime), 530124946407493)
GO
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (55, 2, 2)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (56, 2, 20)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (57, 2, 202)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (58, 2, 203)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (59, 2, 200)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (60, 2, 201)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (61, 2, 3)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (62, 2, 30)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (63, 2, 300)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (64, 2, 301)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (65, 2, 302)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (66, 2, 4)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (67, 2, 40)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (68, 2, 403)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (69, 2, 404)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (70, 2, 400)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (71, 2, 401)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (72, 2, 402)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (73, 2, 5)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (74, 2, 50)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (75, 2, 500)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (76, 2, 501)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (77, 2, 502)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (78, 2, 6)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (79, 2, 60)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (80, 2, 61)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233164357, 1, 1)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233168453, 1, 540535066394693)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172549, 1, 10)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172550, 1, 100)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172551, 1, 101)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172552, 1, 102)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172553, 1, 11)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172554, 1, 110)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172555, 1, 111)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172556, 1, 112)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172557, 1, 113)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172558, 1, 12)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172559, 1, 120)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172560, 1, 121)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172561, 1, 122)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172562, 1, 13)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172563, 1, 130)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172564, 1, 131)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172565, 1, 132)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172566, 1, 14)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172567, 1, 2)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172568, 1, 20)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172569, 1, 200)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172570, 1, 201)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172571, 1, 202)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172572, 1, 203)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172573, 1, 3)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172574, 1, 30)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172575, 1, 300)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172576, 1, 301)
INSERT [dbo].[SysRoleMenu] ([Id], [RoleId], [MenuId]) VALUES (540541233172577, 1, 302)
GO
INSERT [dbo].[SysUser] ([Id], [Name], [Account], [Password], [Salt], [Phone], [IdCard], [Address], [Remark], [UserState], [CreateTime], [CreateUserId], [UpdateTime], [UpdateUserId]) VALUES (530124946407493, N'测试名称', N'admin', N'1680efb4eda56fdf7fe72b0c0bce553f', N'5vwQC3aX6SWZpUfNOEOlJQ==', N'15317285539', N'430981197105034434', N'湖南省 益阳市 沅江市', N'111111', 1, CAST(N'2024-03-28T09:43:57.763' AS DateTime), NULL, CAST(N'2024-04-26T20:11:48.757' AS DateTime), 530124946407493)
GO
INSERT [dbo].[SysUserRole] ([Id], [UserId], [RoleId]) VALUES (537634613358661, 530124946407493, 1)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix01]    Script Date: 2024/4/28 0:24:25 ******/
CREATE UNIQUE NONCLUSTERED INDEX [ix01] ON [dbo].[SysDept]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix1]    Script Date: 2024/4/28 0:24:25 ******/
CREATE NONCLUSTERED INDEX [ix1] ON [dbo].[SysDictData]
(
	[TypeId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix1]    Script Date: 2024/4/28 0:24:25 ******/
CREATE NONCLUSTERED INDEX [ix1] ON [dbo].[SysDictType]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [ix01]    Script Date: 2024/4/28 0:24:25 ******/
CREATE UNIQUE NONCLUSTERED INDEX [ix01] ON [dbo].[SysRole]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级部门ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'TypeId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典值1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'Value1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典值2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'Value2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典值3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'Value3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据字典数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据字典类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'DeptId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'IP' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'IP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'UAStr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'浏览器' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'Browser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'OS'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'Device'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLoginLog', @level2type=N'COLUMN',@level2name=N'LoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Icon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'路由地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组件路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Component'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否隐藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IsHide'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否缓存' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IsKeepAlive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否内嵌网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IsIframe'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否固定(不允许关闭)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IsAffix'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否外部连接(新标签打开)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'IsLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重定向地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Redirect'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部或者Iframe网址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'访问的api控制器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'ApiTag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'Permission'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysNotice', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysNotice', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysNotice', @level2type=N'COLUMN',@level2name=N'Content'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型(10公告 20通知)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysNotice', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysNotice', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据范围' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'DataRang'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据权限(当范围为自定义时选择的部门)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'Permission'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRoleMenu', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRoleMenu', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRoleMenu', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Account'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码加盐码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Salt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'身份证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'IdCard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'UserState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'CreateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUserRole', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUserRole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
USE [master]
GO
ALTER DATABASE [AhChaFortunate] SET  READ_WRITE 
GO
