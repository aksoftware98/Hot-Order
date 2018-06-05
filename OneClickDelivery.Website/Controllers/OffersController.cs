using Microsoft.AspNet.Identity;
using OneClickDelivery.Models;
using OneClickDelivery.Website.Assistant;
using OneClickDelivery.Website.Models;
using OneClickDelivery.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OneClickDelivery.Website.Controllers
{
    public class OffersController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string userId = string.Empty;

        // GET: Offers
        public ActionResult Index()
        {
            // Get the user id 
            userId = User.Identity.GetUserId();

            // Get the resturant of the logged in user 
            var restaurant = db.Resturants.FirstOrDefault(r => r.UserId == userId);

            // Get List of Offers for this resturant 
            OfferViewModel vm = new OfferViewModel
            {
                Resturant = restaurant,
                Offers = db.Items.OfType<Offer>().Where(o => o.ResturantId == restaurant.ResturantId).OrderByDescending(o => o.StartDate).ToList()
            };

            return View(vm);
        }

        #region Create
        // GET: Offers/Create
        public ActionResult Create()
        {
            var vm = new OfferViewModel();
            vm.StartDate = DateTime.Now;
            vm.EndDate = DateTime.Now.AddDays(7);

            return View(vm);
        }

        // POST: Offers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,StartDate,EndDate,Cost,")]OfferViewModel offer)
        {
            // Get the user id 
            userId = User.Identity.GetUserId();
            // Get the resturant of the user 
            var restaurant = db.Resturants.FirstOrDefault(r => r.UserId == userId);

            if (ModelState.IsValid)
            {
                // Create a new offer item 
                var offerItem = new Offer
                {
                    Name = offer.Name,
                    Description = offer.Description,
                    Cost = offer.Cost,
                    AddedDate = DateTime.Now.ToUniversalTime(),
                    StartDate = offer.StartDate.ToUniversalTime(),
                    EndDate = offer.EndDate.ToUniversalTime(),
                    UserId = userId,
                    ResturantId = restaurant.ResturantId
                };

                // Check the start and end date 
                if (offer.EndDate.ToUniversalTime() < DateTime.Now.ToUniversalTime() || offer.EndDate.ToUniversalTime() < offer.StartDate.ToUniversalTime())
                {
                    offer.Message = new MessageViewModel { Message = "The End Date of the offer must be bigger than current date and Start Date", MessageType = MessageType.Warning };
                    return View(offer);
                }

                // Get the image file of the offer 
                HttpPostedFileBase imageFile = Request.Files["OfferImage"];

                // Check the photo if it is existing 
                if (imageFile.FileName == "")
                {
                    offer.Message = new MessageViewModel { Message = "Please choose an image to the offer", MessageType = MessageType.Warning };
                    return View(offer);
                }

                // Get the extension of the file 
                string extension = Path.GetExtension(imageFile.FileName);
                // check the extension of the image file 
                if (!extension.IsImageExtension())
                {
                    offer.Message = new MessageViewModel { Message = "Please choose an image with a .jpg, .png or .bmp extension", MessageType = MessageType.Warning };
                    return View(offer);
                }

                // Greate a file name 
                string fileName = "Images/OfferImage-" + Guid.NewGuid() + extension;

                // Save the image 
                imageFile.SaveAs(Server.MapPath("~/" + fileName));

                offerItem.ItemImage = fileName;
                offerItem.ItemImageThump = fileName;

                // Save to db 
                db.Items.Add(offerItem);
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            return View(offer);
        }
        #endregion

        #region Edit
        // GET: Offers/Edit/3
        public ActionResult Edit(int? id)
        {

            // Gheck the id 
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // Get the offer 
            var offer = db.Items.Find(id) as Offer;
            if (offer == null)
                return HttpNotFound();

            // create a view model object 
            var vm = new OfferViewModel
            {
                Name = offer.Name,
                Description = offer.Description,
                PhotoPath = offer.ItemImage,
                StartDate = offer.StartDate,
                EndDate = offer.EndDate,
                Cost = offer.Cost
            };

            return View(vm);
        }

        // POST: Offers/Edit/3
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "Name,Description,StartDate,EndDate,Cost")]OfferViewModel offer)
        {
            // Get the offer 
            var offerItem = db.Items.Find(id) as Offer;

            if (ModelState.IsValid)
            {
                // Check the end date
                if (offer.EndDate.ToUniversalTime() < DateTime.Now.ToUniversalTime() || offer.EndDate.ToUniversalTime() < offer.StartDate.ToUniversalTime())
                {
                    offer.Message = new MessageViewModel { Message = "The End Date of the offer must be bigger than the current date and Start Date", MessageType = MessageType.Warning };
                    return View(offer);
                }

                // Check and get the image file
                HttpPostedFileBase imageFile = Request.Files["OfferImage"];
                if (imageFile.FileName != "")
                {
                    // Get the extension of the image 
                    string extension = Path.GetExtension(imageFile.FileName);
                    if (!extension.IsImageExtension())
                    {
                        offer.Message = new MessageViewModel { Message = "Please select an image file with the .jpg, .png or .bmp extension", MessageType = MessageType.Error };
                        return View(offer);
                    }

                    // Create a file name and save the new file 
                    string fileName = "Images/OfferImage-" + Guid.NewGuid() + extension;

                    // Save the file 
                    imageFile.SaveAs(Server.MapPath("~/" + fileName));

                    // Remove the old file 
                    System.IO.File.Delete(Server.MapPath("~/" + offerItem.ItemImage));

                    // Set the new property of the item image 
                    offerItem.ItemImage = fileName;
                    offerItem.ItemImageThump = fileName;
                }

                // Set other properties 
                offerItem.Name = offer.Name;
                offerItem.Description = offer.Description;
                offerItem.StartDate = offer.StartDate;
                offerItem.EndDate = offer.EndDate;
                offerItem.Cost = offer.Cost;

                // Save the changes 
                db.SaveChanges();

                return RedirectToAction("Index");


            }
            return View(offer);
        }

        #endregion

        #region Delete
        // POST: Offers/Delete/23
        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Get the item 
            var offer = db.Items.Find(id) as Offer;

            // Remove from the database 
            db.Items.Remove(offer);
            db.SaveChanges();

            // Remove the image file 
            System.IO.File.Delete(Server.MapPath("~/" + offer.ItemImage));

            return RedirectToAction("Index");
        }
        #endregion
    }
}