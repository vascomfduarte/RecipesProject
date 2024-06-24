# Recipe Web App
A web application built with the ASP.NET framework, featuring functionalities for recipe search, user authentication, recipe creation and editing, and more. This project interacts with a MS SQL database using ADO.NET.


## Technologies Used
- Framework: ASP.NET
- Programming Language: C#
- Styling: Bootstrap
- Database Interaction: ADO.NET
- Database: MS SQL
- Frontend: Razor Pages


## Features
### User Features

1. Login:
> Allows users to log in using their email and password, validating the credentials through service and repository layers and comparing them with the database stored data.
> If correct, the user is redirected to the homepage. When registering the user is redirected to their account page.

2. Recipe Search:
> Users can search for recipes based on keywords.

3. Recipe Creation and Editing:
> Users can create new recipes or edit existing ones, adding ingredients and cooking instructions.

4. Comment and Rating:
> Users can comment on and rate recipes.

5. Recipe Favoriting:
> Users can favorite recipes for quick access later.

---

### Admin Features

1. User Management:
> Admins can manage users, including blocking, unblocking, and deleting user accounts.

2. Recipe Management:
> Admins can review, approve, or delete recipes. All recipe must be admin approved after creation or editing.

3. Comment Management:
> Admins can manage all comments in the recipes.

4. Ingredient Management:
> Admins can manage the products and units database.


## Installation

1. Clone the repository https://github.com/vascomfduarte/RecipesProject.git. I recomend to clone it directly from Visual Studio.
   
2. Install MS SQL Server.

3. Run RecipesProject/DB/RecipesProject.sql query for database installation.

4. Run the code.

5. Open your browser and go to https://localhost:7219.

6. After that, explore the various features like searching for recipes, creating new ones, and more.


## Contact
If you have any questions or suggestions, please contact me at vasco.mfd@gmail.com.


## Links
LinkedIn: https://www.linkedin.com/in/vascoduarte/
