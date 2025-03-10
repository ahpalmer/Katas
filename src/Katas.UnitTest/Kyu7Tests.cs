﻿using Katas;

namespace Katas.UnitTest;

[TestClass]
public class Kyu7Tests
{
    [TestMethod]
    public void ValidatePinTest()
    {
        Assert.AreEqual(false, Kyu7.ValidatePin("1"), "Wrong output for \"1\"");
        Assert.AreEqual(false, Kyu7.ValidatePin("12"), "Wrong output for \"12\"");
        Assert.AreEqual(false, Kyu7.ValidatePin("123"), "Wrong output for \"123\"");
        Assert.AreEqual(false, Kyu7.ValidatePin("12345"), "Wrong output for \"12345\"");
        Assert.AreEqual(false, Kyu7.ValidatePin("1234567"), "Wrong output for \"1234567\"");
        Assert.AreEqual(false, Kyu7.ValidatePin("a234"), "Wrong output for \"a234\"");
        Assert.AreEqual(false, Kyu7.ValidatePin(".234"), "Wrong output for \".234\"");
        Assert.AreEqual(true, Kyu7.ValidatePin("1234"), "Wrong output for \"1234\"");
        Assert.AreEqual(true, Kyu7.ValidatePin("0000"), "Wrong output for \"0000\"");
        Assert.AreEqual(true, Kyu7.ValidatePin("1111"), "Wrong output for \"1111\"");
        Assert.AreEqual(true, Kyu7.ValidatePin("123456"), "Wrong output for \"123456\"");
        Assert.AreEqual(true, Kyu7.ValidatePin("098765"), "Wrong output for \"098765\"");
        Assert.AreEqual(true, Kyu7.ValidatePin("000000"), "Wrong output for \"000000\"");
        Assert.AreEqual(true, Kyu7.ValidatePin("090909"), "Wrong output for \"090909\"");

    }
    [TestMethod]
    public void BinaryArrayToNumberTest()
    {
        int[] Test1 = new int[] { 0, 0, 0, 0 };
        int[] Test2 = new int[] { 1, 1, 1, 1 };
        int[] Test3 = new int[] { 0, 1, 1, 0 };
        int[] Test4 = new int[] { 0, 1, 0, 1 };

        Assert.AreEqual(0, Kyu7.BinaryArrayToNumber(Test1));
        Assert.AreEqual(15, Kyu7.BinaryArrayToNumber(Test2));
        Assert.AreEqual(6, Kyu7.BinaryArrayToNumber(Test3));
        Assert.AreEqual(5, Kyu7.BinaryArrayToNumber(Test4));
    }

    [TestMethod]
    public void GetMiddleTest()
    {
        Assert.AreEqual("es", Kyu7.GetMiddle("test"));
        Assert.AreEqual("t", Kyu7.GetMiddle("testing"));
        Assert.AreEqual("dd", Kyu7.GetMiddle("middle"));
        Assert.AreEqual("A", Kyu7.GetMiddle("A"));
    }

    [TestMethod]
    public void GetAnimalsCountTest()
    {
        var answer = Kyu7.get_animals_count(34, 11, 6);
        Assert.AreEqual(3, answer["cows"]);
        Assert.AreEqual(3, answer["rabbits"]);
        Assert.AreEqual(5, answer["chickens"]);
    }


    [TestMethod]
    public void SquareDigitsTest()
    {
        Assert.AreEqual(811181, Kyu7.SquareDigits(9119));
        Assert.AreEqual(0, Kyu7.SquareDigits(0));
    }

    [TestMethod]
    public void ToJadenCaseTest()
    {
        Assert.AreEqual("How Can Mirrors Be Real If Our Eyes Aren't Real",
                    "How can mirrors be real if our eyes aren't real".ToJadenCase(),
                    "Strings didn't match.");
    }

    [TestMethod]
    public void GetVowelCountTest()
    {
        Assert.AreEqual(5, Kyu7.GetVowelCount("abracadabra"));

    }

    [TestMethod]
    public void RemoveSmallestTest()
    {
        CollectionAssert.AreEqual(new List<int> { 2, 3, 4, 5 }, Kyu7.RemoveSmallest(new List<int> { 1, 2, 3, 4, 5 }));
        CollectionAssert.AreEqual(new List<int> { 5, 3, 2, 4 }, Kyu7.RemoveSmallest(new List<int> { 5, 3, 2, 1, 4 }));
        CollectionAssert.AreEqual(new List<int> { 2, 3, 1, 1 }, Kyu7.RemoveSmallest(new List<int> { 1, 2, 3, 1, 1 }));
        CollectionAssert.AreEqual(new List<int>(), Kyu7.RemoveSmallest(new List<int>()));
    }

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
