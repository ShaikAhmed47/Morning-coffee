USE MorningCoffeeDb;
GO
INSERT INTO Categories(Name,Description) VALUES
('Espresso','Classic espresso based drinks'),
('Cold Coffee','Chilled and refreshing coffee'),
('Tea','Hot tea and infusions'),
('Pastries','Fresh morning bakes'),
('Snacks','Quick bites');
GO
INSERT INTO Products(Name,Description,Price,CategoryId,ImageUrl) VALUES
('Espresso','Rich single shot espresso',90,1,NULL),
('Cappuccino','Espresso with steamed milk and foam',150,1,NULL),
('Cafe Latte','Smooth espresso with creamy milk',160,1,NULL),
('Mocha','Chocolate, espresso and steamed milk',180,1,NULL),
('Cold Coffee','Classic chilled blended coffee',170,2,NULL),
('Iced Latte','Cold espresso with milk and ice',180,2,NULL),
('Masala Tea','Indian spiced tea',80,3,NULL),
('Green Tea','Light and refreshing green tea',90,3,NULL),
('Butter Croissant','Flaky buttery croissant',120,4,NULL),
('Chocolate Muffin','Soft muffin with chocolate chips',110,4,NULL),
('Veg Sandwich','Fresh vegetable grilled sandwich',150,5,NULL),
('French Fries','Crispy seasoned fries',130,5,NULL);
GO
-- SHA256 hashes are generated for demo accounts.
INSERT INTO Users(FullName,Email,PasswordHash,Role) VALUES
('MorningCoffee Admin','admin@morningcoffee.com','e86f78a8a3caf0b60d8e74e5942aa6d86dc150cd3c03338aef25b7d2d7e3acc7','Admin');
-- Replace the admin hash above with the value printed by the app if needed.
-- Customer demo password hash for Customer@123:
INSERT INTO Users(FullName,Email,PasswordHash,Role) VALUES
('Demo Customer','customer@morningcoffee.com','98ec654a8df28f8f0f8f02220483d46916b85017de0b74d8ec755c28cb8539a8','Customer');
INSERT INTO Customers(FullName,Email,Phone,Address) VALUES
('Demo Customer','customer@morningcoffee.com','9000000000','Hyderabad, Telangana');
GO
