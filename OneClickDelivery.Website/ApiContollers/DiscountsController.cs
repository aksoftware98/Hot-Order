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
    [Route("api/v1/discounts")]
    public class DiscountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Discounts
        public List<Discount> GetDiscounts()
        {
            return db.Discounts.ToList();
        }

        // GET: discounts of food that are stell available 
        // api/v1/discounts?foodid=10
        [ResponseType(typeof(Discount))]
        public IHttpActionResult GetFoodDiscount(int foodId)
        {
            Food food = db.Items.Find(foodId) as Food;

            if (food == null)
                return NotFound(); 

            Discount discount = food.Discounts.FirstOrDefault(d => d.EndDate >= DateTime.Now && d.StartDate <= DateTime.Now);

            if (discount == null)
                return NotFound(); 

            return Ok(discount); 
        }

        // GET: api/Discounts/5
        [ResponseType(typeof(Discount))]
        public IHttpActionResult GetDiscount(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }

            return Ok(discount);
        }

        // PUT: api/Discounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDiscount(int id, Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != discount.DiscountId)
            {
                return BadRequest();
            }

            db.Entry(discount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiscountExists(id))
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

        // POST: api/Discounts
        [ResponseType(typeof(Discount))]
        public IHttpActionResult PostDiscount(Discount discount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Discounts.Add(discount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = discount.DiscountId }, discount);
        }

        // DELETE: api/Discounts/5
        [ResponseType(typeof(Discount))]
        public IHttpActionResult DeleteDiscount(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return NotFound();
            }

            db.Discounts.Remove(discount);
            db.SaveChanges();

            return Ok(discount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscountExists(int id)
        {
            return db.Discounts.Count(e => e.DiscountId == id) > 0;
        }
    }
}