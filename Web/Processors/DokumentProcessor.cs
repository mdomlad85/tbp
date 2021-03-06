﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Processors
{
    public class DokumentProcessor
    {
        public static async Task<int> GenerirajNarudzbenicu(tbpfoiContext context)
        {
            Dokument dokument = new Dokument();
            dokument.ZaposlenikId = context.Zaposlenik.First().Id;
            dokument.StatusId = context.StatusDokumenta.First().Id;
            dokument.VrstaId = context.VrstaDokumenta.Single(x => x.Naziv == "Narudžbenica").Id;
            dokument.DatumKreiranja = DateTime.Now;
            dokument.DatumAzuriranja = DateTime.Now;

            context.Dokument.Add(dokument);
            await context.SaveChangesAsync();
            
            var stanjeProizvoda = await context.StanjeProizvoda.ToListAsync();

            foreach (var item in stanjeProizvoda)
            {
                if(item.Stanje < item.MaksimalnaKolicina)
                {
                    context.StavkaDokumenta.Add(new StavkaDokumenta
                    {
                        ProizvodId = item.ProizvodId,
                        Kolicina = item.MaksimalnaKolicina - item.Stanje,
                        DokumentId = dokument.Id
                    });
                }
            }

            await context.SaveChangesAsync();

            return dokument.Id;
        }

        public static async Task<int> GenerirajPrimkuIzNarudzbenice(tbpfoiContext context, int id)
        {
            Dokument editDok = await context.Dokument
                .Include(m => m.StavkaDokumenta)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (editDok == null)
            {
                return -1;
            }

            Dokument dokument = new Dokument();
            dokument.ZaposlenikId = editDok.ZaposlenikId;
            dokument.StatusId = context.StatusDokumenta.First().Id;
            dokument.VrstaId = context.VrstaDokumenta.Single(x => x.Naziv == "Primka").Id;
            dokument.DatumKreiranja = DateTime.Now;
            dokument.DatumAzuriranja = DateTime.Now;

            context.Dokument.Add(dokument);
            await context.SaveChangesAsync();

            foreach (var item in editDok.StavkaDokumenta)
            {
                context.StavkaDokumenta.Add(new StavkaDokumenta
                {
                    ProizvodId = item.ProizvodId,
                    Kolicina = item.Kolicina,
                    DokumentId = dokument.Id
                });
            }

            await context.SaveChangesAsync();

            return dokument.Id;
        }
    }
}
