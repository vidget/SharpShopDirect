using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Postal;

namespace SharpShopDirect.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About";
            return View();
        }

        public ActionResult Collections()
        {
            ViewBag.Message = "The Collection";
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }

        public ActionResult Jackets()
        {
            ViewBag.Message = "The Jackets";
            return View();
        }

        public ActionResult Tops()
        {
            ViewBag.Message = "The Tops";
            return View();
        }

        public ActionResult Skirts()
        {
            ViewBag.Message = "The Skirts";
            return View();
        }

        public ActionResult Pants()
        {
            ViewBag.Message = "The Pants";
            return View();
        }

        public ActionResult Dresses()
        {
            ViewBag.Message = "The Dresses";
            return View();
        }

        public ActionResult Accessories()
        {
            ViewBag.Message = "The Accessories";
            return View();
        }
        public ActionResult Necessities() 
        {
            ViewBag.Message = "The Necessities";
            return View();
        }

        public ActionResult TrunkShow()
        {
            ViewBag.Message = "The Trunk Show";
            return View();
        }
    }
}