using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

internal class Kyu6
{
    public int Persistence(long n)
    {
        int count = 0;
        while(n.ToString().Length > 1)
        {
            var result = n.ToString().Select(x => Convert.ToInt32(x) - 48);
            n = result.Aggregate((workingTotal, next) => workingTotal * next);
            count++;
        }

        return count;
    }
}
