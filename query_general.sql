
SELECT * FROM [interviewBackendMain24].[dbo].[OrderSteps]
SELECT * FROM [interviewBackendMain24].[dbo].[OrderStepFlows]
SELECT * FROM [interviewBackendMain24].[dbo].[PaymentMethods]
SELECT * FROM [interviewBackendMain24].[dbo].[Customers]
SELECT * FROM [interviewBackendMain24].[dbo].[Products]
SELECT * FROM [interviewBackendMain24].[dbo].[Orders]
SELECT * FROM [interviewBackendMain24].[dbo].[OrderProducts]

DELETE FROM [interviewBackendMain24].[dbo].[OrderSteps]
DELETE FROM [interviewBackendMain24].[dbo].[OrderStepFlows]
DELETE FROM [interviewBackendMain24].[dbo].[PaymentMethods]
DELETE FROM [interviewBackendMain24].[dbo].[Customers]
DELETE FROM [interviewBackendMain24].[dbo].[Orders]
DELETE FROM [interviewBackendMain24].[dbo].[Products]

TRUNCATE TABLE [interviewBackendMain24].[dbo].[PaymentMethods]
TRUNCATE TABLE [interviewBackendMain24].[dbo].[OrderSteps]
TRUNCATE TABLE [interviewBackendMain24].[dbo].[OrderStepFlows]
TRUNCATE TABLE [interviewBackendMain24].[dbo].[Customers]
TRUNCATE TABLE [interviewBackendMain24].[dbo].[Products]
TRUNCATE TABLE [interviewBackendMain24].[dbo].[Orders]


--DROP TABLE [interviewBackendMain24].[dbo].[PaymentMethods]
--DROP TABLE [interviewBackendMain24].[dbo].[OrderSteps]
--DROP TABLE [interviewBackendMain24].[dbo].[OrderStepFlows]
--DROP TABLE [interviewBackendMain24].[dbo].[Customers]
--DROP TABLE [interviewBackendMain24].[dbo].[Orders]
--DROP TABLE [interviewBackendMain24].[dbo].[Products]
--DROP TABLE [interviewBackendMain24].[dbo].[OrderProducts]
--DROP TABLE [interviewBackendMain24].[dbo].[__EFMigrationsHistory]

 INSERT INTO Orders (OrderStepId, PaymentMethodId, CustomerId, DateCreate,DateEdit)
 VALUES (1,1,1,GETDATE(),NULL)
 
 INSERT INTO OrderProducts (OrderId, ProductId)
 VALUES (1,1), (1,2), (1,3)


