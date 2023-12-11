using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2023.Days
{
    public static class Day8
    {
        class Ghost
        {
            public int Steps { get; set; } = 0;

            public Ghost(string here, List<bool> directions, Dictionary<string, (string left, string right)> map)
            {
                for (; here[2] != 'Z'; ++Steps)
                {
                    Debug.Assert(map.ContainsKey(here));
                    here = directions[Steps % directions.Count] ? map[here].right : map[here].left;
                }
                Debug.Assert(Steps % directions.Count == 0);
            }
        }

        static long LCM(long a, long b)
        {
            long auxA = a;
            long auxB = b;
            while (auxA != auxB)
            { 
                if (auxA > auxB)
                {
                    auxA -= auxB;
                }
                else
                {
                    auxB -= auxA;
                }
            }
            return a * b / auxA;
        }

        static public string Solution()
        {
            var map = new Dictionary<string, (string left, string right)>();
            List<bool> directions = null;
            foreach (var line in File.ReadLines("Inputs/day8.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                if (directions == null)
                {
                    directions = new List<bool>();
                    foreach (var c in line)
                    {
                        Debug.Assert(c == 'L' || c == 'R');
                        directions.Add(c == 'R');
                    }
                    continue;
                }
                map[line.Substring(0, 3)] = (line.Substring(7, 3), line.Substring(12, 3));
            }
            var ghost = new Ghost("AAA", directions, map);
            var ghosts = new List<Ghost>();
            foreach (var mapKey in map.Keys)
            {
                if (mapKey[2] == 'A')
                {
                    ghosts.Add(new Ghost(mapKey, directions, map));
                }
            }
            long multiSteps = 0;
            if (ghosts.Count == 1)
            {
                multiSteps = ghosts[0].Steps;
            }
            else if (ghosts.Count > 1)
            {
                multiSteps = LCM(ghosts[0].Steps, ghosts[1].Steps);
                for (int iG = 2; iG < ghosts.Count; ++iG)
                {
                    multiSteps = LCM(multiSteps, ghosts[iG].Steps);
                }
            }
            return $"{ghost.Steps} {multiSteps}";
        }
    }
}
