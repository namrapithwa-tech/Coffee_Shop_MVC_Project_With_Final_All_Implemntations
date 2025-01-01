-- Create the User table
CREATE TABLE [dbo].[User] (
    [UserID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [UserName] VARCHAR(100) NOT NULL,
    [Email] VARCHAR(100) NOT NULL,
    [Password] VARCHAR(100) NOT NULL,
    [MobileNo] VARCHAR(15) NOT NULL,
    [Address] VARCHAR(100) NOT NULL,
    [IsActive] BIT NOT NULL
);

-- Create the Product table
CREATE TABLE [dbo].[Product] (
    [ProductID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [ProductName] VARCHAR(100) NOT NULL,
    [ProductPrice] DECIMAL(10,2) NOT NULL,
    [ProductCode] VARCHAR(100) NOT NULL,
    [Description] VARCHAR(100) NOT NULL,
    [UserID] INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);

-- Create the Customer table
CREATE TABLE [dbo].[Customer] (
    [CustomerID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [CustomerName] VARCHAR(100) NOT NULL,
    [HomeAddress] VARCHAR(100) NOT NULL,
    [Email] VARCHAR(100) NOT NULL,
    [MobileNo] VARCHAR(15) NOT NULL,
    [GSTNO] VARCHAR(15) NOT NULL,
    [CityName] VARCHAR(100) NOT NULL,
    [PinCode] VARCHAR(15) NOT NULL,
    [NetAmount] DECIMAL(10,2) NOT NULL,
    [UserID] INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);

-- Create the Order table
CREATE TABLE [dbo].[Order] (
    [OrderID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[OrderNumber] VARCHAR(8) NULL,
    [OrderDate] DATETIME NOT NULL,
    [CustomerID] INT NOT NULL FOREIGN KEY REFERENCES [Customer](CustomerID),
    [PaymentMode] VARCHAR(100) NULL,
    [TotalAmount] DECIMAL(10,2) NULL,
    [ShippingAddress] VARCHAR(100) NOT NULL,
    [UserID] INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);

-- Create the OrderDetail table
CREATE TABLE [dbo].[OrderDetail] (
    [OrderDetailID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [OrderID] INT NOT NULL FOREIGN KEY REFERENCES [Order](OrderID),
    [ProductID] INT NOT NULL FOREIGN KEY REFERENCES [Product](ProductID),
    [Quantity] INT NOT NULL,
    [Amount] DECIMAL(10,2) NOT NULL,
    [TotalAmount] DECIMAL(10,2) NOT NULL,
    [UserID] INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);

-- Create the Bills table
CREATE TABLE [dbo].[Bills] (
    [BillID] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [BillNumber] VARCHAR(100) NOT NULL,
    [BillDate] DATETIME NOT NULL,
    [OrderID] INT NOT NULL FOREIGN KEY REFERENCES [Order](OrderID),
    [TotalAmount] DECIMAL(10,2) NOT NULL,
    [Discount] DECIMAL(10,2) NULL,
    [NetAmount] DECIMAL(10,2) NOT NULL,
    [UserID] INT NOT NULL FOREIGN KEY REFERENCES [User](UserID)
);


-- Insert records into User
INSERT INTO [dbo].[User] ([dbo].[User].[UserName], [dbo].[User].[Email], [dbo].[User].[Password], [dbo].[User].[MobileNo], [dbo].[User].[Address], [dbo].[User].[IsActive])
VALUES 
('John Doe', 'john@example.com', 'password1', '1234567890', '123 Main St', 1),
('Jane Smith', 'jane@example.com', 'password2', '0987654321', '456 Elm St', 1),
('Alice Johnson', 'alice@example.com', 'password3', '1122334455', '789 Oak St', 1),
('Bob Brown', 'bob@example.com', 'password4', '2233445566', '101 Pine St', 1),
('Charlie Davis', 'charlie@example.com', 'password5', '3344556677', '202 Cedar St', 1);

-- Insert records into Product
INSERT INTO [dbo].[Product] ([dbo].[Product].[ProductName], [dbo].[Product].[ProductPrice], [dbo].[Product].[ProductCode], [dbo].[Product].[Description], [dbo].[Product].[UserID])
VALUES 
('Espresso', 2.50, 'P001', 'Rich and bold coffee', 1),
('Latte', 3.50, 'P002', 'Smooth coffee with milk', 2),
('Cappuccino', 3.00, 'P003', 'Coffee with foam', 3),
('Mocha', 4.00, 'P004', 'Coffee with chocolate', 4),
('Americano', 2.00, 'P005', 'Espresso with water', 5);

-- Insert records into Customer
INSERT INTO [dbo].[Customer] ([dbo].[Customer].[CustomerName], [dbo].[Customer].[HomeAddress], [dbo].[Customer].[Email], [dbo].[Customer].[MobileNo], [dbo].[Customer].[GSTNO], [dbo].[Customer].[CityName], [dbo].[Customer].[PinCode], [dbo].[Customer].[NetAmount], [dbo].[Customer].[UserID])
VALUES 
('Customer One', '123 Main St', 'customer1@example.com', '1234567890', 'GST001', 'CityA', '100001', 9.50, 1),
('Customer Two', '456 Elm St', 'customer2@example.com', '0987654321', 'GST002', 'CityB', '100002', 14.00, 2),
('Customer Three', '789 Oak St', 'customer3@example.com', '1122334455', 'GST003', 'CityC', '100003', 18.50, 3),
('Customer Four', '101 Pine St', 'customer4@example.com', '2233445566', 'GST004', 'CityD', '100004', 23.00, 4),
('Customer Five', '202 Cedar St', 'customer5@example.com', '3344556677', 'GST005', 'CityE', '100005', 27.50, 5);

-- Insert records into Order
INSERT INTO [dbo].[Order] ([dbo].[Order].[OrderNumber],[dbo].[Order].[OrderDate], [dbo].[Order].[CustomerID], [dbo].[Order].[PaymentMode], [dbo].[Order].[TotalAmount], [dbo].[Order].[ShippingAddress], [dbo].[Order].[UserID])
VALUES 
('ORD78355','2024-01-01', 1, 'Credit Card', 10.00, '123 Main St', 1),
('ORD78478','2024-01-02', 2, 'Cash', 15.00, '456 Elm St', 2),
('ORD25755','2024-01-03', 3, 'Debit Card', 20.00, '789 Oak St', 3),
('ORD52785','2024-01-04', 4, 'Credit Card', 25.00, '101 Pine St', 4),
('ORD29945','2024-01-05', 5, 'Cash', 30.00, '202 Cedar St', 5);

-- Insert records into OrderDetail
INSERT INTO [dbo].[OrderDetail] ([dbo].[OrderDetail].[OrderID], [dbo].[OrderDetail].[ProductID], [dbo].[OrderDetail].[Quantity], [dbo].[OrderDetail].[Amount], [dbo].[OrderDetail].[TotalAmount], [dbo].[OrderDetail].[UserID])
VALUES 
(1, 1, 2, 5.00, 10.00, 1),
(2, 2, 3, 5.00, 15.00, 2),
(3, 3, 4, 5.00, 20.00, 3),
(4, 4, 5, 5.00, 25.00, 4),
(5, 5, 6, 5.00, 30.00, 5);

-- Insert records into Bills
INSERT INTO [dbo].[Bills] ([dbo].[Bills].[BillNumber], [dbo].[Bills].[BillDate], [dbo].[Bills].[OrderID], [dbo].[Bills].[TotalAmount], [dbo].[Bills].[Discount], [dbo].[Bills].[NetAmount], [dbo].[Bills].[UserID])
VALUES 
('B001', '2024-01-01', 1, 10.00, 0.50, 9.50, 1),
('B002', '2024-01-02', 2, 15.00, 1.00, 14.00, 2),
('B003', '2024-01-03', 3, 20.00, 1.50, 18.50, 3),
('B004', '2024-01-04', 4, 25.00, 2.00, 23.00, 4),
('B005', '2024-01-05', 5, 30.00, 2.50, 27.50, 5);




-- Insert into Product
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Product_Insert]
    @ProductName VARCHAR(100),
    @ProductPrice DECIMAL(10,2),
    @ProductCode VARCHAR(100),
    @Description VARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Product]([dbo].[Product].[ProductName], [dbo].[Product].[ProductPrice], [dbo].[Product].[ProductCode], [dbo].[Product].[Description], [dbo].[Product].[UserID])
	VALUES (@ProductName, @ProductPrice, @ProductCode, @Description, @UserID);
END;
GO

-- Insert into User
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_User_Insert]
    @UserName VARCHAR(100),
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @MobileNo VARCHAR(15),
    @Address VARCHAR(100),
    @IsActive BIT
AS
BEGIN
    INSERT INTO [dbo].[User]([dbo].[User].[UserName], [dbo].[User].[Email], [dbo].[User].[Password], [dbo].[User].[MobileNo], [dbo].[User].[Address], [dbo].[User].[IsActive])
	VALUES (@UserName, @Email, @Password, @MobileNo, @Address, @IsActive);
END;
GO

-- Insert into Order
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Order_Insert]
    @OrderDate DATETIME,
	@OrderNumber VARCHAR(8),
    @CustomerID INT,
    @PaymentMode VARCHAR(100),
    @TotalAmount DECIMAL(10,2),
    @ShippingAddress VARCHAR(100),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Order] ([dbo].[Order].[OrderNumber],[dbo].[Order].[OrderDate], [dbo].[Order].[CustomerID], [dbo].[Order].[PaymentMode], [dbo].[Order].[TotalAmount], [dbo].[Order].[ShippingAddress], [dbo].[Order].[UserID])
	VALUES (@OrderNumber,@OrderDate, @CustomerID, @PaymentMode, @TotalAmount, @ShippingAddress, @UserID);
END;
GO

-- Insert into OrderDetail
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_OrderDetail_Insert]
    @OrderID INT,
    @ProductID INT,
    @Quantity INT,
    @Amount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[OrderDetail]([dbo].[OrderDetail].[OrderID], [dbo].[OrderDetail].[ProductID], [dbo].[OrderDetail].[Quantity], [dbo].[OrderDetail].[Amount], [dbo].[OrderDetail].[TotalAmount], [dbo].[OrderDetail].[UserID])
	VALUES (@OrderID, @ProductID, @Quantity, @Amount, @TotalAmount, @UserID);
END;
GO

-- Insert into Bills
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Bills_Insert]
    @BillNumber VARCHAR(100),
    @BillDate DATETIME,
    @OrderID INT,
    @TotalAmount DECIMAL(10,2),
    @Discount DECIMAL(10,2),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Bills]([dbo].[Bills].[BillNumber], [dbo].[Bills].[BillDate], [dbo].[Bills].[OrderID], [dbo].[Bills].[TotalAmount], [dbo].[Bills].[Discount], [dbo].[Bills].[NetAmount], [dbo].[Bills].[UserID])
	VALUES (@BillNumber, @BillDate, @OrderID, @TotalAmount, @Discount, @NetAmount, @UserID);
END;
GO

-- Insert into Customer
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Customer_Insert]
    @CustomerName VARCHAR(100),
    @HomeAddress VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo VARCHAR(15),
    @GSTNO VARCHAR(15),
    @CityName VARCHAR(100),
    @PinCode VARCHAR(15),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    INSERT INTO [dbo].[Customer] ([dbo].[Customer].[CustomerName], [dbo].[Customer].[HomeAddress], [dbo].[Customer].[Email], [dbo].[Customer].[MobileNo], [dbo].[Customer].[GSTNO], [dbo].[Customer].[CityName], [dbo].[Customer].[PinCode], [dbo].[Customer].[NetAmount], [dbo].[Customer].[UserID])
	VALUES (@CustomerName, @HomeAddress, @Email, @MobileNo, @GSTNO, @CityName, @PinCode, @NetAmount, @UserID);
END;
GO




-- Update Product
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Product_UpdateByPk]
    @ProductID INT,
    @ProductName VARCHAR(100),
    @ProductPrice DECIMAL(10,2),
    @ProductCode VARCHAR(100),
    @Description VARCHAR(100),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Product]
    SET [dbo].[Product].[ProductName] = @ProductName, 
        [dbo].[Product].[ProductPrice] = @ProductPrice, 
        [dbo].[Product].[ProductCode] = @ProductCode, 
        [dbo].[Product].[Description] = @Description, 
        [dbo].[Product].[UserID] = @UserID
    WHERE [dbo].[Product].[ProductID] = @ProductID;
END;
GO

-- Update User
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_User_UpdateByPk]
    @UserID INT,
    @UserName VARCHAR(100),
    @Email VARCHAR(100),
    @Password VARCHAR(100),
    @MobileNo VARCHAR(15),
    @Address VARCHAR(100),
    @IsActive BIT
AS
BEGIN
    UPDATE [dbo].[User]
    SET [dbo].[User].[UserName] = @UserName, 
        [dbo].[User].[Email] = @Email, 
        [dbo].[User].[Password] = @Password, 
        [dbo].[User].[MobileNo] = @MobileNo, 
        [dbo].[User].[Address] = @Address, 
        [dbo].[User].[IsActive] = @IsActive
    WHERE [dbo].[User].[UserID] = @UserID;
END;
GO

-- Update Order
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Order_UpdateByPk]
    @OrderID INT,
	@OrderNumber VARCHAR(8),
    @OrderDate DATETIME,
    @CustomerID INT,
    @PaymentMode VARCHAR(100),
    @TotalAmount DECIMAL(10,2),
    @ShippingAddress VARCHAR(100),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Order]
    SET [dbo].[Order].[OrderDate] = @OrderDate,
		[dbo].[Order].[OrderNumber] = @OrderNumber,
        [dbo].[Order].[CustomerID] = @CustomerID, 
        [dbo].[Order].[PaymentMode] = @PaymentMode, 
        [dbo].[Order].[TotalAmount] = @TotalAmount, 
        [dbo].[Order].[ShippingAddress] = @ShippingAddress, 
        [dbo].[Order].[UserID] = @UserID
    WHERE [dbo].[Order].[OrderID] = @OrderID;
END;
GO

-- Update OrderDetail
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_OrderDetail_UpdateByPk]
    @OrderDetailID INT,
    @OrderID INT,
    @ProductID INT,
    @Quantity INT,
    @Amount DECIMAL(10,2),
    @TotalAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[OrderDetail]
    SET [dbo].[OrderDetail].[OrderID] = @OrderID, 
        [dbo].[OrderDetail].[ProductID] = @ProductID, 
        [dbo].[OrderDetail].[Quantity] = @Quantity, 
        [dbo].[OrderDetail].[Amount] = @Amount, 
        [dbo].[OrderDetail].[TotalAmount] = @TotalAmount, 
        [dbo].[OrderDetail].[UserID] = @UserID
    WHERE [dbo].[OrderDetail].[OrderDetailID] = @OrderDetailID;
END;
GO

-- Update Bills
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Bills_UpdateByPk]
    @BillID INT,
    @BillNumber VARCHAR(100),
    @BillDate DATETIME,
    @OrderID INT,
    @TotalAmount DECIMAL(10,2),
    @Discount DECIMAL(10,2),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Bills]
    SET [dbo].[Bills].[BillNumber] = @BillNumber, 
        [dbo].[Bills].[BillDate] = @BillDate, 
        [dbo].[Bills].[OrderID] = @OrderID, 
        [dbo].[Bills].[TotalAmount] = @TotalAmount, 
        [dbo].[Bills].[Discount] = @Discount, 
        [dbo].[Bills].[NetAmount] = @NetAmount, 
        [dbo].[Bills].[UserID] = @UserID
    WHERE [dbo].[Bills].[BillID] = @BillID;
END;
GO

-- Update Customer
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Customer_UpdateByPk]
    @CustomerID INT,
    @CustomerName VARCHAR(100),
    @HomeAddress VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo VARCHAR(15),
    @GSTNO VARCHAR(15),
    @CityName VARCHAR(100),
    @PinCode VARCHAR(15),
    @NetAmount DECIMAL(10,2),
    @UserID INT
AS
BEGIN
    UPDATE [dbo].[Customer]
    SET [dbo].[Customer].[CustomerName] = @CustomerName, 
        [dbo].[Customer].[HomeAddress] = @HomeAddress, 
        [dbo].[Customer].[Email] = @Email, 
        [dbo].[Customer].[MobileNo] = @MobileNo, 
        [dbo].[Customer].[GSTNO] = @GSTNO, 
        [dbo].[Customer].[CityName] = @CityName, 
        [dbo].[Customer].[PinCode] = @PinCode, 
        [dbo].[Customer].[NetAmount] = @NetAmount, 
        [dbo].[Customer].[UserID] = @UserID
    WHERE [dbo].[Customer].[CustomerID] = @CustomerID;
END;
GO




-- Delete from Product
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Product_DeleteByPk]
    @ProductID INT
AS
BEGIN
    DELETE FROM [dbo].[Product]
    WHERE [dbo].[Product].[ProductID] = @ProductID;
END;
GO

-- Delete from User
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_User_DeleteByPk]
    @UserID INT
AS
BEGIN
    DELETE FROM [dbo].[User]
    WHERE [dbo].[User].[UserID] = @UserID;
END;
GO

-- Delete from Order
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Order_DeleteByPk]
    @OrderID INT
AS
BEGIN
    DELETE FROM [dbo].[Order]
    WHERE [dbo].[Order].[OrderID] = @OrderID;
END;
GO

-- Delete from OrderDetail
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_OrderDetail_DeleteByPk]
    @OrderDetailID INT
AS
BEGIN
    DELETE FROM [dbo].[OrderDetail]
    WHERE [dbo].[OrderDetail].[OrderDetailID] = @OrderDetailID;
END;
GO

-- Delete from Bills
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Bills_DeleteByPk]
    @BillID INT
AS
BEGIN
    DELETE FROM [dbo].[Bills]
    WHERE [dbo].[Bills].[BillID] = @BillID;
END;
GO

-- Delete from Customer
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Customer_DeleteByPk]
    @CustomerID INT
AS
BEGIN
    DELETE FROM [dbo].[Customer]
    WHERE [dbo].[Customer].[CustomerID] = @CustomerID;
END;
GO




-- Select all from Product
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Product_SelectAll]
AS
BEGIN
    SELECT [dbo].[Product].[ProductID], 
           [dbo].[Product].[ProductName], 
           [dbo].[Product].[ProductPrice], 
           [dbo].[Product].[ProductCode], 
           [dbo].[Product].[Description], 
		   [dbo].[Product].[UserID], 
           [dbo].[User].[UserName],
		   [dbo].[User].[Address],
		   [dbo].[User].[IsActive]
    FROM [dbo].[Product]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID]=[dbo].[Product].[UserID]
	ORDER BY [dbo].[User].[UserName],[dbo].[Product].[ProductName];
END;
GO

-- Select all from User
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_User_SelectAll]
AS
BEGIN
    SELECT [dbo].[User].[UserID], 
           [dbo].[User].[UserName], 
           [dbo].[User].[Email], 
           [dbo].[User].[Password], 
           [dbo].[User].[MobileNo], 
           [dbo].[User].[Address], 
           [dbo].[User].[IsActive]
    FROM [dbo].[User]
    ORDER BY [dbo].[User].[UserName];
END;
GO

-- Select all from Order
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Order_SelectAll]
AS
BEGIN
    SELECT [dbo].[Order].[OrderID], 
		   [dbo].[Order].[OrderNumber],
           [dbo].[Order].[OrderDate], 
           [dbo].[Order].[CustomerID], 
		   [dbo].[Customer].[CustomerName],
           [dbo].[Order].[PaymentMode], 
           [dbo].[Order].[TotalAmount], 
           [dbo].[Order].[ShippingAddress], 
           [dbo].[Order].[UserID], 
           [dbo].[User].[UserName]
    FROM [dbo].[Order]
    INNER JOIN [dbo].[Customer] ON [dbo].[Order].[CustomerID] = [dbo].[Customer].[CustomerID]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[Customer].[UserID]
    ORDER BY [dbo].[Customer].[CustomerName],[dbo].[User].[UserName];
END;
GO

-- Select all from OrderDetail
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_OrderDetail_SelectAll]
AS
BEGIN
    SELECT [dbo].[OrderDetail].[OrderDetailID], 
           [dbo].[OrderDetail].[OrderID], 
		   [dbo].[Order].[OrderNumber],
           [dbo].[OrderDetail].[ProductID],
           [dbo].[Product].[ProductName],
           [dbo].[OrderDetail].[Quantity], 
           [dbo].[OrderDetail].[Amount], 
           [dbo].[OrderDetail].[TotalAmount], 
           [dbo].[OrderDetail].[UserID], 
		   [dbo].[User].[UserName]
    FROM [dbo].[OrderDetail]
    INNER JOIN [dbo].[Product] ON [dbo].[OrderDetail].[ProductID] = [dbo].[Product].[ProductID]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[OrderDetail].[UserID]
	INNER JOIN [dbo].[Order] ON  [dbo].[Order].[OrderID] = [dbo].[OrderDetail].[OrderID]
    ORDER BY [dbo].[Product].[ProductName],[dbo].[User].[UserName];
END;
GO

-- Select all from Bills
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Bills_SelectAll]
AS
BEGIN
    SELECT [dbo].[Bills].[BillID], 
           [dbo].[Bills].[BillNumber], 
           [dbo].[Bills].[BillDate], 
           [dbo].[Bills].[OrderID], 
		   [dbo].[Order].[OrderNumber],
		   [dbo].[Order].[OrderDate], 
           [dbo].[Bills].[TotalAmount], 
           [dbo].[Bills].[Discount], 
           [dbo].[Bills].[NetAmount], 
           [dbo].[Bills].[UserID], 
		   [dbo].[User].[UserName],
		   [dbo].[Customer].[CustomerID],
           [dbo].[Customer].[CustomerName]
    FROM [dbo].[Bills]
    INNER JOIN [dbo].[Order] ON [dbo].[Bills].[OrderID] = [dbo].[Order].[OrderID]
    INNER JOIN [dbo].[Customer] ON [dbo].[Order].[CustomerID] = [dbo].[Customer].[CustomerID]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[Customer].[UserID]
	ORDER BY [dbo].[User].[UserName],[dbo].[Customer].[CustomerName];
END;
GO

-- Select all from Customer
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Customer_SelectAll]
AS
BEGIN
    SELECT [dbo].[Customer].[CustomerID], 
           [dbo].[Customer].[CustomerName], 
           [dbo].[Customer].[HomeAddress], 
           [dbo].[Customer].[Email], 
           [dbo].[Customer].[MobileNo], 
           [dbo].[Customer].[GSTNO], 
           [dbo].[Customer].[CityName], 
           [dbo].[Customer].[PinCode], 
           [dbo].[Customer].[NetAmount], 
           [dbo].[Customer].[UserID],
		   [dbo].[User].[UserName],
		   [dbo].[User].[IsActive]
    FROM [dbo].[Customer]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[Customer].[UserID]
    ORDER BY [dbo].[Customer].[CustomerID],[dbo].[User].[UserName],[dbo].[Customer].[CityName];
END;
GO




-- Select Product by ID
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Product_SelectByPk]
    @ProductID INT
AS
BEGIN
    SELECT [dbo].[Product].[ProductID], 
           [dbo].[Product].[ProductName], 
           [dbo].[Product].[ProductPrice], 
           [dbo].[Product].[ProductCode], 
           [dbo].[Product].[Description], 
		   [dbo].[Product].[UserID], 
           [dbo].[User].[UserName],
		   [dbo].[User].[Address],
		   [dbo].[User].[IsActive]
    FROM [dbo].[Product]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID]=[dbo].[Product].[UserID]
    WHERE [dbo].[Product].[ProductID] = @ProductID
	ORDER BY [dbo].[User].[UserName],[dbo].[Product].[ProductName];
END;
GO

-- Select User by ID
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_User_SelectByPk]
    @UserID INT
AS
BEGIN
    SELECT [dbo].[User].[UserID], 
           [dbo].[User].[UserName], 
           [dbo].[User].[Email], 
           [dbo].[User].[Password], 
           [dbo].[User].[MobileNo], 
           [dbo].[User].[Address], 
           [dbo].[User].[IsActive]
    FROM [dbo].[User]
    WHERE [dbo].[User].[UserID] = @UserID
    ORDER BY [dbo].[User].[UserID];
END;
GO

-- Select Order by ID
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Order_SelectByPk]
    @OrderID INT
AS
BEGIN
   SELECT [dbo].[Order].[OrderID], 
           [dbo].[Order].[OrderDate], 
		   [dbo].[Order].[OrderNumber],
           [dbo].[Order].[CustomerID], 
		   [dbo].[Customer].[CustomerName],
           [dbo].[Order].[PaymentMode], 
           [dbo].[Order].[TotalAmount], 
           [dbo].[Order].[ShippingAddress], 
           [dbo].[Order].[UserID], 
           [dbo].[User].[UserName]
    FROM [dbo].[Order]
    INNER JOIN [dbo].[Customer] ON [dbo].[Order].[CustomerID] = [dbo].[Customer].[CustomerID]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[Customer].[UserID]
    WHERE [dbo].[Order].[OrderID] = @OrderID
    ORDER BY [dbo].[Customer].[CustomerName],[dbo].[User].[UserName];
END;
GO

-- Select OrderDetail by ID
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_OrderDetail_SelectByPk]
    @OrderDetailID INT
AS
BEGIN
    SELECT [dbo].[OrderDetail].[OrderDetailID], 
           [dbo].[OrderDetail].[OrderID],
		   [dbo].[Order].[OrderNumber],
           [dbo].[OrderDetail].[ProductID],
           [dbo].[Product].[ProductName],
           [dbo].[OrderDetail].[Quantity], 
           [dbo].[OrderDetail].[Amount], 
           [dbo].[OrderDetail].[TotalAmount], 
           [dbo].[OrderDetail].[UserID], 
		   [dbo].[User].[UserName]
    FROM [dbo].[OrderDetail]
    INNER JOIN [dbo].[Product] ON [dbo].[OrderDetail].[ProductID] = [dbo].[Product].[ProductID]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[OrderDetail].[UserID]
	INNER JOIN [dbo].[Order] ON  [dbo].[Order].[OrderID] = [dbo].[OrderDetail].[OrderID]
    WHERE [dbo].[OrderDetail].[OrderDetailID] = @OrderDetailID
    ORDER BY [dbo].[Product].[ProductName],[dbo].[User].[UserName];
END;
GO

-- Select Bills by ID
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Bills_SelectByPk]
    @BillID INT
AS
BEGIN
    SELECT [dbo].[Bills].[BillID], 
           [dbo].[Bills].[BillNumber], 
           [dbo].[Bills].[BillDate], 
           [dbo].[Bills].[OrderID], 
		   [dbo].[Order].[OrderNumber],
		   [dbo].[Order].[OrderDate], 
           [dbo].[Bills].[TotalAmount], 
           [dbo].[Bills].[Discount], 
           [dbo].[Bills].[NetAmount], 
           [dbo].[Bills].[UserID], 
		   [dbo].[User].[UserName],
		   [dbo].[Customer].[CustomerID],
           [dbo].[Customer].[CustomerName]
    FROM [dbo].[Bills]
    INNER JOIN [dbo].[Order] ON [dbo].[Bills].[OrderID] = [dbo].[Order].[OrderID]
    INNER JOIN [dbo].[Customer] ON [dbo].[Order].[CustomerID] = [dbo].[Customer].[CustomerID]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[Customer].[UserID]
    WHERE [dbo].[Bills].[BillID] = @BillID
    ORDER BY [dbo].[User].[UserName],[dbo].[Customer].[CustomerName];
END;
GO

-- Select Customer by ID
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Customer_SelectByPk]
    @CustomerID INT
AS
BEGIN
    SELECT [dbo].[Customer].[CustomerID], 
           [dbo].[Customer].[CustomerName], 
           [dbo].[Customer].[HomeAddress], 
           [dbo].[Customer].[Email], 
           [dbo].[Customer].[MobileNo], 
           [dbo].[Customer].[GSTNO], 
           [dbo].[Customer].[CityName], 
           [dbo].[Customer].[PinCode], 
           [dbo].[Customer].[NetAmount], 
           [dbo].[Customer].[UserID],
		   [dbo].[User].[UserName],
		   [dbo].[User].[IsActive]
    FROM [dbo].[Customer]
	INNER JOIN [dbo].[User] ON [dbo].[User].[UserID] = [dbo].[Customer].[UserID]
    WHERE [dbo].[Customer].[CustomerID] = @CustomerID
     ORDER BY [dbo].[Customer].[CustomerID],[dbo].[User].[UserName],[dbo].[Customer].[CityName];
END;
GO

--Customer DropDown
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Customer_DropDown]
AS
BEGIN
    SELECT
		[dbo].[Customer].[CustomerID],
        [dbo].[Customer].[CustomerName]
    FROM
        [dbo].[Customer]
END
GO

--Order DropDown
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Order_DropDown]
AS
BEGIN
   SELECT 
		[dbo].[Order].[OrderID],
		[dbo].[Order].[OrderNumber]
   FROM 
		[dbo].[Order]
END
GO

--Product DropDown
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_Product_DropDown]
AS
BEGIN
    SELECT
		[dbo].[Product].[ProductID],
        [dbo].[Product].[ProductName]
    FROM
        [dbo].[Product]
END
GO

--User DropDown
GO
CREATE OR ALTER PROCEDURE [dbo].[PR_User_DropDown]
AS
BEGIN
    SELECT
		[dbo].[User].[UserName],
		[dbo].[User].[UserID]
    FROM
        [dbo].[User]
END

--User Login
GO
CREATE PROCEDURE [dbo].[PR_User_Login]
    @UserName NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT 
        [dbo].[User].[UserID], 
        [dbo].[User].[UserName], 
        [dbo].[User].[MobileNo], 
        [dbo].[User].[Email], 
        [dbo].[User].[Password],
        [dbo].[User].[Address]
    FROM 
        [dbo].[User] 
    WHERE 
        [dbo].[User].[UserName] = @UserName 
        AND [dbo].[User].[Password] = @Password;
END