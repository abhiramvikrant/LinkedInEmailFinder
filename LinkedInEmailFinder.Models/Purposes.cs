using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models
{
	public class Purposes
	{
		public Purposes()
		{

		}
		[Key]
		public int PurposeId { get; set; }
		public string Purpose { get; set; }
		public bool IsActive { get; set; }

		public Purposes(int PurposeId_, string Purpose_, bool IsActive_)
		{
			this.PurposeId = PurposeId_;
			this.Purpose = Purpose_;
			this.IsActive = IsActive_;
		}
	}
}
