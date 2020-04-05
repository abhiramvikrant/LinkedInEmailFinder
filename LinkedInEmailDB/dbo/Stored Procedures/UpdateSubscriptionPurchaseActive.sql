CREATE PROC UpdateSubscriptionPurchaseActive
(@purchaseid UNIQUEIDENTIFIER, 
 @mode       BIT
)
AS
     IF(@mode = 1)
         BEGIN
             UPDATE SubscriptionPurchases
               SET 
                   IsActive = 0
             WHERE PurchaseId = @purchaseid
     END
         ELSE
         IF(@mode = 0)
             BEGIN
                 UPDATE SubscriptionPurchases
                   SET 
                       IsActive = 1
                 WHERE PurchaseId = @purchaseid
         END