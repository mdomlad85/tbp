using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Processors;

namespace Web.Controllers
{
    public class DokumentController : Controller
    {
        private readonly tbpfoiContext _context;

        public DokumentController(tbpfoiContext context)
        {
            _context = context;    
        }

        // GET: Dokument
        public async Task<IActionResult> Index()
        {
            var tbpfoiContext = _context.Dokument.Include(d => d.Status).Include(d => d.Vrsta).Include(d => d.Zaposlenik);
            return View(await tbpfoiContext.ToListAsync());
        }

        // GET: Dokument/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokument = await _context.Dokument
                .Include(d => d.Status)
                .Include(d => d.Vrsta)
                .Include(d => d.Zaposlenik)
                .Include(d => d.StavkaDokumenta)
                .Include("StavkaDokumenta.Proizvod")
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dokument == null)
            {
                return NotFound();
            }

            return View(dokument);
        }

        // GET: Dokument/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.StatusDokumenta, "Id", "Naziv");
            ViewData["VrstaId"] = new SelectList(_context.VrstaDokumenta, "Id", "Naziv");
            ViewData["ZaposlenikId"] = new SelectList(_context.Zaposlenik, "Id", "Ime");
            return View();
        }

        // POST: Dokument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DatumKreiranja,DatumAzuriranja,ZaposlenikId,StatusId,VrstaId,Godina")] Dokument dokument)
        {
            if (ModelState.IsValid)
            {
                dokument.DatumKreiranja = DateTime.Now;
                dokument.DatumAzuriranja = DateTime.Now;
                _context.Add(dokument);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["StatusId"] = new SelectList(_context.StatusDokumenta, "Id", "Naziv", dokument.StatusId);
            ViewData["VrstaId"] = new SelectList(_context.VrstaDokumenta, "Id", "Naziv", dokument.VrstaId);
            ViewData["ZaposlenikId"] = new SelectList(_context.Zaposlenik, "Id", "Ime", dokument.ZaposlenikId);
            return View(dokument);
        }

        // GET: Dokument/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokument = await _context.Dokument
                .Include(m => m.Vrsta)
                .Include(m => m.StavkaDokumenta)
                .Include("StavkaDokumenta.Proizvod")
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dokument == null)
            {
                return NotFound();
            }

            ViewData["Narudzbenica"] = "Narudžbenica";
            ViewData["StatusId"] = new SelectList(_context.StatusDokumenta, "Id", "Naziv", dokument.StatusId);
            ViewData["ZaposlenikId"] = new SelectList(_context.Zaposlenik, "Id", "Ime", dokument.ZaposlenikId);
            return View(dokument);
        }

        // POST: Dokument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DatumKreiranja,DatumAzuriranja,ZaposlenikId,StatusId,VrstaId,Godina")] Dokument dokument)
        {
            if (id != dokument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dokument.DatumAzuriranja = DateTime.Now;
                    _context.Update(dokument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DokumentExists(dokument.Id))
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
            ViewData["StatusId"] = new SelectList(_context.StatusDokumenta, "Id", "Naziv", dokument.StatusId);
            ViewData["ZaposlenikId"] = new SelectList(_context.Zaposlenik, "Id", "Ime", dokument.ZaposlenikId);
            return View(dokument);
        }

        // GET: Dokument/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dokument = await _context.Dokument
                .Include(d => d.Status)
                .Include(d => d.Vrsta)
                .Include(d => d.Zaposlenik)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dokument == null)
            {
                return NotFound();
            }

            return View(dokument);
        }

        // POST: Dokument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dokument = await _context.Dokument.SingleOrDefaultAsync(m => m.Id == id);
            _context.Dokument.Remove(dokument);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteStavka(int id)
        {
            var stavkaDokumenta = await _context.StavkaDokumenta.SingleOrDefaultAsync(m => m.Id == id);
            var dokumentId = stavkaDokumenta.DokumentId;
            _context.StavkaDokumenta.Remove(stavkaDokumenta);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = dokumentId });
        }

        private bool DokumentExists(int id)
        {
            return _context.Dokument.Any(e => e.Id == id);
        }

        public ActionResult StavkaModalAction(int Id)
        {
            ViewData["Proizvodi"] = new SelectList(_context.Proizvod, "Id", "Naziv");
            var stavkaDokumenta = new StavkaDokumenta();
            stavkaDokumenta.DokumentId = Id;
            return PartialView("StavkaDokumenta", stavkaDokumenta);
        }

        public async Task<IActionResult> DodajStavku([Bind("DokumentId,ProizvodId,Kolicina")] StavkaDokumenta stavkaDokumenta)
        {
            if (ModelState.IsValid )
            {
                _context.StavkaDokumenta.Add(stavkaDokumenta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Edit", new { id = stavkaDokumenta.DokumentId });
        }

        public async Task<IActionResult> GenerirajNarudzbenicu()
        {
            return RedirectToAction("Edit", new {
                id = await DokumentProcessor.GenerirajNarudžbenicu(_context)
            });
        }

        public async Task<IActionResult> GenerirajPrimkuIzNarudzbenice(int id)
        {
            return RedirectToAction("Edit", new
            {
                id = await DokumentProcessor.GenerirajPrimkuIzNarudzbenice(_context, id)
            });
        }
    }
}
