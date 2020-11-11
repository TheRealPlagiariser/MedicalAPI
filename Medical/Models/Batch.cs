using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Medical.Models
{
    public class Batch
    {
        [Key]
        public int batch_id { get; set; }

        public DateTime? batch_date_from { get; set; }
        public DateTime? batch_date_to { get; set; }
       
       

        //public int batch_claims_qty { get; set; }

        //public ICollection<Messenger> Messengers { get; set; }
       
    }
}