using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NeaguDenisa_Proiect.Data;
using NeaguDenisa_Proiect.Models;

namespace NeaguDenisa_Proiect.Controllers
{
    [Authorize(Policy = "OnlyManager")]
    public class ProgramaresController : Controller
    {
        private readonly SpitalContext _context;

        public ProgramaresController(SpitalContext context)
        {
            _context = context;
        }

        // GET: Programares
        public async Task<IActionResult> Index()
        {
            var spitalContext = _context.Programari.Include(p => p.Medic).Include(p => p.Pacient);
            return View(await spitalContext.ToListAsync());
        }

        // GET: Programares/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari
                .Include(p => p.Medic)
                .Include(p => p.Pacient)
                .FirstOrDefaultAsync(m => m.ProgramareID == id);
            if (programare == null)
            {
                return NotFound();
            }

            return View(programare);
        }

        // GET: Programares/Create
        public IActionResult Create()
        {
            ViewData["MedicID"] = new SelectList(_context.Medici, "ID", "ID");
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID");
            return View();
        }

        // POST: Programares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramareID,PacientID,MedicID,DataProgramarii")] Programare programare)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programare);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicID"] = new SelectList(_context.Medici, "ID", "ID", programare.MedicID);
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID", programare.PacientID);
            return View(programare);
        }

        // GET: Programares/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari.FindAsync(id);
            if (programare == null)
            {
                return NotFound();
            }
            ViewData["MedicID"] = new SelectList(_context.Medici, "ID", "ID", programare.MedicID);
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID", programare.PacientID);
            return View(programare);
        }

        // POST: Programares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramareID,PacientID,MedicID,DataProgramarii")] Programare programare)
        {
            if (id != programare.ProgramareID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programare);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramareExists(programare.ProgramareID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicID"] = new SelectList(_context.Medici, "ID", "ID", programare.MedicID);
            ViewData["PacientID"] = new SelectList(_context.Pacienti, "PacientID", "PacientID", programare.PacientID);
            return View(programare);
        }

        // GET: Programares/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programare = await _context.Programari
                .Include(p => p.Medic)
                .Include(p => p.Pacient)
                .FirstOrDefaultAsync(m => m.ProgramareID == id);
            if (programare == null)
            {
                return NotFound();
            }

            return View(programare);
        }

        // POST: Programares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programare = await _context.Programari.FindAsync(id);
            _context.Programari.Remove(programare);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramareExists(int id)
        {
            return _context.Programari.Any(e => e.ProgramareID == id);
        }
    }
}
