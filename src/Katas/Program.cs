namespace Katas;

public class Program
{
    public Program()
    {
    }
    public static void Main(string[] args)
    {
        RunKyu5();
    }

    public static void RunKyu5()
    {
        Kyu5.orderWeight("56 65 74 100 99 68 86 180 90");
    }

    public static void RunKyu8()
    {
        Kyu8 sharp = new Kyu8();
        Console.WriteLine($"-5%2: {((-5) % 2)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(8)}");
        Console.WriteLine($"Should be Odd: {sharp.EvenOrOdd(-5)}");
        Console.WriteLine($"Should be Even: {sharp.EvenOrOdd(-8)}");
    }

    public static void RunKyu6()
    {
        //Kyu6.IsPrime(2147483647);

        //Kyu6.AlphabetPosition("The sunset sets at twelve o' clock.");

        //Kyu6.TestSolution();
        //Kyu6.TestFind();


        //Console.WriteLine(kyu6.Persistence(999));
        //Console.WriteLine(kyu6.Persistence(39));
        //Console.WriteLine(kyu6.Persistence(4));
    }

    public static void RunKyu7()
    {
        Console.WriteLine(Kyu7.Maskify("hello"));
    }
}
