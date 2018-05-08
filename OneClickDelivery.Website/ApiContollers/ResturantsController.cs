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
using OneClickDelivery.Models;
using OneClickDelivery.Website.Models;

namespace OneClickDelivery.Website.ApiContollers
{
    [Route("api/v1/resturants")]
    public class ResturantsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Resturants
        public List<Resturant> GetResturants()
        {
            return db.Resturants.ToList();
        }

        // GET: api/Resturants/5
        [ResponseType(typeof(Resturant))]
        public IHttpActionResult GetResturant(int id)
        {
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return NotFound();
            }

            return Ok(resturant);
        }

        // PUT: api/Resturants/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResturant(int id, Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resturant.ResturantId)
            {
                return BadRequest();
            }

            db.Entry(resturant).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResturantExists(id))
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

        // POST: api/Resturants
        [ResponseType(typeof(Resturant))]
        public IHttpActionResult PostResturant(Resturant resturant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Resturants.Add(resturant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = resturant.ResturantId }, resturant);
        }

        // DELETE: api/Resturants/5
        [ResponseType(typeof(Resturant))]
        public IHttpActionResult DeleteResturant(int id)
        {
            Resturant resturant = db.Resturants.Find(id);
            if (resturant == null)
            {
                return NotFound();
            }

            db.Resturants.Remove(resturant);
            db.SaveChanges();

            return Ok(resturant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResturantExists(int id)
        {
            return db.Resturants.Count(e => e.ResturantId == id) > 0;
        }
    }
}