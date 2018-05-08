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
    [Route("api/v1/statstics")]
    public class StatsticsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Statstics
        public List<Statstic> GetStatstics()
        {
            return db.Statstics.ToList();
        }

        // GET: api/Statstics/5
        [ResponseType(typeof(Statstic))]
        public IHttpActionResult GetStatstic(int id)
        {
            Statstic statstic = db.Statstics.Find(id);
            if (statstic == null)
            {
                return NotFound();
            }

            return Ok(statstic);
        }

        // PUT: api/Statstics/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStatstic(int id, Statstic statstic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statstic.StatsticId)
            {
                return BadRequest();
            }

            db.Entry(statstic).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatsticExists(id))
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

        // POST: api/Statstics
        [ResponseType(typeof(Statstic))]
        public IHttpActionResult PostStatstic(Statstic statstic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Statstics.Add(statstic);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = statstic.StatsticId }, statstic);
        }

        // DELETE: api/Statstics/5
        [ResponseType(typeof(Statstic))]
        public IHttpActionResult DeleteStatstic(int id)
        {
            Statstic statstic = db.Statstics.Find(id);
            if (statstic == null)
            {
                return NotFound();
            }

            db.Statstics.Remove(statstic);
            db.SaveChanges();

            return Ok(statstic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatsticExists(int id)
        {
            return db.Statstics.Count(e => e.StatsticId == id) > 0;
        }
    }
}