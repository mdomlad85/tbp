using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Kontakt
    {
        public Kontakt()
        {
            PartnerKontakt = new HashSet<PartnerKontakt>();
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string Naziv { get; set; }
        public string Telefon { get; set; }

        public virtual ICollection<PartnerKontakt> PartnerKontakt { get; set; }
        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
