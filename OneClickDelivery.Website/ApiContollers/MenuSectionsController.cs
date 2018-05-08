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
    [Route("api/v1/menusections")]
    public class MenuSectionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MenuSections
        public List<MenuSection> GetMenuSections()
        {
            return db.MenuSections.ToList();
        }

        // Get: get the menu sections in a specific resturant 
        // api/v1/menusections?resturantId=10
        public List<MenuSection> GetMenuSectionsByResturant(int resturantId)
        {
            Resturant resturant = db.Resturants.Find(resturantId);

            List<MenuSection> menuSections = new List<MenuSection>(); 

            if(resturant != null)
            {
                menuSections = db.MenuSections.Where(m => m.Menu.Resturant.ResturantId == resturantId).ToList(); 
            }

            return menuSections; 
        }

        // GET: api/MenuSections/5
        [ResponseType(typeof(MenuSection))]
        public IHttpActionResult GetMenuSection(int id)
        {
            MenuSection menuSection = db.MenuSections.Find(id);
            if (menuSection == null)
            {
                return NotFound();
            }

            return Ok(menuSection);
        }

        // PUT: api/MenuSections/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMenuSection(int id, MenuSection menuSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuSection.MenuSectionId)
            {
                return BadRequest();
            }

            db.Entry(menuSection).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuSectionExists(id))
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

        // POST: api/MenuSections
        [ResponseType(typeof(MenuSection))]
        public IHttpActionResult PostMenuSection(MenuSection menuSection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuSections.Add(menuSection);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = menuSection.MenuSectionId }, menuSection);
        }

        // DELETE: api/MenuSections/5
        [ResponseType(typeof(MenuSection))]
        public IHttpActionResult DeleteMenuSection(int id)
        {
            MenuSection menuSection = db.MenuSections.Find(id);
            if (menuSection == null)
            {
                return NotFound();
            }

            db.MenuSections.Remove(menuSection);
            db.SaveChanges();

            return Ok(menuSection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuSectionExists(int id)
        {
            return db.MenuSections.Count(e => e.MenuSectionId == id) > 0;
        }
    }
}