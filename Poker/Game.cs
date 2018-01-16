using Newtonsoft.Json;
using Poker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Game
    {
        [JsonProperty]
        public List<Card> Deck { get; set; }

        [JsonProperty]
        public List<Card> CardsOnTable { get; set; }

        [JsonProperty]
        public List<Player> Players { get; set; }

        [JsonProperty]
        public int CurrentPlayerId { get; set; }

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
            CurrentPlayerId = 0;
        }

        public void GameStart()
        {
            Deck = new DeckOfCards().Deck;
            CardsOnTable.Clear();
            CurrentPlayerId = 0;
            foreach (var player in Players)
            {
                player.Cards = new List<Card>();
            }
        }

        public void PlayerTurn(PlayerAction playerAction)
        {
            
        }

        private List<Card> GetNextCards()
        {
            throw new Exception();
        }

        public void GetWinners()
        {
            
        }
    }
}
