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
    public class gonderilersController : Controller
    {
        private MyBlogSiteEntities db = new MyBlogSiteEntities();

        // GET: gonderilers
        public async Task<ActionResult> Index()
        {

            if (Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            var gonderiler = db.gonderiler.Include(g => g.kategoriler).OrderByDescending(x=>x.saat).ToList();
             return View(gonderiler);                     
            
        }

        // GET: gonderilers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null|| Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            gonderiler gonderiler = await db.gonderiler.FindAsync(id);
            if (gonderiler == null)
            {
                return HttpNotFound();
            }
            return View(gonderiler);
        }

        // GET: gonderilers/Create
        public ActionResult Create()
        {
            if (Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            ViewBag.katid = new SelectList(db.kategoriler, "katid", "kategori");
            return View();
        }

        // POST: gonderilers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "gonderiid,baslik,gonderi,saat,resim,katid,tiklanma_say")] gonderiler gonderiler)
        {
            if (ModelState.IsValid)
            {
                gonderiler.asagilama = 0;
                gonderiler.begenme = 0;
                gonderiler.tiklanma_say = 0;
                gonderiler.saat = DateTime.Now;
                db.gonderiler.Add(gonderiler);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.katid = new SelectList(db.kategoriler, "katid", "kategori", gonderiler.katid);
            return View(gonderiler);
        }

        // GET: gonderilers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null || Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            gonderiler gonderiler = await db.gonderiler.FindAsync(id);
            if (gonderiler == null)
            {
                return HttpNotFound();
            }
            ViewBag.katid = new SelectList(db.kategoriler, "katid", "kategori", gonderiler.katid);
            return View(gonderiler);
        }

        // POST: gonderilers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "gonderiid,baslik,gonderi,saat,resim,katid,tiklanma_say")] gonderiler gonderiler)
        {
            if (ModelState.IsValid)
            {
                gonderiler.saat = DateTime.Now;
                db.Entry(gonderiler).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.katid = new SelectList(db.kategoriler, "katid", "kategori", gonderiler.katid);
            return View(gonderiler);
        }

        // GET: gonderilers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null || Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            gonderiler gonderiler = await db.gonderiler.FindAsync(id);
            if (gonderiler == null)
            {
                return HttpNotFound();
            }
            return View(gonderiler);
        }

        // POST: gonderilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            gonderiler gonderiler = await db.gonderiler.FindAsync(id);
            db.gonderiler.Remove(gonderiler);
            await db.SaveChangesAsync();
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
