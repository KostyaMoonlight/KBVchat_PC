using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blackjack.DTO;
using System.Collections.Generic;
using Blackjack;
using System.Linq;
using Blackjack.Enum;

namespace BlackjackTests
{
    [TestClass]
    public class GameTest
    {

        [TestMethod]
        public void CasinoTurnTest_FirstCasinosTurn()
        {

            var expectedCountOfCardsInCasino = 2;
            var expectedCountOfNonActiveCardsInDeck = 2;
            var expectedCountOfHiddenCards = 1;

            var game = new Game
            {
                Casino = new Player
                {
                    Cards = new List<Card>()
                },
                Cards = new Deck().Cards.ToList()
            };
            game.CasinosTurn();
            var realCountOfCardsInCasino = game.Casino.Cards.Count;
            var realCountOfNonActiveCardsInDeck = game.Cards.Where(x => x.Active == false).Count();
            var realCountOfHiddenCards = game.Casino.Cards.Where(x => x.Hidden == true).Count();


            Assert.IsTrue(realCountOfNonActiveCardsInDeck == expectedCountOfNonActiveCardsInDeck, "Cards state didn't changed.");

            Assert.IsTrue(realCountOfCardsInCasino == expectedCountOfCardsInCasino, "Casino doesn't possess 2 cards.");

            Assert.IsTrue(realCountOfHiddenCards == expectedCountOfHiddenCards, "Casino doesn't possess 1 hidden card.");
        }

        [TestMethod]
        public void CasinoTurnTest_SecondCasinosTurn()
        {

            var minCardsValue = 17;
            var expectedCountOfHiddenCards = 0;

            var game = new Game
            {
                Casino = new Player
                {
                    Cards = new List<Card>()
                },
                Cards = new Deck().Cards.ToList()
            };
            game.CasinosTurn();
            game.CasinosTurn();

            var realCardsValue = game.Casino.Cards.Select(x => x.Value).Sum();
            var realCountOfHiddenCards = game.Casino.Cards.Where(x => x.Hidden == true).Count();

            Assert.IsTrue(minCardsValue <= realCardsValue, "Cards state didn't changed.");

            Assert.IsTrue(realCountOfHiddenCards == expectedCountOfHiddenCards, "Casino doesn't possess 1 hidden card.");
        }

        [TestMethod]
        public void PlayerTurnTest_FirstTurn()
        {

            var expectedCountUsersCards = 2;

            var game = new Game
            {
                Players = new List<Player>(){
                    new Player
                    {
                        Cards = new List<Card>()
                    }
                },
                CurrentPlayer = 0,
                Cards = new Deck().Cards.ToList()
            };
            game.PlayerTurn(PlayerAction.FirstTurn);

            var realCountUsersCards = game.Players[0].Cards.Count;

            Assert.IsTrue(realCountUsersCards == expectedCountUsersCards, "User didn't passess 2 cards.");
        }

        [TestMethod]
        public void PlayerTurnTest_Hit()
        {

            var expectedCountUsersCardsAfterFirstHit = 3;
            var expectedCountUsersCardsAfterSecondHit = 4;

            var game = new Game
            {
                Players = new List<Player>(){
                    new Player
                    {
                        Cards = new List<Card>()
                    }
                },
                CurrentPlayer = 0,
                Cards = new Deck().Cards.ToList()
            };
            game.PlayerTurn(PlayerAction.FirstTurn);

            if (game.Players[0].Cards.Select(x => x.Value).Sum() < 21)
            {

                game.PlayerTurn(PlayerAction.Hit);

                var realCountUsersCardsAfterFirstHit = game.Players[0].Cards.Count;

                Assert.IsTrue(realCountUsersCardsAfterFirstHit == expectedCountUsersCardsAfterFirstHit, "User didn't passess 3 cards after first hit.");

                if (game.Players[0].Cards.Select(x => x.Value).Sum() < 21)
                {

                    game.PlayerTurn(PlayerAction.Hit);

                    var realCountUsersCardsAfterSecondHit = game.Players[0].Cards.Count;

                    Assert.IsTrue(realCountUsersCardsAfterSecondHit == expectedCountUsersCardsAfterSecondHit, "User didn't passess 3 cards after first hit.");
                }
            }
        }

        [TestMethod]
        public void PlayerTurnTest_Stand()
        {

            var expectedCurrentUser = 2;

            var game = new Game
            {
                Players = new List<Player>(){
                    new Player
                    {
                    }
                },
                CurrentPlayer = 1,
                Cards = new Deck().Cards.ToList()
            };

            game.PlayerTurn(PlayerAction.Stand);

            var realCurrentUser = game.CurrentPlayer;

            Assert.IsTrue(realCurrentUser == expectedCurrentUser, "Wrong current user.");
        }

        [TestMethod]
        public void PlayerTurnTest_Double()
        {

            var expectedBet = 200;
            var expectedCardCount = 3;

            var game = new Game
            {
                Players = new List<Player>(){
                    new Player
                    {
                        Cards = new List<Card>(),
                        Bet = 100
                    }
                },
                CurrentPlayer = 0,
                Cards = new Deck().Cards.ToList()
            };

            game.PlayerTurn(PlayerAction.FirstTurn);

            if (game.Players[0].Cards.Select(x => x.Value).Sum() < 21)
            {

                game.PlayerTurn(PlayerAction.Double);

                var realBet = game.Players[0].Bet;
                var realCardCount = game.Players[0].Cards.Count;

                Assert.IsTrue(realBet == expectedBet, "Wrong users bet.");

                Assert.IsTrue(realCardCount == expectedCardCount, "Wrong users bet.");
            }
        }

        [TestMethod]
        public void GetWinnersTest_CasinoWon()
        {

            var expectedWinnerId = 0;
            var expectedMoney = 300;

            var game = new Game
            {
                Casino = new Player
                {
                    Id = 0,
                    Bet = 100,
                    Cards = new List<Card>()
                    {
                        new Card { Value = 11},
                        new Card { Value = 10}
                    }
                },
                Players = new List<Player>
                {
                    new Player
                    {
                        Id=1,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 10},
                            new Card { Value = 10}
                        }
                    },

                    new Player
                    {
                        Id=2,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 0},
                            new Card { Value = 10}
                        }
                    },
                },
                Cards = new Deck().Cards.ToList()
            };

            var winners = game.GetWinners();

            var realWinnerId = winners.Ids.FirstOrDefault();
            var realMoney = winners.Money;

            Assert.IsTrue(realWinnerId == expectedWinnerId, "Wrong winner id.");

            Assert.IsTrue(realMoney == expectedMoney, "Wrong money.");
        }

        [TestMethod]
        public void GetWinnersTest_TwoWinners()
        {

            List<int> expectedWinnersIds = new List<int> { 0, 2 };
            expectedWinnersIds.Sort();
            var expectedMoney = 150;

            var game = new Game
            {
                Casino = new Player
                {
                    Id = 0,
                    Bet = 100,
                    Cards = new List<Card>()
                    {
                        new Card { Value = 11},
                        new Card { Value = 10}
                    }
                },
                Players = new List<Player>
                {
                    new Player
                    {
                        Id=1,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 10},
                            new Card { Value = 10}
                        }
                    },

                    new Player
                    {
                        Id=2,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 10},
                            new Card { Value = 11}
                        }
                    },
                },
                Cards = new Deck().Cards.ToList()
            };

            var winners = game.GetWinners();

            List<int> realWinnersIds = winners.Ids.ToList();
            realWinnersIds.Sort();
            var realMoney = winners.Money;

            CollectionAssert.AreEqual(realWinnersIds, expectedWinnersIds, "Wrong winners ids.");

            Assert.IsTrue(realMoney == expectedMoney, "Wrong money.");
        }

        [TestMethod]
        public void GetWinnersTest_NoWinners()
        {

            int expectedWinnerId = 0;
            var expectedMoney = 300;

            var game = new Game
            {
                Casino = new Player
                {
                    Id = 0,
                    Bet = 100,
                    Cards = new List<Card>()
                    {
                        new Card { Value = 11},
                        new Card { Value = 10},
                        new Card { Value = 2 }
                    }
                },
                Players = new List<Player>
                {
                    new Player
                    {
                        Id=1,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 11},
                            new Card { Value = 11}
                        }
                    },

                    new Player
                    {
                        Id=2,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 11},
                            new Card { Value = 11}
                        }
                    },
                },
                Cards = new Deck().Cards.ToList()
            };

            var winners = game.GetWinners();

            int realWinnerId = winners.Ids.FirstOrDefault();
            var realMoney = winners.Money;

            Assert.AreEqual(realWinnerId, expectedWinnerId, "Wrong winner id.");

            Assert.IsTrue(realMoney == expectedMoney, "Wrong money.");
        }

        [TestMethod]
        public void GetWinnersTest_2ActivePlayers()
        {

            var expectedActivePlayersCount = 2;

            var game = new Game
            {
                Casino = new Player
                {
                    Id = 0,
                    Bet = 100,
                    Cards = new List<Card>()
                    {
                        new Card { Value = 11},
                        new Card { Value = 10},
                        new Card { Value = 2 }
                    }
                },
                Players = new List<Player>
                {
                    new Player
                    {
                        Id=1,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 10},
                            new Card { Value = 11}
                        }
                    },

                    new Player
                    {
                        Id=2,
                        Bet = 100,
                        Cards = new List<Card>()
                        {
                            new Card { Value = 11},
                            new Card { Value = 11}
                        }
                    },
                    new Player
                    {
                        Id=2,
                        Bet = 100,
                        Cards = new List<Card>()
                    }
                },
                Cards = new Deck().Cards.ToList()
            };



            var realActivePlayersCount = game.ActivePlayers;

            Assert.IsTrue(realActivePlayersCount == expectedActivePlayersCount, "Wrong count of active players.");
        }
    }
}


