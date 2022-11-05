using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GratitudeApp;

namespace GratitudeApp.Controllers
{
    public class KirjausController : Controller
    {
        private gratitudeEntities db = new gratitudeEntities();

        // GET: Kirjaus
        public ActionResult Index()
        {
            var kirjaus = db.Kirjaus.Include(k => k.Kayttajat).Include(k => k.Talot);
            return View(kirjaus.ToList());
        }

        // GET: Kirjaus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kirjaus kirjaus = db.Kirjaus.Find(id);
            if (kirjaus == null)
            {
                return HttpNotFound();
            }
            return View(kirjaus);
        }

        // GET: Kirjaus/Create
        public ActionResult Create()
        {
            ViewBag.kayttaja_id = new SelectList(db.Kayttajat, "kayttaja_id", "username");
            ViewBag.talo_id = new SelectList(db.Talot, "talo_id", "linkki");
            return View();
        }

        // POST: Kirjaus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kirjaus_id,otsake,teksti,kayttaja_id,talo_id,pvm")] Kirjaus kirjaus)
        {
            if (ModelState.IsValid)
            {
                db.Kirjaus.Add(kirjaus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.kayttaja_id = new SelectList(db.Kayttajat, "kayttaja_id", "username", kirjaus.kayttaja_id);
            ViewBag.talo_id = new SelectList(db.Talot, "talo_id", "linkki", kirjaus.talo_id);
            return View(kirjaus);
        }

        // GET: Kirjaus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kirjaus kirjaus = db.Kirjaus.Find(id);
            if (kirjaus == null)
            {
                return HttpNotFound();
            }
            ViewBag.kayttaja_id = new SelectList(db.Kayttajat, "kayttaja_id", "username", kirjaus.kayttaja_id);
            ViewBag.talo_id = new SelectList(db.Talot, "talo_id", "linkki", kirjaus.talo_id);
            return View(kirjaus);
        }

        // POST: Kirjaus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kirjaus_id,otsake,teksti,kayttaja_id,talo_id,pvm")] Kirjaus kirjaus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kirjaus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.kayttaja_id = new SelectList(db.Kayttajat, "kayttaja_id", "username", kirjaus.kayttaja_id);
            ViewBag.talo_id = new SelectList(db.Talot, "talo_id", "linkki", kirjaus.talo_id);
            return View(kirjaus);
        }

        // GET: Kirjaus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kirjaus kirjaus = db.Kirjaus.Find(id);
            if (kirjaus == null)
            {
                return HttpNotFound();
            }
            return View(kirjaus);
        }

        // POST: Kirjaus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kirjaus kirjaus = db.Kirjaus.Find(id);
            db.Kirjaus.Remove(kirjaus);
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
