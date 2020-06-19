using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD2
{
    public class Racun
    {
        public int ID { get; set; }

        //Trenutna dodela samo zbog SQL
        public DateTime VremeIzdavanja { get; set; } = DateTime.Now;
        public List<ArtKol> Artikli { get; set; } = new List<ArtKol>();

        public decimal Total { get => Artikli.Sum(a => a.Zbir); }
    }
}
