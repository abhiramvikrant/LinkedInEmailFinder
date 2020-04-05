CREATE TABLE [dbo].[SubscriptionPurchases] (
    [PurchaseId]      UNIQUEIDENTIFIER CONSTRAINT [DF_SubscriptionPayments_PurchaseId] DEFAULT (newid()) NOT NULL,
    [UserId]          NVARCHAR (450)   NOT NULL,
    [SubscriptionUID] UNIQUEIDENTIFIER NOT NULL,
    [PurchaseOrderId] INT              IDENTITY (1, 1) NOT NULL,
    [PurchasedOn]     DATETIME         NOT NULL,
    [StartDate]       DATETIME         NOT NULL,
    [EndDate]         DATETIME         NOT NULL,
    [IsActive]        BIT              CONSTRAINT [DF_SubscriptionPayments_IsActive] DEFAULT ((1)) NOT NULL,
    [AmountPaid]      MONEY            NOT NULL,
    [IsTrial]         BIT              CONSTRAINT [DF_SubscriptionPayments_IsTrial] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_SubscriptionPayments] PRIMARY KEY CLUSTERED ([PurchaseId] ASC),
    CONSTRAINT [FK_SubscriptionPurchases_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

