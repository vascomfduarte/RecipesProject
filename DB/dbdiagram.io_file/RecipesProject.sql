CREATE TABLE [recipe] (
  [id] integer PRIMARY KEY,
  [title] nvarchar(255),
  [instructions] text,
  [image_source] nvarchar(255),
  [minutes_to_cook] integer,
  [is_aproved] integer,
  [user_id] integer,
  [created_at] timestamp
)
GO

CREATE TABLE [recipe_comments] (
  [recipe_id] integer,
  [comment_id] integer
)
GO

CREATE TABLE [comment] (
  [id] integer,
  [body] text,
  [user_id] integer,
  [created_at] timestamp
)
GO

CREATE TABLE [recipe_ratings] (
  [recipe_id] integer,
  [rating_id] integer
)
GO

CREATE TABLE [rating] (
  [id] integer,
  [value] integer
)
GO

CREATE TABLE [recipe_ingredients] (
  [recipe_id] integer,
  [ingredients_id] integer
)
GO

CREATE TABLE [ingredients] (
  [id] integer PRIMARY KEY,
  [name] string,
  [amount] integer,
  [unit_id] integer
)
GO

CREATE TABLE [unit] (
  [id] integer PRIMARY KEY,
  [name] nvarchar(255)
)
GO

CREATE TABLE [users] (
  [id] integer PRIMARY KEY,
  [username] nvarchar(255),
  [password] nvarchar(255),
  [email] nvarchar(255),
  [first_name] nvarchar(255),
  [last_name] nvarchar(255),
  [content_bio] text,
  [image_source] nvarchar(255),
  [is_admin] int,
  [is_blocked] int,
  [created_at] timestamp
)
GO

CREATE TABLE [user_comments] (
  [user_id] integer,
  [comment_id] integer
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

ALTER TABLE [recipe] ADD FOREIGN KEY ([id]) REFERENCES [recipe_ratings] ([recipe_id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([id]) REFERENCES [recipe_comments] ([recipe_id])
GO

ALTER TABLE [recipe_comments] ADD FOREIGN KEY ([comment_id]) REFERENCES [comment] ([id])
GO

ALTER TABLE [recipe_ratings] ADD FOREIGN KEY ([rating_id]) REFERENCES [rating] ([id])
GO

ALTER TABLE [ingredients] ADD FOREIGN KEY ([id]) REFERENCES [recipe_ingredients] ([ingredients_id])
GO

ALTER TABLE [unit] ADD FOREIGN KEY ([id]) REFERENCES [ingredients] ([unit_id])
GO

ALTER TABLE [recipe] ADD FOREIGN KEY ([user_id]) REFERENCES [users] ([id])
GO

ALTER TABLE [user_comments] ADD FOREIGN KEY ([user_id]) REFERENCES [users] ([id])
GO

ALTER TABLE [user_comments] ADD FOREIGN KEY ([user_id]) REFERENCES [comment] ([id])
GO
