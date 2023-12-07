using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2023.Days
{
    public static class Day7
    {
        static int CardValue(char c)
        {
            return
                c == 'A' ? 14 :
                c == 'K' ? 13 :
                c == 'Q' ? 12 :
                c == 'J' ? 11 :
                c == 'T' ? 10 :
                c - '0';
        }

        static int CardValueJoker(char c)
        {
            return
                c == 'A' ? 14 :
                c == 'K' ? 13 :
                c == 'Q' ? 12 :
                c == 'J' ? -1 :
                c == 'T' ? 10 :
                c - '0';
        }

        enum HandType
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind,
        }

        class Hand : IComparable<Hand>
        {
            public int[] Cards { get; set; } = new int[5];
            public HandType HandType { get; set; }

            public override string ToString()
            {
                var builder = new StringBuilder();
                builder.Append(HandType.ToString());
                builder.Append(" ");
                foreach (var c in Cards)
                {
                    builder.Append((char)(
                        c == 14 ? 'A' :
                        c == 13 ? 'K' :
                        c == 12 ? 'Q' :
                        c == -1 || c == 11 ? 'J' :
                        c == 10 ? 'T' :
                        '0' + c));
                }
                return builder.ToString();
            }

            HandType CalculateType(Dictionary<int, int> symbols, int jokers)
            {
                if (symbols.Count == 5)
                {
                    return HandType.HighCard;
                }
                else if (symbols.Count == 4)
                {
                    return HandType.OnePair;
                }
                else if (symbols.Count == 3)
                {
                    return jokers > 0 || symbols.Values.Any(s => s == 3) ? HandType.ThreeOfAKind : HandType.TwoPair;
                }
                else if (symbols.Count == 2)
                {
                    return jokers > 1 || jokers == 1 && symbols.Values.Any(s => s == 3) || symbols.Values.Any(s => s == 4) ? HandType.FourOfAKind : HandType.FullHouse;
                }
                Debug.Assert(symbols.Count == 1 || symbols.Count == 0 && jokers == 5);
                return HandType.FiveOfAKind;
            }

            public void CalculateType()
            {
                var symbols = new Dictionary<int, int>();
                foreach (var card in Cards)
                {
                    if (!symbols.ContainsKey(card))
                    {
                        symbols[card] = 0;
                    }
                    ++symbols[card];
                }
                HandType = CalculateType(symbols, 0);
            }

            public void CalculateTypeJoker()
            {
                var symbols = new Dictionary<int, int>();
                int jokers = 0;
                foreach (var card in Cards)
                {
                    if (card == -1)
                    {
                        ++jokers;
                        continue;
                    }
                    if (!symbols.ContainsKey(card))
                    {
                        symbols[card] = 0;
                    }
                    ++symbols[card];
                }
                HandType = CalculateType(symbols, jokers);
            }

            public int CompareTo(Hand other)
            {
                if (other == null)
                    return 1;
                if (HandType > other.HandType)
                    return 1;
                if (HandType < other.HandType)
                    return -1;
                for (int iCard = 0; iCard < Cards.Length; ++iCard)
                {
                    if (Cards[iCard] > other.Cards[iCard])
                        return 1;
                    if (Cards[iCard] < other.Cards[iCard])
                        return -1;
                }
                return 0;
            }
        }

        static public string Solution()
        {
            var hands = new SortedList<Hand, long>();
            var handsJoker = new SortedList<Hand, long>();
            foreach (var line in File.ReadLines("Inputs/day7.txt"))
            {
                var spaceSplit = line.Split(' ');
                var hand = new Hand();
                var handJoker = new Hand();
                Debug.Assert(spaceSplit[0].Length == hand.Cards.Length);
                for (int iC = 0; iC < hand.Cards.Length; ++iC)
                {
                    hand.Cards[iC] = CardValue(spaceSplit[0][iC]);
                    handJoker.Cards[iC] = CardValueJoker(spaceSplit[0][iC]);
                }
                hand.CalculateType();
                hands.Add(hand, long.Parse(spaceSplit[1]));
                handJoker.CalculateTypeJoker();
                handsJoker.Add(handJoker, long.Parse(spaceSplit[1]));
            }
            long value = 0;
            long valueJoker = 0;
            for (int i = 0; i < hands.Count; ++i)
            {
                value += (i + 1) * hands.Values[i];
                valueJoker += (i + 1) * handsJoker.Values[i];
            }
            return $"{value} - {valueJoker}";
        }
    }
}
