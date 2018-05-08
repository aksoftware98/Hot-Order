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
    [Route("api/v1/faq")]
    public class FaqsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Faqs
        public List<Faq> GetFAQs()
        {
            return db.FAQs.ToList();
        }

        // GET: api/Faqs/5
        [ResponseType(typeof(Faq))]
        public IHttpActionResult GetFaq(int id)
        {
            Faq faq = db.FAQs.Find(id);
            if (faq == null)
            {
                return NotFound();
            }

            return Ok(faq);
        }

        // PUT: api/Faqs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFaq(int id, Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faq.FaqId)
            {
                return BadRequest();
            }

            db.Entry(faq).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaqExists(id))
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

        // POST: api/Faqs
        [ResponseType(typeof(Faq))]
        public IHttpActionResult PostFaq(Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FAQs.Add(faq);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = faq.FaqId }, faq);
        }

        // DELETE: api/Faqs/5
        [ResponseType(typeof(Faq))]
        public IHttpActionResult DeleteFaq(int id)
        {
            Faq faq = db.FAQs.Find(id);
            if (faq == null)
            {
                return NotFound();
            }

            db.FAQs.Remove(faq);
            db.SaveChanges();

            return Ok(faq);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FaqExists(int id)
        {
            return db.FAQs.Count(e => e.FaqId == id) > 0;
        }
    }
}