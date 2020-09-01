using System;
using System.Collections.Generic;
using System.Text;
using VideoData.Models;

namespace VideoData.Repositories
{
    public interface IKlantRepository
    {
        Klant GetKlant(string voornaam, int postcode);
    }
}
