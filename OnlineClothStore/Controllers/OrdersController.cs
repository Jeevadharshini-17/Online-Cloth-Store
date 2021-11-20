using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineClothStore.Models;

namespace OnlineClothStore.Controllers
{
    public class OrdersController : Controller
    {
        private OnlineClothStoreDBContext db = new OnlineClothStoreDBContext();
        // GET: Orders
        public ActionResult Index()
        {
            return View(db.Order.ToList());
        }


        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        public ActionResult CustomerHistory(string email)
        {

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int CustomerId = (int)db.Customer.Where(c => c.CustomerEmail == email).Select(c => c.CustomerId).FirstOrDefault();

            IQueryable<Order> order = db.Order.Where(o => o.CustomerId == CustomerId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order.ToList());
        }
        public ActionResult VendorHistory(string email)
        {

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int VendorId = (int)db.Vendor.Where(v => v.VendorEmail == email).Select(v => v.VendorId).FirstOrDefault();

            IQueryable<Order> order = db.Order.Where(o => o.VendorId == VendorId);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order.ToList());
        }
        public ActionResult VendorPendingOrders(string email)
        {

            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int VendorId = (int)db.Vendor.Where(v => v.VendorEmail == email).Select(v => v.VendorId).FirstOrDefault();

            IQueryable<Order> order = db.Order.Where(o => (o.VendorId == VendorId&&o.OrderStatus=="Pending"));
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order.ToList());
        }
        public ActionResult OrdersByVendorId(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int VendorId = (int)db.Vendor.Where(v => v.VendorEmail == email).Select(v => v.VendorId).FirstOrDefault();

            IQueryable<Order> order = db.Order.Where(o => (o.VendorId == VendorId));
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order.ToList());
        }

        // GET: Orders/Create
        [Authorize]
        public ActionResult Create()
        {
            
            Order order = TempData["orderDetails"] as Order;
            TempData["ToPassOrderDetails"] = order;
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "OrderId,CustomerId,VendorId,ProductId,ProductQuantity,OrderStatus,OrderDate,OrderTotal")] Order order)
        {
            string CusEmail=Convert.ToString(Request["CustomerEmail"].ToString());
            order.CustomerId = (int)db.Customer.Where(c => c.CustomerEmail == CusEmail).Select(c => c.CustomerId).FirstOrDefault();
            float CusWallet=(float)db.Customer.Where(c => c.CustomerEmail == CusEmail).Select(c => c.CustomerWalletBalance).FirstOrDefault();
            Product product = TempData["currentOrderProduct"] as Product;
            Order storedOrder = TempData["ToPassOrderDetails"] as Order;
            order.OrderStatus = storedOrder.OrderStatus;
            order.ProductId = storedOrder.ProductId;
            order.OrderDate = storedOrder.OrderDate;
            order.VendorId = storedOrder.VendorId;
            order.OrderTotal = order.ProductQuantity * product.ProductPrice;
            if (order.OrderTotal > CusWallet)
            {
                ModelState.AddModelError("","Insufficient Balance.Please Recharge your wallet.");
            }
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("CustomerDashboard","Customers");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,VendorId,ProductId,ProductQuantity,OrderStatus,OrderDate,OrderTotal")] Order order)
        {
            if (ModelState.IsValid)
            {
                int customerId = order.CustomerId;
                Customer customer = db.Customer.Find(customerId);
                customer.CustomerWalletBalance = customer.CustomerWalletBalance - order.OrderTotal;
                int vendorId = order.VendorId;
                Vendor vendor = db.Vendor.Find(vendorId);
                vendor.VWalletBalance = vendor.VWalletBalance + order.OrderTotal;
                db.Entry(customer).State = EntityState.Modified;
                db.Entry(vendor).State = EntityState.Modified;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(order);
        }
        // GET: Orders/Edit/5
        public ActionResult EditForDeny(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForDeny([Bind(Include = "OrderId,CustomerId,VendorId,ProductId,ProductQuantity,OrderStatus,OrderDate,OrderTotal")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
