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
    public class StanjeProizvodaController : Controller
    {
        private readonly tbpfoiContext _context;

        public StanjeProizvodaController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: StanjeProizvoda
        public async Task<IActionResult> Index()
        {
            var tbpfoiContext = _context.StanjeProizvoda.Include(s => s.JedinicaMjere).Include(s => s.Proizvod);
            return View(await tbpfoiContext.ToListAsync());
        }

        // GET: StanjeProizvoda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanjeProizvoda = await _context.StanjeProizvoda
                .Include(s => s.JedinicaMjere)
                .Include(s => s.Proizvod)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stanjeProizvoda == null)
            {
                return NotFound();
            }

            return View(stanjeProizvoda);
        }

        // GET: StanjeProizvoda/Create
        public IActionResult Create()
        {
            ViewData["JedinicaMjereId"] = new SelectList(_context.JedinicaMjere, "Id", "Naziv");
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Naziv");
            return View();
        }

        // POST: StanjeProizvoda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaksimalnaKolicina,MinimalnaKolicina,Stanje,JedinicaMjereId,ProizvodId")] StanjeProizvoda stanjeProizvoda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stanjeProizvoda);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["JedinicaMjereId"] = new SelectList(_context.JedinicaMjere, "Id", "Naziv", stanjeProizvoda.JedinicaMjereId);
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Naziv", stanjeProizvoda.ProizvodId);
            return View(stanjeProizvoda);
        }

        // GET: StanjeProizvoda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanjeProizvoda = await _context.StanjeProizvoda.SingleOrDefaultAsync(m => m.Id == id);
            if (stanjeProizvoda == null)
            {
                return NotFound();
            }
            ViewData["JedinicaMjereId"] = new SelectList(_context.JedinicaMjere, "Id", "Naziv", stanjeProizvoda.JedinicaMjereId);
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Naziv", stanjeProizvoda.ProizvodId);
            return View(stanjeProizvoda);
        }

        // POST: StanjeProizvoda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaksimalnaKolicina,MinimalnaKolicina,Stanje,JedinicaMjereId,ProizvodId")] StanjeProizvoda stanjeProizvoda)
        {
            if (id != stanjeProizvoda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stanjeProizvoda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanjeProizvodaExists(stanjeProizvoda.Id))
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
            ViewData["JedinicaMjereId"] = new SelectList(_context.JedinicaMjere, "Id", "Naziv", stanjeProizvoda.JedinicaMjereId);
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Naziv", stanjeProizvoda.ProizvodId);
            return View(stanjeProizvoda);
        }

        // GET: StanjeProizvoda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stanjeProizvoda = await _context.StanjeProizvoda
                .Include(s => s.JedinicaMjere)
                .Include(s => s.Proizvod)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stanjeProizvoda == null)
            {
                return NotFound();
            }

            return View(stanjeProizvoda);
        }

        // POST: StanjeProizvoda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stanjeProizvoda = await _context.StanjeProizvoda.SingleOrDefaultAsync(m => m.Id == id);
            _context.StanjeProizvoda.Remove(stanjeProizvoda);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StanjeProizvodaExists(int id)
        {
            return _context.StanjeProizvoda.Any(e => e.Id == id);
        }
    }
}
