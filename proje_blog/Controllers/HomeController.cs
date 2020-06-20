using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje_blog.DAL;
using System.Data.Entity;
using PagedList;

namespace proje_blog.Controllers
{
    public class HomeController : Controller
    {
        public static int aydi;
        private MyBlogSiteEntities veritabani = new MyBlogSiteEntities();
        // GET: Home
        public ActionResult Index(int? sayfaNo)
        {
            int _sayfaNo = sayfaNo ?? 1;

            var gonderi = veritabani.gonderiler.OrderByDescending(x => x.saat).ToPagedList<gonderiler>(_sayfaNo, 3);
            return View(gonderi);

        }
        public ActionResult Haber_Detay(gonderiler model ,string begen,string begenmeme)
        
        {
            gonderiler tik = veritabani.gonderiler.Where(x => x.gonderiid == model.gonderiid).SingleOrDefault();
            if (begen != null)
            {
                gonderiler gonderi = veritabani.gonderiler.Where(x => x.gonderiid == aydi).SingleOrDefault();
                Response.Cookies["Idi"]["Begen"] = Convert.ToString(aydi);
                Response.Cookies["Idi"].Expires = DateTime.Now.AddDays(1);
                if (Request.Cookies["Idi"] == null)
                {
                    int a = Convert.ToInt32(gonderi.begenme);
                    gonderi.begenme = a + 1;
                    veritabani.Entry(gonderi).State = EntityState.Modified;
                    veritabani.SaveChanges();
                }
                else
                {
                    if (Request.Cookies["Idi"]["Begen"] != Convert.ToString(aydi))
                    {
                        int a = Convert.ToInt32(gonderi.begenme);
                        gonderi.begenme = a + 1;
                        veritabani.Entry(gonderi).State = EntityState.Modified;
                        veritabani.SaveChanges(); ;
                    }
                }
                return View(veritabani.gonderiler.Where(X => X.gonderiid == aydi).ToList());
            }
            if (begenmeme != null)
            {
                gonderiler gonderi = veritabani.gonderiler.Where(x => x.gonderiid == aydi).SingleOrDefault();
                Response.Cookies["Iydi"]["Begenmeme"] = Convert.ToString(aydi);
                Response.Cookies["Iydi"].Expires = DateTime.Now.AddDays(1);
                if (Request.Cookies["Iydi"] == null)
                {
                    int a = Convert.ToInt32(gonderi.asagilama);
                    gonderi.asagilama = a + 1;
                    veritabani.Entry(gonderi).State = EntityState.Modified;
                    veritabani.SaveChanges();
                }
                else
                {
                    if (Request.Cookies["Iydi"]["Begenmeme"] != Convert.ToString(aydi))
                    {
                        int a = Convert.ToInt32(gonderi.asagilama);
                        gonderi.asagilama = a + 1;
                        veritabani.Entry(gonderi).State = EntityState.Modified;
                        veritabani.SaveChanges(); ;
                    }
                }
                return View(veritabani.gonderiler.Where(X => X.gonderiid == aydi).ToList());
            }
            if (tik != null)
            {
                aydi = tik.gonderiid;
                Response.Cookies["ID"]["HaberID"] = Convert.ToString(tik.gonderiid);
                Response.Cookies["ID"].Expires = DateTime.Now.AddDays(1);
                if (Request.Cookies["ID"] == null)
                {
                    int a = Convert.ToInt32(tik.tiklanma_say);
                    tik.tiklanma_say = a + 1;
                    veritabani.Entry(tik).State = EntityState.Modified;
                    veritabani.SaveChanges();
                }
                else
                {
                    if (Request.Cookies["ID"]["HaberID"] != Convert.ToString(tik.gonderiid))
                    {
                        int a = Convert.ToInt32(tik.tiklanma_say);
                        tik.tiklanma_say = a + 1;
                        veritabani.Entry(tik).State = EntityState.Modified;
                        veritabani.SaveChanges();
                    }
                }
                return View(veritabani.gonderiler.Where(X=>X.gonderiid==tik.gonderiid).ToList());
            }



            else
            {
               List<gonderiler> gonderi = veritabani.gonderiler.OrderByDescending(x => x.saat).Take(3).ToList();
                return View(gonderi);

            }
            
        }
        public ActionResult Sirali_Haber(kategoriler model, int? sayfaNo)
        {
            int _sayfaNo = sayfaNo ?? 1;
            if (model.katid != 0)
            {

                var _gonderiler2 = veritabani.gonderiler.Where(x => x.katid == model.katid).OrderByDescending(x => x.saat).ToPagedList(_sayfaNo, 3);
                return View(_gonderiler2);
            }
            else
            {
                var _gonderiler = veritabani.gonderiler.OrderByDescending(x => x.tiklanma_say).Take(5).ToPagedList(_sayfaNo, 3);
                return View(_gonderiler);
            }

        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Arama_Sonuclari(string Aranacakkelime,int? sayfaNo)
        {
            int _sayfaNo1 = sayfaNo ?? 1;
            if (Aranacakkelime != null)
            {
                
                var _gonderi = veritabani.gonderiler.Where(x => x.baslik.Contains(Aranacakkelime)).OrderByDescending(x=>x.saat).ToPagedList(_sayfaNo1, 3);
                return View(_gonderi);
            }
            else
                return RedirectToAction("Hata", "Admin");
        }
        public ActionResult İletisim()
        {
            return View();
        }
       
    }
}