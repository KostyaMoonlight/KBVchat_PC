using Blackjack.DTO;
using Blackjack.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
    [JsonObject]
    public class Game
    {
        [JsonProperty]
        public Player Casino { get; set; }
        [JsonProperty]
        public List<Player> Players { get; set; }
        [JsonProperty]
        public List<Card> Cards { get; set; }
        [JsonProperty]
        public int CurrentPlayer { get; set; }
        [JsonIgnore]
        public int ActivePlayers
        {
            get => Players.Where(
                x => x.Cards.Count == 0 || x.Cards.Select(c => c.Value).Sum() <= 21
                ).Count();
        }
        [JsonIgnore]
        public bool IsEnd { get => PlayersCount == CurrentPlayer; }
        [JsonIgnore]
        private int PlayersCount { get => Players.Count; }
        [JsonIgnore]
        private Random Random { get; set; } = new Random();

        public Game()
        {
            Players = new List<Player>();
            Cards = new Deck().Cards.ToList();
            //Random = new Random();
            CurrentPlayer = 0;
        }

        public void GameStart()
        {
            foreach (var player in Players)
            {
                player.Cards = new List<Card>();
            }
            foreach (var card in Cards)
            {
                card.Active = true;
            }
            Casino.Cards = new List<Card>();
            CurrentPlayer = 0;

        }

        public void CasinosTurn()
        {
            if (Casino.Cards.Count == 0)
            {
                var firstCard = GetNextCard();
                var secondCard = GetNextCard();
                firstCard.Active = false;
                secondCard.Active = false;
                secondCard.Hidden = true;

                Casino.Cards.Add(firstCard);
                Casino.Cards.Add(secondCard);
            }
            else
            {
                foreach (var card in Casino.Cards)
                {
                    card.Hidden = false;
                }
                var cardsValue = Casino.Cards.Select(x => x.Value).Sum();
                while (cardsValue < 17)
                {

                    var card = GetNextCard();
                    card.Active = false;
                    Casino.Cards.Add(card);
                    cardsValue += card.Value;
                }
            }
        }

        public void PlayerTurn(PlayerAction playerAction)
        {
            switch (playerAction)
            {
                case PlayerAction.FirstTurn:
                    Players[CurrentPlayer].Cards.Add(GetNextCard());
                    Players[CurrentPlayer].Cards.Add(GetNextCard());
                    if (Players[CurrentPlayer].Cards.Select(x => x.Value).Sum() >= 21)
                    {
                        CurrentPlayer++;
                    }
                    break;

                case PlayerAction.Stand:
                    CurrentPlayer++;
                    break;

                case PlayerAction.Double:
                    Players[CurrentPlayer].Bet *= 2;
                    Casino.Bet *= 2;
                    Players[CurrentPlayer].Cards.Add(GetNextCard());
                    CurrentPlayer++;
                    break;

                case PlayerAction.Hit:
                    Players[CurrentPlayer].Cards.Add(GetNextCard());
                    if (Players[CurrentPlayer].Cards.Select(x => x.Value).Sum() >= 21)
                    {
                        CurrentPlayer++;
                    }
                    break;
            }

        }

        private Card GetNextCard()
        {
            var cards = Cards.Where(x => x.Active == true).ToList();
            var count = cards.Count();
            var cardIndex = Random.Next(count);
            var card = cards[cardIndex];
            card.Active = false;
            return card;
        }

        public Winners GetWinners()
        {
            var casinoScore = new PlayerScore { Id = Casino.Id, Score = Casino.Cards.Select(x => x.Value).Sum() };
            var playersScore = Players
                .Select(x => new PlayerScore
                {
                    Id = x.Id,
                    Score = x.Cards.Select(v => v.Value).Sum()
                }).ToList();
            playersScore.Add(casinoScore);
            var scores = playersScore.Select(x => x.Score).Where(x => x <= 21);
            if (scores.Count() == 0)
            {
                return new Winners
                {
                    Ids = new List<int> { Casino.Id },
                    Names = new List<string> { Casino.Nickname},
                    Money = Players.Select(x => x.Bet).Sum() + Casino.Bet
                };
            }
            var maxScore = scores.Max();
            var winnersIds = playersScore.Where(x => x.Score == maxScore).Select(x => x.Id);
            var winnersNames = Players.Where(x => winnersIds.Contains( x.Id)).Select(x => x.Nickname);
            var winners = new Winners()
            {
                Ids = winnersIds,
                Names = winnersNames,
                Money = (Players.Select(x => x.Bet).Sum() + Casino.Bet) / winnersIds.Count()
            };
            return winners;
        }

    }
}
