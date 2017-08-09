using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class Proizvod
    {
        public Proizvod()
        {
            StavkaDokumenta = new HashSet<StavkaDokumenta>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public float Vrijednost { get; set; }
        public short RokTrajanja { get; set; }

        public virtual StanjeProizvoda StanjeProizvoda { get; set; }
        public virtual ICollection<StavkaDokumenta> StavkaDokumenta { get; set; }
    }
}
