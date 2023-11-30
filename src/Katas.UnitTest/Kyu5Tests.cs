using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.UnitTest;

[TestClass]
public class Kyu5Tests
{
    [TestMethod]
    public void RgbTests()
    {
        Assert.AreEqual("FFFFFF", Kyu5.Rgb(255, 255, 255));
        Assert.AreEqual("FFFFFF", Kyu5.Rgb(255, 255, 300));
        Assert.AreEqual("000000", Kyu5.Rgb(0, 0, 0));
        Assert.AreEqual("9400D3", Kyu5.Rgb(148, 0, 211));
        Assert.AreEqual("9400D3", Kyu5.Rgb(148, -20, 211), "Handle negative numbers.");
        Assert.AreEqual("90C3D4", Kyu5.Rgb(144, 195, 212));
        Assert.AreEqual("D4350C", Kyu5.Rgb(212, 53, 12), "Consider single hex digit numbers.");
    }

    [TestMethod]
    public void ToUnderscoreTests()
    {
        Assert.AreEqual("test_controller", Kyu5.ToUnderscore("TestController"));
        Assert.AreEqual("this_is_beautiful_day", Kyu5.ToUnderscore("ThisIsBeautifulDay"));
        Assert.AreEqual("am7_days", Kyu5.ToUnderscore("Am7Days"));
        Assert.AreEqual("my3_code_is4_times_better", Kyu5.ToUnderscore("My3CodeIs4TimesBetter"));
        Assert.AreEqual("arbitrarily_sending_different_types_to_functions_makes_katas_cool", Kyu5.ToUnderscore("ArbitrarilySendingDifferentTypesToFunctionsMakesKatasCool"));
        Assert.AreEqual("1", Kyu5.ToUnderscore(1), "Numbers should be turned to strings!");
    }

    [TestMethod]
    public void Rot13Tests()
    {
        Assert.AreEqual("ROT13 example.", Kyu5.Rot13("EBG13 rknzcyr."));
    }
}
