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
    [Route("api/v1/menutypes")]
    public class MenuTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MenuTypes
        public List<MenuType> GetMenuTypes()
        {
            return db.MenuTypes.ToList();
        }

        // GET: api/MenuTypes/5
        [ResponseType(typeof(MenuType))]
        public IHttpActionResult GetMenuType(int id)
        {
            MenuType menuType = db.MenuTypes.Find(id);
            if (menuType == null)
            {
                return NotFound();
            }

            return Ok(menuType);
        }

        // PUT: api/MenuTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMenuType(int id, MenuType menuType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuType.MenuTypeId)
            {
                return BadRequest();
            }

            db.Entry(menuType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuTypeExists(id))
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

        // POST: api/MenuTypes
        [ResponseType(typeof(MenuType))]
        public IHttpActionResult PostMenuType(MenuType menuType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MenuTypes.Add(menuType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = menuType.MenuTypeId }, menuType);
        }

        // DELETE: api/MenuTypes/5
        [ResponseType(typeof(MenuType))]
        public IHttpActionResult DeleteMenuType(int id)
        {
            MenuType menuType = db.MenuTypes.Find(id);
            if (menuType == null)
            {
                return NotFound();
            }

            db.MenuTypes.Remove(menuType);
            db.SaveChanges();

            return Ok(menuType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MenuTypeExists(int id)
        {
            return db.MenuTypes.Count(e => e.MenuTypeId == id) > 0;
        }
    }
}