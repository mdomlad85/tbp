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
    public class JedinicaMjereController : Controller
    {
        private readonly tbpfoiContext _context;

        public JedinicaMjereController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: JedinicaMjere
        public async Task<IActionResult> Index()
        {
            return View(await _context.JedinicaMjere.ToListAsync());
        }

        // GET: JedinicaMjere/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jedinicaMjere = await _context.JedinicaMjere
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jedinicaMjere == null)
            {
                return NotFound();
            }

            return View(jedinicaMjere);
        }

        // GET: JedinicaMjere/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JedinicaMjere/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,Opis")] JedinicaMjere jedinicaMjere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jedinicaMjere);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(jedinicaMjere);
        }

        // GET: JedinicaMjere/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jedinicaMjere = await _context.JedinicaMjere.SingleOrDefaultAsync(m => m.Id == id);
            if (jedinicaMjere == null)
            {
                return NotFound();
            }
            return View(jedinicaMjere);
        }

        // POST: JedinicaMjere/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,Opis")] JedinicaMjere jedinicaMjere)
        {
            if (id != jedinicaMjere.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jedinicaMjere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JedinicaMjereExists(jedinicaMjere.Id))
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
            return View(jedinicaMjere);
        }

        // GET: JedinicaMjere/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jedinicaMjere = await _context.JedinicaMjere
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jedinicaMjere == null)
            {
                return NotFound();
            }

            return View(jedinicaMjere);
        }

        // POST: JedinicaMjere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jedinicaMjere = await _context.JedinicaMjere.SingleOrDefaultAsync(m => m.Id == id);
            _context.JedinicaMjere.Remove(jedinicaMjere);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool JedinicaMjereExists(int id)
        {
            return _context.JedinicaMjere.Any(e => e.Id == id);
        }
    }
}
