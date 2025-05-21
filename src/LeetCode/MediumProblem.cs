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
