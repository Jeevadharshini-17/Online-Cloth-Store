using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualBasic.ApplicationServices;
using OnlineClothStore.Models;

namespace OnlineClothStore.Controllers
{
    public class ProductsController : Controller
    {
        private OnlineClothStoreDBContext db = new OnlineClothStoreDBContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Product.ToList());
        }
        public ActionResult ProductsbyVendorId(string email)
        {

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int VendorId = (int)db.Vendor.Where(c => c.VendorEmail == email).Select(c => c.VendorId).FirstOrDefault();

            IQueryable<Product> product = db.Product.Where(p => p.VendorId==VendorId);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.ToList());
        }
        public ActionResult ProductsbyVendorId_Var(int id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IQueryable<Product> product = db.Product.Where(p => p.VendorId == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.ToList());
        }

        public ActionResult UpdateOrderDetails(Product p)
        {
            Order order = new Order();
            {
                order.VendorId = p.VendorId;
                order.ProductId = p.ProductId;
                order.OrderStatus = "Pending";
                order.OrderDate = Convert.ToDateTime(DateTime.Now);
            };
            TempData["orderDetails"] = order;
            TempData["CurrentOrderProduct"] = p;
            return RedirectToAction("Create", "Orders");

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
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
        // GET: Products/Details/5 ( Search By CategoryName)
        public ActionResult DetailsCategory(string category)
        {
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Product.Where(p => p.CategoryName.Contains(category));
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product.ToList());
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase file, Product product)
        {
            string VenEmail = Convert.ToString(Request["VendorEmail"].ToString());
            product.VendorId = (int)db.Vendor.Where(v => v.VendorEmail == VenEmail).Select(c => c.VendorId).FirstOrDefault();
            string filename = Path.GetFileName(file.FileName);
            string _filenameWtDate = DateTime.Now.ToString("yymmssfff") + filename;
            string extention = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/ProductImages/"), _filenameWtDate);

            product.ProductImage = "~/ProductImages/" + _filenameWtDate;
            if (ModelState.IsValid)
            {
                file.SaveAs(path);
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("ProductsbyVendorId_Var", new { id = product.VendorId });
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            TempData["ProductImagePath"] = product.ProductImage;
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase file, [Bind(Include = "ProductId,ProductName,VendorId,ProductQuantity,ProductPrice,ProductImage,CategoryName")] Product product)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string _filenameWtDate = DateTime.Now.ToString("yymmssfff") + filename;
                string extention = Path.GetExtension(file.FileName);
                string path = Path.Combine(Server.MapPath("~/ProductImages/"), _filenameWtDate);

                product.ProductImage = "~/ProductImages/" + _filenameWtDate;
                file.SaveAs(path);

            }
            else
            {
                product.ProductImage = (string)TempData["ProductImagePath"];
            }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProductsbyVendorId_Var", new { id = product.VendorId });

            }
            return View(product);
        }
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            int vendorId = product.VendorId;
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductsbyVendorId_Var", new { id = vendorId });
   
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
