using Newtonsoft.Json;
using Poker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.DTO
{
    public class Card
    {
        [JsonIgnore]
        public string Name { get => Suit.ToString() + " " + Value.ToString(); }
        [JsonProperty]
        public Suit Suit { get; set; }
        [JsonProperty]
        public Value Value { get; set; }
    }
}
