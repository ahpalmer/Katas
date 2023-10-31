using System;
using Katas;

namespace Katas.UnitTest;

[TestClass]
public class Kyu6Tests
{
    [TestMethod]
    public void ToCamelCaseTest()
    {
        Assert.AreEqual("theStealthWarrior", Kyu6.ToCamelCase("the_stealth_warrior"), "Kata.ToCamelCase('the_stealth_warrior') did not return correct value");
        Assert.AreEqual("TheStealthWarrior", Kyu6.ToCamelCase("The-Stealth-Warrior"), "Kata.ToCamelCase('The-Stealth-Warrior') did not return correct value");
    }

    [TestMethod]
    public void PhoneNumberTest()
    {
        Assert.AreEqual("(123) 456-7890", Kyu6.CreatePhoneNumber(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }));
        Assert.AreEqual("(111) 111-1111", Kyu6.CreatePhoneNumber(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }));
    }

    [TestMethod]
    public void RomanDecodeTest()
    {
        Assert.AreEqual(1666, Kyu6.RomanDecode("MDCLXVI"));
        Assert.AreEqual(1990, Kyu6.RomanDecode("MCMXC"));
        Assert.AreEqual(2008, Kyu6.RomanDecode("MMVIII"));

    }

    [TestMethod]
    public void AlphabetTest()
    {
        Assert.AreEqual("20 8 5 19 21 14 19 5 20 19 5 20 19 1 20 20 23 5 12 22 5 15 3 12 15 3 11", Kyu6.AlphabetPosition("The sunset sets at twelve o' clock."));
        Assert.AreEqual("20 8 5 14 1 18 23 8 1 12 2 1 3 15 14 19 1 20 13 9 4 14 9 7 8 20", Kyu6.AlphabetPosition("The narwhal bacons at midnight."));
    }

    [TestMethod]
    public void SpinTests()
    {
        Assert.AreEqual("emocleW", Kyu6.SpinWords("Welcome"));
        Assert.AreEqual("Hey wollef sroirraw", Kyu6.SpinWords("Hey fellow warriors"));
        Assert.AreEqual("This is a test", Kyu6.SpinWords("This is a test"));
        Assert.AreEqual("This is rehtona test", Kyu6.SpinWords("This is another test"));
        Assert.AreEqual("You are tsomla to the last test", Kyu6.SpinWords("You are almost to the last test"));
        Assert.AreEqual("Just gniddik ereht is llits one more", Kyu6.SpinWords("Just kidding there is still one more"));
    }
}