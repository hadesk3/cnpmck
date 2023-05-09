using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index() { return View(); }
        [HttpPost]    
        public ActionResult Login(FormCollection form)
        {
            string username = form["username"];
            string password = form["password"];
            string usertype = form["usertype"];
            Debug.WriteLine($"username: {username}, password: {password}, usertype: {usertype}");

            if (usertype == "admin")
            {
                if (username == "admin" & password == "admin")
                {
                   return  RedirectToAction("index", "admin");
                }
                else
                {
                    return RedirectToAction("index", "login");
                }
           
              
            }
            else
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["managePhoneProductEntities"].ConnectionString;

                // Create SQL query
                using (var db = new managePhoneProductEntities())
                {
                    var st = db.Staffs.FirstOrDefault(a => a.phoneNumber == username);
                    if (st != null)
                    {
                        if (st.passStaff == password)
                        {
                            return RedirectToAction("index", "choose");

                        }
                        else
                        {
                            return RedirectToAction("index", "login");
                        }

                    }
                }
            }
            return View();
        }


    }
}