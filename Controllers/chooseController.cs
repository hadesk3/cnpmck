using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class chooseController : Controller
    {
        private managePhoneProductEntities db = new managePhoneProductEntities();

        // GET: choose
        public ActionResult Index()
        {
            return View(db.items.ToList());
        }



        [HttpPost]

        public ActionResult sendForm()
        {
            var str = HttpContext.Request.Form["name"];
            var str1 = str.Split('*')[0];
            var name = str1.Split(',')[0].Split(':')[1];
            var phone = str1.Split(',')[1].Split(':')[1];
            phone = phone.Replace(" ", "");

            var totalmoney = str.Split('*')[1].Split(',')[1].Split(':')[1];
            cart cart = new cart();
            cart.totalPrice = Int32.Parse(totalmoney);
            cart.status = "unchecked";
            cart.idCart = db.carts.Count() + 1 + "";
            cart.bank = HttpContext.Request.Form["pay"];
            cart.orderItem = str.Split('[')[1].Split(']')[0];
            cart.phone = phone;
            cart.nameAgent = name;
            db.carts.Add(cart);

            db.SaveChanges();
           
            string jsonString = str.Split('*')[1];

            int lastBraceIndex = jsonString.LastIndexOf('}');

            jsonString = jsonString.Substring(0, lastBraceIndex + 1);

            JObject jsonObject = JObject.Parse(jsonString);

            string count = jsonObject["count"].ToString().PadLeft(3, '0');
            decimal total = decimal.Parse(jsonObject["total"].ToString());

            JArray items = (JArray)jsonObject["items"];
            string output = $"{str.Split(',')[0]} - Count: {count}, Total: {total}";

            List<(int, string, int, decimal)> productInfoList = new List<(int, string, int, decimal)>(); // Create a list to store the product info

            foreach (JObject item in items)
            {
                int id = int.Parse(item["id"].ToString());
                string name3 = item["name"].ToString();
                decimal price = decimal.Parse(item["price"].ToString());
                int count2 = int.Parse(item["count"].ToString());
                decimal total2 = decimal.Parse(item["total"].ToString());

                productInfoList.Add((id, name, count2, price)); // Add the product info to the list

                output += $"\nProduct {id}: {name3}, Price: {price}, Count: {count2}, Total: {total2}";
            }

            string[] lines = output.Split('\n');


            // receipt
            receipt r = new receipt();
            r.idReceipt = db.receipts.Count() + 1 + "";
            r.dateofCreation = DateTime.Now;
            r.totalPrice = Int32.Parse(totalmoney);
            db.receipts.Add(r);
            db.SaveChanges();
            (int, string, int, decimal)[] productInfoArray = productInfoList.ToArray();
            for (int i = 0; i < productInfoArray.Length; i++)
            {
                (int id, string name2, int count3, decimal price) = productInfoArray[i];
                detailReceipt dr = new detailReceipt();
                dr.idReceipt = r.idReceipt;
                dr.idItem = id + "";
                dr.quantity = count3;
                dr.nameItem = name2;
                dr.totalPriceItem = (int)price * count3;

                db.detailReceipts.Add(dr);
                db.SaveChanges();
            }
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["managePhoneProductEntities"].ConnectionString;
            // 
            var gmailAgency = "";
            using (var db = new managePhoneProductEntities())
            {
                var agency = db.Agencies.FirstOrDefault(a => a.phoneNumberA == phone );
                if (agency != null)
                {
                    // get gmail
                    gmailAgency = agency.mailAgency;
                    var id = agency.idAgency;
                    Order order = new Order();
                    var idorder = db.Orders.Count() + 1 + "";
                    order.idOrder = idorder;
                    order.idAgency = id + "";
                    order.totalPrice = Int32.Parse(totalmoney);
                    order.dateOfCreation = DateTime.Now;
                    order.orderStatus = "uncheck";
                    order.paymentStatus = "no";
                    order.paymentMethod = HttpContext.Request.Form["pay"];
                    db.Orders.Add(order);
                    db.SaveChanges();
                    for (int i = 0; i < productInfoArray.Length; i++)
                    {
                        (int id2, string name2, int count3, decimal price) = productInfoArray[i];
                        detailOrder d = new detailOrder();
                        d.idOrder = idorder;
                        d.idItem = id2 + "";
                        d.quantity = count3;
                       
                        d.totalPriceItem = (int)price * count3;

                        db.detailOrders.Add(d);
                        db.SaveChanges();
                    }
                }


                
               



            }
            string fromAddress = "521H0447@student.tdtu.edu.vn";
            string toAddress =  gmailAgency;
            string subject = "Order product";
            string body = "Your order has been successfully placed, if not paid, please pay, " +
                "we will check if you have paid, then an email will be sent to you in a few minutes. You can see your order at this link: https://localhost:44312/cartsAgents";

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


            return RedirectToAction("Index");


        }
    }
}
