USE [master]
GO
/****** Object:  Database [JD_FC_VD_RecipesProject]    Script Date: 28/02/2024 01:56:36 ******/
CREATE DATABASE [JD_FC_VD_RecipesProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JD_FC_VD_RecipesProject', FILENAME = N'A:\Programas\MSSQL16.MSSQLSERVER\MSSQL\DATA\JD_FC_VD_RecipesProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JD_FC_VD_RecipesProject_log', FILENAME = N'A:\Programas\MSSQL16.MSSQLSERVER\MSSQL\DATA\JD_FC_VD_RecipesProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JD_FC_VD_RecipesProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET RECOVERY FULL 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET  MULTI_USER 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'JD_FC_VD_RecipesProject', N'ON'
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET QUERY_STORE = ON
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [JD_FC_VD_RecipesProject]
GO
/****** Object:  Table [dbo].[category]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
	[recipe_id] [int] NULL,
 CONSTRAINT [PK__category__3213E83FD958C52C] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comment]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[body] [text] NULL,
	[recipe_id] [int] NULL,
	[user_id] [int] NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK__comment__3213E83FD0AE663B] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[difficulty]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[difficulty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__difficul__3213E83F128E3EA5] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ingredient]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ingredient](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__ingredie__3213E83F430E633E] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rating]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rating](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[value] [int] NOT NULL,
	[recipe_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK__rating__3213E83FF17D262B] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipe]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[instructions] [text] NOT NULL,
	[image_source] [nvarchar](255) NULL,
	[minutes_to_cook] [int] NOT NULL,
	[is_approved] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[difficulty_id] [int] NOT NULL,
	[created_at] [date] NOT NULL,
 CONSTRAINT [PK__recipe__3213E83F73063233] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipe_categories]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe_categories](
	[category_id] [int] NULL,
	[recipe_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[recipe_ingredients]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe_ingredients](
	[recipe_id] [int] NOT NULL,
	[ingredient_id] [int] NOT NULL,
	[amount] [int] NOT NULL,
	[unit_id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unit]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unit](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NULL,
 CONSTRAINT [PK__unit__3213E83F3DE1EB0A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
	[first_name] [nvarchar](255) NOT NULL,
	[last_name] [nvarchar](255) NOT NULL,
	[content_bio] [text] NULL,
	[image_source] [nvarchar](255) NULL,
	[is_admin] [int] NOT NULL,
	[is_blocked] [int] NOT NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK__user__3213E83F8A9F62EE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__user__AB6E6164A05168CF] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__user__F3DBC572C6765D69] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_favorite_recipes]    Script Date: 28/02/2024 01:56:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_favorite_recipes](
	[recipe_id] [int] NULL,
	[user_id] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_recipe] FOREIGN KEY([recipe_id])
REFERENCES [dbo].[recipe] ([id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_recipe]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_user]
GO
ALTER TABLE [dbo].[rating]  WITH CHECK ADD  CONSTRAINT [FK_rating_recipe] FOREIGN KEY([recipe_id])
REFERENCES [dbo].[recipe] ([id])
GO
ALTER TABLE [dbo].[rating] CHECK CONSTRAINT [FK_rating_recipe]
GO
ALTER TABLE [dbo].[rating]  WITH CHECK ADD  CONSTRAINT [FK_rating_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[rating] CHECK CONSTRAINT [FK_rating_user]
GO
ALTER TABLE [dbo].[recipe]  WITH CHECK ADD  CONSTRAINT [FK__recipe__difficul__4CA06362] FOREIGN KEY([difficulty_id])
REFERENCES [dbo].[difficulty] ([id])
GO
ALTER TABLE [dbo].[recipe] CHECK CONSTRAINT [FK__recipe__difficul__4CA06362]
GO
ALTER TABLE [dbo].[recipe]  WITH CHECK ADD  CONSTRAINT [FK__recipe__user_id__5165187F] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[recipe] CHECK CONSTRAINT [FK__recipe__user_id__5165187F]
GO
ALTER TABLE [dbo].[recipe_categories]  WITH CHECK ADD  CONSTRAINT [FK_recipe_categories_category] FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[recipe_categories] CHECK CONSTRAINT [FK_recipe_categories_category]
GO
ALTER TABLE [dbo].[recipe_categories]  WITH CHECK ADD  CONSTRAINT [FK_recipe_categories_recipe] FOREIGN KEY([recipe_id])
REFERENCES [dbo].[recipe] ([id])
GO
ALTER TABLE [dbo].[recipe_categories] CHECK CONSTRAINT [FK_recipe_categories_recipe]
GO
ALTER TABLE [dbo].[recipe_ingredients]  WITH CHECK ADD  CONSTRAINT [FK_recipe_ingredients_ingredients] FOREIGN KEY([ingredient_id])
REFERENCES [dbo].[ingredient] ([id])
GO
ALTER TABLE [dbo].[recipe_ingredients] CHECK CONSTRAINT [FK_recipe_ingredients_ingredients]
GO
ALTER TABLE [dbo].[recipe_ingredients]  WITH CHECK ADD  CONSTRAINT [FK_recipe_ingredients_recipe] FOREIGN KEY([recipe_id])
REFERENCES [dbo].[recipe] ([id])
GO
ALTER TABLE [dbo].[recipe_ingredients] CHECK CONSTRAINT [FK_recipe_ingredients_recipe]
GO
ALTER TABLE [dbo].[recipe_ingredients]  WITH CHECK ADD  CONSTRAINT [FK_recipe_ingredients_unit] FOREIGN KEY([unit_id])
REFERENCES [dbo].[unit] ([id])
GO
ALTER TABLE [dbo].[recipe_ingredients] CHECK CONSTRAINT [FK_recipe_ingredients_unit]
GO
ALTER TABLE [dbo].[user_favorite_recipes]  WITH CHECK ADD  CONSTRAINT [FK_user_favorite_recipes_recipe] FOREIGN KEY([recipe_id])
REFERENCES [dbo].[recipe] ([id])
GO
ALTER TABLE [dbo].[user_favorite_recipes] CHECK CONSTRAINT [FK_user_favorite_recipes_recipe]
GO
ALTER TABLE [dbo].[user_favorite_recipes]  WITH CHECK ADD  CONSTRAINT [FK_user_favorite_recipes_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[user_favorite_recipes] CHECK CONSTRAINT [FK_user_favorite_recipes_user]
GO
EXEC sys.sp_addextendedproperty @name=N'Column_Description', @value=N'Content of the comment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'comment', @level2type=N'COLUMN',@level2name=N'body'
GO
EXEC sys.sp_addextendedproperty @name=N'Column_Description', @value=N'Instruction List' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'recipe', @level2type=N'COLUMN',@level2name=N'instructions'
GO
USE [master]
GO
ALTER DATABASE [JD_FC_VD_RecipesProject] SET  READ_WRITE 
GO
