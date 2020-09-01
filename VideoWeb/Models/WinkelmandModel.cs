using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoData.Models;

namespace VideoWeb.Models
{
    public class WinkelmandModel
    {
        public Dictionary<string, Film> Films { get; set; } = new Dictionary<string, Film>();
    }
}
