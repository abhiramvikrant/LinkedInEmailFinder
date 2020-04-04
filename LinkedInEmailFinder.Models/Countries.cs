using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models
{
	public class Countries
	{
		public Countries()
		{

		}
		[Key]
		public int CountryID { get; set; }
		public string CountryName { get; set; }
		public string TwoCharCountryCode { get; set; }
		public string ThreeCharCountryCode { get; set; }
		public bool IsActive { get; set; }

		public Countries(int CountryID_, string CountryName_, string TwoCharCountryCode_, string ThreeCharCountryCode_, bool IsActive_)
		{
			this.CountryID = CountryID_;
			this.CountryName = CountryName_;
			this.TwoCharCountryCode = TwoCharCountryCode_;
			this.ThreeCharCountryCode = ThreeCharCountryCode_;
			this.IsActive = IsActive_;
		}
	}
}
