using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class cartsController : Controller
    {
        private managePhoneProductEntities db = new managePhoneProductEntities();

        // GET: carts
        public ActionResult Index()
        {
            return View(db.carts.ToList());
        }
        public ActionResult cart()
        {
            return View(db.carts.ToList());
        }
        // GET: carts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCart,orderItem,totalPrice,status,bank,phone,nameAgent")] cart cart)
        {
            if (ModelState.IsValid)
            {
                db.carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: carts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCart,orderItem,totalPrice,status,bank,phone,nameAgent")] cart cart)
        {
            if (ModelState.IsValid)
            {
                Boolean check = cart.bank == "payed" || cart.bank == "cash";
                if ( check == true & cart.status == "preparing")
                {
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["managePhoneProductEntities"].ConnectionString;
                    // 
                    var gmailAgency = "";
                    using (var db = new managePhoneProductEntities())
                    {
                        var agency = db.Agencies.FirstOrDefault(a => a.phoneNumberA == cart.phone);
                        if (agency != null)
                        {

                            gmailAgency = agency.mailAgency;
                        }
                    }
                    string fromAddress = "521H0447@student.tdtu.edu.vn";
                    string toAddress = gmailAgency;
                    string subject = "Order product";
                    string body = "Your order is preparing.";

                    // Create a new MailMessage object
                    MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);

                    // Create a new SmtpClient object
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                    // Set the SMTP credentials
                    smtp.Credentials = new NetworkCredential("521H0447@student.tdtu.edu.vn", "27012003Ace@@");

                    // Enable SSL encryption
                    smtp.EnableSsl = true;

                    // Send the email
                    smtp.Send(message);

                   


                    //cart agency
                }
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: carts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart cart = db.carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            cart cart = db.carts.Find(id);
            db.carts.Remove(cart);
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
