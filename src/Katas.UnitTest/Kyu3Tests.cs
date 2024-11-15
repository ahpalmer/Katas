using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Katas.UnitTest;

[TestClass]
public class Kyu3Tests
{
    [TestMethod]
    public void TestValidateBattlefield()
    {
        int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
        Assert.IsTrue(Kyu3.ValidateBattlefield(field));

        int[,] field2 = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 0, 1},
                      {1, 0, 1, 0, 1, 1, 1, 0, 0, 1},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
        Assert.IsTrue(Kyu3.ValidateBattlefield(field2));

        int[,] field3 = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 1}};
        Assert.IsTrue(Kyu3.ValidateBattlefield(field3));

        int[,] field4 = new int[10, 10]
             {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 1}};
        Assert.IsFalse(Kyu3.ValidateBattlefield(field4));
    }

    [TestMethod]
    public void TestIsShipInContact()
    {
        int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 1, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 1}};
        List<BattleShip> fleet = Kyu3.FindBattleShips(field);
        Assert.IsTrue(Kyu3.IsShipInContact(fleet));

        int[,] field2 = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 1, 0, 0, 0, 1}};
        List<BattleShip> fleet2 = Kyu3.FindBattleShips(field2);
        Assert.IsFalse(Kyu3.IsShipInContact(fleet2));
    }

    [TestMethod]
    public void TestIsCoordinateOccupied()
    {
        BattleShip alreadyThere = new BattleShip(0, 5, 2, "Right");
        (int, int) newCoord = (0, 6);
        Assert.AreEqual(true, Kyu3.IsCoordinateAlreadyOccupied(new List<BattleShip> { alreadyThere }, newCoord));
    }

    [TestMethod]
    public void TestFindBattleShips()
    {
        int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

        List<BattleShip> fleet = Kyu3.FindBattleShips(field);
        Assert.AreEqual(fleet.Count, 10);
    }

    [TestMethod]
    public void TestCreatingBattleships()
    {
        BattleShip USSTest = new BattleShip(1, 1, 3, "Down");
        List<(int x, int y)> coordinatesUSSTest = new List<(int x, int y)>();
        coordinatesUSSTest.Add((1, 1));
        coordinatesUSSTest.Add((2, 1));
        coordinatesUSSTest.Add((3, 1));
        CollectionAssert.AreEqual(coordinatesUSSTest, USSTest.Coordinates);

        BattleShip USSTest2 = new BattleShip(1, 1, 3, "Right");
        List<(int x, int y)> coordinatesUSSTest2 = new List<(int x, int y)>();
        coordinatesUSSTest2.Add((1, 1));
        coordinatesUSSTest2.Add((1, 2));
        coordinatesUSSTest2.Add((1, 3));
        CollectionAssert.AreEqual(coordinatesUSSTest2, USSTest2.Coordinates);

        BattleShip USSTest3 = new BattleShip(1, 1, 1, "Down");
        List<(int x, int y)> coordinatesUSSTest3 = new List<(int x, int y)>();
        coordinatesUSSTest3.Add((1, 1));
        CollectionAssert.AreEqual(coordinatesUSSTest3, USSTest3.Coordinates);
    }

    [TestMethod]
    public void TestFindShipSize()
    {
        int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 1}};
        Assert.AreEqual(3, Kyu3.FindShipSize(field, (2, 4), "Right"));
        Assert.AreEqual(4, Kyu3.FindShipSize(field, (0, 0), "Down"));
    }

    [TestMethod]
    public void TestAddShip()
    {
        int[,] field = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
        BattleShip USSExpected = new BattleShip(5, 4, 3, "Right");
        BattleShip USSActual = Kyu3.AddShip(field, (5, 4));
        Assert.AreEqual(USSExpected.Size, USSActual.Size);
        CollectionAssert.AreEqual(USSExpected.Coordinates, USSActual.Coordinates);

        BattleShip USSExpected2 = new BattleShip(0, 0, 4, "Down");
        BattleShip USSActual2 = Kyu3.AddShip(field, (0, 0));
        Assert.AreEqual(USSExpected2.Size, USSActual2.Size);
        CollectionAssert.AreEqual(USSExpected2.Coordinates, USSActual2.Coordinates);
    }
}
