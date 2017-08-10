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
    public class VrstaDokumentaController : Controller
    {
        private readonly tbpfoiContext _context;

        public VrstaDokumentaController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: VrstaDokumenta
        public async Task<IActionResult> Index()
        {
            return View(await _context.VrstaDokumenta.ToListAsync());
        }

        // GET: VrstaDokumenta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaDokumenta = await _context.VrstaDokumenta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaDokumenta == null)
            {
                return NotFound();
            }

            return View(vrstaDokumenta);
        }

        // GET: VrstaDokumenta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VrstaDokumenta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv")] VrstaDokumenta vrstaDokumenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vrstaDokumenta);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(vrstaDokumenta);
        }

        // GET: VrstaDokumenta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaDokumenta = await _context.VrstaDokumenta.SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaDokumenta == null)
            {
                return NotFound();
            }
            return View(vrstaDokumenta);
        }

        // POST: VrstaDokumenta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv")] VrstaDokumenta vrstaDokumenta)
        {
            if (id != vrstaDokumenta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vrstaDokumenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VrstaDokumentaExists(vrstaDokumenta.Id))
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
            return View(vrstaDokumenta);
        }

        // GET: VrstaDokumenta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vrstaDokumenta = await _context.VrstaDokumenta
                .SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaDokumenta == null)
            {
                return NotFound();
            }

            return View(vrstaDokumenta);
        }

        // POST: VrstaDokumenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vrstaDokumenta = await _context.VrstaDokumenta.SingleOrDefaultAsync(m => m.Id == id);
            _context.VrstaDokumenta.Remove(vrstaDokumenta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool VrstaDokumentaExists(int id)
        {
            return _context.VrstaDokumenta.Any(e => e.Id == id);
        }
    }
}
