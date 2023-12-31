USE [E_SHOPPER_DB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/4/2023 1:09:26 AM ******/
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
/****** Object:  Table [dbo].[Brands]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[Slug] [nvarchar](450) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[Slug] [nvarchar](450) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[Slug] [nvarchar](450) NOT NULL,
	[Status] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[BrandId] [int] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 12/4/2023 1:09:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231124154654_Init-First', N'6.0.24')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231124155143_Init-Second', N'6.0.24')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231125165020_Add-Quantity-in-product', N'6.0.24')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231202113223_IdentityModel', N'6.0.24')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231203153823_Order', N'6.0.24')
GO
SET IDENTITY_INSERT [dbo].[Brands] ON 

INSERT [dbo].[Brands] ([Id], [Name], [Description], [Slug], [Status]) VALUES (3, N'apple', N'Apple là một hãng công nghệ lớn trên thế giới', N'apple', 1)
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Slug], [Status]) VALUES (5, N'Macallan', N'Một thương hiệu lớn', N'Macallan', 1)
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Slug], [Status]) VALUES (6, N'samsung', N'thương hiệu lớn nhất Hàn Quốc và là hãng công nghệ hàng đầu thế giới', N'samsung', 1)
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Slug], [Status]) VALUES (7, N'Glenfiddich', N'hãng nổi tiếng', N'Glenfiddich', 1)
INSERT [dbo].[Brands] ([Id], [Name], [Description], [Slug], [Status]) VALUES (9, N'Oppo', N'Hãng điện tử đến từ Trung Quốc x', N'Oppo', 1)
SET IDENTITY_INSERT [dbo].[Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Description], [Slug], [Status]) VALUES (5, N'Rượu vang', N'Là một loại rượu ngon', N'Rượu-vang', 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [Slug], [Status]) VALUES (10, N'đồ điện tử', N'chuyên bán các mặt hàng điện tử', N'đồ-điện-tử', 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [Slug], [Status]) VALUES (11, N'đồ gia dụng', N'chuyên bán các mặt hàng gia dụng !', N'đồ-gia-dụng', 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [Slug], [Status]) VALUES (14, N'Rượu gạo', N'ngon lắm', N'Rượu-gạo', 1)
INSERT [dbo].[Category] ([Id], [Name], [Description], [Slug], [Status]) VALUES (15, N'Rượu Vodka', N'ngon không kém gì vang đỏ', N'Rượu-Vodka', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (1, 16, 4, CAST(122000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (1, 17, 6, CAST(47000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (2, 17, 6, CAST(47000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (2, 18, 1, CAST(5600000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (2, 19, 1, CAST(122222.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (4, 17, 4, CAST(47000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (4, 18, 1, CAST(5600000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (8, 16, 1, CAST(122000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (8, 17, 3, CAST(47000000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (8, 18, 3, CAST(5600000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (9, 16, 1, CAST(122000.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderDetails] ([OrderId], [ProductId], [Quantity], [Price]) VALUES (9, 17, 3, CAST(47000000.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderCode], [Name], [Address], [CreatedDate], [Status], [Quantity], [TotalPrice], [PhoneNumber], [UserId]) VALUES (1, N'a4cab980-f24b-4dc9-b22b-c7c05087c235', N'Đặng Minh Tiến', N'AD-Hải Phòng', CAST(N'2023-12-03T23:23:07.4462265' AS DateTime2), 1, 10, CAST(282488000.00 AS Decimal(18, 2)), N'0999373888', N'1f533dd6-0620-4688-9e9d-31682e0c7cbe')
INSERT [dbo].[Orders] ([Id], [OrderCode], [Name], [Address], [CreatedDate], [Status], [Quantity], [TotalPrice], [PhoneNumber], [UserId]) VALUES (2, N'a04afca7-df98-4baa-8bfa-5c6b89742d4f', N'Đặng Minh Tiến', N'HT-AD-Hải Phòng', CAST(N'2023-12-03T23:27:36.9377812' AS DateTime2), 0, 8, CAST(287722222.00 AS Decimal(18, 2)), N'0999373888', N'1f533dd6-0620-4688-9e9d-31682e0c7cbe')
INSERT [dbo].[Orders] ([Id], [OrderCode], [Name], [Address], [CreatedDate], [Status], [Quantity], [TotalPrice], [PhoneNumber], [UserId]) VALUES (4, N'92309b6a-6a8f-467c-8ff8-7a547f7be251', N'Đặng Minh Tiến', N'AD-Hải Phòng', CAST(N'2023-12-04T00:11:43.1560716' AS DateTime2), 1, 5, CAST(193600000.00 AS Decimal(18, 2)), N'0999373888', N'5aaa73ca-c0c1-4ca6-baaa-259def27a5e6')
INSERT [dbo].[Orders] ([Id], [OrderCode], [Name], [Address], [CreatedDate], [Status], [Quantity], [TotalPrice], [PhoneNumber], [UserId]) VALUES (8, N'930df5c8-b299-4158-af4a-015feb8da96e', N'Đặng Minh Tiến', N'AD-Hải Phòng', CAST(N'2023-12-04T01:01:29.3673533' AS DateTime2), 1, 7, CAST(157922000.00 AS Decimal(18, 2)), N'0999373888', N'1f533dd6-0620-4688-9e9d-31682e0c7cbe')
INSERT [dbo].[Orders] ([Id], [OrderCode], [Name], [Address], [CreatedDate], [Status], [Quantity], [TotalPrice], [PhoneNumber], [UserId]) VALUES (9, N'cda01b15-134e-4ce9-8d0c-bac756f274bc', N'Đặng Minh Tiến', N'AD-Hải Phòng', CAST(N'2023-12-04T01:02:45.6407242' AS DateTime2), 1, 4, CAST(141122000.00 AS Decimal(18, 2)), N'0999373888', N'1f533dd6-0620-4688-9e9d-31682e0c7cbe')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Slug], [Status], [Price], [CategoryId], [BrandId], [Image], [Quantity]) VALUES (16, N'Rượu vang đỏ', N'Loai rượu hảo hạng', N'Rượu-vang-đỏ', 1, CAST(122000.00 AS Decimal(18, 2)), 5, 5, N'ssucv4oj.png', 899)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Slug], [Status], [Price], [CategoryId], [BrandId], [Image], [Quantity]) VALUES (17, N'Điện thoại IPhone 15', N'Điện thoại hiện đại nhất hiện nay của nhà Apple', N'Điện-thoại-IPhone-15', 1, CAST(47000000.00 AS Decimal(18, 2)), 10, 3, N'g2c3pvlm.jpg', 98)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Slug], [Status], [Price], [CategoryId], [BrandId], [Image], [Quantity]) VALUES (18, N'điện thoại Oppo A54', N'Điện thoại tầm trung của oppo phù hợp với học sinh và sinh viên', N'điện-thoại-Oppo-A54', 1, CAST(5600000.00 AS Decimal(18, 2)), 10, 9, N'5jgc4j43.jpg', 15)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Slug], [Status], [Price], [CategoryId], [BrandId], [Image], [Quantity]) VALUES (19, N'Rượu Chilvad', N'Loại rượu hảo hạng nổi tiếng thế giới', N'Rượu-Chilvad', 1, CAST(122222.00 AS Decimal(18, 2)), 5, 5, N'cseuvxmu.png', 9)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Slug], [Status], [Price], [CategoryId], [BrandId], [Image], [Quantity]) VALUES (20, N'Rượu Vodka Nga', N'Loại rượu nổi tiếng', N'Rượu-Vodka-Nga', 1, CAST(122222.00 AS Decimal(18, 2)), 15, 5, N'3yuq1fmr.png', 111)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'0391a8f7-7413-4e38-a1f9-2815128290fe', N'Edittor', N'EDITTOR', N'31f75e4f-7b5d-4158-905e-4cacbab03a70')
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'69aae242-a6f0-4049-b274-58d3dbd880e4', N'Client', N'CLIENT', N'c9491c5a-e3c2-49a6-b2e5-2e573152753e')
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'728dbda5-b78f-46df-a8ce-6d439e288db9', N'Admin', N'ADMIN', N'bbbaa65e-2e37-4cc1-ad0c-d19e5856e30b')
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'1f533dd6-0620-4688-9e9d-31682e0c7cbe', N'0391a8f7-7413-4e38-a1f9-2815128290fe')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'd534bcab-057b-48e6-a944-2770ee611e1a', N'69aae242-a6f0-4049-b274-58d3dbd880e4')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'1f533dd6-0620-4688-9e9d-31682e0c7cbe', N'728dbda5-b78f-46df-a8ce-6d439e288db9')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'5aaa73ca-c0c1-4ca6-baaa-259def27a5e6', N'728dbda5-b78f-46df-a8ce-6d439e288db9')
GO
INSERT [dbo].[Users] ([Id], [Address], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1f533dd6-0620-4688-9e9d-31682e0c7cbe', N'AD-Hải Phòng', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEM82Mz2gCrCH4f04YqqHZQEilkMWhUIikXJ6vg/Hqdsg5H10X2aNxTPdlRjxqL/nQw==', N'SY2LMWU74B67ECUHE2PVOMLS2MUKSWRQ', N'40dc6e4f-eae6-483e-ac9b-fd6e61f4e788', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [Address], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5aaa73ca-c0c1-4ca6-baaa-259def27a5e6', N'AD-Hải Phòng', N'mtien280168@gmail.com', N'MTIEN280168@GMAIL.COM', N'mtien280168@gmail.com', N'MTIEN280168@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEGdxOL6paKb4fVkSDC9JMw6PFKCroxst26amcPgzBLHRcmKgJ509LV+379o7sBeNmw==', N'WZHITKEQOJEZ5YQHNDG4IBO324DV26CS', N'859e1c6f-7a7c-466f-a08a-6257bc71e0bf', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [Address], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd534bcab-057b-48e6-a944-2770ee611e1a', N'AD-Hải Phòng', N'minhtien280168@gmail.com', N'MINHTIEN280168@GMAIL.COM', N'minhtien280168@gmail.com', N'MINHTIEN280168@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEBxyk7HBhom91nZHYexQXgUaQpVOIHNB3PHwmiUT4J5BcNlPNB2i1PzkuvS+r//SVQ==', N'C2557DCARKWHW5X57YRNYGU65AD7JRCT', N'f0c49b63-3ecf-4c4f-8746-02d1c3d0257b', NULL, 0, 0, NULL, 1, 0)
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'') FOR [Image]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Product_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brands_BrandId] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brands] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Brands_BrandId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category_CategoryId]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
