using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Models
{
    public class Medic
    {
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Sef { get; set; }
        public decimal Salariu { get; set; }
        public ICollection<Programare> Programari{ get; set; }

        //un medic poate lucra la mai multe spitale
        public ICollection<SpitalMedic> SpitalMedici { get; set; }

    }
}
