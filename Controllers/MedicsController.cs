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
    [Authorize(Policy = "OnlyPacient")]
    public class MedicsController : Controller
    {
        private readonly SpitalContext _context;

        public MedicsController(SpitalContext context)
        {
            _context = context;
        }

        // GET: Medics
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NumeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nume_desc" : "";
            ViewData["SalariuSortParm"] = sortOrder == "Salariu" ? "salariu_desc" : "Salariu";
            ViewData["CurrentFilter"] = searchString;
            var medici = from b in _context.Medici
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                medici = medici.Where(s => s.Nume.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nume_desc":
                    medici = medici.OrderByDescending(b => b.Nume);
                    break;
                case "Salariu":
                    medici = medici.OrderBy(b => b.Salariu);
                    break;
                case "salariu_desc":
                    medici = medici.OrderByDescending(b => b.Salariu);
                    break;
                default:
                    medici = medici.OrderBy(b => b.Nume);
                    break;
            }
            return View(await medici.AsNoTracking().ToListAsync());
        }

        // GET: Medics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var medic = await _context.Medici
             .Include(s => s.Programari)
             .ThenInclude(e => e.Pacient)
             .AsNoTracking()
             .FirstOrDefaultAsync(m => m.ID == id);
            if (medic == null)
            {
                return NotFound();
            }

            return View(medic);
        }

        // GET: Medics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nume,Sef,Salariu")] Medic medic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(medic);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }
            return View(medic);
        }

        // GET: Medics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici.FindAsync(id);
            if (medic == null)
            {
                return NotFound();
            }
            return View(medic);
        }

        // POST: Medics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Medici.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Medic>(
            studentToUpdate,
            "",
            s => s.Sef, s => s.Nume, s => s.Salariu))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Medics/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (medic == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Try again";
            }

            return View(medic);
        }

        // POST: Medics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medic = await _context.Medici.FindAsync(id);
            if (medic == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Medici.Remove(medic);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool MedicExists(int id)
        {
            return _context.Medici.Any(e => e.ID == id);
        }
    }
}
