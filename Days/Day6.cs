using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC2023.Days
{
    public static class Day6
    {
        class Record
        {
            public long Time { get; set; }
            public long Distance { get; set; }
        }

        static long GetWays(Record record)
        {
            long ways = 0;
            for (long time = 1; time < record.Time; ++time)
            {
                if ((record.Time - time) * time > record.Distance)
                {
                    ++ways;
                }
            }
            return ways;
        }

        static public string Solution()
        {
            var records = new List<Record>();
            var times = new long[0];
            var distances = new long[0];
            foreach (var line in File.ReadLines("Inputs/day6.txt"))
            {
                var colonSplit = line.Split(':');
                if (colonSplit[0] == "Time")
                {
                    times = colonSplit[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => long.Parse(x)).ToArray();
                }
                else if (colonSplit[0] == "Distance")
                {
                    distances = colonSplit[1].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => long.Parse(x)).ToArray();
                }
            }
            Debug.Assert(times.Length == distances.Length);
            for (int i = 0; i < distances.Length; ++i)
            {
                records.Add(new Record
                {
                    Time = times[i],
                    Distance = distances[i],
                });
            }
            long ways = 1;
            foreach (var record in records)
            {
                ways *= GetWays(record);
            }
            var longRace = new Record();
            foreach (var line in File.ReadLines("Inputs/day6.txt"))
            {
                var colonSplit = line.Split(':');
                if (colonSplit[0] == "Time")
                {
                    longRace.Time = long.Parse(colonSplit[1].Replace(" ", ""));
                }
                else if (colonSplit[0] == "Distance")
                {
                    longRace.Distance = long.Parse(colonSplit[1].Replace(" ", ""));
                }
            }
            long longWays = GetWays(longRace);
            return $"{ways} - {longWays}";
        }
    }
}
