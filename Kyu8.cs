using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

//This class name refers to the codewars KYU system of difficulty.  8 is the least difficult, 1 is the most difficult.
internal class Kyu8
{
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
