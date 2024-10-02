using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

public class Quicksort
{
    public int icount;
    public int jcount;
    public int[] data;

    public Quicksort(int[] data)
    {
        this.data = data;
        this.icount = 1;
        this.jcount = 1;
    }

    public Quicksort(int[] data, int icount, int jcount)
    {
        this.icount = icount;
        this.jcount = jcount;
        this.data = data;
    }
}
