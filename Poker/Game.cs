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
        public double Bet { get; set; }

        [JsonProperty]
        public List<Card> CardsOnTable { get; set; }

        [JsonProperty]
        public List<Player> Players { get; set; }

        [JsonProperty]
        public int CurrentPlayerId { get; set; } = 0;

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
        public bool IsEnd { get => PlayersCount == CurrentPlayerId; }

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
        }

        public void PlayerTurn(PlayerAction playerAction)
        {
            
        }

        private List<Card> GetNextCardsToTable()
        {
            if (GiveCardsCounter == 0)
            {
                var list = new List<Card>();
                list.Add(Deck[0]);
                list.Add(Deck[1]);
                list.Add(Deck[2]);
                Deck.RemoveRange(0, 3);
                GiveCardsCounter++;
                return list;
            }
            if (GiveCardsCounter == 1)
            {
                var list = new List<Card>();
                list.Add(Deck[0]);
                Deck.RemoveAt(0);
                GiveCardsCounter++;
                return list;
            }
            if (GiveCardsCounter == 2)
            {
                var list = new List<Card>();
                list.Add(Deck[0]);
                Deck.RemoveAt(0);
                GiveCardsCounter++;
                return list;
            }
            else
                return null;
        }

        public void GetWinners()
        {
            
        }
    }
}
