using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;

namespace LeetCode;

public class Program
{
    static void Main(string[] args)
    {
        EasyProblem easy = new EasyProblem();
        Console.WriteLine("First Test:");
        int[] arrayOne = new int[] { 0, 1, 2, 2, 3, 0, 4, 2 };
        var result = TestMemoryUsage<int>(() => easy.RemoveElementBasic(ref arrayOne, 2));
        Console.WriteLine($"first test answer: {result}");
        Console.WriteLine("Second Test:");
        int[] arrayTwo = new int[] { 0, 1, 2, 2, 3, 0, 4, 2 };
        var result2 = TestMemoryUsage<int>(() => easy.RemoveElement(ref arrayTwo, 2));
        Console.WriteLine($"second test answer: {result2}");
        Console.WriteLine("Third Test:");
        int[] arrayThree = new int[] { 0, 1, 2, 2, 3, 0, 4, 2 };
        var result3 = TestMemoryUsage<int>(() => easy.RemoveElement(ref arrayThree, 2));
        Console.WriteLine($"Third test answer: {result3}");

        //MediumProblem medium = new MediumProblem();
        //Console.WriteLine("Medium Test:");

    }


    // Your memory-efficient algorithm appears to use more memory than your memory-heavy algorithm - this is a perfect example of why microbenchmarking managed memory in .NET is notoriously unreliable with simple GC.GetTotalMemory() calls.
    // For accurate memory profiling, you'd want to use tools like:
    // BenchmarkDotNet (has built-in memory diagnostics)
    // PerfView or dotMemory
    // Multiple runs with statistical analysis

    //The irony is that the "worse" algorithm appears better due to measurement artifacts!
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

        Console.WriteLine($"Memory used: {memoryUsed / 1048576.0:F6} MB\nElapsed Time: {elapsed.TotalMilliseconds:F2} milliseconds");

        // To trigger garbage collection (if needed):
        GC.Collect();

        return result;
    }
}
