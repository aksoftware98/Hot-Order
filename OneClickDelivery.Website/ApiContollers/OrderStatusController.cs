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
    [Route("api/v1/ordercases")]
    public class OrderStatusController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/OrderStatus
        public List<OrderStatus> GetOrderStatus()
        {
            return db.OrderStatus.ToList();
        }

        // GET: api/OrderStatus/5
        [ResponseType(typeof(OrderStatus))]
        public IHttpActionResult GetOrderStatus(int id)
        {
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(orderStatus);
        }

        // PUT: api/OrderStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderStatus(int id, OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderStatus.OrderStatusId)
            {
                return BadRequest();
            }

            db.Entry(orderStatus).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusExists(id))
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

        // POST: api/OrderStatus
        [ResponseType(typeof(OrderStatus))]
        public IHttpActionResult PostOrderStatus(OrderStatus orderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderStatus.Add(orderStatus);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orderStatus.OrderStatusId }, orderStatus);
        }

        // DELETE: api/OrderStatus/5
        [ResponseType(typeof(OrderStatus))]
        public IHttpActionResult DeleteOrderStatus(int id)
        {
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return NotFound();
            }

            db.OrderStatus.Remove(orderStatus);
            db.SaveChanges();

            return Ok(orderStatus);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderStatusExists(int id)
        {
            return db.OrderStatus.Count(e => e.OrderStatusId == id) > 0;
        }
    }
}