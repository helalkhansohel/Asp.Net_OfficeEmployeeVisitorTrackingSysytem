using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeEmployeeVisitorTrackingSysytem.Models;

namespace OfficeEmployeeVisitorTrackingSysytem.Controllers
{
    public class VisitorsController : Controller
    {
        private ApplicationContext db = new ApplicationContext();


        public ActionResult Visitor()
        {

            return View();
        }

        public ActionResult Company()
        {
            var offices = db.Companies.OrderBy(x => x.Name);
            return View(offices.ToList());
        }

        // GET: Visitors
        public ActionResult CompanyVisitorReport(int? id)
        {
            var visitors = db.Visitors.Include(v => v.Company).Include(v => v.Employee).Where(x=>x.CompanyId==id);
            return View(visitors.ToList());
        }

        // GET: Visitors
        public ActionResult Index()
        {
            var visitors = db.Visitors.Include(v => v.Company).Include(v => v.Employee);
            return View(visitors.ToList());
        }

        // GET: Visitors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitors.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        public ActionResult Employee(int? id)
        {
            var offices = db.Employees.Where(x => x.CompanyID == id).OrderBy(x => x.Id);
            return View(offices.ToList());
        }

        // GET: Visitors/Create
        public ActionResult Create(int? id ,string type="0")
        {
            ViewBag.Type = type;
            var user = db.Employees.Find(id);
            if (id == 0)
            {
                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
                return View();
            }
            else {
                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", user.CompanyID);
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", id);
                return View();
            }
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CompanyId,EmployeeId,CarNumber,Email,LogInTime,LogOutTime,Status")] Visitor visitor, string type)
        {

            DateTime d = DateTime.Now;
            var data = db.Visitors.ToArray().LastOrDefault(x => x.Email == visitor.Email);
            //----------------------LogIn ----------------------------------
            if (type == "1")
            {
                if (data == null || data.Status == "LogOut")
                {

                    visitor.LogInTime = d;
                    visitor.Status = "LogIn";
                    db.Visitors.Add(visitor);
                    db.SaveChanges();
                    ViewBag.Message = "LogIn";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", visitor.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", visitor.EmployeeId);
                    return View();
                }
                else {

                    ViewBag.Message = "Sorry your are already Signed In";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", visitor.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", visitor.EmployeeId);
                    return View();
                }

            }
            //-------------------------LogOut-----------------------------------
            else {

                if (data == null || data.Status == "LogOut")
                {
                    ViewBag.Message = "Sorry you are not sign in";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", visitor.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", visitor.EmployeeId);
                    return View();
                }
                else {
                    var data1 = db.Visitors.ToArray().LastOrDefault(x => x.Email == visitor.Email);
                    Visitor visitor1 = db.Visitors.Find(data1.Id);
                    visitor1.LogOutTime = d;

                    visitor1.Status = "LogOut";
                    db.Entry(visitor1).State = EntityState.Modified;
                    db.SaveChanges();

                    ViewBag.Message = "LogOut";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", visitor.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", visitor.EmployeeId);

                    return View();
                }


                   
            }
            
               
        }
        [NonAction]
        // GET: Visitors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitors.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", visitor.CompanyId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", visitor.EmployeeId);
            return View(visitor);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CompanyId,EmployeeId,CarNumber,Email,LogInTime,LogOutTime,Status")] Visitor visitor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", visitor.CompanyId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", visitor.EmployeeId);
            return View(visitor);
        }
        [NonAction]
        // GET: Visitors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitor visitor = db.Visitors.Find(id);
            if (visitor == null)
            {
                return HttpNotFound();
            }
            return View(visitor);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitor visitor = db.Visitors.Find(id);
            db.Visitors.Remove(visitor);
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
