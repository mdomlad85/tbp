using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/vrstadokumenta")]
    public class VrstaDokumentaController : Controller
    {
        private readonly tbpfoiContext _context;

        public VrstaDokumentaController(tbpfoiContext context)
        {
            _context = context;
        }

        // GET: api/VrstaDokumenta
        [HttpGet]
        public IEnumerable<VrstaDokumenta> GetVrstaDokumenta()
        {
            return _context.VrstaDokumenta;
        }

        // GET: api/VrstaDokumenta/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVrstaDokumenta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vrstaDokumenta = await _context.VrstaDokumenta.SingleOrDefaultAsync(m => m.Id == id);

            if (vrstaDokumenta == null)
            {
                return NotFound();
            }

            return Ok(vrstaDokumenta);
        }

        // PUT: api/VrstaDokumenta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVrstaDokumenta([FromRoute] int id, [FromBody] VrstaDokumenta vrstaDokumenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vrstaDokumenta.Id)
            {
                return BadRequest();
            }

            _context.Entry(vrstaDokumenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VrstaDokumentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VrstaDokumenta
        [HttpPost]
        public async Task<IActionResult> PostVrstaDokumenta([FromBody] VrstaDokumenta vrstaDokumenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VrstaDokumenta.Add(vrstaDokumenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVrstaDokumenta", new { id = vrstaDokumenta.Id }, vrstaDokumenta);
        }

        // DELETE: api/VrstaDokumenta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVrstaDokumenta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vrstaDokumenta = await _context.VrstaDokumenta.SingleOrDefaultAsync(m => m.Id == id);
            if (vrstaDokumenta == null)
            {
                return NotFound();
            }

            _context.VrstaDokumenta.Remove(vrstaDokumenta);
            await _context.SaveChangesAsync();

            return Ok(vrstaDokumenta);
        }

        private bool VrstaDokumentaExists(int id)
        {
            return _context.VrstaDokumenta.Any(e => e.Id == id);
        }
    }
}