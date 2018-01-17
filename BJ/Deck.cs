using Blackjack.DTO;
using Blackjack.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public class Deck
    {
        public IEnumerable<Card> Cards
        {
            get => new List<Card>
        {
            new Card{ Name="2", Suit=Suit.Club, Value=2 },
            new Card{ Name="2", Suit=Suit.Diamond, Value=2 },
            new Card{ Name="2", Suit=Suit.Heart, Value=2 },
            new Card{ Name="2", Suit=Suit.Spade, Value=2 },

            new Card{ Name="3", Suit=Suit.Club, Value=3 },
            new Card{ Name="3", Suit=Suit.Diamond, Value=3 },
            new Card{ Name="3", Suit=Suit.Heart, Value=3 },
            new Card{ Name="3", Suit=Suit.Spade, Value=3 },

            new Card{ Name="4", Suit=Suit.Club, Value=4 },
            new Card{ Name="4", Suit=Suit.Diamond, Value=4 },
            new Card{ Name="4", Suit=Suit.Heart, Value=4 },
            new Card{ Name="4", Suit=Suit.Spade, Value=4 },

            new Card{ Name="5", Suit=Suit.Club, Value=5 },
            new Card{ Name="5", Suit=Suit.Diamond, Value=5 },
            new Card{ Name="5", Suit=Suit.Heart, Value=5 },
            new Card{ Name="5", Suit=Suit.Spade, Value=5 },

            new Card{ Name="6", Suit=Suit.Club, Value=6 },
            new Card{ Name="6", Suit=Suit.Diamond, Value=6 },
            new Card{ Name="6", Suit=Suit.Heart, Value=6 },
            new Card{ Name="6", Suit=Suit.Spade, Value=6 },

            new Card{ Name="7", Suit=Suit.Club, Value=7 },
            new Card{ Name="7", Suit=Suit.Diamond, Value=7 },
            new Card{ Name="7", Suit=Suit.Heart, Value=7 },
            new Card{ Name="7", Suit=Suit.Spade, Value=7 },

            new Card{ Name="8", Suit=Suit.Club, Value=8 },
            new Card{ Name="8", Suit=Suit.Diamond, Value=8 },
            new Card{ Name="8", Suit=Suit.Heart, Value=8 },
            new Card{ Name="8", Suit=Suit.Spade, Value=8 },

            new Card{ Name="9", Suit=Suit.Club, Value=9 },
            new Card{ Name="9", Suit=Suit.Diamond, Value=9 },
            new Card{ Name="9", Suit=Suit.Heart, Value=9 },
            new Card{ Name="9", Suit=Suit.Spade, Value=9 },

            new Card{ Name="10", Suit=Suit.Club, Value=10 },
            new Card{ Name="10", Suit=Suit.Diamond, Value=10 },
            new Card{ Name="10", Suit=Suit.Heart, Value=10 },
            new Card{ Name="10", Suit=Suit.Spade, Value=10 },

            new Card{ Name="Jack", Suit=Suit.Club, Value=10 },
            new Card{ Name="Jack", Suit=Suit.Diamond, Value=10 },
            new Card{ Name="Jack", Suit=Suit.Heart, Value=10 },
            new Card{ Name="Jack", Suit=Suit.Spade, Value=10 },

            new Card{ Name="Queen", Suit=Suit.Club, Value=10 },
            new Card{ Name="Queen", Suit=Suit.Diamond, Value=10 },
            new Card{ Name="Queen", Suit=Suit.Heart, Value=10 },
            new Card{ Name="Queen", Suit=Suit.Spade, Value=10 },

            new Card{ Name="King", Suit=Suit.Club, Value=10 },
            new Card{ Name="King", Suit=Suit.Diamond, Value=10 },
            new Card{ Name="King", Suit=Suit.Heart, Value=10 },
            new Card{ Name="King", Suit=Suit.Spade, Value=10 },

            new Card{ Name="Ace", Suit=Suit.Club, Value=11 },
            new Card{ Name="Ace", Suit=Suit.Diamond, Value=11 },
            new Card{ Name="Ace", Suit=Suit.Heart, Value=11 },
            new Card{ Name="Ace", Suit=Suit.Spade, Value=11 }
        };
        }
    }
}
