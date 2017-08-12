using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.ViewModels
{
    public class KontaktViewModel
    {
        public string Email { get; set; }
        public string Adresa { get; set; }
        [Required(ErrorMessage = "Naziv je obavezan!")]
        public string Naziv { get; set; }
        public string Telefon { get; set; }
        [Required]
        public int PartnerId { get; set; }

        public Kontakt Kontakt { get; set; }

        public PartnerKontakt PartnerKontakt { get; set; }

        public KontaktViewModel()
        {
        }

        public KontaktViewModel(int partnerId)
        {
            PartnerId = partnerId;
        }

        internal void factoryKontakt()
        {
            Kontakt = new Kontakt();
            Kontakt.Naziv = Naziv;
            Kontakt.Email = Email;
            Kontakt.Adresa = Adresa;
            Kontakt.Telefon = Telefon;
        }

        internal void factoryPartnerKontakt()
        {
            PartnerKontakt = new PartnerKontakt();
            PartnerKontakt.KontaktId = Kontakt.Id;
            PartnerKontakt.PartnerId = PartnerId;
        }
    }
}
