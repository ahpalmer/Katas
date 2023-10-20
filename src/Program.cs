﻿using Katas;

namespace katas;

public class Program
{
    public Program()
    {

    }
    public static void Main(string[] args)
    {
        RunKyu6();
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
        Kyu6.TestSolution();
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
