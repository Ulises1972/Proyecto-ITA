/****** Object:  Database [Tutoria]    Script Date: 01/09/2022 07:50:04 a. m. ******/
/****** Object: Databse Update [Tutrias]	Script Date Update: 10/05/2025 14:02:035 p.m *******/

-- Crea nuevamente la base de datos "Tutorias" con configuraciones más detalladas (esto puede ser redundante)
-- Define el archivo físico de datos (.mdf) y de log (.ldf) y sus configuraciones
USE [Tutorias] 

SELECT * FROM Instituto

SELECT * FROM Administrador

SELECT * FROM Carrera

SELECT * FROM Maestro

SELECT * FROM Materia

SELECT * FROM Grupo

SELECT * FROM Alumno

SELECT * FROM Alumno_Materia

SELECT * FROM Grupo_Compuesto

SELECT * FROM Administrador

SELECT Nombre, COUNT(*) 
FROM Grupo 
GROUP BY Nombre 
HAVING COUNT(*) > 1;

CREATE DATABASE [Tutorias]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tutorias', FILENAME = N'D:\OneDrive\Desktop\ProyectoResidencias2\TutoriasWeb\BD\mdf-ldf\Tutorias.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
 --C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Tutorias.mdf
( NAME = N'Tutorias_log', FILENAME = N'D:\OneDrive\Desktop\ProyectoResidencias2\TutoriasWeb\BD\mdf-ldf\Tutorias_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
--C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Tutorias_log.ldf
-- Establece la compatibilidad con SQL Server 2019 (nivel 150)
ALTER DATABASE [Tutorias] SET COMPATIBILITY_LEVEL = 150
GO
-- Habilita el servicio de búsqueda de texto completo (si está instalado)
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tutorias].[dbo].[sp_fulltext_database] @action = 'enable' --Procedimiento almacenado proporcionado por SQL Server para gestionar las configuraciones de Full-Text Search.
end
GO


ALTER DATABASE [Tutorias] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tutorias] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tutorias] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tutorias] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tutorias] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tutorias] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Tutorias] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tutorias] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tutorias] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tutorias] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tutorias] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tutorias] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tutorias] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tutorias] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tutorias] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Tutorias] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tutorias] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tutorias] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tutorias] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tutorias] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tutorias] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tutorias] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tutorias] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Tutorias] SET  MULTI_USER 
GO
ALTER DATABASE [Tutorias] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tutorias] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tutorias] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tutorias] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Tutorias] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Tutorias] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Tutorias] SET QUERY_STORE = OFF
GO
-- Cambia el contexto a la base de datos recién creada para operaciones posteriores
USE [Tutorias]
GO

--TABLA INSTITUTO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instituto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Logo] [varchar](max) NOT NULL,
	[Sitio] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SELECT * FROM Instituto


--TABLA ADMINISTRADOR
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrador](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](30) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[A_Paterno] [varchar](15) NOT NULL,
	[A_Materno] [varchar](15) NOT NULL,
	[Clave] [varchar](150) NOT NULL,
	[ID_Carrera] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SELECT * FROM Administrador


--TABLA CARRERA
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrera](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NOT NULL,
	[Logo] [varchar](max) NOT NULL,
	[Estatus] [varchar](15) NOT NULL,
	[Omoclave] [varchar](15) NOT NULL,
	[ID_Instituto] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SELECT * FROM Carrera


--TABLA MAESTRO 
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Maestro](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RFC] [varchar](13) NOT NULL,
	[Usuario] [varchar](30) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[A_Paterno] [varchar](20) NOT NULL,
	[A_Materno] [varchar](20) NOT NULL,
	[Clave] [varchar](150) NOT NULL,
	[Estatus] [varchar](15) NOT NULL,
	[ID_Carrera] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE UNIQUE NONCLUSTERED INDEX IN_Maestro_Usuario
ON dbo.Maestro ([Usuario] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];
GO

SELECT * FROM Maestro
WHERE ID_Carrera = 4


--TABLA MATERIA
CREATE TABLE [dbo].[Materia](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Nombre_Corto] [varchar](30) NULL,
	[Semestre] [tinyint] NOT NULL,
	[Estatus] [varchar](20) NOT NULL,
	[ID_Carrera] [int] NOT NULL,
	[ID_Maestro] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SELECT * FROM Materia


--TABLA GRUPO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[Estatus] [varchar](15) NOT NULL,
	[ID_Maestro] [int] NOT NULL,
	[ID_Materia] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
[ID] ASC

)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SELECT * FROM Grupo


--TABLA ALUMNO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alumno](
	[No_control] [int] NOT NULL,
	[Usuario] [varchar](30) NOT NULL,
	[Nombre] [varchar](40) NOT NULL,
	[A_Paterno] [varchar](20) NOT NULL,
	[A_Materno] [varchar](20) NOT NULL,
	[Semestre] [int] NOT NULL,
	[Estatus] [varchar](15) NOT NULL,
	[Tutoria] [nvarchar] (15) NOT NULL,
	[Clave] [varchar](200) NULL,
	[ID_Carrera] [int] NOT NULL,
CONSTRAINT [PK__Alumno__D50D6F686547A617] PRIMARY KEY CLUSTERED 
(
	[No_control] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE UNIQUE NONCLUSTERED INDEX IN_Alumno_Usuario
ON dbo.Alumno ([Usuario] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
) ON [PRIMARY];
GO

SELECT * FROM Alumno


--TABLA ALUMNO_MATERIA
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alumno_Materia](
	[No_control] [int] NOT NULL,
	[ID_Materia] [int] NOT NULL,
	[ID_Grupo] [int] NOT NULL,
	[u1_1] [decimal](5, 2) NULL,
	[u1_2] [decimal](5, 2) NULL,
	[u2_1] [decimal](5, 2) NULL,
	[u2_2] [decimal](5, 2) NULL,
	[u3_1] [decimal](5, 2) NULL,
	[u3_2] [decimal](5, 2) NULL,
	[u4_1] [decimal](5, 2) NULL,
	[u4_2] [decimal](5, 2) NULL,
	[u5_1] [decimal](5, 2) NULL,
	[u5_2] [decimal](5, 2) NULL,
	[u6_1] [decimal](5, 2) NULL,
	[u6_2] [decimal](5, 2) NULL,
	[u7_1] [decimal](5, 2) NULL,
	[u7_2] [decimal](5, 2) NULL,
	[u8_1] [decimal](5, 2) NULL,
	[u8_2] [decimal](5, 2) NULL,
	[Promedio] [decimal](5, 2) NULL,
	[Semestre] [int] NOT NULL,
[ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE Alumno_Materia
ADD CONSTRAINT PK_Alumno_Materia PRIMARY KEY (ID);

SELECT * FROM Alumno_Materia


--TABLA GRUPO_COMPUESTO
SET ANSI_NULLS ON 
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grupo_Compuesto](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ID_Grupo] [int] NOT NULL,
	[No_control] [int] NOT NULL,
	[ID_AlumMat][int] NOT NULL, 
	[Comentarios] [varchar](200) NULL,
	[Cal1] [decimal](5, 2) NULL,
	[Cal2] [decimal](5, 2) NULL,
	[Cal3] [decimal](5, 2) NULL,
	[Cal4] [decimal](5, 2) NULL,
	[Cal5] [decimal](5, 2) NULL,
	[Cal6] [decimal](5, 2) NULL,
	[Entrevista1] [bit] NULL,
	[Entrevista2] [bit] NULL,
	[Entrevista3] [bit] NULL,
	[Estatus] [varchar](20) NULL,
	[Semestre] [int] NULL,
	[Asistencia1] [varchar](20) NULL,
	[Asistencia2] [varchar](20) NULL,
	[Asistencia3] [varchar](20) NULL,
	[Promedio] [decimal](5, 2) NULL,
	[Circulo_Estudio] [bit] NULL,
	[Atencion_Medica] [bit] NULL,
	[Platicas] [bit] NULL,
	[Psicologica] [bit] NULL,
	[Apoyo_Externo] [bit] NULL,
	[A] [varchar](10) NULL,
	[B] [varchar](10) NULL,
	[D] [varchar](10) NULL,
	[N] [varchar](10) NULL,
	[I] [varchar](10) NULL,
	[R] [varchar](10) NULL,
 CONSTRAINT [PK__Grupo_Co__3214EC27066CB315] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SELECT * FROM Grupo_Compuesto




--RELACIÓN: ID(instituto) > ID_Instituto(Carrera)
ALTER TABLE Carrera
ADD CONSTRAINT FK_Instituto_Carrera
FOREIGN KEY (ID_Instituto) REFERENCES Instituto(ID);

--RELACIÓN: ID(Carrera) > ID_Carrera(Administrador)
ALTER TABLE Administrador
ADD CONSTRAINT FK_Carrera_Administrador
FOREIGN KEY (ID_Carrera) REFERENCES Carrera(ID)

--RELACIÖN: ID(Carrera) > ID_Carrera(Materia)
ALTER TABLE Materia
ADD CONSTRAINT FK_Carrera_Materia
FOREIGN KEY (ID_Carrera) REFERENCES Carrera(ID)

--RELACIÖN: ID(Carrera) > ID_Carrera(Maestro)
ALTER TABLE Maestro
ADD CONSTRAINT FK_Carrera_Maestro
FOREIGN KEY (ID_Carrera) REFERENCES Carrera(ID) 

--RELACIÖN: ID(Carrera) > ID_Carrera(Alumno)
ALTER TABLE Alumno
ADD CONSTRAINT FK_Carrera_Alumno
FOREIGN KEY (ID_Carrera) REFERENCES Carrera(ID)

--RELACIÖN: ID(Maestro) > ID_Maestro(Materia)
ALTER TABLE Materia
ADD CONSTRAINT FK_Maestro_Materia
FOREIGN KEY (ID_Maestro) REFERENCES Maestro(ID)----------

--RELACIÖN: ID(Maestro) > ID_Maestro(Grupo)
ALTER TABLE Grupo
ADD CONSTRAINT FK_Maestro_Grupo
FOREIGN KEY (ID_Maestro) REFERENCES Maestro(ID)

--RELACIÖN: ID(Materia) > ID_Materia(Grupo)
ALTER TABLE Grupo
ADD CONSTRAINT FK_Materia_Grupo
FOREIGN KEY (ID_Materia) REFERENCES Materia(ID)

--RELACIÖN: No_Control(Alumno) > No_Control(Grupo) -------------------NO-------------------
ALTER TABLE Grupo
ADD CONSTRAINT FK_Alumno_Grupo
FOREIGN KEY (No_Control) REFERENCES Alumno(No_Control)

--RELACIÖN: ID(Grupo) > ID_Grupo(Alumno_Materia)
ALTER TABLE Alumno_Materia
ADD CONSTRAINT FK_Grupo_Alumno_Materia
FOREIGN KEY (ID_Grupo) REFERENCES Grupo(ID)

--RELACIÖN: ID(Grupo) > ID_Grupo(Grupo_Compuesto)
ALTER TABLE Grupo_Compuesto
ADD CONSTRAINT FK_Grupo_Grupo_Compuesto
FOREIGN KEY (ID_Grupo) REFERENCES Grupo(ID)

--RELACIÖN: No_Control(Alumno) > No_Control(Grupo_Compuesto)
ALTER TABLE Grupo_Compuesto
ADD CONSTRAINT FK_Alumno_Grupo_Compuesto
FOREIGN KEY (No_Control) REFERENCES Alumno(No_Control)

--RELACIÖN: No_Control(Alumno) > No_Control(Alumno_Materia)
ALTER TABLE Alumno_Materia
ADD CONSTRAINT FK_Alumno_Alumno_Materia
FOREIGN KEY (No_Control) REFERENCES Alumno(No_Control)

--RELACIÖN: ID(Materia) > ID_Materia(Alumno_Materia)
ALTER TABLE Alumno_Materia
ADD CONSTRAINT FK_Materia_Alumno_Materia
FOREIGN KEY (ID_Materia) REFERENCES Materia(ID)

--RELACIÓN: ID(Alumno_Materia) > ID_AlumMat(Alumno_Materia)
ALTER TABLE Grupo_Compuesto
ADD CONSTRAINT FK_Grupo_Compuesto_Alumno_Materia
FOREIGN KEY (ID_AlumMat) REFERENCES Alumno_Materia(ID);


--CONFIGURACIONES EXTRAS
--Relaciona el grupo con un maestro existente.
ALTER TABLE [dbo].[Grupo]  WITH CHECK ADD FOREIGN KEY([ID_Maestro])
REFERENCES [dbo].[Maestro] ([ID])
GO

--Asegura que Grupo_Compuesto haga referencia a un grupo válido.
ALTER TABLE [dbo].[Grupo_Compuesto]  WITH CHECK ADD  CONSTRAINT [FK__Grupo_Com__ID_Gr__3C69FB99] FOREIGN KEY([ID_Grupo])
REFERENCES [dbo].[Grupo] ([ID])
GO

-- Se vuelve a activar (CHECK) una restricción de clave foránea en la tabla Grupo_Compuesto 
-- que probablemente fue deshabilitada anteriormente para carga masiva o mantenimiento.
ALTER TABLE [dbo].[Grupo_Compuesto] CHECK CONSTRAINT [FK__Grupo_Com__ID_Gr__3C69FB99]
GO

-- Se agrega una nueva restricción de clave foránea a la tabla Grupo_Compuesto 
-- que relaciona la columna No_control con la tabla Alumno.
-- La opción WITH CHECK asegura que los datos existentes sean validados contra la clave foránea.
ALTER TABLE [dbo].[Grupo_Compuesto]  WITH CHECK ADD  CONSTRAINT [FK__Grupo_Com__No_co__3D5E1FD2] FOREIGN KEY([No_control])
REFERENCES [dbo].[Alumno] ([No_control])
GO

-- Se activa la validación de esta nueva restricción de clave foránea
ALTER TABLE [dbo].[Grupo_Compuesto] CHECK CONSTRAINT [FK__Grupo_Com__No_co__3D5E1FD2]
GO

-- Se establece que la base de datos 'Tutoria' esté en modo de lectura/escritura (no solo lectura).
ALTER DATABASE [Tutorias] SET  READ_WRITE 
GO

ALTER TABLE Alumno ADD Carrera VARCHAR(100) NULL;
ALTER TABLE Maestro ADD Carrera VARCHAR(100) NULL;
ALTER TABLE Administrador ADD Carrera VARCHAR(100) NULL;


--VERIFICAR SERVIDOR
SELECT @@SERVERNAME;



SELECT * FROM Carrera
SELECT * FROM Maestro
SELECT * FROM Materia
SELECT * FROM Grupo
SELECT * FROM Grupo_Compuesto




USE [Tutorias]


