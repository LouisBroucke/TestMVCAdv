using System;
using System.Collections.Generic;
using System.Text;

namespace VideoData.Models
{
    public class Verhuring
    {
        public int VerhuringID { get; set; }
        public int KlantID { get; set; }
        public int FilmID { get; set; }
        public DateTime VerhuurDatum { get; set; }

        public virtual Klant Klant { get; set; }
        public virtual Film Film { get; set; }
    }
}
