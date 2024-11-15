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
        return false;
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

    public static bool IsCoordinateAlreadyOccupied(List<BattleShip> fleet, (int row, int column) newCoord)
    {
        if (fleet.Count == 0) return false;

        var matchingCoordinates = fleet.Select(battleship => battleship.Coordinates).ToList().Where(coordinate => newCoord.Equals(coordinate));

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
