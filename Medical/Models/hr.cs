using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medical.Models
{
    public class hr
    {
        [Key]
        public int emp_id { set; get; }

        //public string email { get; set; }

        public string password { get; set; }

    }
}