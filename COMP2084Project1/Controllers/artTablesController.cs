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
    public class artTablesController : Controller
    {
        private a1dbEntities4 db = new a1dbEntities4();

        // GET: artTables
        public ActionResult Index()
        {
            var artTables = db.artTables.Include(a => a.museumTable);
            return View(artTables.ToList());
        }

        // GET: artTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artTable artTable = db.artTables.Find(id);
            if (artTable == null)
            {
                return HttpNotFound();
            }
            return View(artTable);
        }

        // GET: artTables/Create
        public ActionResult Create()
        {
            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name");
            return View();
        }

        // POST: artTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TitleID,Title,Artist,Year,MuseumID")] artTable artTable)
        {
            if (ModelState.IsValid)
            {
                db.artTables.Add(artTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name", artTable.MuseumID);
            return View(artTable);
        }

        // GET: artTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artTable artTable = db.artTables.Find(id);
            if (artTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name", artTable.MuseumID);
            return View(artTable);
        }

        // POST: artTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TitleID,Title,Artist,Year,MuseumID")] artTable artTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name", artTable.MuseumID);
            return View(artTable);
        }

        // GET: artTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            artTable artTable = db.artTables.Find(id);
            if (artTable == null)
            {
                return HttpNotFound();
            }
            return View(artTable);
        }

        // POST: artTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            artTable artTable = db.artTables.Find(id);
            db.artTables.Remove(artTable);
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
