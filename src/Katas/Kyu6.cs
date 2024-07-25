using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Katas;

internal class Kyu6
{
    //Need a constructor for unit tests.  Not actually sure if this is true but it works
    internal Kyu6()
    {
    }

    public long CalculateScrap(int[] scraps, int numberOfRobots)
    {
        List<double> doubleArrayScraps = scraps.Select(x => (double)x).ToList();
        double totalScrap = doubleArrayScraps.Aggregate(1.0, (total, next) => total * (100.0 - next) / 100.0);
        return (long)Math.Ceiling(numberOfRobots * 50 / totalScrap);
    }

    public static string[] SantaSort(string[] unsortedNames)
    {
        // Best solution:
        //return unsortedNames.GroupBy(name => name)
        //            .OrderBy(name => name.Key)
        //            .SelectMany(g => g.Select((name, index) => (name, index)))
        //            .GroupBy(present => present.index, present => present.name)
        //            .SelectMany(present => present)
        //            .ToArray();
        Dictionary<string, int> santaDict = new Dictionary<string, int>();
        foreach(string name in unsortedNames)
        {
            if (santaDict.ContainsKey(name))
            {
                santaDict[name]++;
            }
            else
            {
                santaDict.Add(name, 1);
            }
        }
        List<KeyValuePair<string, int>> santaList = santaDict.ToList();
        List<KeyValuePair<string, int>> sortedSantaList = santaList.OrderBy(x => x.Key).ToList();
        List<string> result = new List<string>();
        int maxCount = sortedSantaList.Count();
        int tempCount = 0;
        for (int i = 0; i < unsortedNames.Count(); i++)
        {
            if (sortedSantaList[tempCount].Value == 0)
            {
                tempCount = tempCount == maxCount - 1 ? 0 : tempCount + 1;
                i--;
            }
            else
            {
                result.Add(sortedSantaList[tempCount].Key);
                sortedSantaList[tempCount] = new KeyValuePair<string, int>(sortedSantaList[tempCount].Key, sortedSantaList[tempCount].Value - 1);
                tempCount = tempCount == maxCount - 1 ? 0 : tempCount + 1;
            }
        }
        return result.ToArray();
    }

    // Time: 46 minutes
    public static string[] CleverSplit(string s)
    {
        Regex regex = new Regex(@"\[.*?\]|\S+");
        MatchCollection matches = regex.Matches(s);

        List<string> stringList = new List<string>();
        foreach (Match match in matches)
        {
            stringList.Add(match.Value);
        }

        string[] split = stringList.ToArray();

        return stringList.ToArray();
    }

    //Time: 6 minutes
    public static bool IsValidWalk(string[] walk)
    {
        if (walk.Count() == 1 && walk.Contains("")) return false;
        if (walk.Count() != 10) return false;

        int north = walk.Where(direction => direction.ToLower() == "n").Count();
        int south = walk.Where(direction => direction.ToLower() == "s").Count();
        int east = walk.Where(direction => direction.ToLower() == "e").Count();
        int west = walk.Where(direction => direction.ToLower() == "w").Count();

        if (north == south && east == west) return true;
        return false;
    }
    //Time: Like 4 minutes
    public static bool IsPangram(string str)
    {
        string input = str.ToLower();
        bool answer = input.Contains("a") && input.Contains("b") && input.Contains("c") && input.Contains("d") && input.Contains("e") && input.Contains("f") && input.Contains("g") && input.Contains("h") && input.Contains("i") && input.Contains("j") && input.Contains("k") && input.Contains("l") && input.Contains("m") && input.Contains("n") && input.Contains("o") && input.Contains("p") && input.Contains("q") && input.Contains("r") && input.Contains("s") && input.Contains("t") && input.Contains("u") && input.Contains("v") && input.Contains("w") && input.Contains("x") && input.Contains("y") && input.Contains("z");

        return answer;
    }

    // Time: forgot to time it.  About 8 minutes.
    public static string Likes(string[] name)
    {
        if (name.Count() == 0) return "no one likes this";
        if (name.Count() == 1) return $"{name.First()} likes this";
        if (name.Count() == 2) return $"{name[0]} and {name[1]} like this";
        if (name.Count() == 3) return $"{name[0]}, {name[1]} and {name[2]} like this";
        else return $"{name[0]}, {name[1]} and {name.Count() - 2} others like this";
    }

    //Time: 57 Minutes
    public static string MyFirstInterpreter(string code)
    {
        string answer = "";
        byte temp = 0;

        foreach (char c in code)
        {
            if (c == '+' && temp == 255)
            {
                temp = 0;
            }
            else if (c == '+')
            {
                temp++;
            }
            else if (c == '.')
            {
                answer = answer + ((char)temp).ToString();
            }
        }

    return answer;
    }

    //Time: 20 minutes
    public static int HighestRank(int[] arr)
    {
        var query = arr.GroupBy(x => x);
        int biggestCount = 0;
        int biggestNum = 0;

        foreach (var number in query)
        {
            var temp = number.Count();

            if (temp > biggestCount)
            {
                biggestCount = temp;
                biggestNum = number.First();
            }
            else if (temp == biggestCount)
            {
                biggestNum = biggestNum > number.First() ? biggestNum : number.First();
            }
        }
        return biggestNum;
    }

    //Time:29 minutes to set up cool new unit tests
    //25 minutes to complete the challenge
    public static bool IsPrime(int n)
    {
        if (n == 0) { return false; }
        if (n == 1) { return false; }
        if (n < 0) { return false; }

        long factor = 2;

        while ((factor * factor) <= n)
        {
            if (n % factor == 0) 
            {
                return false;
            }
            factor++;
        }

        return true;
    }

    //Time:
    //It's a learning day today.  Spent a lot of time on regex learning
    public static string ToCamelCase(string str)
    {
        string replaced;
        string pattern = @"[-_]\w";
        Regex regex = new Regex(pattern);
        MatchEvaluator matchEval = new MatchEvaluator(CapitalizeLetter);

        replaced = regex.Replace(str, matchEval);

        return replaced;
    }

    public static string CapitalizeLetter(Match match)
    {
        return match.Value[1].ToString().ToUpper();
    }

    //Time: 9 minutes
    public static string CreatePhoneNumber(int[] numbers)
    {
        string firstHalf = $"({numbers[0]}{numbers[1]}{numbers[2]})";
        string secondHalf = $"{numbers[3]}{numbers[4]}{numbers[5]}-{numbers[6]}{numbers[7]}{numbers[8]}{numbers[9]}";
        string answer = string.Join(" ", firstHalf, secondHalf);
        return answer;
    }

    //Time: 1hr 40min
    public static int RomanDecode(string roman)
    {
        int currentInt = 0;
        int nextInt = 0;
        int lastInt = 0;
        int answer = 0;
        for(int i = 0; i < roman.Length; i++)
        {
            if (roman.Length == 1)
            {
                return RomanSwitchCase(roman[i]);
            }
            currentInt = RomanSwitchCase(roman[i]);
            if (i == 0)
            {
                nextInt = RomanSwitchCase(roman[i + 1]);
                if (nextInt > currentInt)
                {
                    continue;
                }
                else
                {
                    answer = currentInt;
                    continue;
                }
            }
            if (i == roman.Length - 1)
            {
                lastInt = RomanSwitchCase(roman[i - 1]);
                if (lastInt < currentInt)
                {
                    return answer + (currentInt - lastInt);
                }
                else
                {
                    return answer + currentInt;
                }
            }

            nextInt = RomanSwitchCase(roman[i + 1]);
            lastInt = RomanSwitchCase(roman[i - 1]);
            if (nextInt > currentInt)
            {
                continue;
            }
            else if (currentInt > lastInt)
            {
                answer += (currentInt - lastInt);
                continue;
            }
            else
            {
                answer += currentInt;
            }
        }
        return answer;
    }

    public static int RomanSwitchCase(char c)
    {
        int tempInt = 0;
        switch (c)
        {
            case 'M':
                tempInt = 1000;
                break;
            case 'D':
                tempInt = 500;
                break;
            case 'C':
                tempInt = 100;
                break;
            case 'L':
                tempInt = 50;
                break;
            case 'X':
                tempInt = 10;
                break;
            case 'V':
                tempInt = 5;
                break;
            case 'I':
                tempInt = 1;
                break;
        }
        return tempInt;
    }

    // Time: 1hour 24 mins
    public static string AlphabetPosition(string text)
    {
        //string pattern = "[^a-ZA-Z]";
        //string replacement = "";
        //string shortenedText = Regex.Replace(text, pattern, replacement);
        string pattern2 = @"[A-Z]";
        Regex reg = new Regex("[^a-zA-Z]");
        string shortenedText = reg.Replace(text, "");
        if (shortenedText.Length == 0)
        {
            return string.Empty;
        }

        string shortenedText2 = Regex.Replace(shortenedText, pattern2, m => m.Value.ToLower());
        if (shortenedText2.Length == 0)
        {
            return string.Empty;
        }

        var answer = shortenedText2.Select(c => ((int)c - 96).ToString()).Aggregate((total, next) => total + " " + next);

        return answer;
    }

    //Time: 31 minutes
    //Spent 50 minutes on day 1 setting up a Unit Test that worked with an internal class.  Will solve the Kata tomorrow.
    //Write a function that takes in a string of one or more words, and returns the same string, but with all five or more letter words reversed (Just like the name of this Kata). Strings passed in will consist of only letters and spaces. Spaces will be included only when more than one word is present.
    //Really cool answer from the internet:     return String.Join(" ", sentence.Split(' ').Select(str => str.Length >= 5 ? new string(str.Reverse().ToArray()) : str));
    public static string SpinWords(string sentence)
    {
        string[] strArray = sentence.Split(' ');
        int count = 0;
        string[] answer = new string[strArray.Count()];

        foreach (string str in strArray)
        {
            string tempStr = str;
            if (str.Length > 4)
            {
                var temp = str.ToCharArray().Reverse().ToArray();
                answer[count] = new String(temp);
            }
            else
            {
                answer[count] = str;
            }

            count++;
        }

        string answerFinal = String.Join(" ", answer);
        return answerFinal;
    }

    //Complete the solution so that it splits the string into pairs of two characters. If the string contains an odd number of characters then it should replace the missing second character of the final pair with an underscore ('_').
    public static string[] Solution(string str)
    {
        if (str.Length % 2 == 1)
        {
            str = str + '_';
        }

        string tempStr = str;
        string[] strArray = new string[str.Length / 2];
        int count = 0;

        while (tempStr.Length >= 2)
        {
            strArray[count] = tempStr.Substring(0, 2);
            tempStr = tempStr.Substring(2);
            count++;
        }

        return strArray;
    }

    public static void TestSolution()
    {
        string testOne = "abc";
        string testTwo = "abcdef";
        var testOneArray = Solution(testOne);
        var testTwoArray = Solution(testTwo);

        Console.WriteLine("Test One:");
        foreach (var item in testOneArray)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("Test Two:");
        foreach (var item in testTwoArray)
        {
            Console.WriteLine(item);
        }
    }

    public int Persistence(long n)
    {
        int count = 0;
        while(n.ToString().Length > 1)
        {
            var result = n.ToString().Select(x => Convert.ToInt32(x) - 48);
            n = result.Aggregate((workingTotal, next) => workingTotal * next);
            count++;
        }

        return count;
    }

    //You are given an array (which will have a length of at least 3, but could be very large) containing integers. The array is either entirely comprised of odd integers or entirely comprised of even integers except for a single integer N. Write a method that takes the array as an argument and returns this "outlier" N.
    public static int Find(int[] integers)
    {
        //.Where is the ienumerable method that you should have used
        var stepOne = integers.DistinctBy(x => x % 2);
        var evens = integers.Count(x => x % 2 == 0);
        if (evens == 1)
        {
            return integers.SkipWhile(x => x % 2 == 1).First();
        }
        else
        {
            return integers.SkipWhile(x => x % 2 == 0).First();
        }
    }

    public static void TestFind()
    {
        int[] arrayOne = new int[] { 2, 4, 0, 100, 4, 11, 2602, 36 };
        Console.WriteLine($"Should return 11: {Find(arrayOne)}");
        int[] arrayTwo = new int[] { 160, 3, 1719, 19, 11, 13, -21 };
        Console.WriteLine($"Should return 160: {Find(arrayTwo)}");
    }
}
