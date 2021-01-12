using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Task_2
{
    class CurrencyPair
    {
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("cc")]
        public string Currency { get; set; }
    }
}
