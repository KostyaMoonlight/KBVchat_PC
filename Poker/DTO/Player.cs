using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.DTO
{
    [JsonObject]
    public class Player
    {
        [JsonProperty]
        public int Id { get; set; }
        [JsonProperty]
        public double Bet { get; set; }
        [JsonProperty]
        public double Money { get; set; }
        [JsonProperty]
        public List<Card> Cards { get; set; }
        [JsonProperty]
        public string Nickname { get; set; }
        [JsonProperty]
        public bool IsPlaying { get; set; }

    }
}
