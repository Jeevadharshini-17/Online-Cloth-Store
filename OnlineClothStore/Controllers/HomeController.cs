using OnlineClothStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineClothStore.Controllers
{
    public class HomeController : Controller
    {
        private OnlineClothStoreDBContext db = new OnlineClothStoreDBContext();
        public ActionResult Index()
        {
            //var vendorName = 
            return View(db.Product.ToList());
        }

        // GET: Products/Details/5 (Search By vendor ID)
        public ActionResult DetailsVendor(int? Vid)
        {
            if (Vid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Product.Where(p => p.VendorId == Vid);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.ToList());
        }

        // GET: Products/Details/5 ( Search By Name)
        public ActionResult DetailsName(string name)
        {
            if (name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Product.Where(p => p.ProductName.Contains(name));
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}