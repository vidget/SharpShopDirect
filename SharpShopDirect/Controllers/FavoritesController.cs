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

namespace SharpShopDirect.Controllers
{
    public class FavoritesController : Controller
    {
        private FashionContext db = new FashionContext();

        


        // GET: Favorites
         [AllowAnonymous]
        public ActionResult Index()
        {


            return View(db.Favorites.ToList());
        }


        
        public ActionResult MyFavorite()
        {
             
            //creates a list of favorite
            var GenreLst = new List<Favorite>();

            //get's the string ID of the current Logged in User
            string currentUser = User.Identity.GetUserId();

            //returns the favorites where the UserID matches the logged in user
            var GenreQry = from d in db.Favorites

                           where d.UserId == currentUser
                           select d;

            //adds the object to the List
            GenreLst.AddRange(GenreQry);


            //ItemList.AddRange(ItemList);


            return View(GenreQry.ToList());
        }





        // GET: Favorites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: Favorites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Favorites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriteId,UserId,ItemId")] Favorite favorite)
        {

            favorite.ItemId = 1;
            favorite.UserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Favorites.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(favorite);
        }




        // GET: Favorites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FavoriteId,UserId,ItemId,ItemType,Collection,ItemName,ItemNumber,Price,Description,Color,Image")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(favorite);
        }

        // GET: Favorites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: Favorites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
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
