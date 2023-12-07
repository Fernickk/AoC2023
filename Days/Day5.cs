using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC2023.Days
{
    public static class Day5
    {
        class MapEntry
        {
            public long Begin { get; set; }
            public long End { get; set; }
            public long Modifier { get; set; }
        };

        static long Destination(long seed, ref long range, List<SortedList<long, MapEntry>> maps)
        {
            long destination = seed;
            foreach (var map in maps)
            {
                var entry = map.Where(p => p.Key <= destination && destination < p.Value.End).Select(p => p.Value).FirstOrDefault();
                if (entry != null)
                {
                    if (destination + range > entry.End)
                    {
                        range = entry.End - destination;
                    }
                    destination = destination + entry.Modifier;
                }
                else
                {
                    long actualRange = range;
                    var nextEntry = map.Where(p => destination < p.Key && p.Key <= destination + actualRange).Select(p => p.Value).FirstOrDefault();
                    if (nextEntry != null)
                    {
                        range = nextEntry.Begin - destination;
                    }
                }
            }
            return destination;
        }

        static public string Solution()
        {
            List<long> seeds = null;
            SortedList<long, MapEntry> currentMap = null;
            var maps = new List<SortedList<long, MapEntry>>();
            foreach (var line in File.ReadLines("Inputs/day5.txt"))
            {
                if (seeds == null)
                {
                    Debug.Assert(line.Substring(0, "seeds: ".Length) == "seeds: ");
                    seeds = line.Substring("seeds: ".Length).Split(' ').Select(s => long.Parse(s)).ToList();
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentMap != null)
                    {
                        maps.Add(currentMap);
                        currentMap = null;
                    }
                    continue;
                }
                if (line.EndsWith(" map:"))
                {
                    if (currentMap != null)
                    {
                        maps.Add(currentMap);
                        currentMap = null;
                    }
                    currentMap = new SortedList<long, MapEntry>();
                    continue;
                }
                var split = line.Split(' ').Select(n => long.Parse(n)).ToArray();
                Debug.Assert(split.Length == 3);
                currentMap.Add(split[1], new MapEntry
                {
                    Begin = split[1],
                    End = split[1] + split[2],
                    Modifier = split[0] - split[1],
                });
            }
            if (currentMap != null)
            {
                maps.Add(currentMap);
            }
            long closestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                long range = 1;
                var destination = Destination(seed, ref range, maps);
                if (destination < closestLocation)
                {
                    closestLocation = destination;
                }
                Debug.Assert(range == 1);
            }
            long closestLocationRanges = long.MaxValue;
            for (int iSeed = 0; iSeed + 1 < seeds.Count; iSeed += 2)
            {
                long seed = seeds[iSeed];
                long range = seeds[iSeed + 1];
                while (range > 0)
                {
                    long originalRange = range;
                    var destination = Destination(seed, ref range, maps);
                    if (destination < closestLocationRanges)
                    {
                        closestLocationRanges = destination;
                    }
                    Debug.Assert(range <= originalRange);
                    seed += range;
                    range = originalRange - range;
                }
            }
            return $"{closestLocation} - {closestLocationRanges}";
        }
    }
}
