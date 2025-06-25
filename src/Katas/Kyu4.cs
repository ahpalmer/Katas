using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Katas
{
    public class Kyu4
    {
        // Codewars style ranking system
        public class User
        {
            public int rank;
            public int progress = 0;

            public void incProgress(int actRank)
            {
                Console.WriteLine($"rank: {rank.ToString()}, progress: {progress.ToString()}, actRank: {actRank.ToString()}");
                if (actRank > 8 || actRank < -8 || actRank == 0)
                {
                    throw new ArgumentException();
                }
                int diff;
                if (rank == 8)
                {
                    progress = 0;
                    diff = 3;
                }
                else if (rank < 0 && actRank > 0)
                {
                    diff = rank - actRank + 1;
                }
                else if (rank > 0 && actRank < 0)
                {
                    diff = rank - actRank - 1;
                }
                else
                {
                    diff = rank - actRank;
                }

                if (diff < 2)
                {
                    AdvanceProgress(diff);
                }

            }

            public void AdvanceProgress(int diff)
            {
                if (diff == 0)
                {
                    progress = progress + 3;
                    if (progress > 100) { ProgressOverOneHundred(); }
                }
                else if (diff == 1)
                {
                    progress = progress + 1;
                    if (progress > 100) { ProgressOverOneHundred(); }
                }
                else
                {
                    progress = progress + (((int)Math.Pow(diff, 2) * 10));
                    if (progress > 100) { ProgressOverOneHundred(); }
                }
            }

            public void ProgressOverOneHundred()
            {
                int permanentProgress = progress;
                for (int i = 0; i < permanentProgress / 100; i++)
                {
                    rank++;
                    progress = progress - 100;
                    if (rank >=8)
                    {
                        rank = 8;
                        progress = 0;
                    }

                    if (rank == 0)
                    {
                        rank++;
                    }
                }
            }
        }

        public static string Add(string a, string b)
        {
            Console.WriteLine($"a: {a}, b: {b}");
            string leftAnswer = "0";
            if (a.Count() > 18 || b.Count() > 18)
            {
                string lastEighteenA = a.Length >= 18
                    ? a.Substring(0, a.Length - 18)
                    : a.PadLeft(18, '0');
                string lastEighteenB = b.Length >= 18
                    ? b.Substring(0, b.Length - 18)
                    : b.PadLeft(18, '0');
                leftAnswer = Add(lastEighteenA, lastEighteenB);
                a = a.Substring(a.Length - 18, 18);
                b = b.Substring(b.Length - 18, 18);
            }
            long answer = long.Parse(a) + long.Parse(b);
            if (answer.ToString().Length > 18)
            {
                string answerStr = answer.ToString().Substring(1);
                var tempRightDigits = leftAnswer.Substring(leftAnswer.Length - 3, 3);
                var rightDigits = (Int32.Parse(tempRightDigits) + 1).ToString();
                if (rightDigits.Length < 3)
                {
                    rightDigits = rightDigits.PadLeft(3, '0');
                }

                leftAnswer = leftAnswer.Substring(0, leftAnswer.Length - 3);
                return leftAnswer + rightDigits + answerStr;
            }
            if (answer.ToString().Length < 18)
            {
                string answerStr = answer.ToString().PadLeft(18, '0');
                return (leftAnswer + answerStr).TrimStart('0');
            }

            return (leftAnswer + answer.ToString()).TrimStart('0');
        }

        public static List<int> TreeByLevels(Node node)
        {
            
            return null;
        }

        public static void CreateNodes()
        {
            var nodes = new List<Node>();
            Node one = new Node(null, null, 1);
            Node two = new Node(null, one, 2);
        }

        public static List<string> Top3(string s)
        {
            // Brilliant regex: @"('*[a-z]'*)+"
            //return Regex.Matches(s.ToLowerInvariant(), @"('*[a-z]'*)+")
            //.GroupBy(match => match.Value)
            //.OrderByDescending(g => g.Count())
            //.Select(p => p.Key)
            //.Take(3)
            //.ToList();

            //Regex rgx = new Regex(@"[\p{P}](?!\w)|(?<!\w)[\p{P}]");
            //string noPunctuation = rgx.Replace(noCapitals, " ");
            //Regex regexNoPunctuationTwo = new Regex(@"[\p{P}-[']]");
            //string noPunctuationTwo = regexNoPunctuationTwo.Replace(noCapitals, " ");
            //Regex regexNoLines = new Regex(@"[\r\n]");
            //string noLines = regexNoLines.Replace(noPunctuationTwo, " ");
            //Regex regexNoExtraSpaces = new Regex(@"\s+");
            //string noExtraSpaces = regexNoExtraSpaces.Replace(noLines, " ").Trim();
            //List<string> stringList = noExtraSpaces.Split(' ').ToList();
            //List<string> trimmedList = stringList.Where(s => s != "").ToList();
            //Dictionary<string, int> wordCount = new Dictionary<string, int>();
            //List<string> result = new List<string>();

            Console.WriteLine();
            string noCapitals = s.ToLower();
            List<string> allWords = new List<string>();
            MatchCollection wordMatches = Regex.Matches(noCapitals, @"('*[a-z]'*)+");
            foreach (Match match in wordMatches)
            {
                allWords.Add(match.ToString());
            }

            Dictionary<string, int> wordCount = new Dictionary<string, int>();
            List<string> result = new List<string>();

            if (allWords.Count == 0)
            {
                return result;
            }
            foreach (string word in allWords)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount.Add(word, 1);
                }
            }

            var answer = wordCount.OrderByDescending(key => key.Value);
            foreach (var item in answer.Take(3))
            {
                result.Add(item.Key);
            }
            return result;
            //if (wordCount.Count < 3)
            //{
            //    foreach (var item in answer)
            //    {
            //        result.Add(item.Key);
            //    }
            //    return result;
            //}
            //else
            //{
            //    foreach (var item in answer.Take(3))
            //    {
            //        result.Add(item.Key);
            //    }
            //    return result;
            //}
        }

        // There is a way to do this quickly.  Look at advent of code day 5 2023.
        // The brute force method would be to create an int[] with all ints between the given ranges and then count the int[]
        // I don't want to do that.  It wasn't performant in AOC D5 and I want to learn to do it better.
        // I don't think there's a method to do it.

        public static int SumIntervals((int, int)[] intervals)
        {
            Array.Sort(intervals, (x, y) => x.Item1 - y.Item1);
            int max = int.MinValue;
            int total = 0;
            foreach (var interval in intervals)
            {
                max = Math.Max(max, interval.Item1);
                total += Math.Max(0, interval.Item2 - max);
                max = Math.Max(max, interval.Item2);
            }
            return total;
        }

        //public static int SumIntervals((int, int)[] inputIntervals)
        //{
        //    Array.Sort(inputIntervals, (x, y) => x.Item1 - y.Item1);
        //    List<(int, int)> workingList = new List<(int, int)> ();
        //    bool lastRunWasCombo = false;
        //    int j = 0;
        //    for (int i = 0; i < inputIntervals.Length; i++)
        //    {
        //        j = i;
        //        if (i == 0)
        //        {
        //            bool check = CheckOverlap(inputIntervals[i], inputIntervals[i + 1]);
        //            if (check)
        //            {
        //                workingList.Add(CombineIntervals(inputIntervals[i], inputIntervals[i + 1]));
        //                i++;
        //            }
        //            else
        //            {
        //                workingList.Add(inputIntervals[i]);
        //            }
        //        }
        //        else
        //        {
        //            if (i >= inputIntervals.Length - 1)
        //            {
        //                workingList.Add(inputIntervals[i]);
        //            }
        //            //else if (i >= inputIntervals.Length - 1 && lastRunWasCombo == false)
        //            //{
        //            //    workingList.Add(inputIntervals[i]);
        //            //}
        //            if (CheckOverlap(inputIntervals[i], inputIntervals[i + 1]))
        //            {
        //                var itemOne = workingList[i].Item1;
        //                var itemTwo = workingList[i].Item2;
        //                workingList.Remove((itemOne, itemTwo));
        //                workingList.Add(CombineIntervals((itemOne, itemTwo), inputIntervals[i + 1]));
        //                i++;
        //                j++;
        //                lastRunWasCombo = true;
        //            }
        //            else
        //            {
        //                workingList.Add(inputIntervals[i]);
        //                lastRunWasCombo = false;
        //            }
        //        }
        //    }

        //    if (!lastRunWasCombo && j == )
        //    {
        //        workingList.Add(inputIntervals[j]);
        //    }
        //    return workingList.Select(interval => interval.Item2 - interval.Item1).Sum();
        //}


        public static bool CheckOverlap((int, int) intervalOne, (int, int) intervalTwo)
        {
            if (intervalTwo.Item1 < intervalOne.Item1 && intervalOne.Item1 < intervalTwo.Item2 ||
                intervalTwo.Item1 < intervalOne.Item2 && intervalOne.Item2 < intervalTwo.Item2)
            { return true; }

            return false;
        }

        public static (int, int) CombineIntervals((int, int) intervalOne, (int, int) intervalTwo)
        {
            int minStart = Math.Min(intervalOne.Item1, intervalTwo.Item1);
            int maxEnd = Math.Max(intervalOne.Item2, intervalTwo.Item2);

            return (minStart, maxEnd);
        }

        // No time, I didn't solve this one honestly.  I asked AI if there was a pattern for hamming numbers and it gave me an entire solved algorithm
        // Turn off copilot before you start your kata every morning.  It was too good for me to learn.
        public static long hamming(int n)
        {
            List<long> hamingNumbers = new List<long>();
            hamingNumbers.Add(1);
            int i2 = 0;
            int i3 = 0;
            int i5 = 0;

            for (long i = 1; i < n; i ++)
            {
                long nextSmallestNum = Math.Min((Math.Min(hamingNumbers[i5] * 5, hamingNumbers[i3] * 3)), hamingNumbers[i2] * 2);
                hamingNumbers.Add(nextSmallestNum);
                if (nextSmallestNum == hamingNumbers[i2] * 2)
                {
                    i2++;
                }
                if (nextSmallestNum == hamingNumbers[i3] * 3)
                {
                    i3++;
                }
                if (nextSmallestNum == hamingNumbers[i5] * 5)
                {
                    i5++;
                }
            }

            return hamingNumbers.Last();
        }
    }
}
