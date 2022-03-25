using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Controllers
{
    public class MenuController : Controller
    {
        private readonly AppDbContext _db;

        public MenuController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Kategoriler.Include(x => x.Urunler)
                .ThenInclude(x => x.UrunMalzemeler)
                .ThenInclude(x => x.Malzeme)
                .ToList());
        }
    }
}
