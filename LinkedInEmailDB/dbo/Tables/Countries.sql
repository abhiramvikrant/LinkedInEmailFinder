CREATE TABLE [dbo].[Countries] (
    [CountryID]            INT            IDENTITY (1, 1) NOT NULL,
    [CountryName]          NVARCHAR (100) NOT NULL,
    [TwoCharCountryCode]   NVARCHAR (2)   NULL,
    [ThreeCharCountryCode] NVARCHAR (3)   NULL,
    [IsActive]             BIT            CONSTRAINT [DF_Countries_IsActive] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

