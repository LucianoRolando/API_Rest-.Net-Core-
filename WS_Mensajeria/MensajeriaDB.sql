USE [master]
GO
/****** Object:  Database [MensajeriaDB]    Script Date: 13/11/2020 12:16:22 ******/
CREATE DATABASE [MensajeriaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MensajeriaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MensajeriaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MensajeriaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\MensajeriaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MensajeriaDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MensajeriaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MensajeriaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MensajeriaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MensajeriaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MensajeriaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MensajeriaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MensajeriaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MensajeriaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MensajeriaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MensajeriaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MensajeriaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MensajeriaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MensajeriaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MensajeriaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MensajeriaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MensajeriaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MensajeriaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MensajeriaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MensajeriaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MensajeriaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MensajeriaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MensajeriaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MensajeriaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MensajeriaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MensajeriaDB] SET  MULTI_USER 
GO
ALTER DATABASE [MensajeriaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MensajeriaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MensajeriaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MensajeriaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MensajeriaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MensajeriaDB] SET QUERY_STORE = OFF
GO
USE [MensajeriaDB]
GO
/****** Object:  UserDefinedFunction [dbo].[FunAgregarUsuario]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[FunAgregarUsuario](@nic nvarchar(10)) 

returns int
begin
Declare @id int;

Select @id = ID from Usuario where @nic = Nick;

return @id;
end;
GO
/****** Object:  Table [dbo].[Mensajes]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mensajes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EM] [int] NULL,
	[RE] [int] NULL,
	[FECHA] [datetime] NULL,
	[TEXTO] [nvarchar](max) NULL,
 CONSTRAINT [PK_Mensajes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nick] [nvarchar](10) NULL,
	[Contraseña] [nvarchar](10) NULL,
	[Nombre] [nvarchar](30) NULL,
	[Descripcion] [nvarchar](50) NULL,
	[Enlace] [nvarchar](max) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD  CONSTRAINT [FK_Mensajes_Usuario] FOREIGN KEY([EM])
REFERENCES [dbo].[Usuario] ([ID])
GO
ALTER TABLE [dbo].[Mensajes] CHECK CONSTRAINT [FK_Mensajes_Usuario]
GO
ALTER TABLE [dbo].[Mensajes]  WITH CHECK ADD  CONSTRAINT [FK_Mensajes_Usuario1] FOREIGN KEY([RE])
REFERENCES [dbo].[Usuario] ([ID])
GO
ALTER TABLE [dbo].[Mensajes] CHECK CONSTRAINT [FK_Mensajes_Usuario1]
GO
/****** Object:  StoredProcedure [dbo].[AgregarMensaje]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[AgregarMensaje]
@em int,
@re int,
@fe datetime,
@tx nvarchar(MAX)
as

Insert into Mensajes ( EM,RE,FECHA,TEXTO) VALUES (@em,@re, @fe, @tx);
GO
/****** Object:  StoredProcedure [dbo].[AgregarUsuario]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[AgregarUsuario]
@ni nvarchar(10),
@co nvarchar(10),
@no nvarchar(30),
@de nvarchar(50),
@en nvarchar(MAX)
as

if (Select dbo.FunAgregarUsuario(@ni)) is null
begin
Insert into Usuario ( Nick,Contraseña,Nombre,Descripcion,Enlace) VALUES (@ni,@co,@no,@de,@en);
Select ID from Usuario where Nick=@ni;
end
else 
Select '0';
GO
/****** Object:  StoredProcedure [dbo].[DatosUsuario]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DatosUsuario]
@IDen int 
as
Select * from Usuario where ID=@IDen;
GO
/****** Object:  StoredProcedure [dbo].[EditarUsuario]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[EditarUsuario]
@id int,
@ni nvarchar(10),
@co nvarchar(10),
@no nvarchar(30),
@de nvarchar(50),
@en nvarchar(MAX)
as

Update Usuario set Nick=@ni, Contraseña=@co, Nombre=@no, Descripcion=@de, Enlace=@en where ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[EliminarUsuario]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[EliminarUsuario]
@id int

as

Delete Usuario where ID=@id;
GO
/****** Object:  StoredProcedure [dbo].[ListarMensajes]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[ListarMensajes]
@ide int,
@idr int
as

Select * from Mensajes where (EM=@ide and RE=@idr) or (EM=@idr and RE=@ide) order by FECHA asc;
GO
/****** Object:  StoredProcedure [dbo].[ListarUsuarios]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[ListarUsuarios]
@IDen int 
as
Select * from Usuario where ID!=@IDen;
GO
/****** Object:  StoredProcedure [dbo].[Logueo]    Script Date: 13/11/2020 12:16:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Logueo]
@nic nvarchar(10),
@con nvarchar(10)
as
Select * from Usuario where Nick=@nic and Contraseña=@con;
GO
USE [master]
GO
ALTER DATABASE [MensajeriaDB] SET  READ_WRITE 
GO
