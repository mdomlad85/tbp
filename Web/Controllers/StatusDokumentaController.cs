using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    public class StatusDokumentaController : Controller
    {
        private readonly tbpfoiContext _context;

        public StatusDokumentaController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: StatusDokumenta
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusDokumenta.ToListAsync());
        }

        // GET: StatusDokumenta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusDokumenta = await _context.StatusDokumenta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (statusDokumenta == null)
            {
                return NotFound();
            }

            return View(statusDokumenta);
        }

        // GET: StatusDokumenta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusDokumenta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] StatusDokumenta statusDokumenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusDokumenta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(statusDokumenta);
        }

        // GET: StatusDokumenta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusDokumenta = await _context.StatusDokumenta.SingleOrDefaultAsync(m => m.Id == id);
            if (statusDokumenta == null)
            {
                return NotFound();
            }
            return View(statusDokumenta);
        }

        // POST: StatusDokumenta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] StatusDokumenta statusDokumenta)
        {
            if (id != statusDokumenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusDokumenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusDokumentaExists(statusDokumenta.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(statusDokumenta);
        }

        // GET: StatusDokumenta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusDokumenta = await _context.StatusDokumenta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (statusDokumenta == null)
            {
                return NotFound();
            }

            return View(statusDokumenta);
        }

        // POST: StatusDokumenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusDokumenta = await _context.StatusDokumenta.SingleOrDefaultAsync(m => m.Id == id);
            _context.StatusDokumenta.Remove(statusDokumenta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StatusDokumentaExists(int id)
        {
            return _context.StatusDokumenta.Any(e => e.Id == id);
        }
    }
}
