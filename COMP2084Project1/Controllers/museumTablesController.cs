using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COMP2084Project1.Models;

namespace COMP2084Project1.Controllers
{
    public class museumTablesController : Controller
    {
        private a1dbEntities4 db = new a1dbEntities4();

        // GET: museumTables
        public ActionResult Index()
        {
            return View(db.museumTables.ToList());
        }

        // GET: museumTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            museumTable museumTable = db.museumTables.Find(id);
            if (museumTable == null)
            {
                return HttpNotFound();
            }
            return View(museumTable);
        }

        // GET: museumTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: museumTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MuseumID,Name,Location,Contact,Art")] museumTable museumTable)
        {
            if (ModelState.IsValid)
            {
                db.museumTables.Add(museumTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(museumTable);
        }

        // GET: museumTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            museumTable museumTable = db.museumTables.Find(id);
            if (museumTable == null)
            {
                return HttpNotFound();
            }
            return View(museumTable);
        }

        // POST: museumTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MuseumID,Name,Location,Contact,Art")] museumTable museumTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(museumTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(museumTable);
        }

        // GET: museumTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            museumTable museumTable = db.museumTables.Find(id);
            if (museumTable == null)
            {
                return HttpNotFound();
            }
            return View(museumTable);
        }

        // POST: museumTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            museumTable museumTable = db.museumTables.Find(id);
            db.museumTables.Remove(museumTable);
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
