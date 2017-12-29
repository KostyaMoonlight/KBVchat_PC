using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack.DTO
{
    [JsonObject]
    public class Player
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public double Bet { get; set; }
        [JsonProperty]
        public List<Card> Cards { get; set; }
        [JsonProperty]
        public string Nickname { get; set; }

    }
}
