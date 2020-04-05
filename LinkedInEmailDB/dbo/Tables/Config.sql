CREATE TABLE [dbo].[Config] (
    [ConfigId] INT            IDENTITY (1, 1) NOT NULL,
    [KeyName]  NVARCHAR (100) NOT NULL,
    [Value]    NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED ([ConfigId] ASC)
);

