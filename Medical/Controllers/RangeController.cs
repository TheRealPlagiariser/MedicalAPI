using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medical.Models;
namespace Medical.Controllers
{
    public class RangeController : ApiController
    {
        private MedicalContext db = new MedicalContext();
        public HttpResponseMessage GetClaimsRange(DateTime dateFrom, DateTime dateTo)
        {
            var temp = from c in db.Claims
                       join b in db.Batches
                       on c.batch_id equals b.batch_id
                       where c.claim_date >= dateFrom && c.claim_date <= dateTo
                       group new { c.claim_id, c.emp_id, c.first_name, c.last_name, c.claim_date, b.batch_id, b.batch_date_to, c.number_of_claims } by c.claim_id;
            var query = from t in temp
                        select new
                        {
                            t.FirstOrDefault().emp_id,
                            t.FirstOrDefault().claim_id,
                            t.FirstOrDefault().batch_id,
                            emp_name = t.FirstOrDefault().first_name + " " + t.FirstOrDefault().last_name,
                            date_submit = t.FirstOrDefault().claim_date,
                            t.FirstOrDefault().batch_date_to,
                            t.FirstOrDefault().number_of_claims
                        };
            return Request.CreateResponse(HttpStatusCode.OK, query);
        }
    }
}
















