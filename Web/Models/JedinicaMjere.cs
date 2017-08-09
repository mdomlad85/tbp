using System;
using System.Collections.Generic;

namespace Web.Models
{
    public partial class JedinicaMjere
    {
        public JedinicaMjere()
        {
            StanjeProizvoda = new HashSet<StanjeProizvoda>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<StanjeProizvoda> StanjeProizvoda { get; set; }
    }
}
