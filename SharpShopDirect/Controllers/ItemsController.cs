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
            

            //creates a list of favorite
           // var ItemLst = new List<Item>();
           // var FavList = new List<int>();

            //get's the string ID of the current Logged in User
            string currentUser = User.Identity.GetUserId();

            //returns the favorites where the UserID matches the logged in user
            var GenreQry = from d in db.Favorites

                           where d.UserId == currentUser

                           select d;

            ////adds the object to the List
            //FavList.AddRange(GenreQry);


            //foreach(int itemId in FavList)
            //{
            //                   var ItemList = from e in db.Items
            //                                    where e.ItemId == itemId
            //                                    select e;
            //                   ItemLst.AddRange(ItemList); 

            //}

            ////ItemList.AddRange(ItemList);


            return View(GenreQry.ToList());










            
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



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Details([Bind(Include = "FavoriteId,UserId,ItemId")] Favorite favorite)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Favorites.Add(favorite);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(favorite);
        //}
         
         


        //POST Details
        [HttpPost]
        [ActionName("Details")]
        public ActionResult buttonClick(int? itemId)
        {
            if (User.Identity.IsAuthenticated)
            {
                //Get current user's id
                //var idid = User.Identity.GetUserId();

                //Find current event and current user
                //Favorite userFavorite = db.Favorites.Single(u => u.UserId == idid);


                //SMTP Email set up
                MailMessage msg = new MailMessage("sharpshopdirect@gmail.com", "marya@sharpshop.direct");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("sharpshopdirect@gmail.com", "Lover234");



                //if (userFavorite.ItemId == itemId)
                //{
                //    //Remove favorite from user
                //    @userFavorite.ItemId = 0;
                //    @userFavorite.UserId = null;

                //    //Change button to join event
                //    ViewBag.Here = "FAVORITES";

                //    //Save database changes
                //    db.SaveChanges();

                //    //Send confirmation email
                    msg.Subject = @User.Identity.GetUserName() + " want to purchase";
                    msg.Body = "Hello, " + @User.Identity.GetUserName() + "\n\n Whats to purchase\n\nThank You, \n SharpShop.Direct";
                    client.Send(msg);
                //}
                //else
                //{
                    //Add user to favorite

                //    @userFavorite.UserId = "4f132162-59ec-415d-9ca4-8cec8ba0e7ef";
                //    @userFavorite.ItemId = itemId.GetValueOrDefault();

                //    //Change button to leave event
                //    ViewBag.Here = "Remove";

                //    //Save database changes
                //    db.SaveChanges();

                //    //Send confirmation email
                //    msg.Subject = @User.Identity.GetUserName() + " added to favorites";
                //    msg.Body = "Hello, " + @User.Identity.GetUserName() + "\n\n " + @userFavorite.ItemId + "No longer in favorites\n\nThank You, \n SharpShop.Direct";
                //    client.Send(msg);
                //}


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
