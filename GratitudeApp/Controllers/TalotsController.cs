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
    public class TalotsController : Controller
    {
        private gratitudetietokantaEntities db = new gratitudetietokantaEntities();

        // GET: Talots
        public ActionResult Index()
        {
            return View(db.Talot.ToList());
        }

        // GET: Talots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talot talot = db.Talot.Find(id);
            if (talot == null)
            {
                return HttpNotFound();
            }
            return View(talot);
        }

        // GET: Talots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Talots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "talo_id,linkki,kuvaus,tarkeys")] Talot talot)
        {
            if (ModelState.IsValid)
            {
                db.Talot.Add(talot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(talot);
        }

        // GET: Talots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talot talot = db.Talot.Find(id);
            if (talot == null)
            {
                return HttpNotFound();
            }
            return View(talot);
        }

        // POST: Talots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "talo_id,linkki,kuvaus,tarkeys")] Talot talot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(talot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(talot);
        }

        // GET: Talots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Talot talot = db.Talot.Find(id);
            if (talot == null)
            {
                return HttpNotFound();
            }
            return View(talot);
        }

        // POST: Talots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Talot talot = db.Talot.Find(id);
            db.Talot.Remove(talot);
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
