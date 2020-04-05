using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models
{
  
		public class Subscriptions
		{[Key]
			public Guid SubscriptionId { get; set; }
			public string SubscriptionName { get; set; }
			public decimal SubscriptionPrice { get; set; }
			public string SubscriptionDescription { get; set; }
			public bool UseDiscountPrice { get; set; }
			public decimal? SubscriptionDiscountPrice { get; set; }
			public int SubscriptionPeriodInMonths { get; set; }

		public bool IsActive { get; set; }

		public int SubscriptionDisplayOrder { get; set; }
	}
}
