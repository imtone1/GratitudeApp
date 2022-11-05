using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GratitudeApp;
using GratitudeApp.Services;

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
            
            return View();
        }

        // POST: Kirjaus/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "teksti")] Kirjaus kirjaus)
        {
            if (ModelState.IsValid)
            {
                int kirjautunut = (int)Session["UserId"];
                LoginService lService = new LoginService();
                string kryptattuteksti = lService.md5_string(kirjaus.teksti);

               var talo = (from o in db.Talot
                        select o.talo_id).ToList();
                var taloni = (from o in db.Talot
                              join k in db.Kirjaus on o.talo_id equals k.talo_id
                              join ka in db.Kayttajat on k.kirjaus_id equals ka.kayttaja_id
                              where ka.kayttaja_id == kirjautunut
                              select o).ToList();
                Random rand = new Random();
                //int toSkip = rand.Next(0, talo.Count);

                //talo.Skip(toSkip).Take(1).First();

int taloid=talo.ElementAt(rand.Next(talo.Count()));
                //Tarkistetaan onko olemassa
                //while (taloni)
                //{
                //   var taloni = (from o in db.Talot
                //            join k in db.Kirjaus on o.talo_id equals k.talo_id
                //            join ka in db.Kayttajat on k.kirjaus_id equals ka.kayttaja_id
                //            where ka.kayttaja_id == kirjautunut
                //            select o).FirstOrDefault();
                //};

                //luodaan tekstin
                Kirjaus uusiteksti = new Kirjaus
                    {
                        kayttaja_id = (int)Session["UserId"],
                        otsake=" ",
                        teksti=kryptattuteksti,
                        talo_id=taloid,
                        pvm=DateTime.UtcNow
                };

                    db.Kirjaus.Add(uusiteksti);
                    db.SaveChanges();

                return RedirectToAction("Create");
            }

            return View(kirjaus);
        }

        // GET: Kirjaus/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
