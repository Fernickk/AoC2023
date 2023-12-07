using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC2023.Days
{
    public static class Day4
    {
        static public string Solution()
        {
            int points = 0;
            foreach (var line in File.ReadLines("Inputs/day4.txt"))
            {
                var colonSplit = line.Split(':');
                var sets = colonSplit[1].Split('|');
                var winning = sets[0].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => int.Parse(n)).ToHashSet();
                var bet = sets[1].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => int.Parse(n));
                int wins = bet.Count(b => winning.Contains(b));
                if (wins > 0)
                {
                    points += 1 << (wins - 1);
                }
            }
            int cards = 0;
            var multipliers = new List<int>();
            foreach (var line in File.ReadLines("Inputs/day4.txt"))
            {
                int multiplier = 1;
                if (multipliers.Count > 0)
                {
                    multiplier += multipliers[0];
                    multipliers.RemoveAt(0);
                }
                var colonSplit = line.Split(':');
                var sets = colonSplit[1].Split('|');
                var winning = sets[0].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => int.Parse(n)).ToHashSet();
                var bet = sets[1].Split(' ').Where(n => !string.IsNullOrWhiteSpace(n)).Select(n => int.Parse(n));
                int wins = bet.Count(b => winning.Contains(b));
                for (int w = 0; w < wins; ++w)
                {
                    if (w < multipliers.Count)
                    {
                        multipliers[w] += multiplier;
                    }
                    else
                    {
                        multipliers.Add(multiplier);
                    }
                }
                cards += multiplier;
            }
            return $"{points} - {cards}";
        }
    }
}
