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
     ('Spaghetti Carbonara', 'Classic Italian pasta dish with creamy egg and pancetta sauce.', 'https://t.ly/jjM54', 20, 1, '2024-04-08', 104, 2),
     ('Chicken Alfredo Pasta', 'Creamy pasta dish with grilled chicken, garlic, and Parmesan cheese.', 'https://t.ly/dzljX', 30, 1, '2024-04-08', 104, 3),
     ('Vegetable Stir-Fry', 'Healthy and colorful stir-fried vegetables with tofu, served with rice.', 'https://t.ly/jJV6B', 25, 1, '2024-04-08', 104, 2),
     ('Classic Chocolate Cake', 'Decadent chocolate cake with rich chocolate frosting, perfect for celebrations.', 'https://t.ly/z_vCx', 45, 1, '2024-04-08', 104, 3),
     ('Caprese Salad', 'Simple and fresh salad made with ripe tomatoes, mozzarella cheese, basil, and balsamic vinegar.', 'https://rb.gy/jw4w1v', 10, 1, '2024-04-08', 104, 1),
     ('Grilled Salmon with Lemon-Dill Sauce', 'Delicious and healthy grilled salmon topped with a tangy lemon-dill sauce.', 'https://rb.gy/o4e8wo', 15, 1, '2024-04-08', 104, 2),
     ('Homemade Pizza Margherita', 'Classic Italian pizza topped with fresh tomatoes, mozzarella cheese, and basil leaves.', 'https://rb.gy/aja67u', 30, 1, '2024-04-08', 104, 2);
GO
