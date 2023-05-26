#Chief's Corner

Chief's Corner is a web application that allows users to order food from a variety of real food categories and menus. The application provides features for user registration, login, and logout using Identity. Users can register with their email and receive a confirmation link to verify their account. Once verified, users can log in to the system.

#Features

User Registration: Users can sign up for an account by providing their email and password. A confirmation link is sent to the email address for account verification.
User Login: Registered users can log in to the system using their email and password.
Email Confirmation: Users receive a confirmation email with a link to verify their email address. Only verified users can log in to the system.
User Logout: Logged-in users can log out of the system.
Food Categories and Menus: The application offers a wide range of real food categories and menus for users to choose from.
Menu Customization: Users can select their desired menu, portion size, and quantity to add to their order.
Cart Management: Users can view and update the items in their cart.
Payment: Once the user completes their order, they are redirected to a credit card payment page for payment processing.
Order Completion: After successful payment, the user's cart is completed, and they can create a new order by adding items to the cart again.
Admin Panel: Users with an admin role have access to the admin panel, which allows them to perform control operations.
Admin Control: Admin users can add, update, and delete menus and categories. They can also view and update the status of orders.
Order Filtering: Admin users can filter orders based on user name or email.
Pagination: The application implements pagination to display a desired number of orders per page.

#Installation

To run the Chief's Corner application, follow these steps:

Clone the repository: git clone [https://github.com/your-username/chiefs-corner.git](https://github.com/kaganunal/MVC-ChiefsCorner)
Set up the necessary database and configure the connection string in the appsettings.json file.
Build the solution to restore NuGet packages and compile the project.
Run the database migrations to create the required tables.
Start the application from Visual Studio or deploy it to a hosting environment.


#Usage

Open the Chief's Corner application in your web browser.
If you are a new user, click on the "Sign Up" link to create a new account. Provide your email and password, and follow the instructions in the confirmation email to verify your account.
Once logged in, browse through the available food categories and menus.
Select your desired menu, portion size, and quantity, and add it to your cart.
Manage your cart by updating or removing items as needed.
When ready, proceed to the payment page to complete your order using a credit card.
After successful payment, your order will be completed, and you can create a new order by adding items to the cart again.

If you have an admin role:

Log in to the application using an admin account.
In the navigation bar, the admin panel button will be visible.
Click on the admin panel button to access the admin control features.
From the admin panel, you can add, update, and delete menus and categories.
You can also view and update the status of orders.
Use the order filtering options to search for orders by user name or email.
Pagination is implemented to display a desired number of orders per page.

#Contributing

Contributions to the Chief's Corner project are welcome. If you encounter any issues or have suggestions for improvements, please submit a pull request or create an issue in the repository.
