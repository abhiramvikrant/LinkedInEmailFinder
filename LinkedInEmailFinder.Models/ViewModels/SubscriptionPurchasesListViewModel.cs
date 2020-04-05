using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models.ViewModels
{
    public class SubscriptionPurchasesListViewModel
    {[Key]
		public Guid PurchaseId { get; set; }
		public string UserId { get; set; }

		public string UserName { get; set; }
		public string SubscriptionName { get; set; }
		public Guid SubscriptionUID { get; set; }
		public int PurchaseOrderId { get; set; }
		public DateTime PurchasedOn { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsActive { get; set; }
		public decimal AmountPaid { get; set; }
	
		public bool IsTrial { get; set; }
	}
}
