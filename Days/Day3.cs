using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AoC2023.Days
{
    public static class Day3
    {
        static int Val(char c)
        {
            return c - '0';
        }

        static bool IsNum(char c)
        {
            return c >= '0' && c <= '9';
        }

        static bool IsSymbol(char c)
        {
            return c != '.' && !IsNum(c);
        }

        static int GetNumberFromPos(string line, int iC)
        {
            int result = 0;
            int power = 1;
            for (int iCPrev = iC; iCPrev >= 0 && IsNum(line[iCPrev]); --iCPrev)
            {
                result += Val(line[iCPrev]) * power;
                power *= 10;
            }
            for (int iCNext = iC + 1; iCNext < line.Length && IsNum(line[iCNext]); ++iCNext)
            {
                result = result * 10 + Val(line[iCNext]);
            }
            return result;
        }

        static int GearSum(string preLine, string line, string postLine)
        {
            Debug.Assert(line.Length == preLine.Length);
            Debug.Assert(line.Length == postLine.Length);
            int sum = 0;
            for (int iC = 0; iC < line.Length; ++iC)
            {
                if (line[iC] != '*')
                    continue;
                var adjacents = new List<int>();
                if (iC > 0 && IsNum(preLine[iC - 1]))
                {
                    adjacents.Add(GetNumberFromPos(preLine, iC - 1));
                    if (!IsNum(preLine[iC]) && iC + 1 < line.Length && IsNum(preLine[iC + 1]))
                    {
                        adjacents.Add(GetNumberFromPos(preLine, iC + 1));
                    }
                }
                else if (IsNum(preLine[iC]))
                {
                    adjacents.Add(GetNumberFromPos(preLine, iC));
                }
                else if (iC + 1 < line.Length && IsNum(preLine[iC + 1]))
                {
                    adjacents.Add(GetNumberFromPos(preLine, iC + 1));
                }
                if (iC > 0 && IsNum(line[iC - 1]))
                {
                    adjacents.Add(GetNumberFromPos(line, iC - 1));
                }
                if (iC + 1 < line.Length && IsNum(line[iC + 1]))
                {
                    adjacents.Add(GetNumberFromPos(line, iC + 1));
                }
                if (iC > 0 && IsNum(postLine[iC - 1]))
                {
                    adjacents.Add(GetNumberFromPos(postLine, iC - 1));
                    if (!IsNum(postLine[iC]) && iC + 1 < line.Length && IsNum(postLine[iC + 1]))
                    {
                        adjacents.Add(GetNumberFromPos(postLine, iC + 1));
                    }
                }
                else if (IsNum(postLine[iC]))
                {
                    adjacents.Add(GetNumberFromPos(postLine, iC));
                }
                else if (iC + 1 < line.Length && IsNum(postLine[iC + 1]))
                {
                    adjacents.Add(GetNumberFromPos(postLine, iC + 1));
                }
                if (adjacents.Count == 2)
                {
                    sum += adjacents[0] * adjacents[1];
                }
            }
            return sum;
        }

        static int LineSum(string preLine, string line, string postLine)
        {
            Debug.Assert(line.Length == preLine.Length);
            Debug.Assert(line.Length == postLine.Length);
            int sum = 0;
            int currentNum = 0;
            int currentNumLength = 0;
            for (int iC = 0; iC < line.Length + 1; ++iC)
            {
                if (iC < line.Length)
                {
                    if (IsNum(line[iC]))
                    {
                        currentNum = currentNum * 10 + Val(line[iC]);
                        ++currentNumLength;
                        continue;
                    }
                }
                if (currentNumLength == 0)
                {
                    continue;
                }
                bool adjacent = false;
                for (int iS = Math.Max(0, iC - currentNumLength - 1); iS < Math.Min(iC + 1, line.Length - 1); ++iS)
                {
                    if (IsSymbol(preLine[iS]) || IsSymbol(postLine[iS]))
                    {
                        adjacent = true;
                        break;
                    }
                }
                adjacent =
                    adjacent ||
                    iC - currentNumLength > 0 && IsSymbol(line[iC - currentNumLength - 1]) ||
                    iC < line.Length && IsSymbol(line[iC]);
                if (adjacent)
                {
                    sum += currentNum;
                }
                currentNum = 0;
                currentNumLength = 0;
            }
            return sum;
        }

        static public string Solution()
        {
            string preLine = null;
            string line = null;
            string emptyLine = null;
            int lineSum = 0;
            int gearSum = 0;
            foreach (var postLine in File.ReadLines("Inputs/day3.txt"))
            {
                if (preLine == null)
                {
                    emptyLine = new string('.', postLine.Length);
                    preLine = emptyLine;
                    line = postLine;
                    continue;
                }
                lineSum += LineSum(preLine, line, postLine);
                gearSum += GearSum(preLine, line, postLine);
                preLine = line;
                line = postLine;
            }
            lineSum += LineSum(preLine, line, emptyLine);
            gearSum += GearSum(preLine, line, emptyLine);
            return $"{lineSum} - {gearSum}";
        }
    }
}
