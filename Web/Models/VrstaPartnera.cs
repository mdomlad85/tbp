using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class VrstaPartnera
    {
        public VrstaPartnera()
        {
            Partner = new HashSet<Partner>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Partner> Partner { get; set; }
    }
}
