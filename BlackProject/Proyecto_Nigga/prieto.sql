create database Libreria_EnmaDonuts
go
use Libreria_EnmaDonuts
go

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY (1,1),
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    Address NVARCHAR(255),
    Phone NVARCHAR(15)
);
GO

CREATE TABLE Books (
    Id INT PRIMARY KEY IDENTITY (1,1),
    Title NVARCHAR(255),
    Author NVARCHAR(100),
    Price MONEY,
    Stock INT
);
GO

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY (1,1),
    CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
    OrderDate DATETIME,
    Status NVARCHAR(50)
);
GO

CREATE TABLE OrderDetails (
    Id INT PRIMARY KEY IDENTITY (1,1),
    OrderId INT FOREIGN KEY REFERENCES Orders(Id),
    BookId INT FOREIGN KEY REFERENCES Books(Id),
    Quantity INT,
    UnitPrice DECIMAL(10, 2),
	TotalPrice Decimal(10,2)
);
GO
ALTER TABLE OrderDetails
ADD TotalPrice DECIMAL(10, 2) NOT NULL DEFAULT 0;
go

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY (1,1),
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    Password NVARCHAR(100),
    RegistrationDate DATETIME
);
GO

-- Procedimientos almacenados para Books
-- Obtener todos los libros
CREATE PROCEDURE spBooks_GetAll
AS
BEGIN
    SELECT * FROM Books;
END;
GO

-- Obtener un libro por ID
CREATE PROCEDURE spBooks_GetById
    @Id INT
AS
BEGIN
    SELECT * FROM Books WHERE Id = @Id;
END;
GO

-- Agregar un nuevo libro
CREATE PROCEDURE spBooks_Insert
    @Title NVARCHAR(255),
    @Author NVARCHAR(100),
    @Price DECIMAL(10, 2),
    @Stock INT
AS
BEGIN
    INSERT INTO Books (Title, Author, Price, Stock)
    VALUES (@Title, @Author, @Price, @Stock);
END;
GO

-- Editar un libro existente
CREATE PROCEDURE spBooks_Update
    @Id INT,
    @Title NVARCHAR(255),
    @Author NVARCHAR(100),
    @Price DECIMAL(10, 2),
    @Stock INT
AS
BEGIN
    UPDATE Books
    SET Title = @Title,
        Author = @Author,
        Price = @Price,
        Stock = @Stock
    WHERE Id = @Id;
END;
GO

-- Eliminar un libro
CREATE PROCEDURE spBooks_Delete
    @Id INT
AS
BEGIN
    DELETE FROM Books WHERE Id = @Id;
END;
GO

-- Procedimientos almacenados para Customers
-- Obtener todos los clientes
CREATE PROCEDURE spCustomers_GetAll
AS
BEGIN
    SELECT * FROM Customers;
END;
GO

-- Obtener un cliente por ID
CREATE PROCEDURE spCustomers_GetById
    @Id INT
AS
BEGIN
    SELECT * FROM Customers WHERE Id = @Id;
END;
GO

-- Agregar un nuevo cliente
CREATE PROCEDURE spCustomers_Insert
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Address NVARCHAR(255),
    @Phone NVARCHAR(15)
AS
BEGIN
    INSERT INTO Customers (Name, Email, Address, Phone)
    VALUES (@Name, @Email, @Address, @Phone);
END;
GO

-- Editar un cliente existente
CREATE PROCEDURE spCustomers_Update
    @Id INT,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Address NVARCHAR(255),
    @Phone NVARCHAR(15)
AS
BEGIN
    UPDATE Customers
    SET Name = @Name,
        Email = @Email,
        Address = @Address,
        Phone = @Phone
    WHERE Id = @Id;
END;
GO

-- Eliminar un cliente
CREATE PROCEDURE spCustomers_Delete
    @Id INT
AS
BEGIN
    DELETE FROM Customers WHERE Id = @Id;
END;
GO

-- Procedimientos almacenados para Orders
-- Obtener todos los pedidos
-- Procedimientos para la tabla Orders
CREATE PROCEDURE spOrders_GetAll
AS
BEGIN
    SELECT o.Id, o.CustomerId, c.Name AS CustomerName, o.OrderDate, o.Status
    FROM Orders o
    INNER JOIN Customers c ON o.CustomerId = c.Id;
END;
GO

CREATE PROCEDURE spOrders_GetById
    @Id INT
AS
BEGIN
    SELECT o.Id, o.CustomerId, c.Name AS CustomerName, o.OrderDate, o.Status
    FROM Orders o
    INNER JOIN Customers c ON o.CustomerId = c.Id
    WHERE o.Id = @Id;
END;
GO

CREATE PROCEDURE spOrders_Update
    @Id INT,
    @CustomerId INT,
    @OrderDate DATETIME,
    @Status NVARCHAR(50)
AS
BEGIN
    UPDATE Orders
    SET CustomerId = @CustomerId, OrderDate = @OrderDate, Status = @Status
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE spOrders_Delete
    @Id INT
AS
BEGIN
    DELETE FROM Orders WHERE Id = @Id;
END;
GO

-- Procedimientos para la tabla OrderDetails
CREATE or alter PROCEDURE spOrderDetails_GetAllByOrderId
    @OrderId INT
AS
BEGIN
    SELECT od.Id, od.OrderId, od.BookId, b.Title AS BookTitle, od.Quantity, od.UnitPrice, od.TotalPrice
    FROM OrderDetails od
    INNER JOIN Books b ON od.BookId = b.Id
    WHERE od.OrderId = @OrderId;
END;
GO

CREATE or alter PROCEDURE spOrderDetails_GetById
    @Id INT
AS
BEGIN
    SELECT od.Id, od.OrderId, od.BookId, b.Title AS BookTitle, od.Quantity, od.UnitPrice, od.TotalPrice
    FROM OrderDetails od
    INNER JOIN Books b ON od.BookId = b.Id
    WHERE od.Id = @Id;
END;
GO

CREATE or alter PROCEDURE spOrderDetails_Insert
    @OrderId INT,
    @BookId INT,
    @Quantity INT,
    @UnitPrice DECIMAL(10, 2),
	@TotalPrice Decimal (10,2)
AS
BEGIN
    INSERT INTO OrderDetails (OrderId, BookId, Quantity, UnitPrice, TotalPrice)
    VALUES (@OrderId, @BookId, @Quantity, @UnitPrice, @TotalPrice);
END;
GO

CREATE or alter PROCEDURE spOrderDetails_Update
    @Id INT,
    @OrderId INT,
    @BookId INT,
    @Quantity INT,
    @UnitPrice DECIMAL(10, 2),
	@TotalPrice Decimal (10,2)
AS
BEGIN
    UPDATE OrderDetails
    SET OrderId = @OrderId, BookId = @BookId, Quantity = @Quantity, UnitPrice = @UnitPrice, TotalPrice = @TotalPrice
    WHERE Id = @Id;
END;
GO

CREATE PROCEDURE spOrderDetails_Delete
    @Id INT
AS
BEGIN
    DELETE FROM OrderDetails WHERE Id = @Id;
END;
GO



-- Procedimientos almacenados para Users
-- Obtener todos los usuarios
CREATE PROCEDURE spUsers_GetAll
AS
BEGIN
    SELECT * FROM Users;
END;
GO

-- Obtener un usuario por ID
CREATE PROCEDURE spUsers_GetById
    @Id INT
AS
BEGIN
    SELECT * FROM Users WHERE Id = @Id;
END;
GO

-- Agregar un nuevo usuario
CREATE PROCEDURE spUsers_Insert
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Password NVARCHAR(100),
    @RegistrationDate DATETIME
AS
BEGIN
    INSERT INTO Users (Name, Email, Password, RegistrationDate)
    VALUES (@Name, @Email, @Password, @RegistrationDate);
END;
GO

-- Editar un usuario existente
CREATE PROCEDURE spUsers_Update
    @Id INT,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Password NVARCHAR(100),
    @RegistrationDate DATETIME
AS
BEGIN
    UPDATE Users
    SET Name = @Name,
        Email = @Email,
        Password = @Password,
        RegistrationDate = @RegistrationDate
    WHERE Id = @Id;
END;
GO

-- Eliminar un usuario
CREATE PROCEDURE spUsers_Delete
    @Id INT
AS
BEGIN
    DELETE FROM Users WHERE Id = @Id;
END;
GO

--Modificaciones--
CREATE OR ALTER PROCEDURE spOrders_Insert
(
    @CustomerId INT,
    @OrderDate DATETIME,
    @Status NVARCHAR(50),
    @NewOrderId INT OUTPUT -- Parámetro de salida para el ID
)
AS
BEGIN
    INSERT INTO Orders (CustomerId, OrderDate, Status)
    VALUES (@CustomerId, @OrderDate, @Status);
    
    SET @NewOrderId = SCOPE_IDENTITY();
END;
GO