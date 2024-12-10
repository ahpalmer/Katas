using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

//This class name refers to the codewars KYU system of difficulty.  8 is the least difficult, 1 is the most difficult.
internal class Kyu8
{
    // Time: 10 mins
    public static long[] Digitize(long n)
    {
        List<long> list = new List<long>();
        foreach(char c in n.ToString())
        {
            list.Add((long)char.GetNumericValue(c));
        }

        list.Reverse();
        return list.ToArray();
    }

    public static int summation(int num)
    {
        return (((num * num) + 8) / 2);
    }
    public string EvenOrOdd(int input)
    {
        if (input < 0)
        {
            input = input * (-1);
        }
        if (input%2 == 0)
        {
            return "Even";
        }
        else if (input%2 == 1)
        {
            return "Odd";
        }
        return "Error";
    }

    public int BoilEggs(int eggs)
    {
        if (eggs <= 0)
        {
            return 0;
        }
        return (((eggs - 1) / 8) + 1)* 5 ;
    }


}
