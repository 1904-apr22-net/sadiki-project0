--Order -- Core table, holds all data that the Order Class contains. 
Create Schema BBear
Go



Create Table BBear.Location (
LocationID Int Not Null Identity,
Constraint PK_Location Primary Key(LocationID)
)

Create Table BBear.Customer (
CustomerID Int Not Null Identity,
FirstName NVarchar(30) Not Null,
LastName NVarchar(30) Not Null,
DefLocationID Int,
LastOrder DateTime2(7),
Constraint PK_Customer Primary Key(CustomerID),
Constraint FK_Customer Foreign Key(DefLocationID)
	References BBear.Location (LocationID)
	On Delete Set Null
)

Create Table BBear.Orders (
OrderID Int Not Null Identity,
CustomerID Int Not Null,
LocationID Int Not Null,
PriceTag Decimal(7,2) Not Null,
CreatedAt DateTime2(7) Not Null,
Constraint PK_Order Primary Key(OrderID),
Constraint FK_Order1 Foreign Key(LocationID)
	References BBear.Location (LocationID)
	On Delete Cascade,
Constraint FK_Order2 Foreign Key(CustomerID)
	References BBear.Customer (CustomerID)
	On Delete Cascade,
)

Create Table BBear.SoldBears (
BearID Int Not Null Identity,
OrderID Int Not Null,
Constraint FK_Bears Foreign Key(OrderID)
	References BBear.Orders (OrderID)
	On Delete Cascade,
Constraint PK_Bear Primary Key(BearID)
)


Create Table BBear.Product (
ProductID Int Not Null Identity,
ProductName NVarchar(30) Not Null,
DefPrice Decimal(5,2),
Constraint PK_Product Primary Key(ProductID)
)

Create Table BBear.SoldTraining (
TrainingID Int Not Null Identity,
BearID Int Not Null,
ProductID Int Not Null,
Constraint FK_Training Foreign Key(BearID)
	References BBear.SoldBears (BearID)
	On Delete Cascade,
Constraint PK_Training Primary Key(TrainingID),
Constraint FK_Training2 Foreign Key (ProductID)
	References BBear.Product (ProductID)
)







Create Table BBear.Inventory (
InventoryID Int Not Null Identity,
LocationID Int Not Null,
ProductID Int Not Null,
Quantity Int Not Null Default 0,
Constraint FK_Inventory1 Foreign Key(LocationId)
	References BBear.Location (LocationId)
	On Delete Cascade,
Constraint FK_Inventory2 Foreign Key(ProductID)
	References BBear.Product (ProductID)
	On Delete Cascade,
Constraint PK_Inventory Primary Key(InventoryID)
);
Go

insert into BBear.Product(ProductName, DefPrice ) Values ('Divinity',999.99)
insert into BBear.Product(ProductName, DefPrice ) Values ('Tax Evasion',45.99)
insert into BBear.Product(ProductName, DefPrice ) Values ('Juggling',19.99)
insert into BBear.Product(ProductName, DefPrice ) Values ('Fighting',24.99)
insert into BBear.Product(ProductName, DefPrice ) Values ('Marriage Counselling',69.99)
insert into BBear.Product(ProductName, DefPrice) Values ('Bear', 199.99)
insert into BBear.Product(ProductName, DefPrice) Values ('C#/.Net', 2.99)

Insert into BBear.Location Default values
Insert into BBear.Location Default values
Insert into BBear.Location Default values
Insert into BBear.Location Default values

Insert into BBear.Customer (FirstName, LastName) Values ('Bear', 'Grylls'), ('Walter', 'Sobchak') 

Insert into BBear.Customer (FirstName, LastName) Values ('Horace', 'Greensbury'), ('Nick', 'Escalona'), ('Mimi', 'Parfait') , ('Red', 'Calhoun') 



-- Customer -- Each order has one customer, but a customer can have many orders

-- Location -- Each order has one location, but a location can have many orders. 

-- Bear (Sold) -- Each bear has one order, but an order can have many bears

-- Training(?) Each training has one bear, but a bear can have many trainings

-- Inventory -- Each inventory item has one location, but a location can have many inventory items
