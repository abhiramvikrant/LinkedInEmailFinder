using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LinkedInEmailFinder.Models
{
   public class Config
    {
        [Key]
        public int ConfigId { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
    }
}
