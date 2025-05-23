﻿using System.Diagnostics;

namespace LeetCode;

public class Program
{
    static void Main(string[] args)
    {
        //EasyProblem easy = new EasyProblem();
        //Console.WriteLine("First Test:");
        //var result = TestMemoryUsage<int>(() => easy.RemoveDuplicatesOne(new int[] { 1, 1, 2, 4, 4, 5, 6 }));
        //Console.WriteLine($"first test answer: {result}");
        //Console.WriteLine("Second Test:");
        //var result2 = TestMemoryUsage<int>(() => easy.RemoveDuplicatesTwo(new int[] { 1, 1, 2, 4, 4, 5, 6 }));
        //Console.WriteLine($"second test answer: {result2}");

        MediumProblem medium = new MediumProblem();
        Console.WriteLine("Medium Test:");
        
    }

    public static T TestMemoryUsage<T>(Func<T> methodToTest)
    {
        // Check Memory 
        long memoryBefore = GC.GetTotalMemory(false);
        Console.WriteLine($"Memory before: {memoryBefore} bytes");

        // Check Time
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Run the program
        T result = methodToTest();

        // Measure memory usage after allocation
        long memoryAfter = GC.GetTotalMemory(false);
        Console.WriteLine($"Memory after: {memoryAfter} bytes");

        // Stop time, get elapsed time
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;

        // Calculate memory difference
        long memoryUsed = memoryAfter - memoryBefore;

        Console.WriteLine($"Memory used: {memoryUsed / 1048576.0:F6} MB\nElapsed Time: {elapsed.TotalSeconds:F2} seconds");

        // To trigger garbage collection (if needed):
        GC.Collect();

        return result;
    }
}
