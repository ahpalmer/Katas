using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

internal class CSharp
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
