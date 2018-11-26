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
    [Authorize]
    public class artTablesController : Controller
    {
        //private a1dbEntities4 db = new a1dbEntities4();

        private IArtMock db;

        public artTablesController()
        {
            this.db = new EFartTable();
        }

        public artTablesController(IArtMock artMock)
        {
            this.db = artMock;
        }

        // GET: artTables
        [AllowAnonymous]
        public ActionResult Index()
        {
            //var artTables = db.artTables.Include(a => a.museumTable);
            var artTables = db.artTables.OrderBy(a => a.Title).ThenBy(a => a.Artist).ToList();
            return View("Index", artTables);
        }

        // GET: artTables/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //artTable artTable = db.artTables.Find(id);

            artTable artTable = db.artTables.SingleOrDefault(a => a.TitleID == id);
            if (artTable == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Details", artTable);
        }

        // GET: artTables/Create
        public ActionResult Create()
        {
            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name");
            return View("Create");

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
                //db.artTables.Add(artTable);
                //db.SaveChanges();
                db.Save(artTable);
                return RedirectToAction("Index");
            }

            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name", artTable.MuseumID);
            return View("Create", artTable);
        }

        // GET: artTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //artTable artTable = db.artTables.Find(id);
            artTable artTable = db.artTables.SingleOrDefault(a => a.TitleID == id);
            if (artTable == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name", artTable.MuseumID);
            return View("Edit", artTable);
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
                //db.Entry(artTable).State = EntityState.Modified;
                db.Save(artTable);
                return RedirectToAction("Index");
            }
            ViewBag.MuseumID = new SelectList(db.museumTables, "MuseumID", "Name", artTable.MuseumID);
            return View("Edit", artTable);
        }

        // GET: artTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //artTable artTable = db.artTables.Find(id);
            artTable artTable = db.artTables.SingleOrDefault(a => a.TitleID == id);
            if (artTable == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete", artTable);
        }

        // POST: artTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            //artTable artTable = db.artTables.Find(id);
            //db.artTables.Remove(artTable);
            //db.SaveChanges();
            if (id == null)
            {
                return View("Error");
            }

            artTable artTable = db.artTables.SingleOrDefault(a => a.TitleID == id);

            if (id == null)
            {
                return View("Error");
            }

            db.Delete(artTable);

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
