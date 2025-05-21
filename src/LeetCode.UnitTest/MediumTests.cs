using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.UnitTest;

[TestClass]
public class MediumTests
{
    private MediumProblem _medium;

    [TestInitialize]
    public void Initialize()
    {
        _medium = new MediumProblem();
    }

    [TestMethod]
    public void AddTwoNumbersTest()
    {
        // Arrange:
        ListNode listOneNodeThree = new ListNode(4);
        ListNode listOneNodeTwo = new ListNode(2, listOneNodeThree);
        ListNode listOneNodeOne = new ListNode(1, listOneNodeTwo);
        ListNode listTwoNodeThree = new ListNode(4);
        ListNode listTwoNodeTwo = new ListNode(3, listTwoNodeThree);
        ListNode listTwoNodeOne = new ListNode(1, listTwoNodeTwo);
        ListNode answerListNodeThree = new ListNode(8);
        ListNode answerListNodeTwo = new ListNode(5, answerListNodeThree);
        ListNode answerListNodeOne = new ListNode(2, answerListNodeTwo);
        Assert.AreEqual(GetTotalValues(answerListNodeOne), GetTotalValues(_medium.AddTwoNumbers(listOneNodeOne, listTwoNodeOne)));
    }

    [TestMethod]
    public void AddTwoNumbersLeetTest()
    {
        // Arrange:
        ListNode listOneNodeOne = new ListNode(9);
        ListNode listTwoNodeTen = new ListNode(9);
        ListNode listTwoNodeNine = new ListNode(9, listTwoNodeTen);
        ListNode listTwoNodeEight = new ListNode(9, listTwoNodeNine);
        ListNode listTwoNodeSeven = new ListNode(9, listTwoNodeEight);
        ListNode listTwoNodeSix = new ListNode(9, listTwoNodeSeven);
        ListNode listTwoNodeFive = new ListNode(9, listTwoNodeSix);
        ListNode listTwoNodeFour = new ListNode(9, listTwoNodeFive);
        ListNode listTwoNodeThree = new ListNode(9, listTwoNodeFour);
        ListNode listTwoNodeTwo = new ListNode(9, listTwoNodeThree);
        ListNode listTwoNodeOne = new ListNode(1, listTwoNodeTwo);
        ListNode answerListNodeEleven = new ListNode(1);
        ListNode answerListNodeTen = new ListNode(0, answerListNodeEleven);
        ListNode answerListNodeNine = new ListNode(0, answerListNodeTen);
        ListNode answerListNodeEight = new ListNode(0, answerListNodeNine);
        ListNode answerListNodeSeven = new ListNode(0, answerListNodeEight);
        ListNode answerListNodeSix = new ListNode(0, answerListNodeSeven);
        ListNode answerListNodeFive = new ListNode(0, answerListNodeSix);
        ListNode answerListNodeFour = new ListNode(0, answerListNodeFive);
        ListNode answerListNodeThree = new ListNode(0, answerListNodeFour);
        ListNode answerListNodeTwo = new ListNode(0, answerListNodeThree);
        ListNode answerListNodeOne = new ListNode(0, answerListNodeTwo);
        Assert.AreEqual(GetTotalValues(answerListNodeOne), GetTotalValues(_medium.AddTwoNumbers(listOneNodeOne, listTwoNodeOne)));
    }

    public long GetTotalValues(ListNode list)
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
        return Int64.Parse(values.Aggregate("", (current, value) => current + value));
    }
}
