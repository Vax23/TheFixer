using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFixer.DomainModel
{
    class Vlasnik
    {
        public int idVlasnika { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string telefon { get; set; }
        public string mail { get; set; }
        public List<Uredjaj> uredjaji { get; set; }
    }
}
