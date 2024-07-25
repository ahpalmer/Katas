using System;
using Katas;

namespace Katas.UnitTest;

[TestClass]
public class Kyu6Tests
{
    [TestMethod]
    public void TestRobotorFactory()
    {
        var factory = new Kyu6();
        Assert.AreEqual(5000, factory.CalculateScrap(new[] { 10 }, 90));
        Assert.AreEqual(3820, factory.CalculateScrap(new[] { 20, 10 }, 55));
    }

    [TestMethod]
    public void TestSantaSort()
    {
        var testData = new[] { "Sarah", "Charlie", "Mo" };
        var sorted = Kyu6.SantaSort(testData);
        CollectionAssert.AreEqual(new[] { "Charlie", "Mo", "Sarah" }, sorted);

        var testData2 = new[] { "Sarah", "Sarah", "Charlie", "Charlie", "Charlie", "Mo", "Mo" };
        var sorted2 = Kyu6.SantaSort(testData2);
        CollectionAssert.AreEqual(new[] { "Charlie", "Mo", "Sarah", "Charlie", "Mo", "Sarah", "Charlie" }, sorted2);

        var testData3 = new[] { "Sarah", "Sarah", "Charlie", "Charlie", "Charlie", "Mo", "Mo", "Charlie", "Charlie", "Charlie" };
        var sorted3 = Kyu6.SantaSort(testData3);
        CollectionAssert.AreEqual(new[] { "Charlie", "Mo", "Sarah", "Charlie", "Mo", "Sarah", "Charlie", "Charlie", "Charlie", "Charlie" }, sorted3);
    }

    [TestMethod]
    public void TestCleverSplit()
    {
        CollectionAssert.AreEqual(new string[] { "Buy", "a", "!car", "[!red green !white]", "[cheap or !new]" }, Kyu6.CleverSplit("Buy a !car [!red green !white] [cheap or !new]"));
        CollectionAssert.AreEqual(new string[] { "!Learning", "!C#", "is", "[a joy]" }, Kyu6.CleverSplit("!Learning !C# is [a joy]"));
        CollectionAssert.AreEqual(new string[] { "[Cats and dogs]", "are", "!beautiful", "and", "[cute]" }, Kyu6.CleverSplit("[Cats and dogs] are !beautiful and [cute]"));
    }

    [TestMethod]
    public void TestIsValidWalk()
    {
        Assert.AreEqual(true, Kyu6.IsValidWalk(new string[] { "n", "s", "n", "s", "n", "s", "n", "s", "n", "s" }), "should return true");
        Assert.AreEqual(false, Kyu6.IsValidWalk(new string[] { "w", "e", "w", "e", "w", "e", "w", "e", "w", "e", "w", "e" }), "should return false");
        Assert.AreEqual(false, Kyu6.IsValidWalk(new string[] { "w" }), "should return false");
        Assert.AreEqual(false, Kyu6.IsValidWalk(new string[] { "n", "n", "n", "s", "n", "s", "n", "s", "n", "s" }), "should return false");
    }

    [TestMethod]
    public void TestIsPangram()
    {
        Assert.AreEqual(true, Kyu6.IsPangram("The Quick brown fox jumps over the lazy dog."));
    }

    [TestMethod]
    public void TestLikes()
    {
        Assert.AreEqual("no one likes this", Kyu6.Likes(new string[0]));
        Assert.AreEqual("Peter likes this", Kyu6.Likes(new string[] { "Peter" }));
        Assert.AreEqual("Jacob and Alex like this", Kyu6.Likes(new string[] { "Jacob", "Alex" }));
        Assert.AreEqual("Max, John and Mark like this", Kyu6.Likes(new string[] { "Max", "John", "Mark" }));
        Assert.AreEqual("Alex, Jacob and 2 others like this", Kyu6.Likes(new string[] { "Alex", "Jacob", "Mark", "Max" }));
    }

    [TestMethod]
    public void MyFirstInterpreterTest()
    {
        Assert.AreEqual("Hello, World!", Kyu6.MyFirstInterpreter("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+++++++++++++++++++++++++++++.+++++++..+++.+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+++++++++++++++++++++++++++++++++++++++++++++++++++++++.++++++++++++++++++++++++.+++.++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++."));

        Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Kyu6.MyFirstInterpreter("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+."));
    }

    [TestMethod]
    public void HighestRankTest()
    {
        var arr1 = new int[] { 12, 10, 8, 12, 7, 6, 4, 10, 12 };
        var arr2 = new int[] { 12, 10, 8, 12, 7, 6, 4, 10, 12, 10 };
        var arr3 = new int[] { 12, 10, 8, 8, 3, 3, 3, 3, 2, 4, 10, 12, 10 };
        
        var result1 = Kyu6.HighestRank(arr1);
        var result2 = Kyu6.HighestRank(arr2);
        var result3 = Kyu6.HighestRank(arr3);
        Assert.AreEqual(12, result1);
        Assert.AreEqual(12, result2);
        Assert.AreEqual(3, result3);

    }

    [TestMethod]
    [DynamicData(nameof(GetIsPrimeTestData), DynamicDataSourceType.Method)]
    public void IsPrimeTest(int input, bool answer)
    {
        var result = Kyu6.IsPrime(input);
        Assert.AreEqual(answer, result);
    }

    //This is an extension of the IsPrimeTest
    public static IEnumerable<object[]> GetIsPrimeTestData()
    {
        yield return new object[] { 0, false };
        yield return new object[] { 1, false };
        yield return new object[] { 2, true };
        yield return new object[] { -1, false };
        yield return new object[] { 4, false };
        yield return new object[] { 9, false };
        //yield return new object[] { 2147483647, true };
    }

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