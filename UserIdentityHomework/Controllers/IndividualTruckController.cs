using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserIdentityHomework.Models.DB;

namespace UserIdentityHomework.Controllers
{
    public class IndividualTruckController : Controller
    {
        private readonly DAD_TatianaContext _context;

        public IndividualTruckController(DAD_TatianaContext context)
        {
            _context = context;
        }

        // GET: IndividualTruck
        public async Task<IActionResult> Index()
        {
            var dAD_TatianaContext = _context.IndividualTrucks.Include(i => i.TruckModel);
            return View(await dAD_TatianaContext.ToListAsync());
        }

        // GET: IndividualTruck/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IndividualTrucks == null)
            {
                return NotFound();
            }

            var individualTruck = await _context.IndividualTrucks
                .Include(i => i.TruckModel)
                .FirstOrDefaultAsync(m => m.TruckId == id);
            if (individualTruck == null)
            {
                return NotFound();
            }

            return View(individualTruck);
        }

        // GET: IndividualTruck/Create
        public IActionResult Create()
        {
            ViewData["TruckModelId"] = new SelectList(_context.TruckModels, "ModelId", "ModelId");
            return View();
        }

        // POST: IndividualTruck/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckId,Colour,RegistrationNumber,WofexpiryDate,RegistrationExpiryDate,DateImported,ManufactureYear,Status,FuelType,Transmission,DailyRentalPrice,AdvanceDepositRequired,TruckModelId")] IndividualTruck individualTruck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(individualTruck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TruckModelId"] = new SelectList(_context.TruckModels, "ModelId", "ModelId", individualTruck.TruckModelId);
            return View(individualTruck);
        }

        // GET: IndividualTruck/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IndividualTrucks == null)
            {
                return NotFound();
            }

            var individualTruck = await _context.IndividualTrucks.FindAsync(id);
            if (individualTruck == null)
            {
                return NotFound();
            }
            ViewData["TruckModelId"] = new SelectList(_context.TruckModels, "ModelId", "ModelId", individualTruck.TruckModelId);
            return View(individualTruck);
        }

        // POST: IndividualTruck/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TruckId,Colour,RegistrationNumber,WofexpiryDate,RegistrationExpiryDate,DateImported,ManufactureYear,Status,FuelType,Transmission,DailyRentalPrice,AdvanceDepositRequired,TruckModelId")] IndividualTruck individualTruck)
        {
            if (id != individualTruck.TruckId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(individualTruck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndividualTruckExists(individualTruck.TruckId))
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
            ViewData["TruckModelId"] = new SelectList(_context.TruckModels, "ModelId", "ModelId", individualTruck.TruckModelId);
            return View(individualTruck);
        }

        // GET: IndividualTruck/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IndividualTrucks == null)
            {
                return NotFound();
            }

            var individualTruck = await _context.IndividualTrucks
                .Include(i => i.TruckModel)
                .FirstOrDefaultAsync(m => m.TruckId == id);
            if (individualTruck == null)
            {
                return NotFound();
            }

            return View(individualTruck);
        }

        // POST: IndividualTruck/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IndividualTrucks == null)
            {
                return Problem("Entity set 'DAD_TatianaContext.IndividualTrucks'  is null.");
            }
            var individualTruck = await _context.IndividualTrucks.FindAsync(id);
            if (individualTruck != null)
            {
                _context.IndividualTrucks.Remove(individualTruck);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndividualTruckExists(int id)
        {
          return (_context.IndividualTrucks?.Any(e => e.TruckId == id)).GetValueOrDefault();
        }
    }
}
