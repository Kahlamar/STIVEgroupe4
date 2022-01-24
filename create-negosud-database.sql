USE master
GO

IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'NegoSud'
)
CREATE DATABASE NegoSud
GO

USE NegoSud

-- -------------------------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[Fournisseurs]', 'U') IS NOT NULL
DROP TABLE [dbo].[Fournisseurs]
GO

CREATE TABLE [dbo].[Fournisseurs]
(
    [IdFournisseur] INT PRIMARY KEY IDENTITY(1, 1), 
    [SocieteFournisseur] NVARCHAR(50) NOT NULL
);
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'CreateOneFournisseur'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.CreateOneFournisseur
GO

CREATE PROCEDURE dbo.CreateOneFournisseur
    @SocieteFournisseur NVARCHAR(50) 
AS
BEGIN
    INSERT INTO Fournisseurs(SocieteFournisseur) VALUES (@SocieteFournisseur)
END
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'UpdateOneFournisseur'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.UpdateOneFournisseur
GO

CREATE PROCEDURE dbo.UpdateOneFournisseur
    @IdFournisseur INT,
    @SocieteFournisseur NVARCHAR(50)
AS
BEGIN
    UPDATE Fournisseurs
    SET SocieteFournisseur = @SocieteFournisseur WHERE IdFournisseur = @IdFournisseur
END
GO

-- -----------------------------------------------------------------------------------------------------------------------------------------
IF OBJECT_ID('[dbo].[Clients]', 'U') IS NOT NULL
DROP TABLE [dbo].[Clients]
GO

CREATE TABLE [dbo].[Clients]
(
    [IdClient] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nom] NVARCHAR(50) NOT NULL,
    [Prenom] NVARCHAR(50) NOT NULL,
    [Societe] NVARCHAR(50),
    [AdresseLivraison] NVARCHAR(50)    
);
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'CreateOneClient'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.CreateOneClient
GO

CREATE PROCEDURE dbo.CreateOneClient
    @Nom NVARCHAR(50),
    @Prenom NVARCHAR(50),
    @Societe NVARCHAR(50),
    @AdresseLivraison NVARCHAR(50)
AS
BEGIN
    INSERT INTO Clients(Nom, Prenom, Societe, AdresseLivraison) VALUES(@Nom, @Prenom, @Societe, @AdresseLivraison)
END
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'UpdateOneClient'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.UpdateOneClient
GO

CREATE PROCEDURE dbo.UpdateOneClient
    @IdClient INT,
    @Nom NVARCHAR(50),
    @Prenom NVARCHAR(50),
    @Societe NVARCHAR(50),
    @AdresseLivraison NVARCHAR(50)
AS
BEGIN
    UPDATE Clients
    SET Nom=@Nom, Prenom=@Prenom, Societe=@Societe, AdresseLivraison=@AdresseLivraison WHERE IdClient = @IdClient
END
GO






-- --------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[CommandeClients]', 'U') IS NOT NULL
DROP TABLE [dbo].[CommandeClients]
GO

CREATE TABLE [dbo].[CommandeClients]
(
    [IdCommandeClient] INT PRIMARY KEY IDENTITY(1, 1),
    [QuantiteVin] INT NOT NULL,
    [DateCommande] DATETIME,
    [IdClient] INT NOT NULL FOREIGN KEY REFERENCES Clients(IdClient)
);
GO


IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'CreateOneCommandeClient'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.CreateOneCommandeClient
GO

CREATE PROCEDURE dbo.CreateOneCommandeClient
    @QuantiteVin INT,
    @DateCommande DATETIME,
    @IdClient INT
AS
BEGIN
    INSERT INTO CommandeClients(QuantiteVin, DateCommande, IdClient) VALUES (@QuantiteVin, GETDATE(), @IdClient)
END
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'UpdateOneCommandeClient'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.UpdateOneCommandeClient
GO

CREATE PROCEDURE dbo.UpdateOneCommandeClient
    @QuantiteVin INT,
    @IdCommandeClient INT
AS
BEGIN
    UPDATE CommandeClients
    SET QuantiteVin = @QuantiteVin WHERE IdCommandeClient = @IdCommandeClient
END
GO

-- --------------------------------------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[CommandeFournisseurs]', 'U') IS NOT NULL
DROP TABLE [dbo].[CommandeFournisseurs]
GO

CREATE TABLE [dbo].[CommandeFournisseurs]
(
    [IdCommandeFournisseur] INT PRIMARY KEY IDENTITY(1, 1),
    [QuantiteVin] INT NOT NULL,
    [DateCommande] DATETIME NOT NULL,
    [IdFournisseur] INT NOT NULL FOREIGN KEY REFERENCES Fournisseurs(IdFournisseur) 
);
GO


IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'CreateOneCommandeFournisseur'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.CreateOneCommandeFournisseur
GO

CREATE PROCEDURE dbo.CreateOneCommandeFournisseur
    @QuantiteVin INT,
    @DateCommande DATETIME,   
    @IdFournisseur INT
AS
BEGIN
    INSERT INTO CommandeFournisseurs(QuantiteVin, DateCommande, IdFournisseur) VALUES(@QuantiteVin, GETDATE(), @IdFournisseur)
END
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'UpdateOneCommandeFournisseur'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.UpdateOneCommandeFournisseur
GO

CREATE PROCEDURE dbo.UpdateOneCommandeFournisseur
    @IdCommandeFournisseur INT,
    @QuantiteVin INT
AS
BEGIN
    UPDATE CommandeFournisseurs
   SET QuantiteVin = @QuantiteVin WHERE IdCommandeFournisseur = @IdCommandeFournisseur 
END
GO

-- -------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[Vins]', 'U') IS NOT NULL
DROP TABLE [dbo].[Vins]
GO

CREATE TABLE [dbo].[Vins]
(
    [IdVin] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nom] NVARCHAR(50) NOT NULL,
    [Couleur] NVARCHAR(50) NOT NULL,
    [Annee] INT NOT NULL,
    [DegreAlcool] INT NOT NULL,

);
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'CreateOneVin'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.CreateOneVin
GO

CREATE PROCEDURE dbo.CreateOneVin
    @Nom NVARCHAR(50),
    @Couleur NVARCHAR(50),
    @Annee INT,
    @DegreAlcool INT
AS
BEGIN
    INSERT INTO Vins(Nom, Couleur, Annee, DegreAlcool) VALUES(@Nom, @Couleur, @Annee, @DegreAlcool)
END
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'UpdateOneVin'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.UpdateOneVin
GO

CREATE PROCEDURE dbo.UpdateOneVin
    @IdVin INT,
    @Nom NVARCHAR(50),
    @Couleur NVARCHAR(50),
    @Annee NVARCHAR(50),
    @DegreAlcool NVARCHAR(50)
AS
BEGIN
    UPDATE Vins
    SET Nom=@Nom, Couleur=@Couleur, Annee=@Annee, DegreAlcool=@DegreAlcool WHERE IdVin=@IdVin
END
GO


-- -------------------------------------------------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[Stock]', 'U') IS NOT NULL
DROP TABLE [dbo].[Stock]
GO

CREATE TABLE [dbo].[Stock]
(
    [QuantiteRouge] INT NOT NULL,
    [QuantiteRose] INT NOT NULL,
    [QuantiteBlanc] INT NOT NULL,
    [QuantitePetillant] INT NOT NULL,
    [QuantiteDigestif] INT NOT NULL,
    
);
GO

IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'CreateInitialStock'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.CreateInitialStock
GO

CREATE PROCEDURE dbo.CreateInitialStock
    @QuantiteRouge INT,
    @QuantiteRose INT,
    @QuantiteBlanc INT,
    @QuantitePetillant INT,
    @QuantiteDigestif INT
AS
BEGIN
    INSERT INTO Stock(QuantiteRouge, QuantiteRose, QuantiteBlanc, QuantitePetillant, QuantiteDigestif) VALUES (@QuantiteRouge, @QuantiteRose, @QuantiteBlanc, @QuantitePetillant, @QuantiteDigestif)
END
GO

