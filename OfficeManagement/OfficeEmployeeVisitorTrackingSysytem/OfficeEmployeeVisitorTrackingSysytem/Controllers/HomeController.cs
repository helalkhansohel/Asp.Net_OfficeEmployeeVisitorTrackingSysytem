using OfficeEmployeeVisitorTrackingSysytem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeEmployeeVisitorTrackingSysytem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdminHome()
        {
            return View();
        }


        public ActionResult AdminLogIn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AdminLogIn(string email , string pass)
        {
            var user = db.Admins.FirstOrDefault(x => x.Email == email && x.Password == pass);
            if (user != null)
            {
                Session["Admin"] = user.Email;
                Session["SubAdmin"] = user.Email;
                return RedirectToAction("AdminHome");
            }
            else {
                ViewBag.Message = "Wrong email or password";
                return View();

            }
            
        }

        //LogOut
        public ActionResult LogOut()
        {
            Session["Admin"] = null;
            Session["SubAdmin"] = null;
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ExecutiveLogIn()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult ExecutiveLogIn(string email, string pass , int CompanyId)
        {
            var user = db.Companies.FirstOrDefault(x => x.Email == email && x.Password == pass && x.Id==CompanyId);
            if (user != null)
            {
                Session["CompanyId"] = user.Id;
               
                return RedirectToAction("CompanyDetails", "Companies" , new { id=Convert.ToInt32(Session["CompanyId"]) });
            }
            else
            {
                ViewBag.Message = "Wrong email or password";
                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
                return View();

            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}