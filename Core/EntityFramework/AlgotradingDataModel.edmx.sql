
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/27/2014 14:43:56
-- Generated from EDMX file: C:\Users\Tam\Source\GIT\algorithmic-trading\Core\EntityFramework\AlgotradingDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AlgorithmicTrading];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Portfolio].[FK_Portfolio_Security_Portfolio]', 'F') IS NOT NULL
    ALTER TABLE [Portfolio].[Portfolio_Security] DROP CONSTRAINT [FK_Portfolio_Security_Portfolio];
GO
IF OBJECT_ID(N'[Portfolio].[FK_Portfolio_Security_Security]', 'F') IS NOT NULL
    ALTER TABLE [Portfolio].[Portfolio_Security] DROP CONSTRAINT [FK_Portfolio_Security_Security];
GO
IF OBJECT_ID(N'[Portfolio].[FK_Portfolio_User]', 'F') IS NOT NULL
    ALTER TABLE [Portfolio].[Portfolio] DROP CONSTRAINT [FK_Portfolio_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[HFT].[Tick]', 'U') IS NOT NULL
    DROP TABLE [HFT].[Tick];
GO
IF OBJECT_ID(N'[InterDay].[HistoricalStock]', 'U') IS NOT NULL
    DROP TABLE [InterDay].[HistoricalStock];
GO
IF OBJECT_ID(N'[Portfolio].[Portfolio]', 'U') IS NOT NULL
    DROP TABLE [Portfolio].[Portfolio];
GO
IF OBJECT_ID(N'[Portfolio].[Portfolio_Security]', 'U') IS NOT NULL
    DROP TABLE [Portfolio].[Portfolio_Security];
GO
IF OBJECT_ID(N'[Portfolio].[Security]', 'U') IS NOT NULL
    DROP TABLE [Portfolio].[Security];
GO
IF OBJECT_ID(N'[Portfolio].[User]', 'U') IS NOT NULL
    DROP TABLE [Portfolio].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Ticks'
CREATE TABLE [dbo].[Ticks] (
    [Id] uniqueidentifier  NOT NULL,
    [Symbol] char(10)  NULL,
    [Date] datetime  NOT NULL,
    [Time] time  NOT NULL,
    [Open] real  NOT NULL,
    [High] real  NOT NULL,
    [Low] real  NOT NULL,
    [Close] real  NOT NULL,
    [Volume] int  NOT NULL
);
GO

-- Creating table 'HistoricalStocks'
CREATE TABLE [dbo].[HistoricalStocks] (
    [HistoricalStockId] uniqueidentifier  NOT NULL,
    [Symbol] nchar(10)  NOT NULL,
    [Exchange] nchar(10)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Open] decimal(18,4)  NULL,
    [High] decimal(18,4)  NULL,
    [Low] decimal(18,4)  NULL,
    [Close] decimal(18,4)  NOT NULL,
    [Volume] decimal(18,4)  NOT NULL
);
GO

-- Creating table 'Portfolios'
CREATE TABLE [dbo].[Portfolios] (
    [PortfolioId] uniqueidentifier  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Portfolio_Security'
CREATE TABLE [dbo].[Portfolio_Security] (
    [Id] uniqueidentifier  NOT NULL,
    [PortfolioId] uniqueidentifier  NOT NULL,
    [SecurityId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Securities'
CREATE TABLE [dbo].[Securities] (
    [SecurityId] uniqueidentifier  NOT NULL,
    [Symbol] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Ticks'
ALTER TABLE [dbo].[Ticks]
ADD CONSTRAINT [PK_Ticks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [HistoricalStockId] in table 'HistoricalStocks'
ALTER TABLE [dbo].[HistoricalStocks]
ADD CONSTRAINT [PK_HistoricalStocks]
    PRIMARY KEY CLUSTERED ([HistoricalStockId] ASC);
GO

-- Creating primary key on [PortfolioId] in table 'Portfolios'
ALTER TABLE [dbo].[Portfolios]
ADD CONSTRAINT [PK_Portfolios]
    PRIMARY KEY CLUSTERED ([PortfolioId] ASC);
GO

-- Creating primary key on [Id] in table 'Portfolio_Security'
ALTER TABLE [dbo].[Portfolio_Security]
ADD CONSTRAINT [PK_Portfolio_Security]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [SecurityId] in table 'Securities'
ALTER TABLE [dbo].[Securities]
ADD CONSTRAINT [PK_Securities]
    PRIMARY KEY CLUSTERED ([SecurityId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PortfolioId] in table 'Portfolio_Security'
ALTER TABLE [dbo].[Portfolio_Security]
ADD CONSTRAINT [FK_Portfolio_Security_Portfolio]
    FOREIGN KEY ([PortfolioId])
    REFERENCES [dbo].[Portfolios]
        ([PortfolioId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Portfolio_Security_Portfolio'
CREATE INDEX [IX_FK_Portfolio_Security_Portfolio]
ON [dbo].[Portfolio_Security]
    ([PortfolioId]);
GO

-- Creating foreign key on [UserId] in table 'Portfolios'
ALTER TABLE [dbo].[Portfolios]
ADD CONSTRAINT [FK_Portfolio_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Portfolio_User'
CREATE INDEX [IX_FK_Portfolio_User]
ON [dbo].[Portfolios]
    ([UserId]);
GO

-- Creating foreign key on [SecurityId] in table 'Portfolio_Security'
ALTER TABLE [dbo].[Portfolio_Security]
ADD CONSTRAINT [FK_Portfolio_Security_Security]
    FOREIGN KEY ([SecurityId])
    REFERENCES [dbo].[Securities]
        ([SecurityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Portfolio_Security_Security'
CREATE INDEX [IX_FK_Portfolio_Security_Security]
ON [dbo].[Portfolio_Security]
    ([SecurityId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------