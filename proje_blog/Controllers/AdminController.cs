using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_blog.DAL;
using System.IO;

namespace proje_blog.Controllers
{
    public class AdminController : Controller
    {
        private MyBlogSiteEntities admin = new MyBlogSiteEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if(Session["Kullanici"]=="Admin")
            {
                return RedirectToAction("Anasayfa");
            }
            return View();
        }
        [HttpPost,ActionName("Index")]
        public ActionResult Giris(kullanicilar model)
        {
            kullanicilar kontrol = admin.kullanicilar.Where(x => x.mail == model.mail && x.sifre == model.sifre).SingleOrDefault();
            if(kontrol!=null)
            {
                Session["Kullanici"] = "Admin";
                return RedirectToAction("Anasayfa");
            }
            return View();
        }
        public ActionResult Hata()
        {
            return View();
        }
        public ActionResult Anasayfa(string Aranacakkelime)
        {
            if(Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Index","Admin");
            }
            if(Aranacakkelime !=null)
            {
                return View(admin.gonderiler.Where(x => x.baslik.Contains(Aranacakkelime)).ToList());
            }
            return View();
        }
        public ActionResult yukle()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Dosyalar/"), fileName);
                    file.SaveAs(path);
                     ViewBag.resim = "/Dosyalar/"+fileName+"";
                    //Resim Url sini döndürüyor
                }
            }
            return View();
        }


        public ActionResult GelenKutusu()
        {
            if (Session["Kullanici"] != "Admin")
            {
                return RedirectToAction("Hata", "Admin");
            }
            return View();
        }
        public ActionResult CikisYap()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}



