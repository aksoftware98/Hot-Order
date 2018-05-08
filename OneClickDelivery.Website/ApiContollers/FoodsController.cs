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
    [Route("api/v1/foods")]
    public class FoodsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Foods
        public List<Food> GetItems()
        {
            return db.Items.OfType<Food>().ToList();
        }

        // GET: the food in the resturant 
        // api/v1/foods?resturantid=3
        public List<Food> GetFoodInResturant(int resturantId)
        {
            Resturant resturant = db.Resturants.Find(resturantId);

            List<Food> foods = new List<Food>(); 

            if(resturant != null)
            {
                foods = db.Items.OfType<Food>().Where(p => p.MenuSection.MenuId == resturantId).ToList();  
            }

            return foods;
        }

        // GET: the food in the resturant by name 

        public List<Food> GetFoodInResturantByName(string resturantName)
        {
            List<Resturant> resturants = db.Resturants.ToList(); 
            Resturant resturant = resturants.FirstOrDefault(r => string.Equals(r.Name,resturantName,StringComparison.CurrentCultureIgnoreCase));

            List<Food> foods = new List<Food>();

            if (resturant != null)
            {
                //Menu menu = db.Menus.Find(resturant.Menu.MenuId);

                //// Check the menu if it is not null 
                //if (menu != null)
                //{
                    foods = db.Items.OfType<Food>().Where(p => p.MenuSection.MenuId == resturant.ResturantId).ToList();

                //    foreach (var group in groupedFood)
                //    {
                //        foreach (var item in group.Food)
                //        {
                //            food.Add(item);
                //        }
                //    }

                //}
            }

            return foods;
        }

        // GET: the food in the resturant by id 
        // api/v1/foods?resturantUserId=dsafdsaf
        public List<Food> GetFoodInResturantByUserId(string resturantUserId)
        {
            Resturant resturant = db.Resturants.FirstOrDefault(r => r.UserId == resturantUserId);

            List<Food> food = new List<Food>();

            if (resturant != null)
            {
                Menu menu = db.Menus.Find(resturant.Menu.MenuId);

                // Check the menu if it is not null 
                if (menu != null)
                {
                    var groupedFood = db.Items.OfType<Food>().Where(p => p.MenuSection.Menu.MenuId == menu.MenuId).GroupBy(p => p.MenuSection, (key, groups) => new { Key = key, Food = groups }).ToList();

                    foreach (var group in groupedFood)
                    {
                        foreach (var item in group.Food)
                        {
                            food.Add(item);
                        }
                    }

                }
            }

            return food;
        }

        // GET: api/Foods/5
        [ResponseType(typeof(Food))]
        public IHttpActionResult GetFood(int id)
        {
            Food food = db.Items.Find(id) as Food;
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        // PUT: api/Foods/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.ItemId)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/Foods
        [ResponseType(typeof(Food))]
        public IHttpActionResult PostFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(food);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = food.ItemId }, food);
        }

        // DELETE: api/Foods/5
        [ResponseType(typeof(Food))]
        public IHttpActionResult DeleteFood(int id)
        {
            Food food = db.Items.Find(id) as Food;
            if (food == null)
            {
                return NotFound();
            }

            db.Items.Remove(food);
            db.SaveChanges();

            return Ok(food);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(int id)
        {
            return db.Items.Count(e => e.ItemId == id) > 0;
        }
    }
}