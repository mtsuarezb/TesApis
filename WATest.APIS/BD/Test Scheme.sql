USE [Test]
GO
ALTER TABLE [dbo].[Prestamo] DROP CONSTRAINT [FK_Prestamo_Libro]
GO
ALTER TABLE [dbo].[Prestamo] DROP CONSTRAINT [FK_Prestamo_Estudiante]
GO
ALTER TABLE [dbo].[LibAut] DROP CONSTRAINT [FK_LibAut_Libro]
GO
ALTER TABLE [dbo].[LibAut] DROP CONSTRAINT [FK_LibAut_Autor]
GO
/****** Object:  Table [dbo].[Prestamo]    Script Date: 8/3/2022 12:44:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Prestamo]') AND type in (N'U'))
DROP TABLE [dbo].[Prestamo]
GO
/****** Object:  Table [dbo].[Libro]    Script Date: 8/3/2022 12:44:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Libro]') AND type in (N'U'))
DROP TABLE [dbo].[Libro]
GO
/****** Object:  Table [dbo].[LibAut]    Script Date: 8/3/2022 12:44:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LibAut]') AND type in (N'U'))
DROP TABLE [dbo].[LibAut]
GO
/****** Object:  Table [dbo].[Estudiante]    Script Date: 8/3/2022 12:44:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Estudiante]') AND type in (N'U'))
DROP TABLE [dbo].[Estudiante]
GO
/****** Object:  Table [dbo].[Autor]    Script Date: 8/3/2022 12:44:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Autor]') AND type in (N'U'))
DROP TABLE [dbo].[Autor]
GO
/****** Object:  User [userTest]    Script Date: 8/3/2022 12:44:35 ******/
DROP USER [userTest]
GO
/****** Object:  User [userTest]    Script Date: 8/3/2022 12:44:35 ******/
CREATE USER [userTest] FOR LOGIN [userTest] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Autor]    Script Date: 8/3/2022 12:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autor](
	[IdAutor] [int] NOT NULL,
	[Nombre] [varchar](150) NULL,
	[Nacionalidad] [varchar](50) NULL,
 CONSTRAINT [PK_Autor] PRIMARY KEY CLUSTERED 
(
	[IdAutor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiante]    Script Date: 8/3/2022 12:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiante](
	[IdLector] [int] NOT NULL,
	[CI] [varchar](50) NULL,
	[Nombre] [varchar](150) NULL,
	[Direccion] [varchar](250) NULL,
	[Carrera] [varchar](50) NULL,
	[Edad] [int] NULL,
 CONSTRAINT [PK_Estudiante] PRIMARY KEY CLUSTERED 
(
	[IdLector] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LibAut]    Script Date: 8/3/2022 12:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibAut](
	[IdAutor] [int] NOT NULL,
	[IdLibro] [int] NOT NULL,
 CONSTRAINT [PK_LibAut] PRIMARY KEY CLUSTERED 
(
	[IdAutor] ASC,
	[IdLibro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libro]    Script Date: 8/3/2022 12:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libro](
	[IdLibro] [int] NOT NULL,
	[Titulo] [varchar](150) NULL,
	[Editorial] [varchar](100) NULL,
	[Area] [varchar](100) NULL,
 CONSTRAINT [PK_Libro] PRIMARY KEY CLUSTERED 
(
	[IdLibro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prestamo]    Script Date: 8/3/2022 12:44:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prestamo](
	[IdLector] [int] NOT NULL,
	[IdLibro] [int] NOT NULL,
	[FechaPrestamo] [datetime] NOT NULL,
	[FechaDevolucion] [datetime] NULL,
	[Devuelvo] [bit] NULL,
 CONSTRAINT [PK_Prestamo] PRIMARY KEY CLUSTERED 
(
	[IdLector] ASC,
	[IdLibro] ASC,
	[FechaPrestamo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Estudiante] ([IdLector], [CI], [Nombre], [Direccion], [Carrera], [Edad]) VALUES (1, N'1211512900001C', N'MARIA TERESA SUAREZ BOLAÑOS', N'MANAGUA, NICARAGUA CIUDAD DORAL', N'INGENIERO EN SISTEMA', 31)
GO
INSERT [dbo].[Estudiante] ([IdLector], [CI], [Nombre], [Direccion], [Carrera], [Edad]) VALUES (2, N'1213010890001C', N'HARRY JOSÉ MONTEZ DIAZ', N'MANAGUA, NICARAGUA, SUBASTE', N'ELECTRICIDAD AUTOMOTRIZ ', 32)
GO
INSERT [dbo].[Estudiante] ([IdLector], [CI], [Nombre], [Direccion], [Carrera], [Edad]) VALUES (3, N'0010305900002U', N'JOSE GREGORIO ZAMORA URBINA', N'MANAGUA, NICARAGUA', N'INGENIERÍA EN SISTEMAS', 31)
GO
INSERT [dbo].[Estudiante] ([IdLector], [CI], [Nombre], [Direccion], [Carrera], [Edad]) VALUES (4, N'0043001720004B', N'MARTINA DEL ROSARIO BOLAÑOS', N'JUIGALPA, CHONTALES', N'ADMINISTRACIÓN DE EMPRESA', 55)
GO
INSERT [dbo].[Estudiante] ([IdLector], [CI], [Nombre], [Direccion], [Carrera], [Edad]) VALUES (6, N'1211512900001E', N'ROSA MELINDA ALTAMIRADO ', N'MANAGUA, MATEARES', N'ECONOMÍA', 22)
GO
INSERT [dbo].[Libro] ([IdLibro], [Titulo], [Editorial], [Area]) VALUES (1, N'EL AMOR A CUATRO ESTACIONES', N'SAN RAFAEL', N'ROMANTICA')
GO
INSERT [dbo].[Libro] ([IdLibro], [Titulo], [Editorial], [Area]) VALUES (2, N'LA HIERBA', N'LOS MONTES', N'MIEDO')
GO
INSERT [dbo].[Libro] ([IdLibro], [Titulo], [Editorial], [Area]) VALUES (3, N'UN SITIO EN LA CUMBRE', N'APIA', N'MOTIVACIÓN')
GO
INSERT [dbo].[Libro] ([IdLibro], [Titulo], [Editorial], [Area]) VALUES (4, N'EL JILGUERO', N'LAS CUMBRE', N'MOTIVACIÓN')
GO
INSERT [dbo].[Libro] ([IdLibro], [Titulo], [Editorial], [Area]) VALUES (5, N'ONCE MINUTOS', N'SANTA FE', N'EROTISMO')
GO
INSERT [dbo].[Libro] ([IdLibro], [Titulo], [Editorial], [Area]) VALUES (6, N'AQUELLO QUE CREÍAMOS PERDIDO', N'APIA', N'ROMANTICA')
GO
INSERT [dbo].[Prestamo] ([IdLector], [IdLibro], [FechaPrestamo], [FechaDevolucion], [Devuelvo]) VALUES (1, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), CAST(N'2022-02-15T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Prestamo] ([IdLector], [IdLibro], [FechaPrestamo], [FechaDevolucion], [Devuelvo]) VALUES (1, 1, CAST(N'2022-03-04T00:00:00.000' AS DateTime), CAST(N'2022-03-05T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Prestamo] ([IdLector], [IdLibro], [FechaPrestamo], [FechaDevolucion], [Devuelvo]) VALUES (1, 1, CAST(N'2022-03-07T00:00:00.000' AS DateTime), NULL, 0)
GO
INSERT [dbo].[Prestamo] ([IdLector], [IdLibro], [FechaPrestamo], [FechaDevolucion], [Devuelvo]) VALUES (1, 4, CAST(N'2022-03-07T00:00:00.000' AS DateTime), NULL, 0)
GO
INSERT [dbo].[Prestamo] ([IdLector], [IdLibro], [FechaPrestamo], [FechaDevolucion], [Devuelvo]) VALUES (3, 3, CAST(N'2022-03-07T00:00:00.000' AS DateTime), NULL, 0)
GO
ALTER TABLE [dbo].[LibAut]  WITH CHECK ADD  CONSTRAINT [FK_LibAut_Autor] FOREIGN KEY([IdAutor])
REFERENCES [dbo].[Autor] ([IdAutor])
GO
ALTER TABLE [dbo].[LibAut] CHECK CONSTRAINT [FK_LibAut_Autor]
GO
ALTER TABLE [dbo].[LibAut]  WITH CHECK ADD  CONSTRAINT [FK_LibAut_Libro] FOREIGN KEY([IdLibro])
REFERENCES [dbo].[Libro] ([IdLibro])
GO
ALTER TABLE [dbo].[LibAut] CHECK CONSTRAINT [FK_LibAut_Libro]
GO
ALTER TABLE [dbo].[Prestamo]  WITH CHECK ADD  CONSTRAINT [FK_Prestamo_Estudiante] FOREIGN KEY([IdLector])
REFERENCES [dbo].[Estudiante] ([IdLector])
GO
ALTER TABLE [dbo].[Prestamo] CHECK CONSTRAINT [FK_Prestamo_Estudiante]
GO
ALTER TABLE [dbo].[Prestamo]  WITH CHECK ADD  CONSTRAINT [FK_Prestamo_Libro] FOREIGN KEY([IdLibro])
REFERENCES [dbo].[Libro] ([IdLibro])
GO
ALTER TABLE [dbo].[Prestamo] CHECK CONSTRAINT [FK_Prestamo_Libro]
GO
