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
    public class TruckRentalController : Controller
    {
        private readonly DAD_TatianaContext _context;

        public TruckRentalController(DAD_TatianaContext context)
        {
            _context = context;
        }

        // GET: TruckRental
        public async Task<IActionResult> Index()
        {
            var dAD_TatianaContext = _context.TruckRentals.Include(t => t.Customer).Include(t => t.Truck);
            return View(await dAD_TatianaContext.ToListAsync());
        }

        // GET: TruckRental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckRentals == null)
            {
                return NotFound();
            }

            var truckRental = await _context.TruckRentals
                .Include(t => t.Customer)
                .Include(t => t.Truck)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (truckRental == null)
            {
                return NotFound();
            }

            return View(truckRental);
        }

        // GET: TruckRental/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.TruckCustomers, "CustomerId", "CustomerId");
            ViewData["TruckId"] = new SelectList(_context.IndividualTrucks, "TruckId", "TruckId");
            return View();
        }

        // POST: TruckRental/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,TruckId,CustomerId,RentDate,ReturnDueDate,ReturnDate,TotalPrice")] TruckRental truckRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.TruckCustomers, "CustomerId", "CustomerId", truckRental.CustomerId);
            ViewData["TruckId"] = new SelectList(_context.IndividualTrucks, "TruckId", "TruckId", truckRental.TruckId);
            return View(truckRental);
        }

        // GET: TruckRental/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckRentals == null)
            {
                return NotFound();
            }

            var truckRental = await _context.TruckRentals.FindAsync(id);
            if (truckRental == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.TruckCustomers, "CustomerId", "CustomerId", truckRental.CustomerId);
            ViewData["TruckId"] = new SelectList(_context.IndividualTrucks, "TruckId", "TruckId", truckRental.TruckId);
            return View(truckRental);
        }

        // POST: TruckRental/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,TruckId,CustomerId,RentDate,ReturnDueDate,ReturnDate,TotalPrice")] TruckRental truckRental)
        {
            if (id != truckRental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckRental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckRentalExists(truckRental.RentalId))
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
            ViewData["CustomerId"] = new SelectList(_context.TruckCustomers, "CustomerId", "CustomerId", truckRental.CustomerId);
            ViewData["TruckId"] = new SelectList(_context.IndividualTrucks, "TruckId", "TruckId", truckRental.TruckId);
            return View(truckRental);
        }

        // GET: TruckRental/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckRentals == null)
            {
                return NotFound();
            }

            var truckRental = await _context.TruckRentals
                .Include(t => t.Customer)
                .Include(t => t.Truck)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (truckRental == null)
            {
                return NotFound();
            }

            return View(truckRental);
        }

        // POST: TruckRental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckRentals == null)
            {
                return Problem("Entity set 'DAD_TatianaContext.TruckRentals'  is null.");
            }
            var truckRental = await _context.TruckRentals.FindAsync(id);
            if (truckRental != null)
            {
                _context.TruckRentals.Remove(truckRental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckRentalExists(int id)
        {
          return (_context.TruckRentals?.Any(e => e.RentalId == id)).GetValueOrDefault();
        }
    }
}
