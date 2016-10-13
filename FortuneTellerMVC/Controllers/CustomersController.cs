using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCContext db = new FortuneTellerMVCContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.Age %2 == 0)
            {
                ViewBag.retirementAge = 50;
            }
            else
            {
                ViewBag.retirementAge = 35;
            }
            if (customer.BirthMonth < 1 || customer.BirthMonth > 12)
            {
                ViewBag.moneyInTheBank = 0.00F;
            }
            else if (customer.BirthMonth <= 4)
            {
                ViewBag.moneyInTheBank = 200000.00F;
            }
            else if (customer.BirthMonth <= 8)
            {
                ViewBag.moneyInTheBank = 10000.00F;
            }
            else
            {
                ViewBag.moneyInTheBank = 50000.00F;
            }

            customer.FavoriteColor = customer.FavoriteColor.ToLower();
            switch (customer.FavoriteColor)
            {
                case "red":
                    ViewBag.transportation = "sports car";
                    break;
                case "orange":
                    ViewBag.transportation = "taxi";
                    break;
                case "yellow":
                    ViewBag.transportation = "bicycle";
                    break;
                case "green":
                    ViewBag.transportation = "normal car";
                    break;
                case "blue":
                    ViewBag.transportation = "boat";
                    break;
                case "indigo":
                    ViewBag.transportation = "very efficient car, you hipster";
                    break;
                case "violet":
                    ViewBag.transportation = "motorcycle";
                    break;
                default:
                    ViewBag.transportation = "pair of running shoes";
                    break;
            }
            if (customer.NumberOfSiblings< 0)
            {
                ViewBag.ViewBag.vacationHome = "a very small and sad hole";
            }
            else if (customer.NumberOfSiblings == 0)
            {
                ViewBag.vacationHome = "the tropics";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.vacationHome = "a city penthouse";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.vacationHome = "the Alps";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.vacationHome = "a nice suburb";
            }
            else
            {
                ViewBag.vacationHome = "a cultural metropolis";
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfcustomer.NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfcustomer.NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
            Customer customer = db.Customers.Find(id);
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
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
