using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using proje_blog.DAL;

namespace proje_blog.Controllers
{
    public class kullanicilarsController : Controller
    {
        private MyBlogSiteEntities db = new MyBlogSiteEntities();

        // GET: kullanicilars
        public async Task<ActionResult> Index()
        {
            if (Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View(await db.kullanicilar.ToListAsync());
        }

        // GET: kullanicilars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null || Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            kullanicilar kullanicilar = await db.kullanicilar.FindAsync(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // GET: kullanicilars/Create

        // GET: kullanicilars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null || Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            kullanicilar kullanicilar = await db.kullanicilar.FindAsync(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }
            return View(kullanicilar);
        }

        // POST: kullanicilars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,adi,soyadi,mail,sifre,hakkinda,telefon,resim,nick")] kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kullanicilar).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kullanicilar);
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
