CREATE TABLE [dbo].[Subscriptions] (
    [SubscriptionId]             UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [SubscriptionName]           NVARCHAR (100)   NOT NULL,
    [SubscriptionPrice]          MONEY            NOT NULL,
    [SubscriptionDescription]    NVARCHAR (255)   NOT NULL,
    [UseDiscountPrice]           BIT              CONSTRAINT [DF_Subscriptions_UseDiscountPrice] DEFAULT ((0)) NOT NULL,
    [SubscriptionPeriodInMonths] INT              NOT NULL,
    [SubscriptionDisplayOrder]   INT              NULL,
    [SubscriptionDiscountPrice]  MONEY            DEFAULT ((0)) NULL,
    [IsActive]                   BIT              DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([SubscriptionId] ASC)
);

