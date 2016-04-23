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

        
        [Authorize]

        // GET: Favorites
        [Authorize(Users = "marya194@hotmail.com")]
        public ActionResult Index(string userID,string searchString) 
        {
            var GenreLst = new List<string>();

            //Searchs through the list of favorites to find all the userId's
            var GenreQry = from d in db.Favorites
                           orderby d.FavoriteId
                           select d.UserId;

            //Add only unique users to the 
            GenreLst.AddRange(GenreQry.Distinct());
            ViewBag.UserId = new SelectList(GenreLst);

            var favoriteList = from m in db.Favorites
                      select m;

            //USED with a SEARCH BOX
            //if (!String.IsNullOrEmpty(searchString))
            //{

            //    car = car.Where(s => s.UserId.Contains(searchString));
            //}

            if (!String.IsNullOrEmpty(userID))
            {

                favoriteList = favoriteList.Where(x => x.UserId == userID);
            }
             
            return View(favoriteList);

        }
        [Authorize]
        public ActionResult MyFavorite()
        {
             
            //creates a list of favorite
            var GenreLst = new List<Favorite>();

            //get's the string ID of the current Logged in User
            string currentUser = User.Identity.Name;

            //returns the favorites where the UserID matches the logged in user
            var GenreQry = from d in db.Favorites

                           where d.UserId == currentUser
                           select d;

            //adds the object to the List
            GenreLst.AddRange(GenreQry);


            return View(GenreQry.ToList());
        }


        // GET: Favorites/Details/5
       [Authorize]
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

        [Authorize(Users = "marya194@hotmail.com")]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Favorites/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
            db.SaveChanges();
            return RedirectToAction("MyFavorite", "Favorites");

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
