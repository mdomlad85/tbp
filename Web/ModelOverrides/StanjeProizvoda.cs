using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;

namespace Web.Models
{
    [ModelMetadataType(typeof(StanjeProizvodaMetaData))]
    public partial class StanjeProizvoda
    { }

    public class StanjeProizvodaMetaData
    {

        [DisplayName("Maksimalna količina")]
        public double MaksimalnaKolicina { get; set; }

        [DisplayName("Minimalna količina")]
        public double MinimalnaKolicina { get; set; }

        [DisplayName("Jedinica Mjere")]
        public int? JedinicaMjereId { get; set; }
    }
}
