namespace Katas;

public class Program
{
    public Program()
    {
    }
    public static void Main(string[] args)
    {
        RunKyu6();
    }

    public static void RunKyu4()
    {
        Kyu4.Top3("in the lance-rack, an old's buckler, \na lean hack, and a greyhound for");
    }

    public static void RunKyu5()
    {
        int[] input = new int[] { 30, 50, 5, 30, 51, 10, 20, 99, 18, 30, 100, 33 };
        Kyu5.QuicksortAlgorithm(input);

        //Kyu5.GetReadableTime(10000);
        
        
        //Kyu5.orderWeight("56 65 74 100 99 68 86 180 90");
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
        var answer = Kyu6.Tribonacci(new double[] { 10, 7, 15 }, 23);
        foreach (var item in answer)
        {
            Console.WriteLine(item);
        }
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
