using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD2
{
    public class ArtKol
    {
        public int ID { get; set; }
        public Artikal Art { get; set; }
        public int Kolicina { get; set; }

        public decimal Zbir { get => Art.IzlaznaCena * Kolicina; }

        public ArtKol() { }
        public ArtKol(Artikal a, int k)
        {
            Art = a;
            Kolicina = k;
        }
    }
}
