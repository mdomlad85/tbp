
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    public class KontaktController : Controller
    {
        private readonly tbpfoiContext _context;

        public KontaktController(tbpfoiContext context)
        {
            _context = context;
        }

        // GET: Kontakt/Delete/5
        public async Task<IActionResult> Delete(int id, string akcija)
        {
            var partnerKontakt = await _context.PartnerKontakt
                .Include(m => m.Kontakt)
                .SingleOrDefaultAsync(m => m.KontaktId == id);
            var partnerId = partnerKontakt.PartnerId;
            var kontakt = partnerKontakt.Kontakt;
            _context.PartnerKontakt.Remove(partnerKontakt);
            _context.Kontakt.Remove(kontakt);
            await _context.SaveChangesAsync();

            return RedirectToAction(akcija, "Partner", new { id =  partnerId });
        }
    }
}