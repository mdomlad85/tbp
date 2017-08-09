using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class PartnerKontakt
    {
        public int PartnerId { get; set; }
        public int KontaktId { get; set; }

        public virtual Kontakt Kontakt { get; set; }
        public virtual Partner Partner { get; set; }
    }
}
