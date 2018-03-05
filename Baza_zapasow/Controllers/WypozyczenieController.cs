using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Baza_zapasow.Models;
using System.Dynamic;
using Baza_zapasow.ViewModels;

namespace Baza_zapasow.Controllers
{
    public class WypozyczenieController : Controller
    {
        private Zapasy_bazaEntities db = new Zapasy_bazaEntities();

        // GET: Wypozyczenie
        public ActionResult Index()
        {
            var wypozyczenie = db.Wypozyczenie.Include(w => w.Stacje).Include(w => w.Uzytkownik);
            return View(wypozyczenie.ToList());
        }

        // GET: Wypozyczenie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // GET: Wypozyczenie/Create
        [Authorize]
        public ActionResult Create()
        {
            
            ViewBag.Nr_stacji = new SelectList(db.Stacje, "Nr_stacji", "Nr_stacji");
            ViewBag.Uzytkownik_Id = new SelectList(db.Uzytkownik, "Uzytkownik_Id", "ImieNazwisko");
            return View();
        }

        // POST: Wypozyczenie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Wypozyczenie_Id,Zgloszenie,Data_wypozyczenia,Data_zwrotu,Uzytkownik_Id,Nr_stacji")] Wypozyczenie wypozyczenie)
        {
            if (ModelState.IsValid)
            {
                db.Wypozyczenie.Add(wypozyczenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Nr_stacji = new SelectList(db.Stacje, "Nr_stacji", "Nr_stacji", wypozyczenie.Nr_stacji);
            ViewBag.Uzytkownik_Id = new SelectList(db.Uzytkownik, "Uzytkownik_Id", "ImieNazwisko", wypozyczenie.Uzytkownik_Id);
            return View(wypozyczenie);
        }

        // GET: Wypozyczenie/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nr_stacji = new SelectList(db.Stacje, "Nr_stacji", "Nr_stacji", wypozyczenie.Nr_stacji);
            ViewBag.Uzytkownik_Id = new SelectList(db.Uzytkownik, "Uzytkownik_Id", "ImieNazwisko", wypozyczenie.Uzytkownik_Id);
            return View(wypozyczenie);
        }

        // POST: Wypozyczenie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Wypozyczenie_Id,Zgloszenie,Data_wypozyczenia,Data_zwrotu,Uzytkownik_Id,Nr_stacji,Zwrot")] Wypozyczenie wypozyczenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Nr_stacji = new SelectList(db.Stacje, "Nr_stacji", "Nr_stacji", wypozyczenie.Nr_stacji);
            ViewBag.Uzytkownik_Id = new SelectList(db.Uzytkownik, "Uzytkownik_Id", "ImieNazwisko", wypozyczenie.Uzytkownik_Id);
            return View(wypozyczenie);
        }

        // GET: Wypozyczenie/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            if (wypozyczenie == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenie);
        }

        // POST: Wypozyczenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            db.Wypozyczenie.Remove(wypozyczenie);
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
