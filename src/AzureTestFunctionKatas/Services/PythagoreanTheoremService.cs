using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureTestFunctionKatas.Services;

public class PythagoreanTheoremService : IPythagoreanTheoremService
{
    public double Solve(double a, double b)
    {
        double aSquared = Squared(a);
        double bSquared = Squared(b);

        double cSquared = aSquared + bSquared;
        double c = SquareRoot(cSquared);
        Console.WriteLine(c);
        return c;
    }

    public double Squared(double value)
    {
        return value * value;
    }

    public double SquareRoot(double value)
    {
        return Math.Sqrt(value);
    }

    // Example of prioritizing brevity over simplicity
    public double EntirePythagoreanTheorem(double a, double b)
    {
        double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

        return c;
    }
}
