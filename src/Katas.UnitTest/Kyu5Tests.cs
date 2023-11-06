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
    public void Rot13Tests()
    {
        Assert.AreEqual("ROT13 example.", Kyu5.Rot13("EBG13 rknzcyr."));
    }
}
