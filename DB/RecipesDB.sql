USE [JD_FC_VD_RecipesWebAppDB]
GO
/****** Object:  Table [dbo].[recipe]    Script Date: 23/02/2024 01:38:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[recipe](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](50) NOT NULL,
	[instructions] [varchar](500) NOT NULL,
	[image_source] [varchar](500) NOT NULL,
	[minutes_to_cook] [int] NOT NULL,
	[is_aproved] [int] NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_Recipe] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utilizador]    Script Date: 23/02/2024 01:38:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utilizador](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[first_name] [varchar](50) NOT NULL,
	[last_name] [varchar](50) NOT NULL,
	[content_bio] [varchar](500) NOT NULL,
	[img_src] [varchar](250) NOT NULL,
	[is_admin] [int] NOT NULL,
	[is_blocked] [int] NOT NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[recipe]  WITH CHECK ADD  CONSTRAINT [FK_recipe_User] FOREIGN KEY([user_id])
REFERENCES [dbo].[utilizador] ([id])
GO
ALTER TABLE [dbo].[recipe] CHECK CONSTRAINT [FK_recipe_User]
GO
