using System;
using System.Collections.Generic;
using System.Text;

namespace VideoData.Models
{
    public class Film
    {
        public int FilmID { get; set; }
        public string Titel { get; set; }
        public int GenreID { get; set; }
        public int InVoorraad { get; set; }
        public int UitVoorraad { get; set; }
        public decimal Prijs { get; set; }
        public int TotaalVerhuurd { get; set; }

        public virtual Genre Genre { get; set; }
    }
}
