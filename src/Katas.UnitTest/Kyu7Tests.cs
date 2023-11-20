using Katas;

namespace Katas.UnitTest;

[TestClass]
public class Kyu7Tests
{
    [TestMethod]
    public void FindNextSquareTest()
    {
        Assert.AreEqual(-1, Kyu7.FindNextSquare(155));
        Assert.AreEqual(144, Kyu7.FindNextSquare(121));
        Assert.AreEqual(676, Kyu7.FindNextSquare(625));
        Assert.AreEqual(320356, Kyu7.FindNextSquare(319225));
        Assert.AreEqual(15241630849, Kyu7.FindNextSquare(15241383936));
        Assert.AreEqual(-1, Kyu7.FindNextSquare(4503599627370497));
    }
}
