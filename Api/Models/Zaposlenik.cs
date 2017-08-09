using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            Dokument = new HashSet<Dokument>();
        }

        public int Id { get; set; }
        public string Prezime { get; set; }
        public string Ime { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public int? KontaktId { get; set; }

        public virtual ICollection<Dokument> Dokument { get; set; }
        public virtual Kontakt Kontakt { get; set; }
    }
}
