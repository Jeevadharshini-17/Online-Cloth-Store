using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

using OnlineClothStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineClothStore.Controllers
{
    public class CustomersController : Controller
    {
        private OnlineClothStoreDBContext db = new OnlineClothStoreDBContext();
        private ApplicationDbContext db1 = new ApplicationDbContext();
        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customer.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = (Customer)db.Customer.SingleOrDefault(c => c.CustomerEmail==email);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,CustomerName,CustomerEmail,CustomerPassword,CustomerAddress,CustomerPhone,CustomerWalletBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customer.Add(customer);
                if (!db1.Users.Any(u => u.Email == customer.CustomerEmail))
                {
                    var store = new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>(db1);
                    var manager = new UserManager<ApplicationUser>(store);
                    var user = new ApplicationUser { UserName = customer.CustomerEmail, Email = customer.CustomerEmail };
                    manager.Create(user, customer.CustomerPassword);
                    

                }
                db.SaveChanges();
                return RedirectToAction("Login","Account");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,CustomerName,CustomerEmail,CustomerPassword,CustomerAddress,CustomerPhone,CustomerWalletBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerDashboard");
            }
            return View(customer);
        }
        public ActionResult RechargeWallet(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = (Customer)db.Customer.SingleOrDefault(c => c.CustomerEmail == email);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RechargeWallet([Bind(Include = "CustomerId,CustomerName,CustomerEmail,CustomerPassword,CustomerAddress,CustomerPhone,CustomerWalletBalance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerDashboard");
            }
            return View(customer);
        }


        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customer.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customer.Find(id);
            db.Customer.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CustomerDashboard()
        {
            return View();
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
