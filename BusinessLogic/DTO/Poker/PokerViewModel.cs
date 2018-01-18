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

        public double DefaultBet { get; set; }

        public bool IsfinishedCircle { get => TurnsPerStage >= ActivePlayers; }

        public int TurnsPerStage { get; set; }

        public bool IsFinishedStage
        {
            get
            {
                if (PlayersCount == 0)
                    return false;
                if (IsfinishedCircle)   // did everybody make turn?
                {
                    double max = Players.Max(player => player.Bet);
                    var maxPlayersCount = Players.Where(player => player.Bet == max).Count();

                    if (maxPlayersCount == ActivePlayers)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        public List<Card> Deck { get; set; }

        public List<Card> CardsOnTable { get; set; }

        public List<Player> Players { get; set; }

        public int CurrentPlayer { get; set; } = 0;

        public int CurrentPlayerId { get => CurrentPlayer < Players.Count ? Players[CurrentPlayer].Id : -1; }

        public int GiveCardsCounter { get; set; } = 0;

        public int ActivePlayers { get => Players.Where(player => player.IsPlaying).Count(); }

        public bool IsEnd
        {
            get
            {
                if (PlayersCount == 0)
                    return false;
                else if (GiveCardsCounter >= 3 && IsFinishedStage)
                    return true;
                else
                    return ActivePlayers == 1 || ActivePlayers == 0;
            }
        }

        private int PlayersCount { get => Players.Count; }

        public int MaxPlayersCount { get; set; }

        public string Winners { get; set; }
    }
}
