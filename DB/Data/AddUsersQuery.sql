INSERT INTO [dbo].[user]
           ([username]
           ,[password]
           ,[email]
           ,[first_name]
           ,[last_name]
           ,[content_bio]
           ,[image_source]
           ,[is_admin]
           ,[is_blocked]
           ,[created_date])
     VALUES 
     ('vascomd', 'vasco12345', 'vasco@gmail.com', 'Vasco', 'Duarte', 'Lorem Ipsum', 'https://i.imgur.com/Wbmo4uN.jpeg', 1, 0, '2024-04-01 00:00:00.000');