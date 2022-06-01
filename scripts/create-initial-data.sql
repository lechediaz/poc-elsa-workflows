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
  ([Id],[Name])
VALUES
  (1 ,'Hierro x5 kg.')
  ,(2 ,'Carbón x2 kg.')
  ,(3 ,'Cemento x10 kg.')
  ,(4 ,'Petróleo x5 gal.');
GO

SET IDENTITY_INSERT RawMaterials OFF;
GO
