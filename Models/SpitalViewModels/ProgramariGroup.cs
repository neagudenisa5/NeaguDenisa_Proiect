using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NeaguDenisa_Proiect.Models.SpitalViewModels
{
    public class ProgramariGroup
    {
        [DataType(DataType.Date)]
        public DateTime? DataProgramarii { get; set; }
        public int ProgramariCount { get; set; }

    }
}
