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
    public class TelewizoryController : Controller
    {
        private Zapasy_bazaEntities db = new Zapasy_bazaEntities();

        // GET: Telewizory
        public ActionResult Index()
        {
            return View(db.Telewizory.ToList());
        }

        // GET: Telewizory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telewizory telewizory = db.Telewizory.Find(id);
            if (telewizory == null)
            {
                return HttpNotFound();
            }
            return View(telewizory);
        }

        // GET: Telewizory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Telewizory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TelewizorID,Nazwa,Rozmiar")] Telewizory telewizory)
        {
            if (ModelState.IsValid)
            {
                db.Telewizory.Add(telewizory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(telewizory);
        }

        // GET: Telewizory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telewizory telewizory = db.Telewizory.Find(id);
            if (telewizory == null)
            {
                return HttpNotFound();
            }
            return View(telewizory);
        }

        // POST: Telewizory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TelewizorID,Nazwa,Rozmiar")] Telewizory telewizory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telewizory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(telewizory);
        }

        // GET: Telewizory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telewizory telewizory = db.Telewizory.Find(id);
            if (telewizory == null)
            {
                return HttpNotFound();
            }
            return View(telewizory);
        }

        // POST: Telewizory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telewizory telewizory = db.Telewizory.Find(id);
            db.Telewizory.Remove(telewizory);
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
