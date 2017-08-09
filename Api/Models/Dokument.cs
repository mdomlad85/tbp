using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class Dokument
    {
        public Dokument()
        {
            StavkaDokumenta = new HashSet<StavkaDokumenta>();
        }

        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumAzuriranja { get; set; }
        public int? ZaposlenikId { get; set; }
        public int? StatusId { get; set; }
        public int? VrstaId { get; set; }
        public short Godina { get; set; }

        public virtual ICollection<StavkaDokumenta> StavkaDokumenta { get; set; }
        public virtual StatusDokumenta Status { get; set; }
        public virtual VrstaDokumenta Vrsta { get; set; }
        public virtual Zaposlenik Zaposlenik { get; set; }
    }
}
