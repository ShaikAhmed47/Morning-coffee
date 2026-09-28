USE MorningCoffeeDb;
GO
CREATE OR ALTER PROCEDURE dbo.GetAvailableProducts
    @Search NVARCHAR(150)=NULL, @CategoryId INT=NULL
AS
BEGIN
 SELECT p.ProductId,p.Name,p.Description,p.Price,p.CategoryId,c.Name AS CategoryName,p.ImageUrl,p.IsAvailable
 FROM Products p INNER JOIN Categories c ON c.CategoryId=p.CategoryId
 WHERE p.IsAvailable=1 AND (@Search IS NULL OR p.Name LIKE '%'+@Search+'%' OR p.Description LIKE '%'+@Search+'%')
 AND (@CategoryId IS NULL OR p.CategoryId=@CategoryId)
 ORDER BY p.Name;
END
GO
CREATE OR ALTER PROCEDURE dbo.GetCustomerOrders @CustomerId INT
AS
BEGIN
 SELECT o.OrderId,o.Status,o.PaymentMethod,o.Subtotal,o.Tax,o.Total,o.OrderDate
 FROM Orders o WHERE o.CustomerId=@CustomerId ORDER BY o.OrderDate DESC;
END
GO
CREATE OR ALTER PROCEDURE dbo.GetSalesSummary
AS
BEGIN
 SELECT COUNT(*) AS TotalOrders,
        SUM(CASE WHEN Status<>'Cancelled' THEN Total ELSE 0 END) AS Revenue,
        (SELECT COUNT(*) FROM Products WHERE IsAvailable=1) AS ActiveProducts,
        (SELECT COUNT(*) FROM Customers) AS Customers
 FROM Orders;
END
GO
