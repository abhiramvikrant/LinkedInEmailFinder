using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models.ViewModels
{
   public  class SubscriptionCreateViewModel
    {
		
		[Required]
		[Display(Name = "Subscription Name")]
		public string SubscriptionName { get; set; }
		[Required]
		[Display(Name = "Subscription Price")]
		public decimal SubscriptionPrice { get; set; }
		[Required]
		[Display(Name = "Subscription Description")]
		public string SubscriptionDescription { get; set; }
		[Required]
		[Display(Name = "Use Discount")]
		public bool UseDiscountPrice { get; set; } = false;
		public decimal? SubscriptionDiscountPrice { get; set; } = 0;
		[Required]
		[Display(Name = "Subscription Period in Months")]
		public int SubscriptionPeriodInMonths { get; set; }

		public bool? IsActive { get; set; }


	}
}
