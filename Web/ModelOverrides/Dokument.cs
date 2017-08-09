using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;

namespace Web.Models
{
    [ModelMetadataType(typeof(DokumentMetaData))]
    public partial class Dokument
    {  }

    public class DokumentMetaData
    {

        [DisplayName("Datum kreiranja")]
        public DateTime DatumKreiranja { get; set; }

        [DisplayName("Datum ažuriranja")]
        public DateTime DatumAzuriranja { get; set; }

        [DisplayName("Zaposlenik")]
        public int? ZaposlenikId { get; set; }

        [DisplayName("Status")]
        public int? StatusId { get; set; }

        [DisplayName("Vrsta")]
        public int? VrstaId { get; set; }
    }
}
