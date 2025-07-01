using Katas;

namespace Katas.UnitTest;

[TestClass]
public class Kyu8Tests
{
    [TestMethod]
    public void TestBoolToWord()
    {
        Assert.AreEqual("Yes", Kyu8.boolToWord(true));
        Assert.AreEqual("No", Kyu8.boolToWord(false));
    }

    [TestMethod]
    public void TestOpposite()
    {
        Assert.AreEqual(-1, Kyu8.Opposite(1));
        Assert.AreEqual(0, Kyu8.Opposite(0));
        Assert.AreEqual(1, Kyu8.Opposite(-1));
    }

    [TestMethod]
    public void Test1()
    {
        CollectionAssert.AreEqual(new long[] { 1, 3, 2 }, Kyu8.Digitize(231));
    }

    [TestMethod]
    public void Test2()
    {
        CollectionAssert.AreEqual(new long[] { 0 }, Kyu8.Digitize(0));
    }

    [TestMethod]
    public void Test3()
    {
        CollectionAssert.AreEqual(new long[] { 1, 2, 3, 4, 5 }, Kyu8.Digitize(54321));
    }
}
