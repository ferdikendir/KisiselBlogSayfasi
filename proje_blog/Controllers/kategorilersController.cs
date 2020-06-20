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
    public class kategorilersController : Controller
    {
        private MyBlogSiteEntities db = new MyBlogSiteEntities();

        // GET: kategorilers
        public async Task<ActionResult> Index()
        {
            if (Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata","Admin");
            }
            
            return View(await db.kategoriler.ToListAsync());
        }

        // GET: kategorilers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null||Session["Kullanici"]!="Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            kategoriler kategoriler = await db.kategoriler.FindAsync(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // GET: kategorilers/Create
        public ActionResult Create()
        {

            if (Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View();
        }

        // POST: kategorilers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "katid,kategori")] kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                db.kategoriler.Add(kategoriler);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(kategoriler);
        }

        // GET: kategorilers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null || Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            kategoriler kategoriler = await db.kategoriler.FindAsync(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: kategorilers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "katid,kategori")] kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategoriler).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(kategoriler);
        }

        // GET: kategorilers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null || Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            kategoriler kategoriler = await db.kategoriler.FindAsync(id);
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: kategorilers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            kategoriler kategoriler = await db.kategoriler.FindAsync(id);
            db.kategoriler.Remove(kategoriler);
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
