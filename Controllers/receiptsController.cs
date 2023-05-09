using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class receiptsController : Controller
    {
        private managePhoneProductEntities db = new managePhoneProductEntities();

        // GET: receipts
        public ActionResult Index()
        {
            var receipts = db.receipts.Include(r => r.Staff);
            return View(receipts.ToList());
        }

        // GET: receipts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receipt receipt = db.receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // GET: receipts/Create
        public ActionResult Create()
        {
            ViewBag.idStaff = new SelectList(db.Staffs, "idStaff", "nameStaff");
            return View();
        }

        // POST: receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idReceipt,idStaff,dateofCreation,totalPrice")] receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.receipts.Add(receipt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idStaff = new SelectList(db.Staffs, "idStaff", "nameStaff", receipt.idStaff);
            return View(receipt);
        }

        // GET: receipts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receipt receipt = db.receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.idStaff = new SelectList(db.Staffs, "idStaff", "nameStaff", receipt.idStaff);
            return View(receipt);
        }

        // POST: receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idReceipt,idStaff,dateofCreation,totalPrice")] receipt receipt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(receipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idStaff = new SelectList(db.Staffs, "idStaff", "nameStaff", receipt.idStaff);
            return View(receipt);
        }

        // GET: receipts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            receipt receipt = db.receipts.Find(id);
            if (receipt == null)
            {
                return HttpNotFound();
            }
            return View(receipt);
        }

        // POST: receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            receipt receipt = db.receipts.Find(id);
            db.receipts.Remove(receipt);
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
