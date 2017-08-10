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
    public class VrstaPartneraController : Controller
    {
        private readonly tbpfoiContext _context;

        public VrstaPartneraController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: VrstaPartnera
        public async Task<IActionResult> Index()
        {
            return View(await _context.VrstaPartnera.ToListAsync());
        }

        // GET: VrstaPartnera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaPartnera = await _context.VrstaPartnera
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaPartnera == null)
            {
                return NotFound();
            }

            return View(vrstaPartnera);
        }

        // GET: VrstaPartnera/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaPartnera/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] VrstaPartnera vrstaPartnera)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaPartnera);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vrstaPartnera);
        }

        // GET: VrstaPartnera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaPartnera = await _context.VrstaPartnera.SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaPartnera == null)
            {
                return NotFound();
            }
            return View(vrstaPartnera);
        }

        // POST: VrstaPartnera/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] VrstaPartnera vrstaPartnera)
        {
            if (id != vrstaPartnera.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaPartnera);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaPartneraExists(vrstaPartnera.Id))
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
            return View(vrstaPartnera);
        }

        // GET: VrstaPartnera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaPartnera = await _context.VrstaPartnera
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaPartnera == null)
            {
                return NotFound();
            }

            return View(vrstaPartnera);
        }

        // POST: VrstaPartnera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vrstaPartnera = await _context.VrstaPartnera.SingleOrDefaultAsync(m => m.Id == id);
            _context.VrstaPartnera.Remove(vrstaPartnera);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VrstaPartneraExists(int id)
        {
            return _context.VrstaPartnera.Any(e => e.Id == id);
        }
    }
}
