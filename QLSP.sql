USE [QLSanPham]
GO
/****** Object:  Table [dbo].[LoaiSP]    Script Date: 12/25/2024 10:11:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiSP](
	[MaLoai] [char](2) NOT NULL,
	[TenLoai] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 12/25/2024 10:11:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [char](6) NOT NULL,
	[TenSP] [nvarchar](30) NULL,
	[Ngaynhap] [datetime] NULL,
	[MaLoai] [char](2) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai]) VALUES (N'1 ', N'Bánh kẹo')
INSERT [dbo].[LoaiSP] ([MaLoai], [TenLoai]) VALUES (N'2 ', N'Giải khát')
GO
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Ngaynhap], [MaLoai]) VALUES (N'SP0001', N'Bánh quy bơ sữa', CAST(N'2014-08-20T12:00:00.000' AS DateTime), N'1 ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Ngaynhap], [MaLoai]) VALUES (N'SP0002', N'Bánh mì kẹp thịt', CAST(N'2014-05-24T12:00:00.000' AS DateTime), N'2 ')
INSERT [dbo].[SanPham] ([MaSP], [TenSP], [Ngaynhap], [MaLoai]) VALUES (N'SP0003', N'Bia lon 333', CAST(N'2014-04-20T12:00:00.000' AS DateTime), N'2 ')
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([MaLoai])
REFERENCES [dbo].[LoaiSP] ([MaLoai])
GO
