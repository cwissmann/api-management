using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CosmosDBInitializer
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
