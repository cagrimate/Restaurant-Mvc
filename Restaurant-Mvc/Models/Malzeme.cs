using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Models
{
    public class Malzeme
    {
        public int Id { get; set; }
        [Required]

        public string Ad { get; set; }
        public List<UrunMalzeme> UrunMalzemeler { get; set; }
    }
}
