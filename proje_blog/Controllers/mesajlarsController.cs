using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proje_blog.DAL;

namespace proje_blog.Content
{
    public class mesajlarsController : Controller
    {
        private MyBlogSiteEntities db = new MyBlogSiteEntities();

        // GET: mesajlars
        public ActionResult Index()
        {
            if (Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(db.mesajlar.OrderByDescending(x=>x.saat).ToList());
        }

        // GET: mesajlars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null|| Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            mesajlar mesajlar = db.mesajlar.Find(id);
            mesajlar mes = db.mesajlar.Where(x => x.id == id).SingleOrDefault();
            if (mes != null)
            {
                mes.okundu = true;
                db.Entry(mes).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            if (mesajlar == null)
            {
                return HttpNotFound();
            }
            return View(mesajlar);
        }

        // GET: mesajlars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mesajlars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,mesaj,saat,isim,okundu,mail,konu")] mesajlar mesajlar)
        {
            if (ModelState.IsValid)
            {
                mesajlar.okundu = false;
                mesajlar.saat = DateTime.Now;
                db.mesajlar.Add(mesajlar);

                db.mesajlar.Add(mesajlar);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(mesajlar);
        }

        // GET: mesajlars/Edit/5


        // GET: mesajlars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null|| Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            mesajlar mesajlar = db.mesajlar.Find(id);
            if (mesajlar == null)
            {
                return HttpNotFound();
            }
            return View(mesajlar);
        }

        // POST: mesajlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mesajlar mesajlar = db.mesajlar.Find(id);
            db.mesajlar.Remove(mesajlar);
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
