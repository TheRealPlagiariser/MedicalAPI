using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medical.Models
{
    public class Claims
    {
        [Key]
        public int claim_id { get; set; }

        public string emp_id { get; set; }

        public string  first_name { get; set; }

        public string last_name { get; set; }

        public int number_of_claims { get; set; }

        public DateTime claim_date { get; set; }


        public Batch Batches{ get; set; }
        public int batch_id { get; set; }
       // [ForeignKey("batch_id")]

        
        //[ForeignKey("emp_id")]

    }
}