using Poker.DTO;
using Poker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class DeckOfCards
    {
        const int NUM_OF_CARDS = 52;
        public List<Card> Deck { get; private set; }

        public DeckOfCards()
        {
            Deck = new List<Card>(NUM_OF_CARDS);
            SetUpDeck();
        }

        public void SetUpDeck()
        {
            Deck.Clear();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    Deck.Add(new Card { Suit = suit, Value = value});
                }
            }
            ShuffleCards();
        }
        
        public void ShuffleCards()
        {
            Random rand = new Random();
            Card temp;

            for (int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
            {
                for (int i = 0; i < NUM_OF_CARDS; i++)
                {
                    int secondCardIndex = rand.Next(13);
                    temp = Deck[i];
                    Deck[i] = Deck[secondCardIndex];
                    Deck[secondCardIndex] = temp;
                }
            }
        }
    }
}
