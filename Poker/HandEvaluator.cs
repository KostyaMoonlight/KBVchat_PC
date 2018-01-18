using Poker.DTO;
using Poker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }

    class HandEvaluator
    {
        private int heartsSum;
        private int diamondSum;
        private int clubSum;
        private int spadesSum;
        private Card[] cards;
        private HandValue handValue;

        public HandEvaluator(List<Card> sortedHand)
        {
            heartsSum = 0;
            diamondSum = 0;
            clubSum = 0;
            spadesSum = 0;
            cards = new Card[5];
            Cards = sortedHand.ToArray();
            handValue = new HandValue();
        }

        public HandValue HandValues
        {
            get { return handValue; }
            set { handValue = value; }
        }

        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
            }
        }

        public Tuple<Hand, HandValue> EvaluateHand()
        {
            //get the number of each suit on hand
            getNumberOfSuit();
            if (RoyalFlush())
                return new Tuple<Hand, HandValue>(Hand.RoyalFlush, handValue);
            else if (StraightFlush())
                return new Tuple<Hand, HandValue>(Hand.StraightFlush, handValue);
            else if (FourOfKind())
                return new Tuple<Hand, HandValue>(Hand.FourKind, handValue);
            else if (FullHouse())
                return new Tuple<Hand, HandValue>(Hand.FullHouse, handValue);
            else if (Flush())
                return new Tuple<Hand, HandValue>(Hand.Flush, handValue);
            else if (Straight())
                return new Tuple<Hand, HandValue>(Hand.Straight, handValue);
            else if (ThreeOfKind())
                return new Tuple<Hand, HandValue>(Hand.ThreeKind, handValue);
            else if (TwoPairs())
                return new Tuple<Hand, HandValue>(Hand.TwoPairs, handValue);
            else if (OnePair())
                return new Tuple<Hand, HandValue>(Hand.OnePair, handValue);

            //if the hand is nothing, than the player with highest card wins
            handValue.HighCard = (int)cards[4].Value;
            return new Tuple<Hand, HandValue>(Hand.Nothing, handValue);
        }

        private void getNumberOfSuit()
        {
            foreach (var element in Cards)
            {
                if (element.Suit == Suit.Heart)
                    heartsSum++;
                else if (element.Suit == Suit.Diamond)
                    diamondSum++;
                else if (element.Suit == Suit.Club)
                    clubSum++;
                else if (element.Suit == Suit.Spade)
                    spadesSum++;
            }
        }

        private bool RoyalFlush()
        {
            //if 5 consecutive values
            if (cards[0].Value == Value.Jack &&
                cards[1].Value == Value.Queen &&
                cards[2].Value == Value.King &&
                cards[3].Value == Value.Ace &&
                (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5))
            {
                //player with the highest value of the last card wins
                handValue.Total = (int)cards[4].Value;
                return true;
            }

            return false;
        }

        private bool StraightFlush()
        {
            //if 5 consecutive values
            if (cards[0].Value + 1 == cards[1].Value &&
                cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value &&
                (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5))
            {
                //player with the highest value of the last card wins
                handValue.Total = (int)cards[4].Value;
                return true;
            }

            return false;
        }

        private bool FourOfKind()
        {
            //if the first 4 cards, add values of the four cards and last card is the highest
            if (cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value && cards[0].Value == cards[3].Value)
            {
                handValue.Total = (int)cards[1].Value * 4;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value && cards[1].Value == cards[3].Value && cards[1].Value == cards[4].Value)
            {
                handValue.Total = (int)cards[1].Value * 4;
                handValue.HighCard = (int)cards[0].Value;
                return true;
            }

            return false;
        }

        private bool FullHouse()
        {
            //the first three cars and last two cards are of the same value
            //first two cards, and last three cards are of the same value
            if ((cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value && cards[3].Value == cards[4].Value) ||
                (cards[0].Value == cards[1].Value && cards[2].Value == cards[3].Value && cards[2].Value == cards[4].Value))
            {
                handValue.Total = (int)(cards[0].Value) + (int)(cards[1].Value) + (int)(cards[2].Value) +
                    (int)(cards[3].Value) + (int)(cards[4].Value);
                return true;
            }

            return false;
        }

        private bool Flush()
        {
            //if all suits are the same
            if (heartsSum == 5 || diamondSum == 5 || clubSum == 5 || spadesSum == 5)
            {
                //if flush, the player with higher cards win
                //whoever has the last card the highest, has automatically all the cards total higher
                handValue.Total = (int)cards[4].Value;
                return true;
            }

            return false;
        }

        private bool Straight()
        {
            //if 5 consecutive values
            if (cards[0].Value + 1 == cards[1].Value &&
                cards[1].Value + 1 == cards[2].Value &&
                cards[2].Value + 1 == cards[3].Value &&
                cards[3].Value + 1 == cards[4].Value)
            {
                //player with the highest value of the last card wins
                handValue.Total = (int)cards[4].Value;
                return true;
            }

            return false;
        }

        private bool ThreeOfKind()
        {
            //if the 1,2,3 cards are the same OR
            //2,3,4 cards are the same OR
            //3,4,5 cards are the same
            //3rds card will always be a part of Three of A Kind
            if ((cards[0].Value == cards[1].Value && cards[0].Value == cards[2].Value) ||
            (cards[1].Value == cards[2].Value && cards[1].Value == cards[3].Value))
            {
                handValue.Total = (int)cards[2].Value * 3;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[2].Value == cards[3].Value && cards[2].Value == cards[4].Value)
            {
                handValue.Total = (int)cards[2].Value * 3;
                handValue.HighCard = (int)cards[1].Value;
                return true;
            }
            return false;
        }

        private bool TwoPairs()
        {
            //if 1,2 and 3,4
            //if 1.2 and 4,5
            //if 2.3 and 4,5
            //with two pairs, the 2nd card will always be a part of one pair 
            //and 4th card will always be a part of second pair
            if (cards[0].Value == cards[1].Value && cards[2].Value == cards[3].Value)
            {
                handValue.Total = ((int)cards[1].Value * 2) + ((int)cards[3].Value * 2);
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[0].Value == cards[1].Value && cards[3].Value == cards[4].Value)
            {
                handValue.Total = ((int)cards[1].Value * 2) + ((int)cards[3].Value * 2);
                handValue.HighCard = (int)cards[2].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value && cards[3].Value == cards[4].Value)
            {
                handValue.Total = ((int)cards[1].Value * 2) + ((int)cards[3].Value * 2);
                handValue.HighCard = (int)cards[0].Value;
                return true;
            }
            return false;
        }

        private bool OnePair()
        {
            //if 1,2 -> 5th card has the highest value
            //2.3
            //3,4
            //4,5 -> card #3 has the highest value
            if (cards[0].Value == cards[1].Value)
            {
                handValue.Total = (int)cards[0].Value * 2;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[1].Value == cards[2].Value)
            {
                handValue.Total = (int)cards[1].Value * 2;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[2].Value == cards[3].Value)
            {
                handValue.Total = (int)cards[2].Value * 2;
                handValue.HighCard = (int)cards[4].Value;
                return true;
            }
            else if (cards[3].Value == cards[4].Value)
            {
                handValue.Total = (int)cards[3].Value * 2;
                handValue.HighCard = (int)cards[2].Value;
                return true;
            }

            return false;
        }

    }
}
