using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Models
{
    public class SpitalMedic
    {
        public int SpitalID { get; set; }
        public int MedicID { get; set; }
        public Spital Spital { get; set; }
        public Medic Medic { get; set; }
    }
}
