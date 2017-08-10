using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class ZaposlenikViewModel
    {
        private readonly tbpfoiContext _context;

        public int Id { get; set; }

        public Zaposlenik Zaposlenik { get; set; }

        [Required(ErrorMessage = "Ime je obavezno!")]
        public String Ime { get; set; }


        [Required(ErrorMessage = "Prezime je obavezno!")]
        public String Prezime { get; set; }


        [Required(ErrorMessage = "Datum rođenja je obavezan!")]
        [DisplayName("Datum rođenja")]
        public DateTime? DatumRodjenja { get; set; }

        public String Adresa { get; set; }

        public String Email { get; set; }

        public String Telefon { get; set; }

        public ZaposlenikViewModel(tbpfoiContext context)
            :base()
        {
            _context = context;
        }

        public ZaposlenikViewModel()
        {
            Zaposlenik = new Zaposlenik();
        }

        public ZaposlenikViewModel(Zaposlenik zaposlenik, tbpfoiContext context)
        {
            this.Zaposlenik = zaposlenik;
            this._context = context;
            this.fillProperties();
        }

        private void fillProperties()
        {
            Id = Zaposlenik.Id;
            Ime = Zaposlenik.Ime;
            Prezime = Zaposlenik.Prezime;
            DatumRodjenja = Zaposlenik.DatumRodjenja;

            if(Zaposlenik.Kontakt != null)
            {
                Adresa = Zaposlenik.Kontakt.Adresa;
                Email = Zaposlenik.Kontakt.Email;
                Telefon = Zaposlenik.Kontakt.Telefon;
            }
        }

        public void factoryZaposlenik()
        {
            Zaposlenik.Ime = Ime;
            Zaposlenik.Prezime = Prezime;
            Zaposlenik.DatumRodjenja = DatumRodjenja;

            if(Zaposlenik.Kontakt == null && (Adresa != null || Email != null || Telefon != null))
            {
                Zaposlenik.Kontakt = new Kontakt();
                Zaposlenik.Kontakt.Naziv = String.Format("{0}, {1}", Zaposlenik.Prezime, Zaposlenik.Ime);
            }

            if(Adresa != null)
            {
                Zaposlenik.Kontakt.Adresa = Adresa;
            }

            if (Email != null)
            {
                Zaposlenik.Kontakt.Email = Email;
            }

            if (Telefon != null)
            {
                Zaposlenik.Kontakt.Telefon = Telefon;
            }
        }
    }
}
