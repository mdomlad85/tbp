using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class StanjeProizvoda
    {
        public int Id { get; set; }
        public double MaksimalnaKolicina { get; set; }
        public double MinimalnaKolicina { get; set; }
        public double Stanje { get; set; }
        public int? JedinicaMjereId { get; set; }
        public int? ProizvodId { get; set; }

        public virtual JedinicaMjere JedinicaMjere { get; set; }
        public virtual Proizvod Proizvod { get; set; }
    }
}
