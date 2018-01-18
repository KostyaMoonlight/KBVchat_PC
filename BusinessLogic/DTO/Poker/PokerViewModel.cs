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

        public bool IsfinishedCircle { get; set; }

        public bool IsFinishedStage
        {
            get
            {
                if (IsfinishedCircle)   // did everybody make turn?
                {
                    double max = Players.Max(player => player.Bet);
                    var maxPlayersCount = Players.Where(player => player.Bet == max).Count();

                    if (CardsOnTable.Count == 0 &&          // if it is first stage, did big blind make turn?
                        CurrentPlayer == 2 &&             // after all
                        maxPlayersCount == ActivePlayers)
                        return true;

                    else if (CurrentPlayer == 1 &&            // in others stage, did smal blind make turn? 
                             maxPlayersCount == ActivePlayers)  // after all
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

        public bool IsEnd { get => PlayersCount == CurrentPlayer; }

        private int PlayersCount { get => Players.Count; }

        public int MaxPlayersCount { get; set; }

        public string Winners { get; set; }
    }
}
