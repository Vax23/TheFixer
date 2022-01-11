using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFixer.DomainModel
{
    class Popravka
    {
        public int idPopravke { get; set; }
        public string datum { get; set; }
        public string opis { get; set; }
        public double cenaPopravke { get; set; }
        public Uredjaj uredjaj { get; set; }
        public Majstor majstor { get; set; }
    }
}
