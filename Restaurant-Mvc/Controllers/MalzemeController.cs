using Microsoft.AspNetCore.Mvc;
using Restaurant_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Controllers
{
    public class MalzemeController : Controller
    {
        private readonly AppDbContext _db;

        public MalzemeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Malzeme> malzemeListesi = _db.Malzemeler.ToList();

            return View(malzemeListesi);
        }
        public IActionResult MalzemeEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MalzemeEkle(Malzeme malzeme)
        {
            if (ModelState.IsValid)
            {
                _db.Malzemeler.Add(malzeme);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(malzeme);
        }
        public IActionResult MalzemeGuncelle(int id)
        {
            Malzeme malzeme = _db.Malzemeler.Find(id);
            if (malzeme == null)
            {
                return NotFound();
            }
            return View(malzeme);
        }
        [HttpPost]
        public IActionResult MalzemeGuncelle(Malzeme malzeme)
        {
            if (ModelState.IsValid)
            {
                _db.Malzemeler.Update(malzeme);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(malzeme);
        }
        public IActionResult MalzemeSil(int id)
        {
            Malzeme malzeme = _db.Malzemeler.Find(id);
            if (malzeme == null)
            {
                return NotFound();
            }
            return View(malzeme);
        }
        [HttpPost]
        [ActionName("MalzemeSil")]
        public IActionResult SilOnayla(int id)
        {
            Malzeme silinecekMalzeme = _db.Malzemeler.Find(id);
            if (silinecekMalzeme != null)
            {
                _db.Malzemeler.Remove(silinecekMalzeme);
                _db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}
