using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Models
{
    public class Kategori
    {
        [Display(Name = "Kategori Id")]

        public int Id { get; set; }
        [Required]
        [Display(Name = "Kategori Adı")]

        public string Ad { get; set; }
        public List<Urun> Urunler { get; set; }
    }
}
