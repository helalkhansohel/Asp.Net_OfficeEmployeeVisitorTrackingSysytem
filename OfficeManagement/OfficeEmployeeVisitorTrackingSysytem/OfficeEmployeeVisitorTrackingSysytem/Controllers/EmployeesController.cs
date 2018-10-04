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
    public class EmployeesController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Company);
            return View(employees.ToList());
        }

        public ActionResult CompanyIndex(int? id)
        {
            var employees = db.Employees.Include(e => e.Company).Where(x=>x.CompanyID==id);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyID,Name,Email,Phone,Address,Password,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Status = "Active";
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name", employee.CompanyID);
            return View(employee);
        }


        public ActionResult CompanyCreate()
        {
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyCreate([Bind(Include = "Id,CompanyID,Name,Email,Phone,Address,Password,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Status = "Active";
                employee.CompanyID = Convert.ToInt32(Session["CompanyId"]);
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("CompanyIndex", "Employees",new { id= Convert.ToInt32(Session["CompanyId"]) });
            }

            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name", employee.CompanyID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name", employee.CompanyID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyID,Name,Email,Phone,Address,Password,Status")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.CompanyID = @Convert.ToInt32(Session["CompanyId"]);
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.Companies, "Id", "Name", employee.CompanyID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
