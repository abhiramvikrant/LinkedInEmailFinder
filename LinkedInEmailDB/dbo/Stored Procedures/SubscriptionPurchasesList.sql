CREATE PROC SubscriptionPurchasesList
AS
     SELECT pur.PurchaseId, 
            pur.UserId, 
            pur.SubscriptionUID, 
            pur.PurchaseOrderId, 
            pur.PurchasedOn, 
            pur.StartDate, 
            pur.EndDate, 
            pur.IsActive, 
            pur.AmountPaid, 
            pur.IsTrial, 
            u.UserName, 
            sub.SubscriptionName, 
            sub.SubscriptionPrice
     FROM SubscriptionPurchases pur
          INNER JOIN AspNetUsers u ON pur.UserId = u.Id
          INNER JOIN Subscriptions sub ON pur.SubscriptionUID = sub.SubscriptionId
