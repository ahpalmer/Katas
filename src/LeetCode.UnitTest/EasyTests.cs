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
    public void CountSymmetricIntegersTest()
    {
        Assert.AreEqual(9, easy.CountSymmetricIntegers(1, 100));
        Assert.AreEqual(4, easy.CountSymmetricIntegers(1200, 1230));
    }
}
