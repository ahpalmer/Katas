using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.UnitTest;

[TestClass]
public class Kyu4Tests
{
    [TestMethod]
    public void HammingTests()
    {
        Assert.AreEqual(1, Kyu4.hamming(1));
        Assert.AreEqual(2, Kyu4.hamming(2));
        Assert.AreEqual(3, Kyu4.hamming(3));
        Assert.AreEqual(4, Kyu4.hamming(4));
        Assert.AreEqual(5, Kyu4.hamming(5));
        Assert.AreEqual(6, Kyu4.hamming(6));
        Assert.AreEqual(8, Kyu4.hamming(7));
        Assert.AreEqual(9, Kyu4.hamming(8));
        Assert.AreEqual(10, Kyu4.hamming(9));
        Assert.AreEqual(12, Kyu4.hamming(10));
        Assert.AreEqual(15, Kyu4.hamming(11));
        Assert.AreEqual(16, Kyu4.hamming(12));
        Assert.AreEqual(18, Kyu4.hamming(13));
        Assert.AreEqual(20, Kyu4.hamming(14));
        Assert.AreEqual(24, Kyu4.hamming(15));
        Assert.AreEqual(25, Kyu4.hamming(16));
        Assert.AreEqual(27, Kyu4.hamming(17));
        Assert.AreEqual(30, Kyu4.hamming(18));
        Assert.AreEqual(32, Kyu4.hamming(19));
        Assert.AreEqual(36, Kyu4.hamming(20));
        Assert.AreEqual(40, Kyu4.hamming(21));
        Assert.AreEqual(45, Kyu4.hamming(22));
        Assert.AreEqual(48, Kyu4.hamming(23));
        Assert.AreEqual(50, Kyu4.hamming(24));
        Assert.AreEqual(54, Kyu4.hamming(25));
        Assert.AreEqual(60, Kyu4.hamming(26));
        Assert.AreEqual(64, Kyu4.hamming(27));
        Assert.AreEqual(72, Kyu4.hamming(28));
        Assert.AreEqual(75, Kyu4.hamming(29));
        Assert.AreEqual(80, Kyu4.hamming(30));
        Assert.AreEqual(81, Kyu4.hamming(31));
        Assert.AreEqual(90, Kyu4.hamming(32));
        Assert.AreEqual(96, Kyu4.hamming(33));
        Assert.AreEqual(100, Kyu4.hamming(34));
        Assert.AreEqual(108, Kyu4.hamming(35));
    }
}
