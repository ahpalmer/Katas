using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Katas;

internal class Kyu5
{
    public static List<int[]> QuicksortAlgorithm(int[] data)
    {
        List<int[]> answer = new List<int[]>();
        int icount = 1;
        int jcount = 1;
        bool done = false;
        while (!done)
        {
            Quicksort snapshot = FindNextSnapshot(data, icount, jcount);
            answer.Add(snapshot.data);
            if (icount > jcount)
            {
                done = true;
            }
            icount = snapshot.icount;
            jcount = snapshot.jcount;
        }



        return new List<int[]>();
    }

    public static Quicksort FindNextSnapshot(int[] data, int icount, int jcount)
    {
        int pivot = data[0];
        int i = data[icount];
        int j = data[data.Length - jcount];
        List<int[]> answer = new List<int[]>();
        while (!(i > pivot) || !(j < pivot))
        {
            if (j >= pivot)
            {
                jcount++;
                j = data[data.Length - jcount];
            }
            if (i <= pivot)
            {
                icount++;
                i = data[icount];
            }
        }

        int temp = data[icount];
        data[icount] = data[data.Length - jcount];
        data[data.Length - jcount] = temp;
        Quicksort snapshot = new Quicksort(data, icount, jcount);
        return snapshot;
    }

    // Time: 37 minutes
    public static string GetReadableTime(int seconds)
    {
        StringBuilder sb = new StringBuilder();

        if (seconds == 0)
        {
            return "00:00:00";
        }
        else if (seconds > 359999)
        {
            return "99:59:59";
        }


        int calcSeconds = 0;
        int calcMinutes = 0;
        int calcHours = 0;

        int intermediateMinutes = seconds / 60;
        calcSeconds = seconds - (intermediateMinutes * 60);

        calcHours = intermediateMinutes / 60;
        calcMinutes = intermediateMinutes - (calcHours * 60);

        string secondsStr = calcSeconds.ToString();
        string minutesStr = calcMinutes.ToString();
        string hoursStr = calcHours.ToString();

        if (calcHours == 0)
        {
            hoursStr = "00";
        }
        else if (calcHours < 10)
        {
            StringBuilder sbHour = new StringBuilder();
            sbHour.Append("0").Append(calcHours);
            hoursStr = sbHour.ToString();
        }
        if (calcMinutes == 0)
        {
            minutesStr = "00";
        }
        else if (calcMinutes < 10)
        {
            StringBuilder sbMinute = new StringBuilder();
            sbMinute.Append("0").Append(calcMinutes);
            minutesStr = sbMinute.ToString();
        }
        if (calcSeconds < 10)
        {
            StringBuilder sbSec = new StringBuilder();
            sbSec.Append("0").Append(calcSeconds);
            secondsStr = sbSec.ToString();
        }
        sb.Append(hoursStr).Append(":").Append(minutesStr).Append(":").Append(secondsStr);

        return sb.ToString();
    }

    // Time:23 minutes.  I finished it much faster but had a problem with the test
    // Lesson for today:  Assert.IsEqual does not work on arrays or lists. Use CollectionAssert.AreEqual() instead 
    // Going for speed.  Set up tests ahead of time.
    public static int[] MoveZeroes(int[] arr)
    {
        List<int> arrList = arr.ToList();
        int count = arrList.Count(x => x == 0);
        List<int> clearedList = arrList.Where(x => x != 0).ToList();
        for (int i = 0; i < count; i++)
            clearedList.Add(0);
        int[] answer = clearedList.ToArray();

        return answer;
    }

    //Time: 26 minutes
    internal static string ToUnderscore(int str)
    {
        return str.ToString();
    }

    public static string orderWeight(string strng)
    {
        if (strng == "")
        {
            return "";
        }

        string stringInput = Regex.Replace(strng, @"\s+", " ");
        List<(long, long)> values = new List<(long, long)>();
        List<string> numbersList = stringInput.Trim().Split(" ").ToList();

        foreach(var number in numbersList)
        {
            List<long> individualNums = number.Select(c => Int64.Parse(c.ToString())).ToList();
            (long, long) tempNum = (Int64.Parse(number), individualNums.Sum());
            values.Add(tempNum);
        }

        var sortedList = values
            .OrderBy(c => c.Item2)
            .ThenBy(b => b.Item1.ToString())
            .ToList();

        string answer = string.Join(" ", sortedList.Select(c => c.Item1));
        //string answer = sortedList.Select(c => c.Item1.ToString()).Aggregate((total, next) => (total + next + " ").ToString());
        return answer;
    }

    //Time: 37 minutes
    public static string Rgb(int r, int g, int b)
    {
        int[] inputs = { r, g, b };
        foreach (var i in inputs.Select((value, index) => new { value, index }))
        {
            if (i.value < 0)
            {
                inputs[i.index] = 0;
            }
            else if (i.value > 255)
            {
                inputs[i.index] = 255;
            }
        }

        string red = inputs[0].ToString("X2");
        string green = inputs[1].ToString("X2");
        string blue = inputs[2].ToString("X2");
        string[] strings = { red, green, blue };

        return String.Join("", strings);
    }
    //Here's a better example from codewars:
    public static string RgbExample(int r, int g, int b)
    {
        r = Math.Max(Math.Min(255, r), 0);
        g = Math.Max(Math.Min(255, g), 0);
        b = Math.Max(Math.Min(255, b), 0);
        return String.Format("{0:X2}{1:X2}{2:X2}", r, g, b);
    }

    internal static string ToUnderscore(string str)
    {
        string temp = Regex.Replace(str, @"^[A-Z]", m => m.Value.ToLower());

        string pattern = @"[A-Z]";
        Regex regex = new Regex(pattern);
        MatchEvaluator matchEval = new MatchEvaluator(ReplaceToUnderscore);

        string answer = regex.Replace(temp, matchEval);

        return answer;
    }

    public static string ReplaceToUnderscore(Match match)
    {
        string letter = match.Value.ToLower();
        return "_" + letter;
    }

    //Time: 45 Minutes
    internal static string Rot13(string input)
    {
        string pattern = @"[a-zA-Z]";
        Regex regex = new Regex(pattern);
        MatchEvaluator matchEval = new MatchEvaluator(ReplaceRot13Low);

        string answer = regex.Replace(input, matchEval);

        return answer;
    }

    public static string ReplaceRot13Low(Match match)
    {
        char letter = match.Value.ToString()[0];
        int asciiInt = (int)letter;
        if (asciiInt > 77 && asciiInt < 91)
        {
            char answer = System.Convert.ToChar(asciiInt - 13);
            return answer.ToString();
        }
        else if (asciiInt < 78)
        {
            char answer = System.Convert.ToChar(asciiInt + 13);
            return answer.ToString();
        }
        else if (asciiInt > 96 && asciiInt <= 109)
        {
            char answer = System.Convert.ToChar(asciiInt + 13);
            return answer.ToString();
        }
        else if (asciiInt > 109 && asciiInt < 123)
        {
            char answer = System.Convert.ToChar(asciiInt - 13);
            return answer.ToString();
        }
        else
        {
            return "Error";
        }
    }
}
