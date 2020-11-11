using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Medical.Models;

namespace Medical.Controllers
{
    public class CountController : ApiController
    {
        private MedicalContext db = new MedicalContext();

        public IHttpActionResult GetClaimsCount()
        {

            var query = (from b in db.Batches
                        join c in db.Claims
                        on b.batch_id equals c.batch_id
                        where b.batch_date_to == null
                        select c.number_of_claims).ToList();
            int sum = query.Sum();
                        //group new { c.number_of_claims } by b.batch_id into countgroup
                        //select new
                        //{
                        //    claim_count = countgroup.Sum(x => x.number_of_claims)

                        //};

            //int sum = query.Sum();

          

            if (query == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(sum);
            }
        }
    }
}
