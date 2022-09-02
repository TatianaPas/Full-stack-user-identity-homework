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
    public class TruckCustomerController : Controller
    {
        private readonly DAD_TatianaContext _context;

        public TruckCustomerController(DAD_TatianaContext context)
        {
            _context = context;
        }

        // GET: TruckCustomer
        public async Task<IActionResult> Index()
        {
            var dAD_TatianaContext = _context.TruckCustomers.Include(t => t.Customer);
            return View(await dAD_TatianaContext.ToListAsync());
        }

        // GET: TruckCustomer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TruckCustomers == null)
            {
                return NotFound();
            }

            var truckCustomer = await _context.TruckCustomers
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (truckCustomer == null)
            {
                return NotFound();
            }

            return View(truckCustomer);
        }

        // GET: TruckCustomer/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.TruckPeople, "PersonId", "PersonId");
            return View();
        }

        // POST: TruckCustomer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,LicenseNumber,Age,LicenseExpiryDate")] TruckCustomer truckCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.TruckPeople, "PersonId", "PersonId", truckCustomer.CustomerId);
            return View(truckCustomer);
        }

        // GET: TruckCustomer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TruckCustomers == null)
            {
                return NotFound();
            }

            var truckCustomer = await _context.TruckCustomers.FindAsync(id);
            if (truckCustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.TruckPeople, "PersonId", "PersonId", truckCustomer.CustomerId);
            return View(truckCustomer);
        }

        // POST: TruckCustomer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,LicenseNumber,Age,LicenseExpiryDate")] TruckCustomer truckCustomer)
        {
            if (id != truckCustomer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckCustomerExists(truckCustomer.CustomerId))
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
            ViewData["CustomerId"] = new SelectList(_context.TruckPeople, "PersonId", "PersonId", truckCustomer.CustomerId);
            return View(truckCustomer);
        }

        // GET: TruckCustomer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TruckCustomers == null)
            {
                return NotFound();
            }

            var truckCustomer = await _context.TruckCustomers
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (truckCustomer == null)
            {
                return NotFound();
            }

            return View(truckCustomer);
        }

        // POST: TruckCustomer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TruckCustomers == null)
            {
                return Problem("Entity set 'DAD_TatianaContext.TruckCustomers'  is null.");
            }
            var truckCustomer = await _context.TruckCustomers.FindAsync(id);
            if (truckCustomer != null)
            {
                _context.TruckCustomers.Remove(truckCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckCustomerExists(int id)
        {
          return (_context.TruckCustomers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
