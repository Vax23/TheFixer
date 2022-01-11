using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFixer.DomainModel
{
    class Uredjaj
    {
        public int idUredjaja { get; set; }
        public string vrsta { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public Vlasnik vlasnik { get; set; }
        public List<Popravka> popravke { get; set; }
    }
}
