using Microsoft.AspNet.Identity;
using OneClickDelivery.Models;
using OneClickDelivery.Website.Models;
using OneClickDelivery.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace OneClickDelivery.Website.Controllers
{
    [Authorize(Roles = "Admin,Resturant")]
    public class MenuSectionsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        string userId = string.Empty;
        

        // GET: MenuSections
        public ActionResult Index()
        {
            // Get the user Id 
            userId = User.Identity.GetUserId();

            MenuSectionViewModel vm = new MenuSectionViewModel();
            var resturant = db.Resturants.FirstOrDefault(r => r.UserId == userId);
            vm.Resturant = resturant;
            vm.Message = new MessageViewModel { Message = "This is the page of the menu types of the resturant " + resturant.Name, MessageType = MessageType.Info }; 
            // Get the menu of the resturant Id 
            var menu = db.Menus.FirstOrDefault(m => m.Resturant.UserId == userId);
            if (menu != null)
                vm.MenuSections = db.MenuSections.Where(m => m.MenuId == menu.MenuId).ToList();

            return View(vm);
        }

        #region Create
        // GET: MenuSections/Create 
        public ActionResult Create()
        {
            // Get the user Id 
            userId = User.Identity.GetUserId();

            MenuSectionViewModel vm = new MenuSectionViewModel();
            // Get the menu of the resturant 
            var resturant = db.Resturants.FirstOrDefault(r => r.UserId == userId);
            var menu = db.Menus.FirstOrDefault(m => m.MenuId == resturant.ResturantId); 
            
            // Get the Menu Types only not in the resturant menus 
            List<MenuType> menutypes = db.MenuTypes.ToList();
            List<MenuSection> menuSections = db.MenuSections.Where(m => m.MenuId == menu.MenuId).ToList();

            var menuTypesNotInMenu = new List<MenuType>(); 
            foreach (var item in menuSections)
            {
                if (menutypes.Contains(item.MenuType))
                    menutypes.Remove(item.MenuType); 
            }

            // Create the list of the select list 
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in menutypes)
            {
                items.Add(new SelectListItem { Text = item.Name, Value = item.MenuTypeId.ToString() }); 
            }

            vm.MenuTypes = new SelectList(items, "Value", "Text");

            vm.Resturant = resturant;

            return View(vm); 
        }

        // POST: MenuSections/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "MenuTypeId")]MenuSectionViewModel menuSection)
        {
            // Get the user Id 
            userId = User.Identity.GetUserId();

            // Get the resturant and menu 
            var resturant = db.Resturants.FirstOrDefault(r => r.UserId == userId); 
            if(ModelState.IsValid)
            {
                // Create a new object of type menu section 
                MenuSection section = new MenuSection();
                section.MenuTypeId = menuSection.MenuTypeId;
                section.MenuId = resturant.ResturantId;

                db.MenuSections.Add(section);
                db.SaveChanges(); 

                return RedirectToAction("Index"); 
            }
            return View(menuSection); 
        }
        #endregion

        #region Delete

        // POST: MenuSections/Delete/10
        [HttpPost]
        public ActionResult Delete(int id)
        {
            MenuSection menuSection = db.MenuSections.Find(id);

            db.MenuSections.Remove(menuSection);
            db.SaveChanges();

            return RedirectToAction("Index"); 
        }

        #endregion



    }
}
