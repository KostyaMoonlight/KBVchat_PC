using Blackjack.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.DTO
{
    [JsonObject]
    public class Card
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int Value { get; set; }
        [JsonProperty]
        public Suit Suit { get; set; }
        [JsonProperty]
        public bool Active { get; set; } = true;
        [JsonProperty]
        public bool Hidden { get; set; } = false;
    }
}
