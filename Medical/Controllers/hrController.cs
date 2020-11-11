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
using System.Threading.Tasks;

namespace Medical.Controllers
{
    public class hrController : ApiController
    {
        private MedicalContext db = new MedicalContext();

        //GET: api/hr
        public IQueryable<hr> Gethrs()
        {
            return db.hrs;
        }

        // GET: api/hr/5
        [ResponseType(typeof(hr))]
        public IHttpActionResult Gethr(int id)
        {
            hr hr = db.hrs.Find(id);
            if (hr == null)
            {
                return NotFound();
            }

            return Ok(hr);
        }

        // PUT: api/hr/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puthr(int id, hr hr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hr.emp_id)
            {
                return BadRequest();
            }

            db.Entry(hr).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!hrExists(id))
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

        //// POST: api/hr
        //[ResponseType(typeof(hr))]
        //public IHttpActionResult Posthr(hr hr)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.hrs.Add(hr);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = hr.emp_id }, hr);
        //}

        [ResponseType(typeof(hr))]
        public async Task<IHttpActionResult> Posthr(hr hr)
        {
            var res = await GetHr(hr);
            if (res != null)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //db.hrs.Add(hr);
            //db.SaveChanges();
            //return CreatedAtRoute("DefaultApi", new { id = hr.emp_id }, hr);
        }

        // DELETE: api/hr/5
        [ResponseType(typeof(hr))]
        public IHttpActionResult Deletehr(int id)
        {
            hr hr = db.hrs.Find(id);
            if (hr == null)
            {
                return NotFound();
            }

            db.hrs.Remove(hr);
            db.SaveChanges();

            return Ok(hr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool hrExists(int id)
        {
            return db.hrs.Count(e => e.emp_id == id) > 0;
        }

        private async Task<hr> GetHr(hr hr)
        {
            return await db.hrs.FirstOrDefaultAsync(e => e.password == hr.password);
        }
    }
}