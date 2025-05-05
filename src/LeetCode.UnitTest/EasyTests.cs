using LeetCode;

namespace LeetCode.UnitTest;

[TestClass]
public sealed class EasyTests
{
    private EasyProblem easy;

    [TestInitialize]
    public void Initialize()
    {
        easy = new EasyProblem();
    }

    [TestMethod]
    public void RemoveDuplicatesTest()
    {
        Assert.AreEqual(2, easy.RemoveDuplicatesOne(new int[] { 1, 1, 2 }));
        Assert.AreEqual(5, easy.RemoveDuplicatesOne(new int[] { 1, 1, 2, 4, 4, 5, 6 }));
        Assert.AreEqual(5, easy.RemoveDuplicatesTwo(new int[] { 1, 1, 2, 4, 4, 5, 6 }));
    }

    [TestMethod]
    public void MergeTwoListsTest()
    {
        // Arrange:
        ListNode listOneNodeThree = new ListNode(4);
        ListNode listOneNodeTwo = new ListNode(2, listOneNodeThree);
        ListNode listOneNodeOne = new ListNode(1, listOneNodeTwo);
        ListNode listTwoNodeThree = new ListNode(4);
        ListNode listTwoNodeTwo = new ListNode(3, listTwoNodeThree);
        ListNode listTwoNodeOne = new ListNode(1, listTwoNodeTwo);
        ListNode answerListNodeSix = new ListNode(4);
        ListNode answerListNodeFive = new ListNode(4, answerListNodeSix);
        ListNode answerListNodeFour = new ListNode(3, answerListNodeFive);
        ListNode answerListNodeThree = new ListNode(2, answerListNodeFour);
        ListNode answerListNodeTwo = new ListNode(1, answerListNodeThree);
        ListNode answerListNodeOne = new ListNode(1, answerListNodeTwo);

        CollectionAssert.AreEqual(GetTotalValues(answerListNodeOne), GetTotalValues(easy.MergeTwoLists(listOneNodeOne, listTwoNodeOne)));
    }
    public int[] GetTotalValues(ListNode list)
    {
        List<int> values = new List<int>();
        values.Add(list.val);
        ListNode next = list.next;
        while (next.next != null)
        {
            values.Add(next.val);
            next = next.next;
        }
        return values.ToArray();
    }

    [TestMethod]
    public void LongestCommonPrefixTest()
    {
        Assert.AreEqual("fl", easy.LongestCommonPrefix(["flower", "flow", "flight"]));
        Assert.AreEqual("", easy.LongestCommonPrefix(["dog", "racecar", "car"]));
        Assert.AreEqual("a", easy.LongestCommonPrefix(["a"]));
        Assert.AreEqual("a", easy.LongestCommonPrefix(["a", "ab"]));
        Assert.AreEqual("", easy.LongestCommonPrefix(["", ""]));
    }

    [TestMethod]
    public void IsValidTest()
    {
        Assert.AreEqual(true, easy.IsValid("()"));
        Assert.AreEqual(true, easy.IsValid("()[]{}"));
        Assert.AreEqual(false, easy.IsValid("(]"));
        Assert.AreEqual(true, easy.IsValid("([])"));
    }

    [TestMethod]
    public void CountSymmetricIntegersTest()
    {
        Assert.AreEqual(9, easy.CountSymmetricIntegers(1, 100));
        Assert.AreEqual(4, easy.CountSymmetricIntegers(1200, 1230));
    }
}
