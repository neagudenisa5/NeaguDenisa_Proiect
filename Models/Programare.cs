using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Models
{
    public class Programare
    {
        public int ProgramareID { get; set; }
        public int PacientID { get; set; }
        public int MedicID { get; set; }
        public DateTime DataProgramarii { get; set; }

        public Pacient Pacient { get; set; }
        public Medic Medic{ get; set; }
    }
}
