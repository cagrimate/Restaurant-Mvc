using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant_Mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Controllers
{
    public class UrunController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UrunController(AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Urun> urunListesi = _db.Urunler.ToList();
            return View(_db.Urunler.Include(x => x.Kategori).Include(x => x.UrunMalzemeler).ThenInclude(x => x.Malzeme).ToList());
        }
        public IActionResult Ekle()
        {
            ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "Id", "Ad");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ekle(Urun urun, IFormFile resim)
        {
            ResimHataKontrolleri(resim);
            if (ModelState.IsValid)
            {
                urun.ResimYolu = ResimYukle(resim);
                _db.Urunler.Add(urun);
                _db.SaveChanges();
                return RedirectToAction("Index", "Urun");

            }
            return View();
        }

        private string ResimYukle(IFormFile resim)
        {
            string ext = Path.GetExtension(resim.FileName);
            string resimAd = Guid.NewGuid() + ext;
            string dosyaYolu = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploads", resimAd);
            using (var stream = new FileStream(dosyaYolu, FileMode.Create))
            {
                resim.CopyTo(stream);
            }
            return resimAd;

        }

        private void ResimHataKontrolleri(IFormFile resim)
        {
            string[] izinVerilenler = { ".jpg", ".png", ".jpeg" };
            if (resim != null)
            {
                string ext = Path.GetExtension(resim.FileName);
                if (!izinVerilenler.Contains(ext))
                {
                    ModelState.AddModelError("resim", "İzin verilen dosya uzantıları jpeg jpg png");
                }
                else if (resim.Length > 1000 * 1000 * 1)//1mb
                {
                    ModelState.AddModelError("resim", "maximim dosya boyutu 1mb");
                }
            }
        }
        public IActionResult Sil(int id)
        {
            Urun silinecekUrun = _db.Urunler.FirstOrDefault(x => x.Id == id);
            if (silinecekUrun == null)
            {
                return NotFound();
            }
            return View(silinecekUrun);
        }
        [HttpPost]
        [ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public IActionResult SilOnay(int id)
        {
            Urun silinecekUrun = _db.Urunler.Find(id);
            _db.Urunler.Remove(silinecekUrun);
            _db.SaveChanges();
            return RedirectToAction("Index", "Urun");
            ResimSil(silinecekUrun.ResimYolu);
            TempData["mesaj"] = "urun basarıyla silindi";

        }

        private void ResimSil(string resimYolu)
        {
            if (!string.IsNullOrEmpty(resimYolu))
            {
                var silYol = Path.Combine(_webHostEnvironment.WebRootPath, "images", "uploads", resimYolu);
                if (System.IO.File.Exists(silYol))
                {
                    System.IO.File.Delete(silYol);
                }
            }

        }

        public IActionResult Duzenle(int id)
        {
            Urun duzenlenecekUrun = _db.Urunler.FirstOrDefault(x => x.Id == id);
            if (duzenlenecekUrun == null)
            {
                return NotFound();
            }
            ViewBag.Kategoriler = new SelectList(_db.Kategoriler.ToList(), "Id", "Ad");
            return View(duzenlenecekUrun);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Duzenle(Urun urun, IFormFile resim)
        {
            if (resim != null)
            {
                ResimHataKontrolleri(resim);
            }
            if (ModelState.IsValid)
            {
                if (resim != null)
                {
                    ResimSil(urun.ResimYolu);
                    urun.ResimYolu = ResimYukle(resim);
                }
                _db.Update(urun);
                _db.SaveChanges();
                return RedirectToAction("Index", "Urun");
            }
            return View(urun);
        }
        public IActionResult MalzemeDuzenle(int id)
        {
            Urun urun = _db.Urunler.Include(x => x.UrunMalzemeler).ThenInclude(x => x.Malzeme).FirstOrDefault(x => x.Id == id);
            if (urun == null)
            {
                return NotFound();
            }
            SelectList selectLists = new SelectList(_db.Malzemeler.ToList(), "Id", "Ad");
            foreach (var item in selectLists)
            {
                if (urun.UrunMalzemeler.Select(x => x.MalzemeId).ToList().Contains(Convert.ToInt32(item.Value)))
                {
                    item.Selected = true;
                }
            }
            ViewBag.Malzemeler = selectLists;

            return View(id);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MalzemeDuzenle(int id, List<int> ids)
        {
            Urun urun = _db.Urunler.Include(x => x.UrunMalzemeler).FirstOrDefault(x => x.Id == id);
            if (urun == null)
            {
                return NotFound();
            }
            foreach (var item in urun.UrunMalzemeler)
            {
                _db.UrunMalzemeler.Remove(item);

            }
            if (ids.Count != 0)
            {
                List<UrunMalzeme> urunMalzemeler = new List<UrunMalzeme>();
                foreach (var item in ids)
                {
                    urunMalzemeler.Add(new UrunMalzeme()
                    {
                        MalzemeId = item,
                        UrunId = id
                    });
                }
                urun.UrunMalzemeler = urunMalzemeler;
            }
            _db.SaveChanges();
            return RedirectToAction("Index", "Urun");
        }

    }
}
