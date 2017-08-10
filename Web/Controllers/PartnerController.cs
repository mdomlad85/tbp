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
    public class PartnerController : Controller
    {
        private readonly tbpfoiContext _context;

        public PartnerController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: Partners
        public async Task<IActionResult> Index()
        {
            var tbpfoiContext = _context.Partner.Include(p => p.Vrsta);
            return View(await tbpfoiContext.ToListAsync());
        }

        // GET: Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Vrsta)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Partners/Create
        public IActionResult Create()
        {
            ViewData["VrstaId"] = new SelectList(_context.VrstaPartnera, "Id", "Naziv");
            return View();
        }

        // POST: Partners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,DatumKreiranja,VrstaId")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(partner);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["VrstaId"] = new SelectList(_context.VrstaPartnera, "Id", "Naziv", partner.VrstaId);
            return View(partner);
        }

        // GET: Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.SingleOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }
            ViewData["VrstaId"] = new SelectList(_context.VrstaPartnera, "Id", "Naziv", partner.VrstaId);
            return View(partner);
        }

        // POST: Partners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,DatumKreiranja,VrstaId")] Partner partner)
        {
            if (id != partner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.Id))
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
            ViewData["VrstaId"] = new SelectList(_context.VrstaPartnera, "Id", "Naziv", partner.VrstaId);
            return View(partner);
        }

        // GET: Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner
                .Include(p => p.Vrsta)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partner.SingleOrDefaultAsync(m => m.Id == id);
            _context.Partner.Remove(partner);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PartnerExists(int id)
        {
            return _context.Partner.Any(e => e.Id == id);
        }
    }
}
