using System.IO;
using System.Linq;

namespace AoC2023.Days
{
    public static class Day1
    {
        static int FirstDigit(string line)
        {
            var values = new (int pos, int value)[]
                {
                    (line.IndexOf("one"), 1),
                    (line.IndexOf("two"), 2),
                    (line.IndexOf("three"), 3),
                    (line.IndexOf("four"), 4),
                    (line.IndexOf("five"), 5),
                    (line.IndexOf("six"), 6),
                    (line.IndexOf("seven"), 7),
                    (line.IndexOf("eight"), 8),
                    (line.IndexOf("nine"), 9),
                    (line.IndexOf('0'), 0),
                    (line.IndexOf('1'), 1),
                    (line.IndexOf('2'), 2),
                    (line.IndexOf('3'), 3),
                    (line.IndexOf('4'), 4),
                    (line.IndexOf('5'), 5),
                    (line.IndexOf('6'), 6),
                    (line.IndexOf('7'), 7),
                    (line.IndexOf('8'), 8),
                    (line.IndexOf('9'), 9),
                }.Where(x => x.pos >= 0);
            int firstDigit = 0;
            int minPos = int.MaxValue;
            foreach (var (pos, value) in values)
            {
                if (pos < minPos)
                {
                    firstDigit = value;
                    minPos = pos;
                }
            }
            return firstDigit;
        }

        static int LastDigit(string line)
        {
            var values = new (int pos, int value)[]
                {
                    (line.LastIndexOf("one"), 1),
                    (line.LastIndexOf("two"), 2),
                    (line.LastIndexOf("three"), 3),
                    (line.LastIndexOf("four"), 4),
                    (line.LastIndexOf("five"), 5),
                    (line.LastIndexOf("six"), 6),
                    (line.LastIndexOf("seven"), 7),
                    (line.LastIndexOf("eight"), 8),
                    (line.LastIndexOf("nine"), 9),
                    (line.LastIndexOf('0'), 0),
                    (line.LastIndexOf('1'), 1),
                    (line.LastIndexOf('2'), 2),
                    (line.LastIndexOf('3'), 3),
                    (line.LastIndexOf('4'), 4),
                    (line.LastIndexOf('5'), 5),
                    (line.LastIndexOf('6'), 6),
                    (line.LastIndexOf('7'), 7),
                    (line.LastIndexOf('8'), 8),
                    (line.LastIndexOf('9'), 9),
                }.Where(x => x.pos >= 0);
            int lastDigit = 0;
            int maxPos = -1;
            foreach (var (pos, value) in values)
            {
                if (pos > maxPos)
                {
                    lastDigit = value;
                    maxPos = pos;
                }
            }
            return lastDigit;
        }

        static public string Solution()
        {
            var digits = new char[10];
            for (int d = 0; d < 10; ++d)
            {
                digits[d] = (char)((int)'0' + d);
            }
            int result1 = 0;
            foreach (var line in File.ReadLines("Inputs/day1.txt"))
            {
                var firstDigitPos = line.IndexOfAny(digits);
                var lastDigitPos = line.LastIndexOfAny(digits);
                result1 += int.Parse(line.Substring(firstDigitPos, 1)) * 10 + int.Parse(line.Substring(lastDigitPos, 1));
            }
            int result2 = 0;
            foreach (var line in File.ReadLines("Inputs/day1.txt"))
            {
                var firstDigit = FirstDigit(line);
                var lastDigit = LastDigit(line);
                result2 += firstDigit * 10 + lastDigit;
            }
            return $"{result1} - {result2}";
        }
    }
}
