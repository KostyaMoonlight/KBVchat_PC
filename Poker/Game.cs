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
        public List<Card> CardsOnTable { get; set; }

        [JsonProperty]
        public bool IsFinishedStage
        {
            get
            {
                double max = Players.Max(player => player.Bet);
                var maxPlayersCount = Players.Where(player => player.Bet == max).Count();
                if (currentPlayerId == 1 &&
                    maxPlayersCount == ActivePlayers)
                    return true;
                return false;
            }
        }

        [JsonProperty]
        public List<Player> Players { get; set; }

        [JsonProperty]
        private int currentPlayerId = 0;

        [JsonProperty]
        public int CurrentPlayerId
        {
            get
            {
                return currentPlayerId;
            }
            set
            {
                currentPlayerId = value % Players.Count;
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
        public bool IsEnd { get => ActivePlayers > 0; }

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
            GiveCardsCounter = 0;
            Deck = new DeckOfCards().Deck;
            CardsOnTable.Clear();
            CurrentPlayerId = 0;
            foreach (var player in Players)
            {
                player.Cards = new List<Card>();
                player.Cards.Add(Deck[0]);
                player.Cards.Add(Deck[1]);
                Deck.RemoveRange(0, 2);
            }
            PlayerTurn(DTO.Action.Bet, DefaultBet);
            PlayerTurn(DTO.Action.Raise, DefaultBet * 2);
        }

        public void PlayerTurn(DTO.Action playerAction, double bet = 0)
        {
            if (playerAction == DTO.Action.Bet)
            {
                Players[CurrentPlayerId].Balance -= bet;
                Players[CurrentPlayerId].Bet += bet;
                CurrentBet += bet;
                CurrentPlayerId++;
            }
            if (playerAction == DTO.Action.Call)
            {
                double maxBet = Players.Max(player => player.Bet);
                double difference = maxBet - Players[CurrentPlayerId].Bet;
                Players[CurrentPlayerId].Balance -= difference;
                Players[CurrentPlayerId].Bet += difference;
                CurrentBet += difference;
                CurrentPlayerId++;
            }
            if (playerAction == DTO.Action.Check)
            {
                CurrentPlayerId++;
            }
            if (playerAction == DTO.Action.Fold)
            {
                Players[CurrentPlayerId].IsPlaying = false;
                CurrentPlayerId++;
            }
            if (playerAction == DTO.Action.Raise)
            {
                double maxBet = Players.Max(player => player.Bet);
                double difference = maxBet - Players[CurrentPlayerId].Bet;
                if (bet > difference)
                {
                    Players[CurrentPlayerId].Balance -= bet;
                    Players[CurrentPlayerId].Bet += bet;
                    CurrentBet += bet;
                    CurrentPlayerId++;
                }
                else
                    throw new ArgumentException("small bet for raise");
            }
        }

        private void GetNextCardsToTable()
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
