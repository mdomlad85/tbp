using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ZaposlenikController : Controller
    {
        private readonly tbpfoiContext _context;

        public ZaposlenikController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: Zaposlenik
        public async Task<IActionResult> Index()
        {
            var tbpfoiContext = _context.Zaposlenik.Include(z => z.Kontakt);
            return View(await tbpfoiContext.ToListAsync());
        }

        // GET: Zaposlenik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik
                .Include(z => z.Kontakt)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(new ZaposlenikViewModel(zaposlenik, _context));
        }

        // GET: Zaposlenik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zaposlenik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([
            Bind("Id,Prezime,Ime,DatumRodjenja,Adresa,Email,Telefon")
            ] ZaposlenikViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.factoryZaposlenik();
                _context.Add(viewModel.Zaposlenik);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: Zaposlenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context
                .Zaposlenik
                .Include(m => m.Kontakt)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (zaposlenik == null)
            {
                return NotFound();
            }
            
            return View(new ZaposlenikViewModel(zaposlenik, _context));
        }

        // POST: Zaposlenik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [
            Bind("Id,Prezime,Ime,DatumRodjenja,Adresa,Email,Telefon")
            ] ZaposlenikViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            viewModel.Zaposlenik = await _context.Zaposlenik.Include(m => m.Kontakt).SingleOrDefaultAsync(m => m.Id == id);
            if (viewModel.Zaposlenik == null)
            {
                return NotFound();
            }

            viewModel.factoryZaposlenik();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Zaposlenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposlenikExists(viewModel.Zaposlenik.Id))
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

            return View(viewModel);
        }

        // GET: Zaposlenik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik
                .Include(z => z.Kontakt)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // POST: Zaposlenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenik = await _context.Zaposlenik.SingleOrDefaultAsync(m => m.Id == id);
            _context.Zaposlenik.Remove(zaposlenik);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ZaposlenikExists(int id)
        {
            return _context.Zaposlenik.Any(e => e.Id == id);
        }
    }
}
