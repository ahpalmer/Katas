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
        Kyu8 cSharp = new Kyu8();
        Kyu6 kyu6 = new Kyu6();

        kyu6.Persistence(12345);
    }

    public static void RunCSharp()
    {
        Kyu8 sharp = new Kyu8();
        Console.WriteLine($"-5%2: {((-5) % 2)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(8)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(-5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(-8)}");
    }
}
