using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhosLeft.Data;

namespace WhosLeft.Controllers
{
    public class AbsenceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AbsenceTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AbsenceTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AbsenceTypes.ToListAsync());
        }

        // GET: AbsenceTypes/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // parametrization - key for preventing the SQL injection attacks

            var absenceType = await _context.AbsenceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absenceType == null)
            {
                return NotFound();
            }

            return View(absenceType);
        }

        // GET: AbsenceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AbsenceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberOfDays")] AbsenceType absenceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(absenceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(absenceType);
        }

        // GET: AbsenceTypes/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceType = await _context.AbsenceTypes.FindAsync(id);
            if (absenceType == null)
            {
                return NotFound();
            }
            return View(absenceType);
        }

        // POST: AbsenceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Id,Name,NumberOfDays")] AbsenceType absenceType)
        {
            if (id != absenceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(absenceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbsenceTypeExists(absenceType.Id))
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
            return View(absenceType);
        }

        // GET: AbsenceTypes/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var absenceType = await _context.AbsenceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (absenceType == null)
            {
                return NotFound();
            }

            return View(absenceType);
        }

        // POST: AbsenceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var absenceType = await _context.AbsenceTypes.FindAsync(id);
            if (absenceType != null)
            {
                _context.AbsenceTypes.Remove(absenceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbsenceTypeExists(uint id)
        {
            return _context.AbsenceTypes.Any(e => e.Id == id);
        }
    }
}
