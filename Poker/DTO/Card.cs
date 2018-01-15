using Newtonsoft.Json;
using Poker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.DTO
{
    class Card
    {
        [JsonProperty]
        public Suit Suit { get; set; }
        [JsonProperty]
        public Value Value { get; set; }
    }
}
