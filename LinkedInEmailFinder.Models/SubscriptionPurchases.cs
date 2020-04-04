using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models
{
	public class SubscriptionPurchases
	{[Key]
		public Guid PurchaseId { get; set; }
		public string UserId { get; set; }
		public Guid SubscriptionUID { get; set; }
	
		public DateTime PurchasedOn { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsActive { get; set; }
		public decimal AmountPaid { get; set; }

		public bool IsTrial { get; set; }

		
	}
}
