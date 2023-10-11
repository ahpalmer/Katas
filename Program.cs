using Katas;

namespace katas;

public class Program
{
    public Program()
    {

    }
    public static void Main(string[] args)
    {
        //RunCSharp();
        CSharp cSharp = new CSharp();
        Console.WriteLine($"Time for 5 eggs? {cSharp.BoilEggs(5)}"); ;
        Console.WriteLine($"Time for 10 eggs? {cSharp.BoilEggs(10)}"); ;
        Console.WriteLine($"Time for 8 eggs? {cSharp.BoilEggs(8)}"); ;
        Console.WriteLine($"Time for 25 eggs? {cSharp.BoilEggs(25)}"); ;


    }

    public static void RunCSharp()
    {
        CSharp sharp = new CSharp();
        Console.WriteLine($"-5%2: {((-5) % 2)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(8)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(-5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(-8)}");
    }
}
