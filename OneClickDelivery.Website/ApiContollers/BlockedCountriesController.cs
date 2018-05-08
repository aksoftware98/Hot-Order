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

namespace OneClickDelivery.Website.ApiControllers
{
    [Route("api/v1/BlockedCountries")]
    public class BlockedCountriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BlockedCountries
        public List<BlockedCountry> GetBlockedCountries()
        {
            return db.BlockedCountries.ToList();
        }

        // GET: api/BlockedCountries/5
        [ResponseType(typeof(BlockedCountry))]
        public IHttpActionResult GetBlockedCountry(int id)
        {
            BlockedCountry blockedCountry = db.BlockedCountries.Find(id);
            if (blockedCountry == null)
            {
                return NotFound();
            }

            return Ok(blockedCountry);
        }

        // PUT: api/BlockedCountries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBlockedCountry(int id, BlockedCountry blockedCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blockedCountry.BlockedCountryId)
            {
                return BadRequest();
            }

            db.Entry(blockedCountry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlockedCountryExists(id))
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

        // POST: api/BlockedCountries
        [ResponseType(typeof(BlockedCountry))]
        public IHttpActionResult PostBlockedCountry(BlockedCountry blockedCountry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BlockedCountries.Add(blockedCountry);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = blockedCountry.BlockedCountryId }, blockedCountry);
        }

        // DELETE: api/BlockedCountries/5
        [ResponseType(typeof(BlockedCountry))]
        public IHttpActionResult DeleteBlockedCountry(int id)
        {
            BlockedCountry blockedCountry = db.BlockedCountries.Find(id);
            if (blockedCountry == null)
            {
                return NotFound();
            }

            db.BlockedCountries.Remove(blockedCountry);
            db.SaveChanges();

            return Ok(blockedCountry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlockedCountryExists(int id)
        {
            return db.BlockedCountries.Count(e => e.BlockedCountryId == id) > 0;
        }
    }
}