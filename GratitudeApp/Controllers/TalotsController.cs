using System;
using GratitudeApp.ViewModel;
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
    public class TalotsController : Controller
    {
        private gratitudeEntities db = new gratitudeEntities();

        // GET: Talots
        
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                string username = Session["UserName"].ToString();

                var talotLista = from a in db.Talot
                                  join o in db.Kirjaus on a.talo_id equals o.talo_id
                                  join k in db.Kayttajat on o.kayttaja_id equals k.kayttaja_id
                                  where k.username == username
                                  

                                          select new TaloKirjaus
                                          {
                                              Talo_Id = a.talo_id,
                                              Linkki = a.linkki,
                                             Kirjaaja_id=k.kayttaja_id,
                                             tarkeys = a.tarkeys
                                          };

                return View(talotLista);
            }
        }

        // GET: Kirjaus/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Kirjaus kirjaus = db.Kirjaus.Find(id);
        //    if (kirjaus == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(kirjaus);
        //}
        //// GET: Talots/Create
        //[Authorize]
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Talots/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "talo_id,linkki,kuvaus,tarkeys")] Talot talot)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Talot.Add(talot);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(talot);
        //}

        // GET: Talots/Edit/5
        //[Authorize]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Talot talot = db.Talot.Find(id);
        //    if (talot == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(talot);
        //}

        //// POST: Talots/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "talo_id,linkki,kuvaus,tarkeys")] Talot talot)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(talot).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(talot);
        //}

        //// GET: Talots/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Talot talot = db.Talot.Find(id);
        //    if (talot == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(talot);
        //}

        //// POST: Talots/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Talot talot = db.Talot.Find(id);
        //    db.Talot.Remove(talot);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
