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
