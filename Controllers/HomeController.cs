using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NeaguDenisa_Proiect.Models;
using NeaguDenisa_Proiect.Data;
using Microsoft.EntityFrameworkCore;
using NeaguDenisa_Proiect.Models.SpitalViewModels;

namespace NeaguDenisa_Proiect.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpitalContext _context;
        public HomeController(SpitalContext context)
        {
            _context = context;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //calculeaza numarul de programari efectuate pentru fiecare data calendaristica
        public async Task<ActionResult> Statistics()
        {
            IQueryable<ProgramariGroup> data =
            from order in _context.Programari
            group order by order.DataProgramarii into dateGroup
            select new ProgramariGroup()
            {
                DataProgramarii = dateGroup.Key,
                ProgramariCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
        public IActionResult Chat()
        {
            return View();
        }
    }
}
