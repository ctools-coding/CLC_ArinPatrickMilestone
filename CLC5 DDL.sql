USE [master]
GO
/****** Object:  Database [dbMinesweeper]    Script Date: 3/29/2021 4:50:15 PM ******/
CREATE DATABASE [dbMinesweeper]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbMinesweeperUser', FILENAME = N'C:\Users\snipa\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\dbMinesweeperUser.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'dbMinesweeperUser_log', FILENAME = N'C:\Users\snipa\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\dbMinesweeperUser.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [dbMinesweeper] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbMinesweeper].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbMinesweeper] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [dbMinesweeper] SET ANSI_NULLS ON 
GO
ALTER DATABASE [dbMinesweeper] SET ANSI_PADDING ON 
GO
ALTER DATABASE [dbMinesweeper] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [dbMinesweeper] SET ARITHABORT ON 
GO
ALTER DATABASE [dbMinesweeper] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbMinesweeper] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbMinesweeper] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbMinesweeper] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbMinesweeper] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [dbMinesweeper] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [dbMinesweeper] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbMinesweeper] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [dbMinesweeper] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbMinesweeper] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbMinesweeper] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbMinesweeper] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbMinesweeper] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbMinesweeper] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbMinesweeper] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbMinesweeper] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbMinesweeper] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbMinesweeper] SET RECOVERY FULL 
GO
ALTER DATABASE [dbMinesweeper] SET  MULTI_USER 
GO
ALTER DATABASE [dbMinesweeper] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbMinesweeper] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbMinesweeper] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbMinesweeper] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [dbMinesweeper] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [dbMinesweeper] SET QUERY_STORE = OFF
GO
USE [dbMinesweeper]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [dbMinesweeper]
GO
/****** Object:  Table [dbo].[Games]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Games](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GameString] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[First] [nvarchar](25) NULL,
	[Last] [nvarchar](25) NULL,
	[Gender] [nvarchar](25) NULL,
	[Age] [int] NULL,
	[State] [nvarchar](2) NULL,
	[Email] [nvarchar](50) NULL,
	[Username] [nvarchar](25) NULL,
	[Password] [nvarchar](25) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteUser]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteUser]
	@UserID int
AS
BEGIN
	DELETE FROM Users WHERE UserID=@UserID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllUsers]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetAllUsers]
AS
BEGIN
	SELECT * FROM Users
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetByUserAndPass]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetByUserAndPass]
	@Username nvarchar(25),
	@Password nvarchar(25)
AS
BEGIN
	SELECT * FROM Users WHERE Username=@Username AND Password=@Password
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserByID]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetUserByID]
	@UserID int
AS
BEGIN
	SELECT * FROM Users WHERE UserID = @UserID
END
GO
/****** Object:  StoredProcedure [dbo].[usp_InsertUser]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_InsertUser]
	@First nvarchar(25) = '',
	@Last nvarchar(25) = '',
	@Gender nvarchar(25) = '',
	@Age int = 0,
	@State nvarchar(25) = '',
	@Email nvarchar(50) = '',
	@Username nvarchar(25) = '',
	@Password nvarchar(25) = ''
AS
BEGIN
	INSERT INTO Users (First, Last, Gender, Age, State, Email, Username, Password)
	Values (@First, @Last, @Gender, @Age, @State, @Email, @Username, @Password)
END
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateUser]    Script Date: 3/29/2021 4:50:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateUser]
	@UserID int = 0,
	@First nvarchar(25) = '',
	@Last nvarchar(25) = '',
	@Gender nvarchar(25) = '',
	@Age int = 0,
	@State nvarchar(25) = '',
	@Email nvarchar(50) = '',
	@Username nvarchar(25) = '',
	@Password nvarchar(25) = ''
AS
BEGIN
	UPDATE Users SET First=@First, Last=@Last, Gender=@Gender, Age=@Age,
	State=@State, Email=@Email, Username=@Username, Password=@Password
	WHERE UserID=@UserID
END
GO
USE [master]
GO
ALTER DATABASE [dbMinesweeper] SET  READ_WRITE 
GO
