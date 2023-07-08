using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;
using MyStore.Models;

namespace MyStore.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var model = _context.Sales.Include(s => s.Client);
            return View(await model.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] =
                new SelectList((from c in _context.Clients.ToList()
                    select new
                    {
                        Id = c.Id,
                        FullName = c.CustomerNumber + " / " + c.FullName
                    }), "Id", "FullName");
            
            ViewBag.Products = _context.Products.ToList();
            
            var model = new Sale
            {
                Date = DateTime.Now,
                Time = DateTime.Now,
                Status = SaleStatus.Pending,
                Paid = false
            };
            
            return View(model);
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,SaleIdentifier,Date,Time,ClientId,Observations,FinalValue,Status,Paid")]
            Sale sale, IFormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
                
                // Save the products associated with the sale
                var productCount = (formCollection.Count - 7) / 5;
                
                for (int i = 1; i <= productCount; i++) // Assuming there are two products in the form
                {
                    var saleProduct = new SaleProduct
                    {
                        SaleId = sale.Id,
                        ProductId = int.Parse(formCollection[$"Products_{i}_Id"]),
                        Quantity = int.Parse(formCollection[$"Products_{i}_Quantity"]),
                        SumValue = decimal.Parse(formCollection[$"Products_{i}_Sum"])
                    };

                    _context.SaleProducts.Add(saleProduct);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Address", sale.ClientId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Address", sale.ClientId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,SaleIdentifier,Date,Time,ClientId,Observations,FinalValue,Status,Paid")]
            Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
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

            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Address", sale.ClientId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sales'  is null.");
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}