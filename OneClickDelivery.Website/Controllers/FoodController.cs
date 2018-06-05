using OneClickDelivery.Models;
using OneClickDelivery.Website.Models;
using OneClickDelivery.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OneClickDelivery.Website.Assistant;

namespace OneClickDelivery.Website.Controllers
{
    public class FoodController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        string userId = string.Empty;

        // GET: Food/1 id for menuSection 
        public ActionResult Index(int? id)
        {
            // Check the id of the menu section 
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // check the menu section if it is existing 
            MenuSection menuSection = db.MenuSections.Find(id);
            if (menuSection == null)
                return HttpNotFound();

            FoodViewModel vm = new FoodViewModel();
            vm.MenuSection = menuSection; // Set the menu section 
            vm.Food = db.Items.OfType<Food>().Where(f => f.MenuSectionId == id).ToList(); // Get the food 

            return View(vm);
        }

        #region Create
        // GET: Food/Create/1 id for menu Section 
        public ActionResult Create(int? id)
        {
            // check the id if existing 
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Get the Menu Section 
            var menuSection = db.MenuSections.Find(id);
            if (menuSection == null)
                return HttpNotFound();

            FoodViewModel vm = new FoodViewModel();
            vm.MenuSection = menuSection; 
            return View(vm);
        }

        // POST: Food/Create/1 if for menu section 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(int id, [Bind(Include = "Name,Description,Ingredients,Size,Cost")]FoodViewModel food)
        {
            // Get the menu section 
            var menuSection = db.MenuSections.Find(id);
            food.MenuSection = menuSection; 

            if (ModelState.IsValid)
            {
                // Create a new Item 
                Food foodItem = new Food();
                foodItem.Name = food.Name;
                foodItem.Description = food.Description;
                foodItem.Ingredients = food.Ingredients;
                foodItem.Size = food.Size;
                foodItem.MenuSectionId = id;
                foodItem.Cost = food.Cost;
                foodItem.AddedDate = DateTime.Now;

                // Save the Image 
                HttpPostedFileBase imageFile = Request.Files["FoodImage"];

                // Check if the file not existing 
                if (imageFile.FileName == "")
                {
                    // Create an alert message to send to the user 
                    food.Message = new MessageViewModel { Message = "Please choose an image", MessageType = MessageType.Warning };
                    return View(food);
                }

                // check the extension of the image 
                string imageExtension = Path.GetExtension(imageFile.FileName);
                // Check the extension if it's not for image 
                if (!imageExtension.IsImageExtension())
                {
                    // Create an alert message to send to the user 
                    food.Message = new MessageViewModel { Message = "Please choose an image file with an extension .jpg, .png or .bmp", MessageType = MessageType.Warning };
                    return View(food);
                }

                // Create the image file name 
                string fileName = "Images/FoodImage-" + Guid.NewGuid() + imageExtension;

                // Save the file 
                imageFile.SaveAs(Server.MapPath("~/" + fileName));

                // Set the property of the food image 
                foodItem.ItemImage = fileName;
                foodItem.ItemImageThump = fileName;

                db.Items.Add(foodItem);
                db.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
           
            return View(food);
        }
        #endregion

        #region Edit 
        // GET: Food/Edit/10 
        public ActionResult Edit(int? id)
        {

            // Check the id 
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            
            // Get food item 
            Food food = db.Items.Find(id) as Food;

            // check the food 
            if (food == null)
                return HttpNotFound();

            // Create View Model Object 
            FoodViewModel vm = new FoodViewModel();
            vm.Name = food.Name; 
            vm.Cost = food.Cost;
            vm.Description = food.Description;
            vm.Ingredients = food.Ingredients;
            vm.MenuSection = db.MenuSections.Find(food.MenuSectionId);
            vm.Size = food.Size;
            vm.AddedDate = food.AddedDate;
            vm.PhotoPath = food.ItemImage;

            return View(vm); 
        }

        // POST: Food/Edit/10
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id,[Bind(Include = "Name,Description,Ingredients,Size,Cost")]FoodViewModel food)
        {
            // Get the food
            var foodItem = db.Items.Find(id) as Food;
            // Get the Menu Section 
            food.MenuSection = db.MenuSections.Find(foodItem.MenuSectionId); 

            if(ModelState.IsValid)
            {
                // Set the new values of the food
                foodItem.Name = food.Name;
                foodItem.Ingredients = food.Ingredients;
                foodItem.Description = food.Description;
                foodItem.Cost = food.Cost;
                foodItem.Size = food.Size;

                // Check if the a new file uploaded 
                var imageFile = Request.Files["FoodImage"]; 

                // If file uploaded 
                if(imageFile.FileName != "")
                {
                    // Check the extension of the file 
                    string extension = Path.GetExtension(imageFile.FileName); 
                    if(!extension.IsImageExtension())
                    {
                        food.Message = new MessageViewModel { Message = "Please choose an image with .jpg, .png or .bmp extension", MessageType = MessageType.Warning };
                        return View(food); 
                    }

                    // Create a new file name to the new file 
                    string fileName = "Images/FoodImage-" + Guid.NewGuid() + extension;

                    // Save the new File 
                    imageFile.SaveAs(Server.MapPath("~/" + fileName));

                    // Delete the old file 
                    System.IO.File.Delete(Server.MapPath("~/" + foodItem.ItemImage));

                    // Set the new properyt of the file 
                    foodItem.ItemImage = fileName;
                    foodItem.ItemImageThump = fileName; 
                }

                db.SaveChanges();

                return RedirectToAction("Index", new { id = foodItem.MenuSectionId }); 
            }

            return View(food); 
        }
        #endregion

        #region Delete
        // POST: Food/Delete/10
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Get the food 
            var food = db.Items.Find(id) as Food;

            // Remove the item 
            db.Items.Remove(food);
            db.SaveChanges();

            // Remove the image from the files 
            System.IO.File.Delete(Server.MapPath("~/" + food.ItemImage)); 

            return RedirectToAction("Index", new { id = food.MenuSectionId });
        }
        #endregion 

    }
}