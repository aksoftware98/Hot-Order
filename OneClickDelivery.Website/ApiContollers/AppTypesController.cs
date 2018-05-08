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
    [Route("api/v1/AppTypes")]
    public class AppTypesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppTypes
        public List<AppType> GetAppTypes()
        {
            return db.AppTypes.ToList();
        }

        // GET: api/AppTypes/5
        [ResponseType(typeof(AppType))]
        public IHttpActionResult GetAppType(int id)
        {
            AppType appType = db.AppTypes.Find(id);
            if (appType == null)
            {
                return NotFound();
            }

            return Ok(appType);
        }

        // PUT: api/AppTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppType(int id, AppType appType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appType.AppTypeId)
            {
                return BadRequest();
            }

            db.Entry(appType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppTypeExists(id))
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

        // POST: api/AppTypes
        [ResponseType(typeof(AppType))]
        public IHttpActionResult PostAppType(AppType appType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppTypes.Add(appType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appType.AppTypeId }, appType);
        }

        // DELETE: api/AppTypes/5
        [ResponseType(typeof(AppType))]
        public IHttpActionResult DeleteAppType(int id)
        {
            AppType appType = db.AppTypes.Find(id);
            if (appType == null)
            {
                return NotFound();
            }

            db.AppTypes.Remove(appType);
            db.SaveChanges();

            return Ok(appType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppTypeExists(int id)
        {
            return db.AppTypes.Count(e => e.AppTypeId == id) > 0;
        }
    }
}