using Blackjack.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.BJ
{
    public class BlackjackViewModel
    {
        public int GameId { get; set; }
        public double DefaultBet { get; set; }
        public Player Casino { get; set; }
        public bool IsEnd { get; set; }
        public List<Player> Players { get; set; }
        public List<Card> Cards { get; set; }
        public int CurrentPlayer { get; set; }
        public int MaxPlayersCount { get; set; }
        public int CurrentPlayerId { get => CurrentPlayer < Players.Count ? Players[CurrentPlayer].Id : -1; }
        public string Winners { get; set; }
    }
}
