using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Partner
    {
        public Partner()
        {
            PartnerKontakt = new HashSet<PartnerKontakt>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public TimeSpan DatumKreiranja { get; set; }
        public int? VrstaId { get; set; }

        public virtual ICollection<PartnerKontakt> PartnerKontakt { get; set; }
        public virtual VrstaPartnera Vrsta { get; set; }
    }
}
