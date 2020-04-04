using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models
{
	public class States
	{
		public States()
		{

		}
		[Key]
		public int StateId { get; set; }
		public string StateName { get; set; }
		public string StateCode { get; set; }
		public int CountryId { get; set; }
		public bool IsActive { get; set; }

		public States(int StateId_, string StateName_, string StateCode_, int CountryId_, bool IsActive_)
		{
			this.StateId = StateId_;
			this.StateName = StateName_;
			this.StateCode = StateCode_;
			this.CountryId = CountryId_;
			this.IsActive = IsActive_;
		}
	}
}
