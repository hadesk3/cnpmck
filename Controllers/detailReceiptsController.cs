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
    public class detailReceiptsController : Controller
    {
        private managePhoneProductEntities db = new managePhoneProductEntities();

        // GET: detailReceipts
        public ActionResult Index()
        {
            var detailReceipts = db.detailReceipts.Include(d => d.item).Include(d => d.receipt);
            return View(detailReceipts.ToList());
        }

        // GET: detailReceipts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detailReceipt detailReceipt = db.detailReceipts.Find(id);
            if (detailReceipt == null)
            {
                return HttpNotFound();
            }
            return View(detailReceipt);
        }

        // GET: detailReceipts/Create
        public ActionResult Create()
        {
            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem");
            ViewBag.idReceipt = new SelectList(db.receipts, "idReceipt", "idStaff");
            return View();
        }

        // POST: detailReceipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idReceipt,idItem,nameItem,quantity,totalPriceItem")] detailReceipt detailReceipt)
        {
            if (ModelState.IsValid)
            {
                db.detailReceipts.Add(detailReceipt);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem", detailReceipt.idItem);
            ViewBag.idReceipt = new SelectList(db.receipts, "idReceipt", "idStaff", detailReceipt.idReceipt);
            return View(detailReceipt);
        }

        // GET: detailReceipts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detailReceipt detailReceipt = db.detailReceipts.Find(id);
            if (detailReceipt == null)
            {
                return HttpNotFound();
            }
            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem", detailReceipt.idItem);
            ViewBag.idReceipt = new SelectList(db.receipts, "idReceipt", "idStaff", detailReceipt.idReceipt);
            return View(detailReceipt);
        }

        // POST: detailReceipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idReceipt,idItem,nameItem,quantity,totalPriceItem")] detailReceipt detailReceipt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailReceipt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem", detailReceipt.idItem);
            ViewBag.idReceipt = new SelectList(db.receipts, "idReceipt", "idStaff", detailReceipt.idReceipt);
            return View(detailReceipt);
        }

        // GET: detailReceipts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detailReceipt detailReceipt = db.detailReceipts.Find(id);
            if (detailReceipt == null)
            {
                return HttpNotFound();
            }
            return View(detailReceipt);
        }

        // POST: detailReceipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            detailReceipt detailReceipt = db.detailReceipts.Find(id);
            db.detailReceipts.Remove(detailReceipt);
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
