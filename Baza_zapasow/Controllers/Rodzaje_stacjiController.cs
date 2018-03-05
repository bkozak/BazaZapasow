using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Baza_zapasow.Models;

namespace Baza_zapasow.Controllers
{
    public class Rodzaje_stacjiController : Controller
    {
        private Zapasy_bazaEntities db = new Zapasy_bazaEntities();

        // GET: Rodzaje_stacji
        public ActionResult Index()
        {
            return View(db.Rodzaje_stacji.ToList());
        }

        // GET: Rodzaje_stacji/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzaje_stacji rodzaje_stacji = db.Rodzaje_stacji.Find(id);
            if (rodzaje_stacji == null)
            {
                return HttpNotFound();
            }
            return View(rodzaje_stacji);
        }

        // GET: Rodzaje_stacji/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rodzaje_stacji/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rodzaj_Id,Rodzaj")] Rodzaje_stacji rodzaje_stacji)
        {
            if (ModelState.IsValid)
            {
                db.Rodzaje_stacji.Add(rodzaje_stacji);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rodzaje_stacji);
        }

        // GET: Rodzaje_stacji/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzaje_stacji rodzaje_stacji = db.Rodzaje_stacji.Find(id);
            if (rodzaje_stacji == null)
            {
                return HttpNotFound();
            }
            return View(rodzaje_stacji);
        }

        // POST: Rodzaje_stacji/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rodzaj_Id,Rodzaj")] Rodzaje_stacji rodzaje_stacji)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rodzaje_stacji).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rodzaje_stacji);
        }

        // GET: Rodzaje_stacji/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rodzaje_stacji rodzaje_stacji = db.Rodzaje_stacji.Find(id);
            if (rodzaje_stacji == null)
            {
                return HttpNotFound();
            }
            return View(rodzaje_stacji);
        }

        // POST: Rodzaje_stacji/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rodzaje_stacji rodzaje_stacji = db.Rodzaje_stacji.Find(id);
            db.Rodzaje_stacji.Remove(rodzaje_stacji);
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
