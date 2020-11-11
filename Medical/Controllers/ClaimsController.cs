using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Medical.Models;

namespace Medical.Controllers
{
    public class ClaimsController : ApiController
    {
        private MedicalContext db = new MedicalContext();

        // GET: api/Claims
        //public IQueryable<Claims> GetClaims()
        //{
        //    return db.Claims;
        //}
        public HttpResponseMessage GetClaims()
        {
            var temp = from c in db.Claims
                       join b in db.Batches
                       on c.batch_id equals b.batch_id
                       group new { c.claim_id, c.emp_id,c.first_name,c.last_name, c.claim_date, b.batch_id, b.batch_date_to,c.number_of_claims } by c.claim_id;

            var query = from t in temp
                        select new
                        {

                            t.FirstOrDefault().emp_id,
                            t.FirstOrDefault().claim_id,
                            t.FirstOrDefault().batch_id,
                            emp_name = t.FirstOrDefault().first_name +" "+ t.FirstOrDefault().last_name,
                            date_submit = t.FirstOrDefault().claim_date,
                            t.FirstOrDefault().batch_date_to,
                            t.FirstOrDefault().number_of_claims
                        };
            return Request.CreateResponse(HttpStatusCode.OK, query);
        }

        // GET: api/Claims/5
        [ResponseType(typeof(Claims))]
        public IHttpActionResult GetClaims(int id)
        {
            Claims claims = db.Claims.Find(id);
            if (claims == null)
            {
                return NotFound();
            }

            return Ok(claims);
        }

        // PUT: api/Claims/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClaims(int id, Claims claims)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != claims.claim_id)
            {
                return BadRequest();
            }

            db.Entry(claims).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Claims
        [ResponseType(typeof(Claims))]
        public IHttpActionResult PostClaims([FromBody] Claims claims)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Claims.Add(claims);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = claims.claim_id }, claims);
        }

        // DELETE: api/Claims/5
        [ResponseType(typeof(Claims))]
        public IHttpActionResult DeleteClaims(int id)
        {
            Claims claims = db.Claims.Find(id);
            if (claims == null)
            {
                return NotFound();
            }

            db.Claims.Remove(claims);
            db.SaveChanges();

            return Ok(claims);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClaimsExists(int id)
        {
            return db.Claims.Count(e => e.claim_id == id) > 0;
        }
    }
}