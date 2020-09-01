using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VideoWeb.Models
{
    public class KlantViewModel
    {
        [Required(ErrorMessage ="Naam is verplicht")]
        public string Naam { get; set; }

        [Required(ErrorMessage ="Postcode is verplicht en ligt tussen 1000 en 9999")]
        [Range(1000,9999)]
        public int Postcode { get; set; }
    }
}
