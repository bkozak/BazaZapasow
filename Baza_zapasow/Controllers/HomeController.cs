using Baza_zapasow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Baza_zapasow.Controllers
{
    public class HomeController : Controller
    {
        private Zapasy_bazaEntities db = new Zapasy_bazaEntities();
        public ActionResult Index()
        {
            var wypozyczenie = db.Wypozyczenie.Include(w => w.Stacje).Include(w => w.Uzytkownik);
            ViewBag.Test = "Test";
           
            return View(wypozyczenie.ToList());
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TelewizoryTest()
        {
            int dni = DateTime.DaysInMonth(2017, 09);


            return View();
        }

        public void ZwrotA(int? id)
        {
            if(id==null)
            {
                HttpNotFound();
            }
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);
            db.Wypozyczenie.Attach(wypozyczenie);
            var entry = db.Entry(wypozyczenie);
            entry.Property(e => e.Zwrot).IsModified = true;
            wypozyczenie.Zwrot = true;
            db.SaveChanges();

            Response.Redirect(Url.Action("Index", "Home"));
            ViewBag.Ostatni = wypozyczenie.Nr_stacji.ToString();
        }

        public void OstatnioOddanyA(int? idO)
        {
            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(idO);
            ViewBag.Ostatni = wypozyczenie.Nr_stacji.ToString();
             
        }
    }
}