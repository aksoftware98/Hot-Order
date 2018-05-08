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
    [Route("api/v1/AppVersions")]
    public class AppVersionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AppVersions
        public List<AppVersion> GetAppVersions()
        {
            return db.AppVersions.ToList();
        }

        // GET: api/AppVersions/5
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult GetAppVersion(int id)
        {
            AppVersion appVersion = db.AppVersions.Find(id);
            if (appVersion == null)
            {
                return NotFound();
            }

            return Ok(appVersion);
        }

        // PUT: api/AppVersions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAppVersion(int id, AppVersion appVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appVersion.AppVersionId)
            {
                return BadRequest();
            }

            db.Entry(appVersion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppVersionExists(id))
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

        // POST: api/AppVersions
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult PostAppVersion(AppVersion appVersion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AppVersions.Add(appVersion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = appVersion.AppVersionId }, appVersion);
        }

        // DELETE: api/AppVersions/5
        [ResponseType(typeof(AppVersion))]
        public IHttpActionResult DeleteAppVersion(int id)
        {
            AppVersion appVersion = db.AppVersions.Find(id);
            if (appVersion == null)
            {
                return NotFound();
            }

            db.AppVersions.Remove(appVersion);
            db.SaveChanges();

            return Ok(appVersion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AppVersionExists(int id)
        {
            return db.AppVersions.Count(e => e.AppVersionId == id) > 0;
        }
    }
}