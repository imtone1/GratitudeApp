﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GratitudeApp;
using GratitudeApp.Services;

namespace GratitudeApp.Controllers
{
    public class KayttajatsController : Controller
    {
        private gratitudetietokantaEntities db = new gratitudetietokantaEntities();

        // GET: Kayttajats
        public ActionResult Index()
        {
            return View(db.Kayttajat.ToList());
        }

        // GET: Kayttajats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kayttajat kayttajat = db.Kayttajat.Find(id);
            if (kayttajat == null)
            {
                return HttpNotFound();
            }
            return View(kayttajat);
        }

        // GET: Kayttajats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kayttajats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kayttaja_id,username,password")] Kayttajat kayttajat)
        {
            if (ModelState.IsValid)
            {
                LoginService lService = new LoginService();
                string kryptattuSalasana = lService.md5_string(kayttajat.password);

                //Tarkistetaan onko olemassa
                Kayttajat kayttis = new Kayttajat();
                kayttis = (from o in db.Kayttajat
                           where kayttajat.username == o.username
                           select o).FirstOrDefault();

                if (kayttis != null)
                {
                    //luodaan käyttäjätunnuksen
                    Kayttajat uusikayttaja = new Kayttajat
                    {
                        username = kayttajat.username,
                        password = kryptattuSalasana,
                    };


                    db.Kayttajat.Add(uusikayttaja);
                    db.SaveChanges();
                  
                }

                return View(kayttajat);
            }
            return View(kayttajat);
        }

        // GET: Kayttajats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kayttajat kayttajat = db.Kayttajat.Find(id);
            if (kayttajat == null)
            {
                return HttpNotFound();
            }
            return View(kayttajat);
        }

        // POST: Kayttajats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kayttaja_id,username,password")] Kayttajat kayttajat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kayttajat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kayttajat);
        }

        // GET: Kayttajats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kayttajat kayttajat = db.Kayttajat.Find(id);
            if (kayttajat == null)
            {
                return HttpNotFound();
            }
            return View(kayttajat);
        }

        // POST: Kayttajats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kayttajat kayttajat = db.Kayttajat.Find(id);
            db.Kayttajat.Remove(kayttajat);
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
