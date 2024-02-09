using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas
{
    public class Kyu4
    {
        // There is a way to do this quickly.  Look at advent of code day 5 2023.
        // The brute force method would be to create an int[] with all ints between the given ranges and then count the int[]
        // I don't want to do that.  It wasn't performant in AOC D5 and I want to learn to do it better.
        // I don't think there's a method to do it.
        public static int SumIntervals((int, int)[] intervals)
        {
            for (int i = 0; i < intervals.Length - 1; i++)
            {
                bool check = CheckOverlap(intervals[i], intervals[i + 1]);
                if (check)
                {
                    CombineIntervals(intervals[i], intervals[i + 1]);
                    i++;
                    break;
                }
            }

            // Todo: finish tomorrow
            throw new NotImplementedException();
        }

        public static bool CheckOverlap((int, int) intervalOne, (int, int) intervalTwo)
        {
            if (intervalTwo.Item1 < intervalOne.Item1 && intervalOne.Item1 < intervalTwo.Item2 ||
                intervalTwo.Item1 < intervalOne.Item2 && intervalOne.Item2 < intervalTwo.Item2)
            { return true; }

            return false;
        }

        public static (int, int) CombineIntervals((int, int) intervalOne, (int, int) intervalTwo)
        {
            int minStart = Math.Min(intervalOne.Item1, intervalTwo.Item2);
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
