
# Burger Shop

## Overview
The BurgerShop application manages users, orders, and a burger menu, featuring user registration, JWT-based authentication, menu management, order placement, and an admin interface for burger item management.

## User Registration

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/Register_kx2xux.png)

## User Login

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/Login_oc5e7o.png)

## Admin Registeration

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/Admin_Register_mktvwd.png)

## User Burger Page

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/User_Burger_Page_t7osaz.png)

## User Cart

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/User_Cart_atd5f0.png)

## User Order History

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/User_Order_History_klbx83.png)

## Admin Burger Page-1

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/Admin_Burger_Page_pxrahb.png)

## Admin Burger Page-2

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/Admin_Burger_Page2_tlxrta.png)

## Add new Burgers 

![Register Page](https://res.cloudinary.com/dhkwphjgs/image/upload/v1723862452/Burger%20Shop%20images/Admin_Create_Burger_gxob6w.png)



# SRS Documentation

Software Requirements for BurgerShop Application
(Single Device Application)

## Functional Requirements
### User Management:
1. Users can register by providing a unique mobile number and a secure password.
2. Registered users can log in to their accounts.
3. Admin users can assign roles (e.g., User, Admin) to other users.
4. One Admin user is assign by default whose mobile No is 9999999999 so new Admin is set by providing the default admin user mobile No with new mobile No who you want to make as Admin
### Burger Menu Management:
1. Admin users can create new burger items with details like name, description, base price.(Veg)
2. Burgers are of  3 types Veg,Non-Veg,Egg hence, Set base price would increase by 50rs for Egg and 80rs for Non-Veg respectively 
3. Admin users can update existing menu items.
4. Admin users can delete burger items from the menu.
### Order Management::
1. Users can place orders from the available menu items.
2. Users can view their order history, including details like burger type, category, quantity, price, and total price. 
3. User Authorization is also implement so that JWT Token is set for 5 min within which the Burger Shop Routes are valid else they become authorized.
4. Cart Items are stored on the local Storage which implies that this application is for single device user where in user is logged in from only one device
### Authentication and Authorization:
1. User authentication is secured using JWT tokens.
2. Admin-specific functionalities are restricted based on user roles.
3. Route inside the Burger Shop Application are protected ex ViewOrders,PlaceOrder etc

### System Feature 1: User Management
#### Description: Enable users to manage their accounts efficiently.
#### Use Cases:
1. Register User
2. Login User
3. Role Assignment
4. Password Management
#### Functional Requirements:
1. Users must provide a unique mobile number and a secure password during registration.
2. Registered users should be able to log in using their credentials.
3. Admin users can assign roles to other users.
### System Feature 2: Burger Menu Management
#### Description: Allow admin users to manage the menu items effectively.
#### Use Cases:
1. Create New Burger Item
2. Update Burger Item
3. Delete Burger Item
#### Functional Requirements:
1. Admin users can create new burger items with details like name, description and base price.
2. Admin users can update existing menu items with new information.
3. Admin users can delete burger items from the menu.

### System Feature 3: Order Management
#### Description: Provide users with a seamless order placement process.
#### Use Cases:
1. Place Order
2. View Order History
3. Admin Order View
#### Functional Requirements:
1. Users should be able to place orders from the available menu items.
2. Users can view their order history, including details like burger type, category, quantity, price, and total price.

### System Feature 4: Authentication and Authorization
#### Description: Ensure secure authentication and role-based access control.
#### Functional Requirements:
1. User authentication is implemented using JWT tokens for security.
2. Admin-specific functionalities are restricted based on user roles.

###  Future Scope for BurgerShop Application

####  1. Multi-Device Support :- Extend the application to allow users to log in from multiple devices.

####  2. Advanced Order Management Features :- Introduce features such as order tracking, estimated delivery times, and notifications for order status updates.

####  3. Integration with Payment Gateways :- Enable secure online payment options through popular payment gateways (e.g., PayPal, Stripe).

####  4. User Feedback and Ratings System :- Introduce a feedback mechanism allowing users to rate burgers and provide comments.








