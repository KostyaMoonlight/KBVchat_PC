using Blackjack;
using Blackjack.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackTests
{
    [TestClass]
    public class JsonGameTest
    {
        [TestMethod]
        public void JsonTest()
        {
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
            string jsonGame = JsonConvert.SerializeObject(game);
            Assert.IsNotNull(jsonGame);
            var deserializedGame = JsonConvert.DeserializeObject<Game>(jsonGame);
            Assert.IsNotNull(deserializedGame);
        }
    }
}
