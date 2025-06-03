using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode;

public class MediumProblem
{
    public MediumProblem()
    {
    }

    public int LengthOfLongestSubstring(string s)
    {
        if (s.Count() == 0) { return 0; }
        if (s.Count() == 1) { return 1; }

        List<char> inputList = s.Select(c => (char)c).ToList();

        int finalCount = 0;
        //int currentCount = 0;
        int currentStartingIndex = 0;
        for (int i = 1; i <= s.Count(); i++)
        {
            List<string> tempStringList = inputList.Skip(currentStartingIndex).Take(i - currentStartingIndex).Select(c => c.ToString()).ToList();
            if (tempStringList.Count() == tempStringList.Distinct().Count())
            {
                //finalCount = tempStringList.Count();
                if (tempStringList.Count() > finalCount)
                {
                    finalCount = tempStringList.Count();
                }
            }
            else
            {
                currentStartingIndex++;
            }
        }

        return finalCount;
    }

    // LengthOfLongestSubstring best answer from LeetCode:
    //public int LengthOfLongestSubstring(string s)
    //{
    //    // the amount of characters available
    //    Span<int> characters = stackalloc int[256];

    //    int longest = 0, startSub = 0, currentChar = 0;
    //    for (int i = 0; i < s.Length; i++)
    //    {
    //        currentChar = s[i];
    //        // checking if the character has ever appeared
    //        // will never happen in first iteration
    //        if (characters[currentChar] > 0)
    //        {
    //            // if so update the last time you saw the currentchar
    //            // aka start of substring
    //            startSub = Math.Max(startSub, characters[currentChar]);
    //        }

    //        // checks for longest every iterration
    //        // current index - last aperance of currentChar + 1 (for offset)
    //        longest = Math.Max(longest, i - startSub + 1);
    //        // setting the new location of the currentChar.
    //        characters[currentChar] = i + 1;
    //    }
    //    return longest;
    //}

    // LengthOfLongestSubstring most reasonable answer from LeetCode:
    //public int LengthOfLongestSubstring(string s)
    //{
    //    HashSet<char> set = new HashSet<char>();

    //    int maxLength = 0, left = 0, right = 0;

    //    while (right < s.Length)
    //    {
    //        if (set.Contains(s[right]))
    //        {
    //            set.Remove(s[left]);
    //            left++;
    //        }
    //        else
    //        {
    //            set.Add(s[right]);
    //            maxLength = Math.Max(maxLength, right - left + 1);
    //            right++;
    //        }
    //    }
    //    return maxLength;
    //}

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        BigInteger l1Value = GetTotalValues(l1);
        BigInteger l2Value = GetTotalValues(l2);
        BigInteger sum = l1Value + l2Value;

        List<char> chars = sum.ToString().ToCharArray().ToList();
        ListNode final = new ListNode();
        final = new ListNode(Int32.Parse(chars[0].ToString()));
        for (int i = 1; i < chars.Count(); i++)
        {
            final = new ListNode(Int32.Parse(chars[i].ToString()), final);
        }
        return final;
    }

    public BigInteger GetTotalValues(ListNode list)
    {
        List<string> values = new List<string>();
        values.Add(list.val.ToString());
        ListNode next = list.next;
        while (next != null)
        {
            values.Add(next.val.ToString());
            next = next.next;
        }

        values.Reverse();
        return BigInteger.Parse(values.Aggregate("", (current, value) => current + value));
    }
}
