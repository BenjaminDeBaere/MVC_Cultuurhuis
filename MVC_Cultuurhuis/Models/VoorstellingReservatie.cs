using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Cultuurhuis.Models
{
    public partial class VoorstellingReservatie
    {
        public int VoorstellingsNr { get; set; }
        public string Titel { get; set; }
        public string  Uitvoerders { get; set; }
        public DateTime Datum { get; set; }
        [DisplayFormat(DataFormatString ="{0:€ #,##0.00}")]
        public decimal Prijs { get; set; }
        public int Plaatsen { get; set; }
        public bool TeVerwijderen { get; set; }

        public VoorstellingReservatie(int nr, string Titel, string Uitvoerders, DateTime Datum, decimal Prijs, int Plaatsen )
        {
            this.VoorstellingsNr = nr;
            this.Titel = Titel;
            this.Uitvoerders = Uitvoerders;
            this.Datum = Datum;
            this.Prijs = Prijs;
            this.Plaatsen = Plaatsen;
            this.TeVerwijderen = false;
        }
    }
}