using Newtonsoft.Json;
using Poker.DTO;
using Poker.Enums;
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

        [JsonIgnore]
        public bool IsfinishedCircle { get => TurnsPerStage >= ActivePlayers; }

        [JsonProperty]
        public int TurnsPerStage { get; set; }

        [JsonProperty]
        public List<Card> CardsOnTable { get; set; }

        [JsonIgnore]
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
                else if (GiveCardsCounter >= 3 && IsFinishedStage)
                    return true;
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
            TurnsPerStage = 0;
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
            TurnsPerStage = 0;
        }

        public void PlayerTurn(DTO.Action playerAction, double bet = 0)
        {
            if (playerAction == DTO.Action.Bet)
            {
                Players[CurrentPlayer].Balance -= bet;
                Players[CurrentPlayer].Bet += bet;
                CurrentBet += bet;
            }
            if (playerAction == DTO.Action.Call)
            {
                double maxBet = Players.Max(player => player.Bet);
                double difference = maxBet - Players[CurrentPlayer].Bet;
                Players[CurrentPlayer].Balance -= difference;
                Players[CurrentPlayer].Bet += difference;
                CurrentBet += difference;
            }
            if (playerAction == DTO.Action.Check)
            {

            }
            if (playerAction == DTO.Action.Fold)
            {
                Players[CurrentPlayer].IsPlaying = false;
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
                }
                else
                    throw new ArgumentException("small bet for raise");                
            }
            CurrentPlayer++;
            TurnsPerStage++;
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
                TurnsPerStage = 0;
                return;
            }
            if (GiveCardsCounter == 1)
            {
                CardsOnTable.Add(Deck[0]);
                Deck.RemoveAt(0);
                GiveCardsCounter++;
                TurnsPerStage = 0;
                return;
            }
            if (GiveCardsCounter == 2)
            {
                CardsOnTable.Add(Deck[0]);
                Deck.RemoveAt(0);
                GiveCardsCounter++;
                TurnsPerStage = 0;
                return;
            }
        }

        public Winners GetWinners()
        {
            List<Tuple<Hand, HandValue, Player>> evaluators = new List<Tuple<Hand, HandValue, Player>>();

            foreach (var player in Players)
            {
                List<HandEvaluator> tempEvaluatore = new List<HandEvaluator>()
                {
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[0],
                        CardsOnTable[1],
                        CardsOnTable[2]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[1],
                        CardsOnTable[2],
                        CardsOnTable[3]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[2],
                        CardsOnTable[3],
                        CardsOnTable[4]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[0],
                        CardsOnTable[1],
                        CardsOnTable[3]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[0],
                        CardsOnTable[1],
                        CardsOnTable[4]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[1],
                        CardsOnTable[3],
                        CardsOnTable[4]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[0],
                        CardsOnTable[2],
                        CardsOnTable[4]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[0],
                        CardsOnTable[2],
                        CardsOnTable[3]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[0],
                        CardsOnTable[3],
                        CardsOnTable[4]
                    }),
                    new HandEvaluator( new List<Card>
                    {
                        player.Cards[0],
                        player.Cards[1],
                        CardsOnTable[1],
                        CardsOnTable[2],
                        CardsOnTable[4]
                    }),
                };

                var tempHands = tempEvaluatore.Select(evaluator => evaluator.EvaluateHand());
                var maxHand = tempHands.
                    OrderBy(hand => hand.Item2.HighCard).
                    OrderBy(hand => hand.Item2.Total).
                    OrderBy(hand => hand.Item1).
                    FirstOrDefault();
                if (maxHand != null)
                    evaluators.Add(new Tuple<Hand, HandValue, Player>(maxHand.Item1, maxHand.Item2, player));
            }

            var winnerHand = evaluators.Max(hand => hand.Item1);
            var winners = evaluators.Where(hand => hand.Item1 == winnerHand);
            if (winners.Count() > 1)
            {
                var winnerTotal = evaluators.Max(hand => hand.Item2.Total);
                winners = winners.Where(hand => hand.Item2.Total == winnerTotal);
                if (winners.Count() > 1)
                {
                    var winnerKicker = evaluators.Max(hand => hand.Item2.HighCard);
                    winners = winners.Where(hand => hand.Item2.HighCard == winnerKicker);
                }
            }
            Winners winnerWinnerChickenDinner = new Winners();
            winnerWinnerChickenDinner.Ids = winners.Select(win => win.Item3.Id);
            winnerWinnerChickenDinner.Names = winners.Select(win => win.Item3.Nickname);
            winnerWinnerChickenDinner.Money = CurrentBet / winners.Count();

            foreach (var player in Players.Where(x => winnerWinnerChickenDinner.Ids.Contains(x.Id)))
                player.Balance += winnerWinnerChickenDinner.Money;
            return winnerWinnerChickenDinner;
        }
    }
}
