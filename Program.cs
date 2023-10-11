using Katas;

namespace katas;

public class Program
{
    public Program()
    {

    }
    public static void Main(string[] args)
    {
        CSharp sharp = new CSharp();
        Console.WriteLine($"-5%2: {((-5) % 2)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(8)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(-5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(-8)}");

    }
}
