using Katas;

namespace Katas.UnitTest;

[TestClass]
public class Kyu7Tests
{
    [TestMethod]
    public void StringEndingTest()
    {
        Assert.AreEqual(true, Kyu7.StringEnding("samurai", "ai"));
        Assert.AreEqual(false, Kyu7.StringEnding("sumo", "omo"));
        Assert.AreEqual(true, Kyu7.StringEnding("ninja", "ja"));
        Assert.AreEqual(true, Kyu7.StringEnding("sensei", "i"));
        Assert.AreEqual(false, Kyu7.StringEnding("samurai", "ra"));
        Assert.AreEqual(false, Kyu7.StringEnding("abc", "abcd"));
        Assert.AreEqual(true, Kyu7.StringEnding("abc", ""));
        Assert.AreEqual(false, Kyu7.StringEnding(":-)", ":-("));
        Assert.AreEqual(true, Kyu7.StringEnding("!@#$%^&*() :-)", ":-)"));
        Assert.AreEqual(false, Kyu7.StringEnding("abc\n", "abc"));
    }

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
