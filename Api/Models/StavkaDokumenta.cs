using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class StavkaDokumenta
    {
        public int Id { get; set; }
        public int? DokumentId { get; set; }
        public int? ProizvodId { get; set; }
        public double Kolicina { get; set; }

        public virtual Dokument Dokument { get; set; }
        public virtual Proizvod Proizvod { get; set; }
    }
}
