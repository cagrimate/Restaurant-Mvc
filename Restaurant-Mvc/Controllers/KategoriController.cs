using Microsoft.AspNetCore.Mvc;
using Restaurant_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Controllers
{
    public class KategoriController : Controller
    {
        private readonly AppDbContext _db;

        public KategoriController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Kategori> kategoriListesi = _db.Kategoriler.ToList();
            return View(kategoriListesi);
        }
        public IActionResult KategoriEkle()
        {
            return View();

        }
        [HttpPost]
        public IActionResult KategoriEkle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _db.Kategoriler.Add(kategori);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult KategoriGuncelle(int id)
        {
            Kategori kategori = _db.Kategoriler.Find(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }
        [HttpPost]
        public IActionResult KategoriGuncelle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _db.Kategoriler.Update(kategori);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }
        public IActionResult KategoriSil(int id)
        {
            Kategori kategori = _db.Kategoriler.Find(id);
            if (kategori==null)
            {
                return NotFound();
            }
            return View(kategori);
        }
        [HttpPost]
        [ActionName("KategoriSil")]
        public IActionResult SilOnayla(int id)
        {
            Kategori silinecekKategori = _db.Kategoriler.Find(id);
            if (silinecekKategori!=null)
            {
                _db.Kategoriler.Remove(silinecekKategori);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
