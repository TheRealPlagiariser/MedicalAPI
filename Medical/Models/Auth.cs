using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Medical.Models
{
    public class Auth
    {
        [Key]
        [Required]
        public string email { set; get; }
        [Required]
        public string password { set; get; }
    
}
}