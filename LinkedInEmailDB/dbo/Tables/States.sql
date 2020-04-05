CREATE TABLE [dbo].[States] (
    [StateId]   INT            IDENTITY (1, 1) NOT NULL,
    [StateName] NVARCHAR (150) NOT NULL,
    [StateCode] NVARCHAR (10)  NULL,
    [CountryId] INT            NOT NULL,
    [IsActive]  BIT            CONSTRAINT [DF_States_IsActive] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([StateId] ASC),
    CONSTRAINT [FK_Sate_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryID])
);

