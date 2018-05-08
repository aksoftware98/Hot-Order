using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OneClickDelivery.Models;
using OneClickDelivery.Website.Models;
using OneClickDelivery.Website.ViewModels;

namespace OneClickDelivery.Website.Controllers
{
    public class MenuSectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MenuSections
        public ActionResult Index()
        {
            var menuSections = db.MenuSections.Include(m => m.Menu).Include(m => m.MenuType);
            return View(menuSections.ToList());
        }

        // GET: MenuSections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuSection menuSection = db.MenuSections.Find(id);
            if (menuSection == null)
            {
                return HttpNotFound();
            }

            MenuSectionViewModel vm = new MenuSectionViewModel();
            vm.MenuSection = menuSection;
            vm.Food = db.Items.OfType<Food>().Where(f => f.MenuSectionId == id).ToList(); 

            return View(vm);
        }

        // GET: MenuSections/Create
        public ActionResult Create()
        {
            // Get the menu types and send them to the User Interface 
            MenuSectionViewModel vm = new MenuSectionViewModel();
            vm.MenuTypes = db.MenuTypes.ToList(); 

            return View(vm);
        }

        // POST: MenuSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuSection menuSection)
        {
            return View(menuSection);
        }

        // GET: MenuSections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuSection menuSection = db.MenuSections.Find(id);
            if (menuSection == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "MenuImage", menuSection.MenuId);
            ViewBag.MenuTypeId = new SelectList(db.MenuTypes, "MenuTypeId", "Name", menuSection.MenuTypeId);
            return View(menuSection);
        }

        // POST: MenuSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MenuSectionId,MenuTypeId,MenuId")] MenuSection menuSection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuSection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.Menus, "MenuId", "MenuImage", menuSection.MenuId);
            ViewBag.MenuTypeId = new SelectList(db.MenuTypes, "MenuTypeId", "Name", menuSection.MenuTypeId);
            return View(menuSection);
        }

        // GET: MenuSections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuSection menuSection = db.MenuSections.Find(id);
            if (menuSection == null)
            {
                return HttpNotFound();
            }
            return View(menuSection);
        }

        // POST: MenuSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuSection menuSection = db.MenuSections.Find(id);
            db.MenuSections.Remove(menuSection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
