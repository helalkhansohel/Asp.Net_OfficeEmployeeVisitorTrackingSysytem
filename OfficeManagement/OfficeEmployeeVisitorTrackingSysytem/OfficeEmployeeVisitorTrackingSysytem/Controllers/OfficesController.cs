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
    public class OfficesController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Offices
        public ActionResult Index()
        {
            var offices = db.Offices.Include(o => o.Employee);
            return View(offices.ToList());
        }

        //000000000 Report --------------------
        public ActionResult CompanyOfficeReport(int id)
        {
            var offices = db.Offices.Include(o => o.Employee).Where(x=>x.CompanyId==id);
            return View(offices.ToList());
        }


        public ActionResult Office()
        {
            var offices = db.Companies.OrderBy(x=>x.Name);
            return View(offices.ToList());
        }

        public ActionResult Employee(int? id)
        {
            var offices = db.Employees.Where(x=>x.CompanyID==id).OrderBy(x => x.Id);
            return View(offices.ToList());
        }

        // GET: Offices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // GET: Offices/Create
        public ActionResult Create(int? id , int eid=0)
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name",id);
            var data = db.Offices.ToArray().LastOrDefault(x => x.EmployeeId == eid);
            var User = db.Employees.FirstOrDefault(x=>x.Id==eid);
            ViewBag.Name = User.Name;

            if (data == null) {
                ViewBag.status = "LogIn";
                return View();
            }

           else if (data.CurrentStatus == "LogOut")
            {
                ViewBag.status = "LogIn";
                return View();
            }
            else if (data.CurrentStatus == "LogIn")
            {
                ViewBag.status = "LogOut";
                return View();
            }
            else {
                ViewBag.status = "LogOut";
                return View();
            }
            
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,CompanyId,Date,LogInTime,LogOutTime,CurrentStatus")] Office office ,string pass)
        {
            //DateTime dt = DateTime.Now.ToUniversalTime().AddHours(6);

            int EId = (office.EmployeeId - 100)??0;
            var user = db.Employees.FirstOrDefault(x => x.Id == EId && x.Password == pass);

            if (user==null) {
                ViewBag.Message = "Wrong User Id or Password";
                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", office.CompanyId);
                return View();
            }
            //----------------------------------------------------------------------------
            DateTime d = DateTime.Now;
            var data = db.Offices.ToArray().LastOrDefault(x => x.EmployeeId == EId);


            if (data==null|| data.CurrentStatus=="LogOut") {
                office.LogInTime = d;
                office.CurrentStatus = "LogIn";
                office.EmployeeId = EId;
                db.Offices.Add(office);

                db.SaveChanges();

                ViewBag.Message = "LogIn Succesful";
                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", office.CompanyId);

                return View();
            }

            if (data.CurrentStatus == "LogIn")
            {
                var data1 = db.Offices.ToArray().LastOrDefault(x => x.EmployeeId == EId);
                Office office1 = db.Offices.Find(data1.Id);
             
                office1.LogInTime = data1.LogInTime;

                office1.LogOutTime = d;
                office1.CurrentStatus = "LogOut";
                office1.EmployeeId = EId;
                office1.CompanyId = data1.CompanyId;
                db.Entry(office1).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Message = "LogOut Succesful";

                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", office.CompanyId);

                return View();
            }


            if (ModelState.IsValid)
            {
                office.LogInTime = d;
                db.Offices.Add(office);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", office.CompanyId);
            return View(office);
        }

        // GET: Offices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", office.EmployeeId);
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,Date,LogInTime,LogOutTime,CurrentStatus")] Office office)
        {
            if (ModelState.IsValid)
            {
                db.Entry(office).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", office.EmployeeId);
            return View(office);
        }

        // GET: Offices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Office office = db.Offices.Find(id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Office office = db.Offices.Find(id);
            db.Offices.Remove(office);
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
