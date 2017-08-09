﻿using System;
using System.Collections.Generic;

namespace Api.Models
{
    public partial class StatusDokumenta
    {
        public StatusDokumenta()
        {
            Dokument = new HashSet<Dokument>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Dokument> Dokument { get; set; }
    }
}
