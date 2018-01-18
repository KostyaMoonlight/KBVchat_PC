using Newtonsoft.Json;
using Poker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Game
    {
        [JsonProperty]
        public List<Card> Deck { get; set; }

        [JsonProperty]
        public double DefaultBet { get; set; }

        [JsonProperty]
        public double CurrentBet { get; set; }

        [JsonProperty]
        public bool IsfinishedCircle { get; set; } = false;

        [JsonProperty]
        public List<Card> CardsOnTable { get; set; }

        [JsonProperty]
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

        [JsonProperty]
        public List<Player> Players { get; set; }

        [JsonProperty]
        private int currentPlayer = 0;

        [JsonProperty]
        public int CurrentPlayer
        {
            get
            {
                return currentPlayer;
            }
            set
            {
                if (PlayersCount == 0)
                {
                    currentPlayer = 0;
                    return;
                }
                if (value == PlayersCount)
                    IsfinishedCircle = true;
                currentPlayer = value % PlayersCount;
            }
        }

        [JsonProperty]
        public int GiveCardsCounter { get; set; } = 0;

        [JsonProperty]
        public int MaxPlayersCount { get; set; }

        [JsonIgnore]
        public int ActivePlayers
        {
            get => Players.Where(player => player.IsPlaying).Count();
        }

        [JsonIgnore]
        public bool IsEnd
        {
            get
            {
                if (PlayersCount == 0)
                    return false;
                else
                    return ActivePlayers == 0;
            }
        }

        [JsonIgnore]
        private int PlayersCount { get => Players.Count; }

        [JsonIgnore]
        private Random Random { get; set; } = new Random();

        public Game()
        {
            Players = new List<Player>();
            CardsOnTable = new List<Card>();
        }

        public void GameStart()
        {
            CurrentBet = 0;
            GiveCardsCounter = 0;
            Deck = new DeckOfCards().Deck;
            CardsOnTable.Clear();
            CurrentPlayer = 0;
            foreach (var player in Players)
            {
                player.Cards = new List<Card>();
                player.Cards.Add(Deck[0]);
                player.Cards.Add(Deck[1]);
                Deck.RemoveRange(0, 2);
                player.Bet = 0;
            }
            PlayerTurn(DTO.Action.Bet, DefaultBet);
            PlayerTurn(DTO.Action.Raise, DefaultBet * 2);
        }

        public void PlayerTurn(DTO.Action playerAction, double bet = 0)
        {
            if (playerAction == DTO.Action.Bet)
            {
                Players[CurrentPlayer].Balance -= bet;
                Players[CurrentPlayer].Bet += bet;
                CurrentBet += bet;
                CurrentPlayer++;
            }
            if (playerAction == DTO.Action.Call)
            {
                double maxBet = Players.Max(player => player.Bet);
                double difference = maxBet - Players[CurrentPlayer].Bet;
                Players[CurrentPlayer].Balance -= difference;
                Players[CurrentPlayer].Bet += difference;
                CurrentBet += difference;
                CurrentPlayer++;
            }
            if (playerAction == DTO.Action.Check)
            {
                CurrentPlayer++;
            }
            if (playerAction == DTO.Action.Fold)
            {
                Players[CurrentPlayer].IsPlaying = false;
                CurrentPlayer++;
            }
            if (playerAction == DTO.Action.Raise)
            {
                double maxBet = Players.Max(player => player.Bet);
                double difference = maxBet - Players[CurrentPlayer].Bet;
                if (bet > difference)
                {
                    Players[CurrentPlayer].Balance -= bet;
                    Players[CurrentPlayer].Bet += bet;
                    CurrentBet += bet;
                    CurrentPlayer++;
                }
                else
                    throw new ArgumentException("small bet for raise");
            }
        }

        public void GetNextCardsToTable()
        {
            if (GiveCardsCounter == 0)
            {
                CardsOnTable.Add(Deck[0]);
                CardsOnTable.Add(Deck[1]);
                CardsOnTable.Add(Deck[2]);
                Deck.RemoveRange(0, 3);
                GiveCardsCounter++;
                return;
            }
            if (GiveCardsCounter == 1)
            {
                CardsOnTable.Add(Deck[0]);
                Deck.RemoveAt(0);
                GiveCardsCounter++;
                return;
            }
            if (GiveCardsCounter == 2)
            {
                CardsOnTable.Add(Deck[0]);
                Deck.RemoveAt(0);
                GiveCardsCounter++;
                return;
            }
        }

        // do something here 
        public Winners GetWinners()
        {
            Winners winner = new Winners();
            return winner;
        }
    }
}
