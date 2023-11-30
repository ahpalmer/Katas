using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace Katas;

internal class Kyu5
{
    //Time: 26 minutes
    internal static string ToUnderscore(int str)
    {
        return str.ToString();
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
