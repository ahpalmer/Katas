using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Katas;

public class Kyu3
{
    public static bool ValidateBattlefield(int[,] field)
    {
        List<BattleShip> fleet = FindBattleShips(field);
        if (fleet.Count() == 0) return false;
        return TestFleet(fleet);
    }

    public static bool TestFleet(List<BattleShip> fleet)
    {
        int battleshipCount = 0;
        int cruiserCount = 0;
        int destroyerCount = 0;
        int submarineCount = 0;

        foreach (BattleShip ship in fleet)
        {
            if (ship.Size == 4) battleshipCount++;
            else if (ship.Size == 3) cruiserCount++;
            else if (ship.Size == 2) destroyerCount++;
            else if (ship.Size == 1) submarineCount++;
        }

        bool inContact = IsShipInContact(fleet);

        if (battleshipCount != 1 || cruiserCount != 2 || destroyerCount != 3 || submarineCount != 4) return false;
        if (inContact) return false;

        return true;
    }

    public static List<BattleShip> FindBattleShips(int[,] field)
    {
        List<BattleShip> fleet = new List<BattleShip>();
        for (int row = 0; row < field.GetLength(0); row ++)
        {
            for (int col = 0; col < field.GetLength(1); col ++)
            {
                if (field[row, col] == 1 && !IsCoordinateAlreadyOccupied(fleet, (row, col)))
                {
                    BattleShip newShip = AddShip(field, (row, col));
                    if (newShip.Size == 0) return new List<BattleShip>();
                    else
                    {
                        fleet.Add(newShip);
                    }
                }
            }
        }

        return fleet;
    }

    public static BattleShip AddShip(int[,] field, (int row, int column) coordinate)
    {
        string rightOrDown = RightOrDown(field, coordinate);
        int size = FindShipSize(field, coordinate, rightOrDown);
        switch (rightOrDown)
        {
            case "Both":
                return new BattleShip(0, 0, 0, "False");
            case "Right":
                return new BattleShip(coordinate.row, coordinate.column, size, "Right");
            case "Down":
                return new BattleShip(coordinate.row, coordinate.column, size, "Down");
            case "Neither":
                return new BattleShip(coordinate.row, coordinate.column, 1, "Down");
        }
        return new BattleShip(0, 0, 0, "False");
    }

    public static int FindShipSize(int[,] field, (int row, int column) coordinate, string rightOrDown)
    {
        int count = 0;

        if (rightOrDown == "Neither") return 1;
        else if (rightOrDown == "Right")
        {
            while (field[coordinate.row, coordinate.column + count] != 0  && (coordinate.column + count) <= 9)
            {
                count++;
            }
        }
        else if (rightOrDown == "Down")
        {
            while (field[coordinate.row + count, coordinate.column] != 0 && (coordinate.row + count) <= 9)
            {
                count++;
            }
        }

        return count;
    }

    public static bool IsShipInContact(List<BattleShip> fleet)
    {
        if (fleet.Count == 0) return false;

        var coordinates = fleet.Select(battleship => battleship.Coordinates);

        foreach (BattleShip battleship in fleet)
        {
            foreach (BattleShip testShip in fleet)
            {
                if (!(testShip.Coordinates == battleship.Coordinates))
                {
                    bool shipAdjacent = AreShipsAdjacent(battleship, testShip);
                    if (shipAdjacent) return true;
                }
            }
        }

        return false;
    }

    private static bool AreShipsAdjacent(BattleShip ship1, BattleShip ship2)
    {
        // Check each coordinate of ship1 against each coordinate of ship2
        foreach (var coord1 in ship1.Coordinates)
        {
            foreach (var coord2 in ship2.Coordinates)
            {
                // If coordinates are adjacent (including diagonally)
                if (AreCoordinatesAdjacent(coord1, coord2))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool AreCoordinatesAdjacent((int row, int column) coord1, (int row, int column) coord2)
    {
        // Calculate the absolute difference in rows and columns
        int rowDiff = Math.Abs(coord1.row - coord2.row);
        int colDiff = Math.Abs(coord1.column - coord2.column);

        // If the coordinates are adjacent (including diagonally)
        // they will differ by at most 1 in both row and column
        return rowDiff <= 1 && colDiff <= 1 && !(rowDiff == 0 && colDiff == 0);
    }

    public static bool IsCoordinateAlreadyOccupied(List<BattleShip> fleet, (int row, int column) newCoord)
    {
        if (fleet.Count == 0) return false;

        var coordinates = fleet.Select(battleship => battleship.Coordinates);

        List<(int, int)> matchingCoordinates = new List<(int, int)>();
        foreach (var battleship in coordinates)
        {
            foreach (var coordinate in battleship)
            {
                if (coordinate.Equals(newCoord))
                {
                    matchingCoordinates.Add(coordinate);
                }
            }
        }

        if (matchingCoordinates.Any()) return true;

        return false;
    }

    public static string RightOrDown(int[,] field, (int row, int column) newCoord)
    {
        var fieldList =
            Enumerable.Range(0, field.GetLength(0)).SelectMany(row =>
            Enumerable.Range(0, field.GetLength(1)).Select(col =>
            new { Row = row, Col = col, Value = field[row, col] }));
        bool right = false;
        bool down = false;
        if (newCoord.row != 10)
        {
            down = fieldList.Where(item => item.Col == newCoord.column && item.Row == (newCoord.row + 1) && item.Value == 1).Any();
        }
        if (newCoord.column != 10)
        {
            right = fieldList.Where(item => item.Col == (newCoord.column + 1) && item.Row == newCoord.row && item.Value == 1).Any();
        }
        if (right && down) return "Both";
        else if (right) return "Right";
        else if (down) return "Down";
        else return "Neither";
    }
}

public class BattleShip
{
    public int Size;
    public List<(int row, int column)> Coordinates;

    public BattleShip(int row, int column, int size, string direction)
    {
        Size = size;

        List<(int, int)> coordinates = new List<(int, int)> ();
        coordinates.Add((row, column));
        for (int i = 1; i < size; i ++)
        {
            if (direction == "Right")
            {
                coordinates.Add((row, column + i));
            }
            else if (direction == "Down")
            {
                coordinates.Add((row + i, column));
            }
        }
        Coordinates = coordinates;
    }
}
