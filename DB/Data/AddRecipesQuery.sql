USE [JD_FC_VD_RecipesProject]
GO

INSERT INTO [dbo].[recipe]
           ([title]
           ,[description]
           ,[image_source]
           ,[minutes_to_cook]
           ,[is_approved]
           ,[created_date]
           ,[user_id]
           ,[difficulty_id])
     VALUES
           ('Spaghetti Carbonara', 'Classic Italian pasta dish with creamy egg and pancetta sauce.', 'https://n9.cl/vykrug', 20, 1, GETDATE(), 1, 2),
		   ('Chicken Alfredo Pasta',
        'Creamy pasta dish with grilled chicken, garlic, and Parmesan cheese.',
        'https://n9.cl/lu2a9m',
        30,
        1,
        GETDATE(),
        1, -- User ID is set to 1
        3),
		('Vegetable Stir-Fry',
        'Healthy and colorful stir-fried vegetables with tofu, served with rice.',
        'https://n9.cl/ngv2b',
        25,
        1,
        GETDATE(),
        1, -- User ID is set to 1
        2),
		('Classic Chocolate Cake',
        'Decadent chocolate cake with rich chocolate frosting, perfect for celebrations.',
        'https://n9.cl/ja5wz',
        45,
        1,
        GETDATE(),
        1, -- User ID is set to 1
        3),
		('Caprese Salad',
        'Simple and fresh salad made with ripe tomatoes, mozzarella cheese, basil, and balsamic vinegar.',
        'https://n9.cl/4jqa4',
        10,
        1,
        GETDATE(),
        1, -- User ID is set to 1
        1),
		('Grilled Salmon with Lemon-Dill Sauce',
        'Delicious and healthy grilled salmon topped with a tangy lemon-dill sauce.',
        'https://n9.cl/bumyo',
        15,
        1,
        GETDATE(),
        1, -- User ID is set to 1
        2),
		('Homemade Pizza Margherita',
        'Classic Italian pizza topped with fresh tomatoes, mozzarella cheese, and basil leaves.',
        'https://n9.cl/dux41',
        30,
        1,
        GETDATE(),
        1, -- User ID is set to 1
        2);
GO


