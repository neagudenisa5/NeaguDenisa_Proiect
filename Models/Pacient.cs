using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Models
{
    public class Pacient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PacientID { get; set; }
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public DateTime DataNasterii { get; set; }
        public ICollection<Programare> Programari { get; set; }
    }
}
