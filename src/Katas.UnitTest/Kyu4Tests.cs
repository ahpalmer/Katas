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
    public void TestRankingSystemProgress()
    {
        // Arrange
        Kyu4.User userOne = new Kyu4.User();
        userOne.rank = -8;
        Kyu4.User userTwo = new Kyu4.User();
        userTwo.rank = -8;
        Kyu4.User userThree = new Kyu4.User();
        userThree.rank = -8;
        Kyu4.User userFour = new Kyu4.User();
        userFour.rank = 4;
        Kyu4.User userFive = new Kyu4.User();
        userFive.rank = 8;
        userFive.progress = 0;
        Kyu4.User userSix = new Kyu4.User();
        userSix.rank = 4;
        Kyu4.User userSeven = new Kyu4.User();
        userSeven.rank = -8;
        Kyu4.User userEight = new Kyu4.User();
        userEight.rank = 7;
        userEight.progress = 91;

        // Act
        userOne.incProgress(-7);
        userTwo.incProgress(-6);
        userThree.incProgress(-5);
        userFour.incProgress(5);
        userFive.incProgress(8);
        userSix.incProgress(2);
        userSeven.incProgress(-8);
        userEight.incProgress(8);

        // Assert
        Assert.AreEqual(10, userOne.progress);
        Assert.AreEqual(-8, userOne.rank);
        Assert.AreEqual(40, userTwo.progress);
        Assert.AreEqual(-8, userTwo.rank);
        Assert.AreEqual(90, userThree.progress);
        Assert.AreEqual(-8, userThree.rank);
        Assert.AreEqual(10, userFour.progress);
        Assert.AreEqual(4, userFour.rank);
        Assert.AreEqual(0, userFive.progress);
        Assert.AreEqual(8, userFive.rank);
        Assert.AreEqual(0, userSix.progress);
        Assert.AreEqual(4, userSix.rank);
        Assert.AreEqual(3, userSeven.progress);
        Assert.AreEqual(-8, userSeven.rank);
        Assert.AreEqual(0, userEight.progress);
        Assert.AreEqual(8, userEight.rank);
    }

    [TestMethod]
    public void TestRankingSystemRanks()
    {
        // Arrange
        Kyu4.User userOne = new Kyu4.User();
        userOne.rank = -8;
        Kyu4.User userTwo = new Kyu4.User();
        userTwo.rank = -1;
        Kyu4.User userThree = new Kyu4.User();
        userThree.rank = 1;
        Kyu4.User userFour = new Kyu4.User();
        userFour.rank = 8;
        Kyu4.User userFive = new Kyu4.User();
        userFive.rank = -8;

        // Act
        userOne.incProgress(-4);
        userTwo.incProgress(4);
        userThree.incProgress(5);
        userFour.incProgress(8);
        userFive.incProgress(3);

        // Assert
        Assert.AreEqual(-7, userOne.rank);
        Assert.AreEqual(1, userTwo.rank);
        Assert.AreEqual(2, userThree.rank);
        Assert.AreEqual(8, userFour.rank);
        Assert.AreEqual(3, userFive.rank);
    }

    [TestMethod]
    public void TestRankingSystemMultiple()
    {
        Kyu4.User userOne = new Kyu4.User();
        userOne.rank = -8;

        userOne.incProgress(1);
        Assert.AreEqual(-2, userOne.rank);
        Assert.AreEqual(40, userOne.progress);

        userOne.incProgress(1);
        Assert.AreEqual(-2, userOne.rank);
        Assert.AreEqual(80, userOne.progress);

        userOne.incProgress(1);
        Assert.AreEqual(-1, userOne.rank);
        Assert.AreEqual(20, userOne.progress);

        userOne.incProgress(1);
        Assert.AreEqual(-1, userOne.rank);
        Assert.AreEqual(30, userOne.progress);

        userOne.incProgress(1);
        Assert.AreEqual(-1, userOne.rank);
        Assert.AreEqual(40, userOne.progress);

        userOne.incProgress(2);
        Assert.AreEqual(-1, userOne.rank);
        Assert.AreEqual(80, userOne.progress);

        userOne.incProgress(2);
        Assert.AreEqual(1, userOne.rank);
        Assert.AreEqual(20, userOne.progress);

        userOne.incProgress(-1);
        Assert.AreEqual(1, userOne.rank);
        Assert.AreEqual(21, userOne.progress);
    }

    [TestMethod]
    public void TestAdd()
    {
        // Test the kyu4 method Add(string a, string b)
        Assert.AreEqual("3", Kyu4.Add("1", "2"));
        Assert.AreEqual("110", Kyu4.Add("91", "19"));
        Assert.AreEqual("1111111111", Kyu4.Add("123456789", "987654322"));
        Assert.AreEqual("1000000000", Kyu4.Add("999999999", "1"));
        Assert.AreEqual("1057853509440367665682450458794866464501746580388666517943654", Kyu4.Add("823094582094385190384102934810293481029348123094818923749817", "234758927345982475298347523984572983472398457293847594193837"));
        Assert.AreEqual("597951606561433228496796601885625316676374423166572175420262014157467100456969930453802947481550446", Kyu4.Add("7750389384851621877681014102394024835553906792205960059209239971806475088934683104683281945894850", "590201217176581606619115587783231291840820516374366215361052774185660625368035247349119665535655596"));
        Assert.AreEqual("1131313130303031311313030303131313121212131313129120030130132", Kyu4.Add("666666665656566666666565656666666656565666666665656566666666", "464646464646464644646464646464646464646464646463463463463466"));
        Assert.AreEqual(100, Kyu4.Add("414729893610905672967792275054129975903618204191593626468445431858356477292824794843179434983147546", "677483631672779783671636715000682977902891990903175244638364178151162472033342685433933491969525788").Length);

        Assert.AreEqual(99, Kyu4.Add("75097731143238722605942081892958186525457742193918132489696962044414654045539298930196577223583554", "507969477242044239639040154155539346424429515955984970467047625032397800429632128135457667052551834").Length);

    }

    [TestMethod]
    public void TestTop3()
    {
        CollectionAssert.AreEqual(new List<string> { "e", "d", "a" }, Kyu4.Top3("a a a  b  c c  d d d d  e e e e e"));
        CollectionAssert.AreEqual(new List<string> { "e", "ddd", "aa" }, Kyu4.Top3("e e e e DDD ddd DdD: ddd ddd aa aA Aa, bb cc cC e e e"));
        CollectionAssert.AreEqual(new List<string> { "won't", "wont" }, Kyu4.Top3("  //wont won't won't "));
        CollectionAssert.AreEqual(new List<string> { "e" }, Kyu4.Top3("  , e   .. "));
        CollectionAssert.AreEqual(new List<string> { }, Kyu4.Top3("  ...  "));
        CollectionAssert.AreEqual(new List<string> { }, Kyu4.Top3("  '  "));
        CollectionAssert.AreEqual(new List<string> { }, Kyu4.Top3("  '''  "));
        CollectionAssert.AreEqual(new List<string> { "a", "of", "on" }, Kyu4.Top3(
            string.Join("\n", new string[]{"In a village of La Mancha, the name of which I have no desire to call to",
                  "mind, there lived not long since one of those gentlemen that keep a lance",
                  "in the lance-rack, an old buckler, a lean hack, and a greyhound for",
                  "coursing. An olla of rather more beef than mutton, a salad on most",
                  "nights, scraps on Saturdays, lentils on Fridays, and a pigeon or so extra",
                  "on Sundays, made away with three-quarters of his income." })));
        CollectionAssert.AreEqual(new List<string> { "tujrfwe", "ednb", "xovm" }, Kyu4.Top3("TUjrFWE_TUjrFWE;kAobiQTc ICWqRt' ICWqRt'_LWYfn ICWqRt' eDNb TUjrFWE yFxOMg:LWYfn JvmvrvcQa.eDNb TUjrFWE:bKTF LWYfn LWYfn;ICWqRt'/ICWqRt' xoVm WXEa_TUjrFWE:AYBXZK xoVm eDNb-kAobiQTc NvIJvaLmh TUjrFWE_bKTF TUjrFWE xoVm Zt'JxN eDNb;LWYfn xoVm:ICWqRt'-yFxOMg OfeDPTxFMz xoVm;eDNb!eDNb AYBXZK_bKTF yFxOMg!kAobiQTc eDNb OfeDPTxFMz?xoVm;Zt'JxN?kAobiQTc OfeDPTxFMz:eDNb AYBXZK AYBXZK Zt'JxN AYBXZK TUjrFWE_TUjrFWE/kAobiQTc/Zt'JxN/NvIJvaLmh Zt'JxN/TUjrFWE JvmvrvcQa LWYfn ICWqRt';OfeDPTxFMz yFxOMg,eDNb-TUjrFWE OfeDPTxFMz.xoVm/OfeDPTxFMz bKTF;bKTF NvIJvaLmh xoVm/LWYfn-AYBXZK/bKTF eDNb,TUjrFWE eDNb?yFxOMg eDNb-AYBXZK:yFxOMg LWYfn:bKTF yFxOMg Vaz!JvmvrvcQa-ICWqRt' OfeDPTxFMz eDNb-xoVm NvIJvaLmh.eDNb_LWYfn_Zt'JxN?Zt'JxN_AYBXZK.LWYfn_ICWqRt' yFxOMg WXEa?xoVm ICWqRt'/kAobiQTc LWYfn xoVm JvmvrvcQa bKTF;ICWqRt',kAobiQTc TUjrFWE LWYfn kAobiQTc yFxOMg AYBXZK.AYBXZK JvmvrvcQa/OfeDPTxFMz yFxOMg.LWYfn eDNb,JvmvrvcQa eDNb kAobiQTc;AYBXZK/Vaz NvIJvaLmh eDNb eDNb!LWYfn?ICWqRt' NvIJvaLmh bKTF LWYfn AYBXZK AYBXZK xoVm NvIJvaLmh.JvmvrvcQa NvIJvaLmh eDNb JvmvrvcQa LWYfn-TUjrFWE LWYfn WXEa eDNb.TUjrFWE Zt'JxN Vaz bKTF;bKTF?Zt'JxN JvmvrvcQa-bKTF.Vaz AYBXZK OfeDPTxFMz ICWqRt':ICWqRt' TUjrFWE!AYBXZK?Zt'JxN,kAobiQTc yFxOMg AYBXZK NvIJvaLmh yFxOMg Zt'JxN yFxOMg-OfeDPTxFMz ICWqRt' Vaz NvIJvaLmh_Vaz,Zt'JxN TUjrFWE JvmvrvcQa bKTF;JvmvrvcQa Vaz!yFxOMg LWYfn,Vaz kAobiQTc JvmvrvcQa Vaz:LWYfn WXEa,LWYfn;kAobiQTc_bKTF/LWYfn.JvmvrvcQa!TUjrFWE kAobiQTc bKTF!TUjrFWE OfeDPTxFMz Vaz?eDNb_eDNb!TUjrFWE xoVm JvmvrvcQa.Zt'JxN eDNb!eDNb eDNb NvIJvaLmh?bKTF TUjrFWE Zt'JxN xoVm Zt'JxN_Zt'JxN yFxOMg bKTF,kAobiQTc,NvIJvaLmh.ICWqRt' AYBXZK xoVm;xoVm;bKTF ICWqRt' eDNb TUjrFWE?JvmvrvcQa OfeDPTxFMz Zt'JxN!WXEa NvIJvaLmh-AYBXZK NvIJvaLmh;yFxOMg.xoVm:LWYfn xoVm/Zt'JxN kAobiQTc-bKTF AYBXZK_NvIJvaLmh-eDNb xoVm-xoVm_LWYfn_AYBXZK.bKTF.WXEa_OfeDPTxFMz:ICWqRt' AYBXZK TUjrFWE;JvmvrvcQa!yFxOMg-AYBXZK xoVm JvmvrvcQa_kAobiQTc_NvIJvaLmh Zt'JxN OfeDPTxFMz AYBXZK ICWqRt' LWYfn,WXEa.TUjrFWE LWYfn/Zt'JxN TUjrFWE bKTF ICWqRt' AYBXZK/xoVm yFxOMg xoVm,OfeDPTxFMz;Zt'JxN-Zt'JxN:NvIJvaLmh xoVm;ICWqRt' bKTF?WXEa:Vaz?WXEa:eDNb JvmvrvcQa JvmvrvcQa TUjrFWE_eDNb OfeDPTxFMz!Zt'JxN yFxOMg ICWqRt' TUjrFWE ICWqRt';AYBXZK LWYfn.TUjrFWE WXEa kAobiQTc?xoVm JvmvrvcQa?AYBXZK.TUjrFWE xoVm bKTF/OfeDPTxFMz xoVm kAobiQTc LWYfn/TUjrFWE;xoVm:"));
    }

    [TestMethod]
    public void SumIntervalsTests()
    {
        Assert.AreEqual(7, Kyu4.SumIntervals(new (int, int)[] { (1, 4), (7, 10), (3, 5) }));
        Assert.AreEqual(0, Kyu4.SumIntervals(new (int, int)[] { (4, 4), (6, 6), (8, 8) }));
        Assert.AreEqual(0, Kyu4.SumIntervals(new (int, int)[] { }));
        Assert.AreEqual(9, Kyu4.SumIntervals(new (int, int)[] { (1, 2), (6, 10), (11, 15) }));
        Assert.AreEqual(11, Kyu4.SumIntervals(new (int, int)[] { (4, 8), (9, 10), (15, 21) }));
        Assert.AreEqual(7, Kyu4.SumIntervals(new (int, int)[] { (-1, 4), (-5, -3) }));
        Assert.AreEqual(78, Kyu4.SumIntervals(new (int, int)[] { (-245, -218), (-194, -179), (-155, -119) }));
        Assert.AreEqual(54, Kyu4.SumIntervals(new (int, int)[] { (1, 2), (2, 6), (6, 55) }));
        Assert.AreEqual(23, Kyu4.SumIntervals(new (int, int)[] { (-2, -1), (-1, 0), (0, 21) }));
        Assert.AreEqual(6, Kyu4.SumIntervals(new (int, int)[] { (5, 8), (3, 6), (1, 2) }));
        Assert.AreEqual(19, Kyu4.SumIntervals(new (int, int)[] { (1, 5), (10, 20), (1, 6), (16, 19), (5, 11) }));
        Assert.AreEqual(13, Kyu4.SumIntervals(new (int, int)[] { (2, 5), (-1, 2), (-40, -35), (6, 8) }));
        Assert.AreEqual(1234, Kyu4.SumIntervals(new (int, int)[] { (-7, 8), (-2, 10), (5, 15), (2000, 3150), (-5400, -5338) }));
        Assert.AreEqual(158, Kyu4.SumIntervals(new (int, int)[] { (-101, 24), (-35, 27), (27, 53), (-105, 20), (-36, 26) }));
        Assert.AreEqual(2000000000, Kyu4.SumIntervals(new (int, int)[] { (-1000000000, 1000000000) }));
        Assert.AreEqual(100000030, Kyu4.SumIntervals(new (int, int)[] { (0, 20), (-100000000, 10), (30, 40) }));
    }

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
