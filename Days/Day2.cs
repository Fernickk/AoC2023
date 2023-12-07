using System;
using System.IO;
using System.Linq;

namespace AoC2023.Days
{
    public static class Day2
    {
        static public string Solution()
        {
            int[] totals = { 12, 13, 14 };
            int idSum = 0;
            int powerSum = 0;
            foreach (var line in File.ReadLines("Inputs/day2.txt"))
            {
                var colonSplit = line.Split(':');
                int ID = int.Parse(colonSplit[0].Split(' ')[1]);
                var draws = colonSplit[1].Split(';');
                bool possible = true;
                int[] minTotal = { 0, 0, 0 };
                foreach (var draw in draws)
                {
                    var colors = draw.Split(',');
                    int[] amounts = { 0, 0, 0 };
                    foreach (var color in colors)
                    {
                        var sep = color.Trim().Split(' ');
                        var colorIndex =
                            sep[1] == "red" ? 0 :
                            sep[1] == "green" ? 1 :
                            sep[1] == "blue" ? 2 :
                            throw new Exception($"Invalid color {sep[1]} at {line}");
                        amounts[colorIndex] += int.Parse(sep[0]);
                    }
                    for (int i = 0; i < totals.Length; ++i)
                    {
                        if (amounts[i] > totals[i])
                        {
                            possible = false;
                        }
                        if (amounts[i] > minTotal[i])
                        {
                            minTotal[i] = amounts[i];
                        }
                    }
                }
                if (possible)
                {
                    idSum += ID;
                }
                int power = 1;
                for (int i = 0; i < minTotal.Length; ++i)
                {
                    power *= minTotal[i];
                }
                powerSum += power;
            }
            return $"{idSum} - {powerSum}";
        }
    }
}
