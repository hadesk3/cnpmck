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
    public class cartsAgentsController : Controller
    {
        private managePhoneProductEntities db = new managePhoneProductEntities();

        // GET: cartsAgents
        public ActionResult Index()
        {
            return View(db.carts.ToList());
        }

       
    }
}
