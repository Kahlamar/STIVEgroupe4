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
    [SocieteFournisseur] VARCHAR(MAX) NOT NULL
);
GO

INSERT INTO Fournisseurs(SocieteFournisseur) VALUES('Fournisseur1')
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
    @SocieteFournisseur VARCHAR(MAX) 
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
    @SocieteFournisseur VARCHAR(MAX)
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
    [Nom] VARCHAR(MAX) NOT NULL,
    [Prenom] VARCHAR(MAX) NOT NULL,
    [Societe] VARCHAR(MAX),
    [AdresseLivraison] VARCHAR(MAX) NOT NULL    
);
GO

INSERT INTO [Clients](Nom, Prenom, Societe, AdresseLivraison) VALUES('Toto','Tata', 'TotoCorp', '1 Rue Toto')
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
    @Nom VARCHAR(MAX),
    @Prenom VARCHAR(MAX),
    @Societe VARCHAR(MAX),
    @AdresseLivraison VARCHAR(MAX)
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
    @Nom VARCHAR(MAX),
    @Prenom VARCHAR(MAX),
    @Societe VARCHAR(MAX),
    @AdresseLivraison VARCHAR(MAX)
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
    [QuantiteRouge] INT NOT NULL,
	[QuantiteRose] INT NOT NULL,
	[QuantiteBlanc] INT NOT NULL,
	[QuantiteDigestif] INT NOT NULL,
	[QuantitePetillant] INT NOT NULL,
    [DateCommande] DATETIME,
    [IdClient] INT NOT NULL FOREIGN KEY REFERENCES Clients(IdClient)
);
GO

INSERT INTO [CommandeClients](QuantiteRouge,QuantiteRose,QuantiteBlanc,QuantiteDigestif,QuantitePetillant, DateCommande, IdClient)
VALUES (25,25,25,25,25, GETDATE(), 1)
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
    @QuantiteRouge INT,
	@QuantiteRose INT,
	@QuantiteBlanc INT,
	@QuantitePetillant INT,
	@QuantiteDigestif INT,
    @DateCommande DATETIME,
    @IdClient INT
AS
BEGIN
	DECLARE @QuantiteRougeStock INT 
	SELECT @QuantiteRougeStock = QuantiteRouge FROM Stock;
	
	DECLARE @QuantiteRoseStock INT;
	SELECT @QuantiteRoseStock = QuantiteRose FROM Stock;

	DECLARE @QuantiteBlancStock INT;
	SELECT @QuantiteBlancStock = QuantiteBlanc FROM Stock;

	DECLARE @QuantitePetillantStock INT;
	SELECT @QuantitePetillantStock = QuantitePetillant FROM Stock;

	DECLARE @QuantiteDigestifStock INT
	SELECT @QuantiteDigestifStock = QuantiteDigestif FROM Stock;

    INSERT INTO CommandeClients(QuantiteRouge, QuantiteRose, 
								QuantiteBlanc, QuantitePetillant,
								QuantiteDigestif, DateCommande, IdClient) 
	VALUES (@QuantiteRouge, @QuantiteRose, @QuantiteBlanc, @QuantitePetillant, @QuantiteDigestif,
			GETDATE(), @IdClient)
	
	INSERT INTO CommandeFournisseurs(QuantiteRouge, QuantiteRose, 
								QuantiteBlanc, QuantitePetillant,
								QuantiteDigestif, DateCommande, IdFournisseur) 
	VALUES (@QuantiteRouge, @QuantiteRose, @QuantiteBlanc, @QuantitePetillant, @QuantiteDigestif,
			GETDATE(), @IdClient)
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
    @QuantiteRouge INT,
	@QuantiteRose INT,
	@QuantiteBlanc INT,
	@QuantiteDigestif INT,
	@QuantitePetillant INT,
    @IdCommandeClient INT
AS
BEGIN
    UPDATE CommandeClients
    SET QuantiteRouge = @QuantiteRouge,
	QuantiteRose = @QuantiteRose,
	QuantiteBlanc = @QuantiteBlanc,
	QuantitePetillant = @QuantitePetillant,
	QuantiteDigestif = @QuantiteDigestif	
	WHERE IdCommandeClient = @IdCommandeClient
END
GO

-- --------------------------------------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[CommandeFournisseurs]', 'U') IS NOT NULL
DROP TABLE [dbo].[CommandeFournisseurs]
GO

CREATE TABLE [dbo].[CommandeFournisseurs]
(
    [IdCommandeFournisseur] INT PRIMARY KEY IDENTITY(1, 1),
    [QuantiteRouge] INT NOT NULL,
	[QuantiteRose] INT NOT NULL,
	[QuantiteBlanc] INT NOT NULL,
	[QuantiteDigestif] INT NOT NULL,
	[QuantitePetillant] INT NOT NULL,
    [DateCommande] DATETIME NOT NULL,
    [IdFournisseur] INT NOT NULL FOREIGN KEY REFERENCES Fournisseurs(IdFournisseur) 
);
GO

INSERT INTO [CommandeFournisseurs](QuantiteRouge,QuantiteRose,QuantiteBlanc,QuantiteDigestif,QuantitePetillant, DateCommande, IdFournisseur)
VALUES (25,25,25,25,25, GETDATE(), 1)
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
    @QuantiteRouge INT,
	@QuantiteRose INT,
	@QuantiteBlanc INT,
	@QuantiteDigestif INT,
	@QuantitePetillant INT,
    @DateCommande DATETIME,   
    @IdFournisseur INT
AS
BEGIN
    INSERT INTO CommandeFournisseurs(QuantiteRouge, QuantiteRose, QuantiteBlanc, QuantiteDigestif, QuantitePetillant, DateCommande, IdFournisseur) 
	VALUES(@QuantiteRouge, @QuantiteRose, @QuantiteBlanc, @QuantiteDigestif, @QuantitePetillant, GETDATE(), @IdFournisseur)
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
    @QuantiteRouge INT,
	@QuantiteRose INT,
	@QuantiteBlanc INT,
	@QuantiteDigestif INT,
	@QuantitePetillant INT,
    @IdCommandeFournisseur INT
AS
BEGIN
    UPDATE CommandeFournisseurs
    SET QuantiteRouge = @QuantiteRouge,
	QuantiteRose = @QuantiteRose,
	QuantiteBlanc = @QuantiteBlanc,
	QuantitePetillant = @QuantitePetillant,
	QuantiteDigestif = @QuantiteDigestif	
	WHERE IdCommandeFournisseur = @IdCommandeFournisseur
END
GO

-- -------------------------------------------------------------------------------------------------------------------------

IF OBJECT_ID('[dbo].[Vins]', 'U') IS NOT NULL
DROP TABLE [dbo].[Vins]
GO

CREATE TABLE [dbo].[Vins]
(
    [IdVin] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Nom] VARCHAR(MAX) NOT NULL,
    [Couleur] VARCHAR(MAX) NOT NULL,
    [Annee] INT NOT NULL,
    [DegreAlcool] INT NOT NULL,

);
GO
INSERT INTO Vins(Nom, Couleur, Annee, DegreAlcool)
VALUES 
	('Bordeaux Rouge', 'Rouge', 2019, 15),
	('Bordeaux Rose', 'Rose', 2019, 13),
	('Bordeaux Blanc', 'Blanc', 2019, 14),
	('Bordeaux Petillant', 'Petillant', 2019, 17),
	('Bordeaux Digestif', 'Digestif', 2019, 16)
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
    @Nom VARCHAR(MAX),
    @Couleur VARCHAR(MAX),
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
    @Nom VARCHAR(MAX),
    @Couleur VARCHAR(MAX),
    @Annee VARCHAR(MAX),
    @DegreAlcool VARCHAR(MAX)
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

INSERT INTO Stock([QuantiteRouge], [QuantiteRose], [QuantiteBlanc], [QuantitePetillant], [QuantiteDigestif])
VALUES (100,100,100,100,100)
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

