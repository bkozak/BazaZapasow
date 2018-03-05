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
    public class StacjeController : Controller
    {
        private Zapasy_bazaEntities db = new Zapasy_bazaEntities();

        // GET: Stacje
        public ActionResult Index()
        {
            var stacje = db.Stacje.Include(s => s.Rodzaje_stacji);
            return View(stacje.ToList());
        }

        // GET: Stacje/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stacje stacje = db.Stacje.Find(id);
            if (stacje == null)
            {
                return HttpNotFound();
            }
            return View(stacje);
        }

        // GET: Stacje/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Rodzaj_Id = new SelectList(db.Rodzaje_stacji, "Rodzaj_Id", "Rodzaj");
            return View();
        }

        // POST: Stacje/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nr_stacji,Rodzaj_Id,Uwagi")] Stacje stacje)
        {
            if (ModelState.IsValid)
            {
                db.Stacje.Add(stacje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rodzaj_Id = new SelectList(db.Rodzaje_stacji, "Rodzaj_Id", "Rodzaj", stacje.Rodzaj_Id);
            return View(stacje);
        }

        // GET: Stacje/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stacje stacje = db.Stacje.Find(id);
            if (stacje == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rodzaj_Id = new SelectList(db.Rodzaje_stacji, "Rodzaj_Id", "Rodzaj", stacje.Rodzaj_Id);
            return View(stacje);
        }

        // POST: Stacje/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nr_stacji,Rodzaj_Id,Uwagi")] Stacje stacje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stacje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rodzaj_Id = new SelectList(db.Rodzaje_stacji, "Rodzaj_Id", "Rodzaj", stacje.Rodzaj_Id);
            return View(stacje);
        }

        // GET: Stacje/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stacje stacje = db.Stacje.Find(id);
            if (stacje == null)
            {
                return HttpNotFound();
            }
            return View(stacje);
        }

        // POST: Stacje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Stacje stacje = db.Stacje.Find(id);
            db.Stacje.Remove(stacje);
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
