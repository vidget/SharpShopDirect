using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SharpShopDirect.Context;
using SharpShopDirect.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;


namespace SharpShopDirect.Controllers
{
    public class ItemsController : Controller
    {
        private FashionContext db = new FashionContext();
        private FashionContext db2 = new FashionContext();

        // GET: Items
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

        public ActionResult MyItems() 
        {
            //get's the string ID of the current Logged in User
            string currentUser = User.Identity.GetUserId();

            //returns the favorites where the UserID matches the logged in user
            var GenreQry = from d in db.Favorites
                           where d.UserId == currentUser
                           select d;
            return View(GenreQry.ToList());
        }


        [HttpPost]
        [ActionName("About")]
        public ActionResult buttonClick2(int itemId, string itemType, string collection, string itemName, string description, int itemNumber, decimal price, string color, string itemURL)
        { 
            Favorite favorite = new Favorite();

            if (User.Identity.IsAuthenticated)
            {
                favorite.ItemId = itemId;
                favorite.ItemType = itemType;
                favorite.Collection = collection;
                favorite.ItemName = itemName;
                favorite.ItemNumber = itemNumber;
                favorite.Price = price;
                favorite.Description = description;
                favorite.Color = color;
                favorite.Image = itemURL;
                favorite.UserId = User.Identity.GetUserId();

                if (ModelState.IsValid)
                {
                    db.Favorites.Add(favorite);
                    db.SaveChanges();
                    //Redirects back to Users Favorites after added
                    return RedirectToAction("MyFavorite", "Favorites");
                }

                return View("Details", "Edit", new { itemId });
            }
            else
                return View();
        }

        public ActionResult About(string itemType, string searchString)
        {
            var GenreLst = new List<string>();

            var GenreQry = from d in db.Items
                           orderby d.ItemId
                           select d.ItemType;

            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.itemType = new SelectList(GenreLst);

            var car = from m in db.Items
                      select m;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                car = car.Where(s => s.ItemType.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(itemType))
            {
                car = car.Where(x => x.ItemType == itemType);
            }
            return View(car);
        }

        
        //POST Details
        [HttpPost]
        [ActionName("Details")]
        public ActionResult buttonClick(int itemId, string itemType, string collection, string itemName,string description,int itemNumber,decimal price,string color, string itemURL )
        {
            Favorite favorite = new Favorite();

            if (User.Identity.IsAuthenticated)
            {
                favorite.ItemId = itemId;
                favorite.ItemType = itemType;
                favorite.Collection = collection;
                favorite.ItemName = itemName;
                favorite.ItemNumber = itemNumber;
                favorite.Price = price;
                favorite.Description = description;
                favorite.Color = color;
                favorite.Image = itemURL;
                favorite.UserId = User.Identity.GetUserId();

                if (ModelState.IsValid)
                {
                    db.Favorites.Add(favorite);
                    db.SaveChanges();
                    return RedirectToAction("MyFavorite","Favorites");
                }

                return View("Details", "Edit", new { itemId });
            }
            else
                return View();
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ItemType,Collection,ItemName,ItemNumber,Price,Description,Color,Image")] Item item)
        {
           
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ItemType,Collection,ItemName,ItemNumber,Price,Description,Color,Image")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
