using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas
{
    public class Kyu4
    {
        // No time, I didn't solve this one honestly.  I asked AI if there was a pattern for hamming numbers and it gave me an entire solved algorithm in python.  It is a useful bit of knowledge to have but I can't say that I solved it myself.
        // Turn off copilot before you start your kata every morning.  It was too powerful today for me to learn much.
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
