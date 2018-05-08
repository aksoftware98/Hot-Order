﻿using System;
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
    [Route("api/v1/shoppingcarts")]
    public class ShoppingCartsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ShoppingCarts
        public List<ShoppingCart> GetShoppingCarts()
        {
            return db.ShoppingCarts.ToList();
        }

        // Get: the shoppingCarts of a specific user 
        // api/v1/shoppingCarts?customerId=1
        public List<ShoppingCart> GetShoppingCartsByCustomerUser(int customerId)
        {
            Customer customer = db.Customers.Find(customerId);

            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

            if (customer != null)
            {
                shoppingCarts = db.ShoppingCarts.Where(o => o.CustomerId == customerId).ToList();
            }

            return shoppingCarts;
        }

        // GET: the shoppingCarts of a specific resturant 
        // api/v1/shoppingCarts?resturantId=12
        public List<ShoppingCart> GetShoppingCartsByResturantUser(int resturantId)
        {
            Resturant resturant = db.Resturants.Find(resturantId);

            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();

            if (resturant != null)
            {
                shoppingCarts = db.ShoppingCarts.Where(o => o.ResturantId == resturantId).ToList();
            }

            return shoppingCarts;
        }

        // GET: api/ShoppingCarts/5
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult GetShoppingCart(int id)
        {
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            return Ok(shoppingCart);
        }

        // PUT: api/ShoppingCarts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutShoppingCart(int id, ShoppingCart shoppingCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoppingCart.ShoppingCartId)
            {
                return BadRequest();
            }

            db.Entry(shoppingCart).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoppingCartExists(id))
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

        // POST: api/ShoppingCarts
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult PostShoppingCart(ShoppingCart shoppingCart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ShoppingCarts.Add(shoppingCart);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shoppingCart.ShoppingCartId }, shoppingCart);
        }

        // DELETE: api/ShoppingCarts/5
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult DeleteShoppingCart(int id)
        {
            ShoppingCart shoppingCart = db.ShoppingCarts.Find(id);
            if (shoppingCart == null)
            {
                return NotFound();
            }

            db.ShoppingCarts.Remove(shoppingCart);
            db.SaveChanges();

            return Ok(shoppingCart);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShoppingCartExists(int id)
        {
            return db.ShoppingCarts.Count(e => e.ShoppingCartId == id) > 0;
        }
    }
}