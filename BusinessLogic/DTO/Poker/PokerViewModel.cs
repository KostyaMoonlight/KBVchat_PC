using Poker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.Poker
{
    public class PokerViewModel
    {
        public int GameId { get; set; }

        public double Bet { get; set; }

        public List<Card> Deck { get; set; }

        public List<Card> CardsOnTable { get; set; }

        public List<Player> Players { get; set; }

        public int CurrentPlayerId { get; set; } = 0;

        public int GiveCardsCounter { get; set; } = 0;

        public int ActivePlayers { get => Players.Where(player => player.IsPlaying).Count(); }

        public bool IsEnd { get => PlayersCount == CurrentPlayerId; }

        private int PlayersCount { get => Players.Count; }

        public int MaxPlayersCount { get; set; }

        public string Winners { get; set; }
    }
}
