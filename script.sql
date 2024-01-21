USE [master]
GO
/****** Object:  Database [MasrafOdemeSistemi]    Script Date: 21.01.2024 21:55:33 ******/
CREATE DATABASE [MasrafOdemeSistemi]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MasrafOdemeSistemi', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MasrafOdemeSistemi.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MasrafOdemeSistemi_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MasrafOdemeSistemi_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MasrafOdemeSistemi] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MasrafOdemeSistemi].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MasrafOdemeSistemi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET ARITHABORT OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET  MULTI_USER 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MasrafOdemeSistemi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MasrafOdemeSistemi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MasrafOdemeSistemi] SET QUERY_STORE = ON
GO
ALTER DATABASE [MasrafOdemeSistemi] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MasrafOdemeSistemi]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21.01.2024 21:55:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 21.01.2024 21:55:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminNumber] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expense]    Script Date: 21.01.2024 21:55:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expense](
	[ExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[PersonalNumber] [int] NOT NULL,
	[ExpenseName] [nvarchar](50) NOT NULL,
	[ExpenseCategory] [nvarchar](50) NOT NULL,
	[ApprovalStatus] [int] NOT NULL,
	[ExpenseCreateDate] [datetime2](7) NOT NULL,
	[ExpenseAmount] [decimal](18, 4) NOT NULL,
	[ExpenseDescription] [nvarchar](100) NOT NULL,
	[InvoiceImageFilePath] [nvarchar](150) NOT NULL,
	[Location] [nvarchar](150) NULL,
	[DeciderAdminNumber] [int] NULL,
	[AdminNumber] [int] NULL,
	[DecisionDescription] [nvarchar](max) NULL,
	[DecisionDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 21.01.2024 21:55:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseId] [int] NOT NULL,
	[IBAN] [nvarchar](34) NOT NULL,
	[PaymentAmount] [decimal](18, 4) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[PaymentType] [nvarchar](30) NOT NULL,
	[ExpenseDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 21.01.2024 21:55:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personal](
	[PersonalNumber] [int] IDENTITY(1,1) NOT NULL,
	[IBAN] [nvarchar](34) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED 
(
	[PersonalNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240116144748_initialcrate', N'8.0.0')
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([AdminNumber], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (1, N'aliilman', N'Admin', N'Ali', N'İlman', N'ali.ilman@akbank.com')
INSERT [dbo].[Admin] ([AdminNumber], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (2, N'veliliman', N'Admin', N'Veli', N'liman', N'veli.liman@akbank.com')
INSERT [dbo].[Admin] ([AdminNumber], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (3, N'ismailkartal', N'Admin', N'İsmail', N'Kartal', N'ismail.kartal@fenerbahce.com')
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Expense] ON 

INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (1, 1, N'Akaryatık almı', N'Akaryakıt', 2, CAST(N'2024-01-16T17:48:49.0945033' AS DateTime2), CAST(2000.0000 AS Decimal(18, 4)), N'Muğlaya giderken yakıt aldım', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'Gulf petrol', 1, NULL, N'Harcamanız onaylanmış ve ödeme emri tanımlanmıştır. Onay açılaması: onayladım', CAST(N'2024-01-16T17:58:31.7745024' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (2, 2, N'Akaryatık almı', N'Akaryakıt', 3, CAST(N'2024-01-16T17:53:01.7329286' AS DateTime2), CAST(1000.0000 AS Decimal(18, 4)), N'Muğlaya giderken yakıt aldım', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'Gulf petrol', 1, NULL, N'Harcamanız Reddedilmiştir. Açıkama: Bu harcama şirket harcaması kapsamına giremez', CAST(N'2024-01-17T00:38:31.6401930' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (3, 3, N'Konaklama', N'Konaklama', 1, CAST(N'2024-01-17T14:13:35.4462160' AS DateTime2), CAST(1500.0000 AS Decimal(18, 4)), N'A otelde konaklama ücreti', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'Muğla', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (4, 1, N'Yemek', N'Beslenme', 3, CAST(N'2024-01-17T15:14:36.4007713' AS DateTime2), CAST(1500.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'izmir', 2, NULL, N'Harcamanız Reddedilmiştir. Açıkama: Öğlen yemeği için 1500 lira harcama keyfidir', CAST(N'2024-01-18T17:04:42.6825662' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (5, 5, N'Kırtasiye', N'İçAlım', 2, CAST(N'2024-01-17T15:17:02.4062304' AS DateTime2), CAST(1500.0000 AS Decimal(18, 4)), N'ofis malzemesi temini', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'kadıköy', 1, NULL, N'Harcamanız onaylanmış ve ödeme emri tanımlanmıştır. Onay açılaması: string', CAST(N'2024-01-17T17:07:13.7378447' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (6, 1, N'yemek', N'Beslenme', 3, CAST(N'2024-01-17T15:18:10.8058693' AS DateTime2), CAST(1500.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'beşiktaş', 2, NULL, N'Harcamanız Reddedilmiştir. Açıkama: 1500 lira öğlen yemeği keyfi bir harcamadır', CAST(N'2024-01-18T17:11:04.1475092' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (7, 1, N'Konaklama', N'Konaklama', 3, CAST(N'2023-01-17T15:19:53.3943908' AS DateTime2), CAST(1500.0000 AS Decimal(18, 4)), N'A otelde konaklama ücreti', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'esenyurt', 1, NULL, N'Harcamanız Reddedilmiştir. Açıkama: string', CAST(N'2024-01-21T18:38:10.8942663' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (8, 2, N'Yemek', N'Beslenme', 1, CAST(N'2023-02-17T15:20:58.6071293' AS DateTime2), CAST(550.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'eminönü', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (9, 1, N'Temizlik malzemesi ', N'İçAlım', 1, CAST(N'2023-03-17T15:51:33.1015422' AS DateTime2), CAST(5000.0000 AS Decimal(18, 4)), N'Temizlik malzemesi temini', N'/uploads/799f7b3d-7b46-4728-a756-3445f0ff97d1_fatura-av-asilcan-tuzcu.jpg', N'bornova', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (10, 2, N'Yemek', N'Beslenme', 3, CAST(N'2023-04-17T15:54:09.6722940' AS DateTime2), CAST(1750.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'bodrum', 2, NULL, N'Harcamanız Reddedilmiştir. Açıkama: Öğlen yemeği için 1750 lira harcama keyfidir ', CAST(N'2024-01-18T17:05:38.4200200' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (11, 1, N'Konaklama', N'Konaklama', 1, CAST(N'2023-05-17T15:57:51.6795154' AS DateTime2), CAST(600.0000 AS Decimal(18, 4)), N'A otelde konaklama ücreti', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'ankara', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (12, 6, N'Yemek', N'Beslenme', 1, CAST(N'2023-06-17T16:05:20.2711619' AS DateTime2), CAST(750.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'Muğla', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (13, 2, N'Market', N'İçAlım', 1, CAST(N'2023-07-17T16:09:18.1391083' AS DateTime2), CAST(300.0000 AS Decimal(18, 4)), N'Market malzemesi temini', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'aydın', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (14, 1, N'Yemek', N'Beslenme', 1, CAST(N'2023-10-17T16:18:57.8016182' AS DateTime2), CAST(200.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'eskişehir', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (15, 3, N'Konaklama', N'Konaklama', 1, CAST(N'2023-11-17T16:29:45.9284053' AS DateTime2), CAST(1450.0000 AS Decimal(18, 4)), N'A otelde konaklama ücreti', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'gaziantep', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (16, 1, N'Yemek', N'Beslenme', 1, CAST(N'2023-12-17T16:35:52.2276067' AS DateTime2), CAST(900.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'samsun', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (17, 5, N'Temizlik malzemesi ', N'İçAlım', 1, CAST(N'2024-01-01T16:40:28.0070741' AS DateTime2), CAST(5000.0000 AS Decimal(18, 4)), N'Temizlik malzemesi temini', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'bursa', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (18, 6, N'Yemek', N'Beslenme', 1, CAST(N'2024-01-05T17:04:12.4843858' AS DateTime2), CAST(100.0000 AS Decimal(18, 4)), N'öğlen yemeği', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'kocaeli', NULL, NULL, NULL, NULL)
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (19, 2, N'Konaklama', N'Konaklama', 3, CAST(N'2024-01-10T17:20:23.7100974' AS DateTime2), CAST(850.0000 AS Decimal(18, 4)), N'A otelde konaklama ücreti', N'/uploads/88ff7d06-1be3-45d2-996d-6067c4938e67_300_Akbank_Odev-1_2241a85fb3444bc4bfafafd7def020fd.pdf', N'buca', 1, NULL, N'Harcamanız Reddedilmiştir. Açıkama: Proje Dökümanı kapsamında örnek olarak bu kaydı reddediyorum', CAST(N'2024-01-20T20:43:07.1195189' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (21, 1, N'Akşam Yemeği', N'Beslenme', 2, CAST(N'2024-01-18T22:57:12.0307021' AS DateTime2), CAST(153.0000 AS Decimal(18, 4)), N'Akşam yemepi ücreti', N'filepath', N'aile evi', 1, NULL, N'Harcamanız onaylanmış ve ödeme emri tanımlanmıştır. Onay açılaması: Yemek harcamanız onaylanmıştır', CAST(N'2024-01-18T22:59:23.5148630' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (22, 1, N'Proje raporu harcaması', N'Dokuman hazırlama', 2, CAST(N'2024-01-20T20:22:18.8101147' AS DateTime2), CAST(500.0000 AS Decimal(18, 4)), N'Bu bir örnek harcamadır. Ekran görüntüsü alınıp raporlanacaktır', N'/uploads/a4abd3c2-3f1f-4d8e-899c-6f73a651a23f_ÖrnekFiş.jpg', N'Alinin Evi', 1, NULL, N'Harcamanız onaylanmış ve ödeme emri tanımlanmıştır. Onay açılaması: Test için oluşturulmış kaydınız onaylanmıştır', CAST(N'2024-01-20T20:36:02.0870081' AS DateTime2))
INSERT [dbo].[Expense] ([ExpenseId], [PersonalNumber], [ExpenseName], [ExpenseCategory], [ApprovalStatus], [ExpenseCreateDate], [ExpenseAmount], [ExpenseDescription], [InvoiceImageFilePath], [Location], [DeciderAdminNumber], [AdminNumber], [DecisionDescription], [DecisionDate]) VALUES (23, 1, N'Sunum videsu', N'Raporlama', 2, CAST(N'2024-01-21T18:36:22.2797388' AS DateTime2), CAST(1000.0000 AS Decimal(18, 4)), N'Sunum videomda göstermek amacı ile bunu giriyorum', N'/uploads/16d34852-16a4-4b86-9ba5-95dc769f13c8_a4abd3c2-3f1f-4d8e-899c-6f73a651a23f_ÖrnekFiş.jpg', N'Alinin evi', 1, NULL, N'Harcamanız onaylanmış ve ödeme emri tanımlanmıştır. Onay açılaması: onayladım', CAST(N'2024-01-21T18:37:13.8793263' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Expense] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 

INSERT [dbo].[Payment] ([PaymentId], [ExpenseId], [IBAN], [PaymentAmount], [Description], [PaymentType], [ExpenseDate]) VALUES (1, 1, N'12345678981234456798', CAST(2000.0000 AS Decimal(18, 4)), N'1 nolu şirket harcamanızın ücreti. Onay açıklaması:onayladım', N'EFT', CAST(N'2024-01-16T17:58:31.7748060' AS DateTime2))
INSERT [dbo].[Payment] ([PaymentId], [ExpenseId], [IBAN], [PaymentAmount], [Description], [PaymentType], [ExpenseDate]) VALUES (2, 5, N'12345678981234456798', CAST(1500.0000 AS Decimal(18, 4)), N'5 nolu şirket harcamanızın ücreti. Onay açıklaması:string', N'EFT', CAST(N'2024-01-17T17:07:13.7384335' AS DateTime2))
INSERT [dbo].[Payment] ([PaymentId], [ExpenseId], [IBAN], [PaymentAmount], [Description], [PaymentType], [ExpenseDate]) VALUES (4, 21, N'12345678981234456798258763', CAST(153.0000 AS Decimal(18, 4)), N'21 nolu şirket harcamanızın ücreti. Onay açıklaması:Yemek harcamanız onaylanmıştır', N'EFT', CAST(N'2024-01-18T22:59:26.4785422' AS DateTime2))
INSERT [dbo].[Payment] ([PaymentId], [ExpenseId], [IBAN], [PaymentAmount], [Description], [PaymentType], [ExpenseDate]) VALUES (5, 22, N'12345678981234456798', CAST(500.0000 AS Decimal(18, 4)), N'22 nolu şirket harcamanızın ücreti. Onay açıklaması:Test için oluşturulmış kaydınız onaylanmıştır', N'EFT', CAST(N'2024-01-20T20:36:02.0871924' AS DateTime2))
INSERT [dbo].[Payment] ([PaymentId], [ExpenseId], [IBAN], [PaymentAmount], [Description], [PaymentType], [ExpenseDate]) VALUES (7, 23, N'12345678981234456798', CAST(1000.0000 AS Decimal(18, 4)), N'23 nolu şirket harcamanızın ücreti. Onay açıklaması:onayladım', N'EFT', CAST(N'2024-01-21T18:37:13.8795592' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[Personal] ON 

INSERT [dbo].[Personal] ([PersonalNumber], [IBAN], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (1, N'12345678981234456798', N'ferdikadi', N'personal', N'Ferdi', N'Kadi', N'ferdi.kadi@akbank.com')
INSERT [dbo].[Personal] ([PersonalNumber], [IBAN], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (2, N'56412345678981234456798', N'ardagul', N'personal', N'Arda', N'Gul', N'Arda.gul@akbank.com')
INSERT [dbo].[Personal] ([PersonalNumber], [IBAN], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (3, N'1233456789856451234456798', N'sebastiansimanski', N'personal', N'Sebastian', N'simanski', N'sebastian.simanski@akbank.com')
INSERT [dbo].[Personal] ([PersonalNumber], [IBAN], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (5, N'12345678981541654234456798', N'edinceko', N'personal', N'Edin', N'Ceko', N'Edin.ceko@akbank.com')
INSERT [dbo].[Personal] ([PersonalNumber], [IBAN], [UserName], [Password], [FirstName], [LastName], [Email]) VALUES (6, N'15875313848453155153484351', N'dusantadic', N'personal', N'Dusan', N'Tadic', N'dusan.tadic@akbank.com')
SET IDENTITY_INSERT [dbo].[Personal] OFF
GO
/****** Object:  Index [IX_Admin_AdminNumber]    Script Date: 21.01.2024 21:55:33 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Admin_AdminNumber] ON [dbo].[Admin]
(
	[AdminNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Expense_AdminNumber]    Script Date: 21.01.2024 21:55:33 ******/
CREATE NONCLUSTERED INDEX [IX_Expense_AdminNumber] ON [dbo].[Expense]
(
	[AdminNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Expense_ExpenseId]    Script Date: 21.01.2024 21:55:33 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Expense_ExpenseId] ON [dbo].[Expense]
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Expense_PersonalNumber]    Script Date: 21.01.2024 21:55:33 ******/
CREATE NONCLUSTERED INDEX [IX_Expense_PersonalNumber] ON [dbo].[Expense]
(
	[PersonalNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Payment_ExpenseId]    Script Date: 21.01.2024 21:55:33 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Payment_ExpenseId] ON [dbo].[Payment]
(
	[ExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Payment_PaymentId]    Script Date: 21.01.2024 21:55:33 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Payment_PaymentId] ON [dbo].[Payment]
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Personal_PersonalNumber]    Script Date: 21.01.2024 21:55:33 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Personal_PersonalNumber] ON [dbo].[Personal]
(
	[PersonalNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Admin_AdminNumber] FOREIGN KEY([AdminNumber])
REFERENCES [dbo].[Admin] ([AdminNumber])
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Admin_AdminNumber]
GO
ALTER TABLE [dbo].[Expense]  WITH CHECK ADD  CONSTRAINT [FK_Expense_Personal_PersonalNumber] FOREIGN KEY([PersonalNumber])
REFERENCES [dbo].[Personal] ([PersonalNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Expense] CHECK CONSTRAINT [FK_Expense_Personal_PersonalNumber]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Expense_ExpenseId] FOREIGN KEY([ExpenseId])
REFERENCES [dbo].[Expense] ([ExpenseId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Expense_ExpenseId]
GO
USE [master]
GO
ALTER DATABASE [MasrafOdemeSistemi] SET  READ_WRITE 
GO
