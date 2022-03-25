using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Models
{
    public class Urun
    {
        public int Id { get; set; }
        [Required]
        public string Ad { get; set; }
        [MaxLength()]
        public string Tanim { get; set; }

        [Column(TypeName = "decimal(8,2)")]       

        public decimal Fiyat { get; set; }        

        public string ResimYolu { get; set; }

        public int KategoriId { get; set; }
        public Kategori Kategori { get; set; }
        public List<UrunMalzeme> UrunMalzemeler { get; set; }
    }
}
