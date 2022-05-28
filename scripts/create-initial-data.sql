USE FactoryDb;
GO

DELETE FROM Users;
GO

SET IDENTITY_INSERT Users ON;
GO

INSERT INTO Users
  ([Id],[Name],[Email],[Role],[SupervisorId])
VALUES
  (1 ,'Oscar Díaz' ,'lechediaz@gmail.com' ,'ADMIN' ,NULL)
  ,(2 ,'Karen Medina' ,'karenmedina@fakemail.com' ,'ADMIN' ,NULL)
  ,(3 ,'Elizabeth Sanchez' ,'elizabeths@fakemail.com' ,'PRODUCTION' ,1)
  ,(4 ,'Julian Ramirez' ,'julianramirez@fakemail.com' ,'PRODUCTION' ,1)
  ,(5 ,'Patricia Mejia' ,'patriciamejia@fakemail.com' ,'PRODUCTION' ,2);
GO

SET IDENTITY_INSERT Users OFF;
GO

DELETE FROM RawMaterials;

SET IDENTITY_INSERT RawMaterials ON;
GO

INSERT INTO RawMaterials
  ([Id],[Name],[Description])
VALUES
  (1 ,'Arroz x1 lb.' ,'Grano.')
  ,(2 ,'Huevos x12 Und.' ,'Docena de huevos.')
  ,(3 ,'Cebolla x1 lb.' ,'Te puede hacer llorar.')
  ,(4 ,'Carne de res x1 lb.' ,'Muy importante para el asado.');
GO

SET IDENTITY_INSERT RawMaterials OFF;
GO
