using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Models
{
    public class Spital
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nume")]
        [StringLength(50)]
        public string NumeSpital { get; set; }

        [StringLength(70)]
        public string Adresa { get; set; }

        //un spital poate detine mai multi medici
        public ICollection<SpitalMedic> SpitalMedici { get; set; }
    }
}
