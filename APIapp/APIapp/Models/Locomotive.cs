using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIapp.Models
{
    public class Locomotive
    {
        [JsonProperty(PropertyName = "id")]
        public string Baureihe { get; set; }

        public string Typ { get; set; }

        public int Laenge { get; set; }

        public int Dienstmasse { get; set; }

        public int Vmax { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
