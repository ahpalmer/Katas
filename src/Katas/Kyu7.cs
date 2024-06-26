﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Katas;

internal class Kyu7
{
    public static int SquareDigits(int n)
    {
        string input = n.ToString();
        IEnumerable<double> doubleAnswer = input.Select(i => Math.Pow(Int32.Parse(i.ToString()), 2));
        string strAnswer = String.Join("", doubleAnswer.Select(i => i.ToString()));
        return Int32.Parse(strAnswer);
    }

    public static int GetVowelCount(string str)
    {
        int vowelCount = 0;
        char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

        foreach(char letter in str)
        {
            if (vowels.Contains(letter))
            {
                vowelCount++;
            }
        }

        return vowelCount;
    }

    public static List<int> RemoveSmallest(List<int> numbers)
    {
        if (numbers.Count == 0)
        {
            return numbers;
        }

        int min = numbers.Min();
        var stuff = numbers.FindIndex(x => x == min);
        List<int> answer = numbers.Where((x, y) => y != stuff).ToList();
        return answer;
        //Best answer:
        //numbers.Remove(numbers.DefaultIfEmpty().Min());
        //return numbers;
    }

    public static bool StringEnding(string str, string ending)
    {
        if (ending == "") return true;
        if (str.Length < ending.Length) return false;

        string test = str.Substring(str.Length - ending.Length);
        if (test == ending) return true;
        return false;
    }

    public static long FindNextSquare(long num)
    {
        double squared = Math.Sqrt(num);
        if (squared % 1 == 0 && squared * squared == num)
        {
            long answer = (long)(squared + 1) * (long)(squared + 1);
            return answer;
        }
        return -1;
    }


    public static string Maskify(string cc)
    {
        IEnumerable<char> ccList = cc.AsEnumerable();
        int ccCount = ccList.Count() - 4;
        IEnumerable<char> lastFourList = ccList.TakeLast(4);
        //string answer = lastFourList.Append('#').ToArray().ToString()!;
        List<char> poundList = new List<char>();
        for(int i = 0; i < ccCount; i++)
        {
            poundList.Add('#');
        }
        IEnumerable<char> answerList = poundList.Concat(lastFourList);
        string answerOne = new string(answerList.ToArray());
        return answerOne;

        //Best answer:
        //public static string Maskify(string cc)
        //{
        //    int len = cc.Length;
        //    if (len <= 4)
        //        return cc;

        //    return cc.Substring(len - 4).PadLeft(len, '#');
        //}
    }
}

public static class JadenCase
{
    // Time: 5 minutes
    public static string ToJadenCase(this string phrase)
    {
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
        return myTI.ToTitleCase(phrase);
    }
}