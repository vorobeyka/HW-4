using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Task_2
{
    class CurrencyPare
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("cc")]
        public string Currency { get; set; }

        public override string ToString()
        {
            return $"rate {Rate}\ncc {Currency}";
        }
    }
}
