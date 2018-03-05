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
    public class WypozyczenieTVController : Controller
    {
        private Zapasy_bazaEntities db = new Zapasy_bazaEntities();

        // GET: WypozyczenieTV
        public ActionResult Index()
        {
            var wypozyczenieTV = db.WypozyczenieTV.Include(w => w.Telewizory);
            return View(wypozyczenieTV.ToList());
        }

        // GET: WypozyczenieTV/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WypozyczenieTV wypozyczenieTV = db.WypozyczenieTV.Find(id);
            if (wypozyczenieTV == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenieTV);
        }

        // GET: WypozyczenieTV/Create
        public ActionResult Create()
        {
            ViewBag.TelewizorID = new SelectList(db.Telewizory, "TelewizorID", "Nazwa");
            return View();
        }

        // POST: WypozyczenieTV/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WypozyczenieTVID,DataWypozyczenia,DataZwrotu,TelewizorID,Dodatkowo,Opis")] WypozyczenieTV wypozyczenieTV)
        {
            if (ModelState.IsValid)
            {
                db.WypozyczenieTV.Add(wypozyczenieTV);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TelewizorID = new SelectList(db.Telewizory, "TelewizorID", "Nazwa", wypozyczenieTV.TelewizorID);
            return View(wypozyczenieTV);
        }

        // GET: WypozyczenieTV/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WypozyczenieTV wypozyczenieTV = db.WypozyczenieTV.Find(id);
            if (wypozyczenieTV == null)
            {
                return HttpNotFound();
            }
            ViewBag.TelewizorID = new SelectList(db.Telewizory, "TelewizorID", "Nazwa", wypozyczenieTV.TelewizorID);
            return View(wypozyczenieTV);
        }

        // POST: WypozyczenieTV/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WypozyczenieTVID,DataWypozyczenia,DataZwrotu,TelewizorID,Dodatkowo,Opis")] WypozyczenieTV wypozyczenieTV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenieTV).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TelewizorID = new SelectList(db.Telewizory, "TelewizorID", "Nazwa", wypozyczenieTV.TelewizorID);
            return View(wypozyczenieTV);
        }

        // GET: WypozyczenieTV/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WypozyczenieTV wypozyczenieTV = db.WypozyczenieTV.Find(id);
            if (wypozyczenieTV == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenieTV);
        }

        // POST: WypozyczenieTV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WypozyczenieTV wypozyczenieTV = db.WypozyczenieTV.Find(id);
            db.WypozyczenieTV.Remove(wypozyczenieTV);
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
