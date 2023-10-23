using System;
using Katas;

namespace Katas.UnitTest;

[TestClass]
public class Kyu6SpinwordsTests
{

    [TestMethod]
    public void Test1()
    {
        Assert.AreEqual("emocleW", Kyu6.SpinWords("Welcome"));
    }

    [TestMethod]
    public void Test2()
    {
        Assert.AreEqual("Hey wollef sroirraw", Kyu6.SpinWords("Hey fellow warriors"));
    }

    [TestMethod]
    public void Test3()
    {
        Assert.AreEqual("This is a test", Kyu6.SpinWords("This is a test"));
    }

    [TestMethod]
    public void Test4()
    {
        Assert.AreEqual("This is rehtona test", Kyu6.SpinWords("This is another test"));
    }

    [TestMethod]
    public void Test5()
    {
        Assert.AreEqual("You are tsomla to the last test", Kyu6.SpinWords("You are almost to the last test"));
    }

    [TestMethod]
    public void Test6()
    {
        Assert.AreEqual("Just gniddik ereht is llits one more", Kyu6.SpinWords("Just kidding there is still one more"));
    }
}