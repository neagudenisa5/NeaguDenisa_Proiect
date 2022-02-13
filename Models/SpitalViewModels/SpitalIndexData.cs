using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Models.SpitalViewModels
{
    public class SpitalIndexData
    {
        public IEnumerable<Spital> Spitale { get; set; }
        public IEnumerable<Medic> Medici { get; set; }
        public IEnumerable<Programare> Programari { get; set; }
    }
}
