using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_Mvc.Models
{
    public class UrunMalzeme
    {
        public Malzeme Malzeme { get; set; }
        public int MalzemeId { get; set; }
        public Urun Urun { get; set; }
        public int UrunId { get; set; }

    }
}
