using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Baza_zapasow.Models;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Printing;
using System.Diagnostics;
using Microsoft.Office.Interop.Word;
//using System.IO;

namespace Baza_zapasow.Controllers
{
    public class UzytkownikController : Controller
    {
        private Zapasy_bazaEntities db = new Zapasy_bazaEntities();
        

        // GET: Uzytkownik
        public ActionResult Index()
        {
            return View(db.Uzytkownik.ToList());
        }

        // GET: Uzytkownik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // GET: Uzytkownik/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uzytkownik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Uzytkownik_Id,Imie,Nazwisko,Adres,Kod_pocztowy,Miasto,Dowod,Nr_telefonu")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                db.Uzytkownik.Add(uzytkownik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uzytkownik);
        }

        // GET: Uzytkownik/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Uzytkownik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Uzytkownik_Id,Imie,Nazwisko,Adres,Kod_pocztowy,Miasto,Dowod,Nr_telefonu")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uzytkownik).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uzytkownik);
        }

        // GET: Uzytkownik/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uzytkownik uzytkownik = db.Uzytkownik.Find(id);
            if (uzytkownik == null)
            {
                return HttpNotFound();
            }
            return View(uzytkownik);
        }

        // POST: Uzytkownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uzytkownik uzytkownik = db.Uzytkownik.Find(id);
            db.Uzytkownik.Remove(uzytkownik);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PolaWPdf(int? id)
        {
            if (id == null)
            {
                new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);

            string TemplatePdf = @"C:\PDF\WzorTokeny.pdf";
            string GotowyPdf = @"C:\PDF\umowa.pdf";

            using (var TemplateStream = new FileStream(TemplatePdf, FileMode.Open))
            using (var GotowyStream = new FileStream(GotowyPdf, FileMode.Create))
            {

                var pdfReader = new PdfReader(TemplateStream);
                var stamper = new PdfStamper(pdfReader, GotowyStream);

                var form = stamper.AcroFields;

                AcroFields af = stamper.AcroFields;

                List<string> ListaPDF = new List<string>();

                foreach (var field in af.Fields)
                {
                    ListaPDF.Add(field.Key + " " + field.Value);
                }

                //form.SetField("DataZawarcia", wypozyczenie.Data_wypozyczenia.ToString().Remove(10).ToUpper());
                //form.SetField("OsobaIT", "Bartosz Kozak".ToUpper());
                //form.SetField("Uzytkownik", wypozyczenie.Uzytkownik.ImieNazwisko.ToString().ToUpper());
                //form.SetField("KodMiasto", wypozyczenie.Uzytkownik.Kod_pocztowy.ToString().ToUpper());
                //form.SetField("Dowod", wypozyczenie.Uzytkownik.Dowod.ToString().ToUpper());
                //form.SetField("Adres", wypozyczenie.Uzytkownik.Adres.ToString().ToUpper());
                //form.SetField("NumerStacji", wypozyczenie.Nr_stacji.ToString().ToUpper());
                //form.SetField("DataZwrotu", wypozyczenie.Data_zwrotu.ToString().Remove(10).ToUpper());

                stamper.FormFlattening = true;

                stamper.Close();
                pdfReader.Close();

                //DoDrukarki(GotowyPdf);
                //ViewBag.Lista = ListaPDF;
                //Response.Redirect(Url.Action("Index", "Uzytkownik"));
                return View(ListaPDF);


            }
            
        }

        public ActionResult PobranyDOC(int? id)
        {

            //OpenFileDialog file = new OpenFileDialog();

            Application word = new Application();
            Document doc = new Document();

            object fileName = @"C:\PDF\umowa.doc";
            // Define an object to pass to the API for missing parameters
            object missing = System.Type.Missing;
            doc = word.Documents.Open(ref fileName,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);

            String read = string.Empty;
            List<string> data = new List<string>();
            for (int i = 0; i < doc.Paragraphs.Count; i++)
            {
                string temp = doc.Paragraphs[i + 1].Range.Text.Trim();
                if (temp != string.Empty)
                    data.Add(temp);
            }
            ((_Document)doc).Close();
            ((_Application)word).Quit();
            return View(data);
        }

        public void FormatujListe(List<string> lista)
        {
            var znalezne = lista.Where(x => x.Contains("Panią"));

        }

        //public void UmowaDoc (int? id)
        //{
        //    Application word = new Application();
        //    Document doc = new Document();

        //    object fileName = @"C:\PDF\umowa.doc";
        //    // Define an object to pass to the API for missing parameters
        //    object missing = System.Type.Missing;
        //    doc = word.Documents.Open(ref fileName,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing, ref missing,
        //            ref missing, ref missing, ref missing);

        //    String read = string.Empty;
        //    List<string> data = new List<string>();
        //    for (int i = 0; i < doc.Paragraphs.Count; i++)
        //    {
        //        string temp = doc.Paragraphs[i + 1].Range.Text.Trim();
        //        if (temp != string.Empty)
        //            data.Add(temp);
        //    }
        //    ((_Document)doc).Close();
        //    ((_Application)word).Quit();
            
        //}
        public void DrukujUmowe (int? id)
        {
            if (id == null)
            {
                new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Wypozyczenie wypozyczenie = db.Wypozyczenie.Find(id);

            string TemplatePdf = @"C:\PDF\WzorTokeny.pdf";
            string GotowyPdf = @"C:\PDF\umowa" + DateTime.Now.ToString("dd-MM-yyyy") + ".pdf";

            using (var TemplateStream = new FileStream(TemplatePdf, FileMode.Open))
            using (var GotowyStream = new FileStream(GotowyPdf, FileMode.Create))
            {                

                var pdfReader = new PdfReader(TemplateStream);
                var stamper = new PdfStamper(pdfReader, GotowyStream);

                var form = stamper.AcroFields;

                AcroFields af = stamper.AcroFields;

                List<string> ListaPDF = new List<string>();
                
                foreach (var field in af.Fields)
                {
                    ListaPDF.Add(field.Key + " " + field.Value);                   
                }

                form.SetField("TokenaData", "11-30-2020r.");
                form.SetField("DataZawarcia", DateTime.Now.ToString("dd.MM.yyyy"));
                form.SetField("OsobaIT", "Bartosz Kozak".ToUpper());
                form.SetField("OsobaOdpowiedzialna", wypozyczenie.Uzytkownik.ImieNazwisko.ToString().ToUpper());
                form.SetField("Seria", wypozyczenie.Uzytkownik.Dowod.ToString().Remove(3,6).ToUpper());
                form.SetField("NumerD", wypozyczenie.Uzytkownik.Dowod.ToString().Remove(0,3).ToUpper());
                form.SetField("KodMiasto", wypozyczenie.Uzytkownik.Kod_pocztowy.ToString().ToUpper() + " " + wypozyczenie.Uzytkownik.Miasto.ToString().ToUpper());
                form.SetField("Ulica", wypozyczenie.Uzytkownik.Adres.ToString().ToUpper());
                form.SetField("Token", "563356352");

                stamper.FormFlattening = true;

                stamper.Close();
                pdfReader.Close();

                //DoDrukarki(GotowyPdf);
                ViewBag.Lista = ListaPDF;
                Response.Redirect(Url.Action("Index", "Uzytkownik"));


            }
           
        }

        
        public void DoDrukarki(string pdf)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = pdf;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
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
