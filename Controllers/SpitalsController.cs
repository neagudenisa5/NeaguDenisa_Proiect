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
using NeaguDenisa_Proiect.Models.SpitalViewModels;

namespace NeaguDenisa_Proiect.Controllers
{
    [Authorize(Policy = "OnlyPacient")]
    public class SpitalsController : Controller
    {
        private readonly SpitalContext _context;

        public SpitalsController(SpitalContext context)
        {
            _context = context;
        }

        // GET: Spitals
        public async Task<IActionResult> Index(int? id, int? medicID)
        {
            var viewModel = new SpitalIndexData();
            viewModel.Spitale = await _context.Spitale
            .Include(i => i.SpitalMedici)
            .ThenInclude(i => i.Medic)
            .ThenInclude(i => i.Programari)
            .ThenInclude(i => i.Pacient)
            .AsNoTracking()
            .OrderBy(i => i.NumeSpital)
            .ToListAsync();
            if (id != null)
            {
                ViewData["SpitalID"] = id.Value;
                Spital spital = viewModel.Spitale.Where(
                i => i.ID == id.Value).Single();
                viewModel.Medici = spital.SpitalMedici.Select(s => s.Medic);
            }
            if (medicID != null)
            {
                ViewData["MedicID"] = medicID.Value;
                viewModel.Programari = viewModel.Medici.Where(
                x => x.ID == medicID).Single().Programari;
            }
            return View(viewModel);
        }

        // GET: Spitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spital = await _context.Spitale
                .FirstOrDefaultAsync(m => m.ID == id);
            if (spital == null)
            {
                return NotFound();
            }

            return View(spital);
        }

        // GET: Spitals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NumeSpital,Adresa")] Spital spital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(spital);
        }

        // GET: Spitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var spital = await _context.Spitale
            .Include(i => i.SpitalMedici).ThenInclude(i => i.Medic)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (spital == null)
            {
                return NotFound();
            }
            PopulateSpitalMedicData(spital);
            return View(spital);

        }
        private void PopulateSpitalMedicData(Spital spital)
        {
            var allMedici = _context.Medici;
            var spitalMedici = new HashSet<int>(spital.SpitalMedici.Select(c => c.MedicID));
            var viewModel = new List<SpitalMedicData>();
            foreach (var medic in allMedici)
            {
                viewModel.Add(new SpitalMedicData
                {
                    MedicID = medic.ID,
                    Nume = medic.Nume,
                    IsAngajat = spitalMedici.Contains(medic.ID)
                });
            }
            ViewData["Medici"] = viewModel;
        }

        // POST: Spitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedMedici)
        {
            if (id == null)
            {
                return NotFound();
            }
            var spitalToUpdate = await _context.Spitale
            .Include(i => i.SpitalMedici)
            .ThenInclude(i => i.Medic)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Spital>(spitalToUpdate,"",
            i => i.NumeSpital, i => i.Adresa))
            {
                UpdateSpitalMedici(selectedMedici, spitalToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateSpitalMedici(selectedMedici, spitalToUpdate);
            PopulateSpitalMedicData(spitalToUpdate);
            return View(spitalToUpdate);
        }
        private void UpdateSpitalMedici(string[] selectedMedici, Spital spitalToUpdate)
        {
            if (selectedMedici == null)
            {
                spitalToUpdate.SpitalMedici = new List<SpitalMedic>();
                return;
            }
            var selectedMediciHS = new HashSet<string>(selectedMedici);
            var spitalMedici = new HashSet<int>
            (spitalToUpdate.SpitalMedici.Select(c => c.Medic.ID));
            foreach (var medic in _context.Medici)
            {
                if (selectedMediciHS.Contains(medic.ID.ToString()))
                {
                    if (!spitalMedici.Contains(medic.ID))
                    {
                        spitalToUpdate.SpitalMedici.Add(new SpitalMedic
                        {
                            SpitalID = spitalToUpdate.ID,
                            MedicID = medic.ID
                        });
                    }
                }
                else
                {
                    if (spitalMedici.Contains(medic.ID))
                    {
                        SpitalMedic medicToRemove = spitalToUpdate.SpitalMedici.FirstOrDefault(i => i.MedicID == medic.ID);
                        _context.Remove(medicToRemove);
                    }
                }
            }
        }

        // GET: Spitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spital = await _context.Spitale
                .FirstOrDefaultAsync(m => m.ID == id);
            if (spital == null)
            {
                return NotFound();
            }

            return View(spital);
        }

        // POST: Spitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spital = await _context.Spitale.FindAsync(id);
            _context.Spitale.Remove(spital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpitalExists(int id)
        {
            return _context.Spitale.Any(e => e.ID == id);
        }
    }
}
