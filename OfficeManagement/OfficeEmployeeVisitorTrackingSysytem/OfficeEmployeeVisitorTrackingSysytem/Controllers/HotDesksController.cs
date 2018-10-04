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
    public class HotDesksController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: HotDesks
        public ActionResult HotDesk()
        {
           
            return View();
        }

        // GET: HotDesks
        public ActionResult Index()
        {
            var hotDesks = db.HotDesks.Include(h => h.Company).Include(h => h.Employee);
            return View(hotDesks.ToList());
        }
        //-----------------------Company report---------------------
        public ActionResult ComapanyHotDesk(int? id)
        {
            var hotDesks = db.HotDesks.Include(h => h.Company).Include(h => h.Employee).Where(x=>x.CompanyId==id);
            return View(hotDesks.ToList());
        }

        // GET: HotDesks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotDesk hotDesk = db.HotDesks.Find(id);
            if (hotDesk == null)
            {
                return HttpNotFound();
            }
            return View(hotDesk);
        }

        // GET: HotDesks/Create
        public ActionResult Create(int? id)
        {
            ViewBag.Type = id.ToString();
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name");
            return View();
        }

        // POST: HotDesks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeId,CompanyId,CarNumber,Email,Date,LogInTime,LogOutTime,CurrentStatus")] HotDesk hotDesk, string pass, string type )
        {
            int EId = (hotDesk.EmployeeId - 100) ?? 0;
            var user = db.Employees.FirstOrDefault(x => x.Id == EId && x.Password == pass);

            if (user == null)
            {
                ViewBag.Message = "Wrong User Id or Password";
                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
                return View();
            }

            DateTime d = DateTime.Now;
            var data = db.HotDesks.ToArray().LastOrDefault(x => x.EmployeeId == EId);
            //-------------------------Log In------------------------------------------------------
            if (type == "1")
            {

                if (data == null || data.CurrentStatus == "LogOut")
                {
                    hotDesk.CurrentStatus = "LogIn";
                    hotDesk.LogInTime = d;
                    hotDesk.EmployeeId = EId;
                    db.HotDesks.Add(hotDesk);
                    db.SaveChanges();
                    ViewBag.Message = "LogIn";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
                    return View();

                }
                else {
                    ViewBag.Message = "Sorry your are already Signed In";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
                    return View();
                }
            }
            //----------------------------Log Out------------------------------------------------------
            else {


                if (data == null || data.CurrentStatus == "LogOut")
                {
                   
                    ViewBag.Message = "Sorry you are not sign in";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
                    return View();

                }

                var data1 = db.HotDesks.ToArray().LastOrDefault(x => x.EmployeeId == EId);
                HotDesk hotDesk1 = db.HotDesks.Find(data1.Id);

                if (ModelState.IsValid)
                {
                    hotDesk1.LogOutTime = d;
                    hotDesk1.CurrentStatus = "LogOut";
                    db.Entry(hotDesk1).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.Message = "LogOut";
                    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
                    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
                    return View();
                }
                ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
                return View(hotDesk);


            }

          

          

            
        }
        [NonAction]
        // GET: HotDesks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotDesk hotDesk = db.HotDesks.Find(id);
            if (hotDesk == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
            return View(hotDesk);
        }

        // POST: HotDesks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeId,CompanyId,CarNumber,Email,Date,LogInTime,LogOutTime,CurrentStatus")] HotDesk hotDesk)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotDesk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", hotDesk.CompanyId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "Name", hotDesk.EmployeeId);
            return View(hotDesk);
        }
        [NonAction]
        // GET: HotDesks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotDesk hotDesk = db.HotDesks.Find(id);
            if (hotDesk == null)
            {
                return HttpNotFound();
            }
            return View(hotDesk);
        }

        // POST: HotDesks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotDesk hotDesk = db.HotDesks.Find(id);
            db.HotDesks.Remove(hotDesk);
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
