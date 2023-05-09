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
    public class detailOrdersController : Controller
    {
        private managePhoneProductEntities db = new managePhoneProductEntities();

        // GET: detailOrders
        public ActionResult Index()
        {
            var detailOrders = db.detailOrders.Include(d => d.item).Include(d => d.Order);
            return View(detailOrders.ToList());
        }

        // GET: detailOrders/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detailOrder detailOrder = db.detailOrders.Find(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

        // GET: detailOrders/Create
        public ActionResult Create()
        {
            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem");
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "idAgency");
            return View();
        }

        // POST: detailOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOrder,idItem,quantity,totalPriceItem")] detailOrder detailOrder)
        {
            if (ModelState.IsValid)
            {
                db.detailOrders.Add(detailOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem", detailOrder.idItem);
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "idAgency", detailOrder.idOrder);
            return View(detailOrder);
        }

        // GET: detailOrders/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detailOrder detailOrder = db.detailOrders.Find(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem", detailOrder.idItem);
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "idAgency", detailOrder.idOrder);
            return View(detailOrder);
        }

        // POST: detailOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOrder,idItem,quantity,totalPriceItem")] detailOrder detailOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idItem = new SelectList(db.items, "idItem", "nameItem", detailOrder.idItem);
            ViewBag.idOrder = new SelectList(db.Orders, "idOrder", "idAgency", detailOrder.idOrder);
            return View(detailOrder);
        }

        // GET: detailOrders/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detailOrder detailOrder = db.detailOrders.Find(id);
            if (detailOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailOrder);
        }

        // POST: detailOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            detailOrder detailOrder = db.detailOrders.Find(id);
            db.detailOrders.Remove(detailOrder);
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
