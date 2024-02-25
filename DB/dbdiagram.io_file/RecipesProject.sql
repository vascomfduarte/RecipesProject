CREATE TABLE [recipe] (
  [id] integer PRIMARY KEY,
  [title] nvarchar(255),
  [instructions] text,
  [image_source] nvarchar(255),
  [minutes_to_cook] integer,
  [is_approved] integer,
  [user_id] integer,
  [difficulty_id] integer,
  [created_at] timestamp
)
GO

CREATE TABLE [recipe_categories] (
  [category_id] integer,
  [recipe_id] integer
)
GO

CREATE TABLE [category] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255),
  [recipe_id] integer
)
GO

CREATE TABLE [comment] (
  [id] integer PRIMARY KEY,
  [body] text,
  [recipe_id] integer,
  [user_id] integer,
  [created_at] timestamp
)
GO

CREATE TABLE [difficulty] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255)
)
GO

CREATE TABLE [rating] (
  [id] integer PRIMARY KEY,
  [value] integer,
  [recipe_id] integer
)
GO

CREATE TABLE [recipe_ingredients] (
  [recipe_id] integer,
  [ingredients_id] integer
)
GO

CREATE TABLE [ingredients] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255),
  [amount] integer,
  [unit_id] integer
)
GO

CREATE TABLE [unit] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255)
)
GO

CREATE TABLE [user] (
  [id] integer PRIMARY KEY,
  [username] nvarchar(255) UNIQUE,
  [password] nvarchar(255),
  [email] nvarchar(255) UNIQUE,
  [first_name] nvarchar(255),
  [last_name] nvarchar(255),
  [content_bio] text,
  [image_source] nvarchar(255),
  [is_admin] int,
  [is_blocked] int,
  [created_at] timestamp
)
GO

CREATE TABLE [user_favorite_recipes] (
  [recipe_id] integer,
  [user_id] integer
)
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Instruction List',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'recipe',
@level2type = N'Column', @level2name = 'instructions';
GO

EXEC sp_addextendedproperty
@name = N'Column_Description',
@value = 'Content of the comment',
@level0type = N'Schema', @level0name = 'dbo',
@level1type = N'Table',  @level1name = 'comment',
@level2type = N'Column', @level2name = 'body';
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([id]) REFERENCES [recipe_ingredients] ([recipe_id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([id]) REFERENCES [rating] ([recipe_id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([id]) REFERENCES [comment] ([recipe_id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([difficulty_id]) REFERENCES [difficulty] ([id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([id]) REFERENCES [recipe_categories] ([recipe_id])
GO

ALTER TABLE [category] ADD FOREIGN KEY ([id]) REFERENCES [recipe_categories] ([category_id])
GO

ALTER TABLE [ingredients] ADD FOREIGN KEY ([id]) REFERENCES [recipe_ingredients] ([ingredients_id])
GO

ALTER TABLE [unit] ADD FOREIGN KEY ([id]) REFERENCES [ingredients] ([unit_id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([user_id]) REFERENCES [user] ([id])
GO

ALTER TABLE [user] ADD FOREIGN KEY ([id]) REFERENCES [comment] ([user_id])
GO

ALTER TABLE [user] ADD FOREIGN KEY ([id]) REFERENCES [user_favorite_recipes] ([user_id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([id]) REFERENCES [user_favorite_recipes] ([recipe_id])
GO
