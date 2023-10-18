using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

internal class Kyu7
{
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
