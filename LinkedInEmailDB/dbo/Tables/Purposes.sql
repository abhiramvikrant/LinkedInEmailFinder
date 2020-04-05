CREATE TABLE [dbo].[Purposes] (
    [PurposeId] INT            IDENTITY (1, 1) NOT NULL,
    [Purpose]   NVARCHAR (100) NOT NULL,
    [IsActive]  BIT            CONSTRAINT [DF_Purposes_IsActive] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Purposes] PRIMARY KEY CLUSTERED ([PurposeId] ASC)
);

