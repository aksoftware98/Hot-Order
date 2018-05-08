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
    [Route("api/v1/offers")]
    public class OffersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Offers
        public List<Offer> GetItems()
        {
            return db.Items.OfType<Offer>().ToList();
        }


        // GET: Get Offers by Resturant 
        // api/v1/offers?resturantId = 1
        public List<Offer> GetOffersOfResturant(int resturantId)
        {
            Resturant resturant = db.Resturants.Find(resturantId);

            List<Offer> offers = new List<Offer>(); 

            if (resturant != null)
            {
                offers = db.Items.OfType<Offer>().Where(o => o.ResturantId == resturant.ResturantId).ToList(); 
            }

            return offers; 
        }

        // GET: api/Offers/5
        [ResponseType(typeof(Offer))]
        public IHttpActionResult GetOffer(int id)
        {
            Offer offer = db.Items.Find(id) as Offer;
            if (offer == null)
            {
                return NotFound();
            }

            return Ok(offer);
        }

        // PUT: api/Offers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOffer(int id, Offer offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != offer.ItemId)
            {
                return BadRequest();
            }

            db.Entry(offer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(id))
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

        // POST: api/Offers
        [ResponseType(typeof(Offer))]
        public IHttpActionResult PostOffer(Offer offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(offer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = offer.ItemId }, offer);
        }

        // DELETE: api/Offers/5
        [ResponseType(typeof(Offer))]
        public IHttpActionResult DeleteOffer(int id)
        {
            Offer offer = db.Items.Find(id) as Offer;
            if (offer == null)
            {
                return NotFound();
            }

            db.Items.Remove(offer);
            db.SaveChanges();

            return Ok(offer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfferExists(int id)
        {
            return db.Items.Count(e => e.ItemId == id) > 0;
        }
    }
}