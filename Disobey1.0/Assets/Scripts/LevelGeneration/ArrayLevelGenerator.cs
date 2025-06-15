using System.Collections.Generic;
using UnityEngine;

public class ArrayLevelGenerator : MonoBehaviour
{
    private string[,] level;

    /// <summary>
    /// This method generates a 2D array that can be used with the MapGenerator to generate a Level.
    /// The structure is as follows:
    /// There is a main street with a given length, which includes random curves. It starts with the start segment and ends with the boss segment
    /// At the intersections of the main street, side streets are generated recursively.
    /// The recursion depth (i.e.the number of consecutive side street segments) is determined by depth.
    /// The probability of generating a side street is controlled by depthChance, and this probability can be reduced with each additional recursive segment using depthChanceReduction.
    /// The level contains exactly one start segment, one boss segment, and a number of loot and enemy segments defined by the values loot and enemy respectively.
    /// If there are less segments than loot + enemy, some loot or enemy segments will not be generated.
    /// 
    /// </summary>
    /// <param name="length"> Number of segments forming the main street </param>
    /// <param name="depth"> Maximum number of consecutive sections forming a side street</param>
    /// <param name="depthChance"> Chance for an intersection branch to be a street (instead of a wall) in %</param>
    /// <param name="depthChanceReduction"> DepthChance will be divided by depthChanceReduction for each consecutive section in a side street. Maps won't get as big with higher input. 1 for no reduction</param>
    /// <param name="loot"> Maximum number of loot segments in a map</param>
    /// <param name="enemy"> Maximum number of enemy segments in a map</param>
    /// <returns> 2D string array in which
    /// c = street combiner sections,
    /// e = enemy sections,
    /// l = loot sections,
    /// b = boss sections,
    /// s = start sections,
    /// + = intersection sections</returns>
    /// 
    /// Example array output for length = 2, depth = 1, depthChance = 30, depthChanceReduction = 1, loot = 2, enemy = 1
    ///     columns_______
    /// rows. . . . . . . .
    ///    |. . . + . . . .
    ///    |. . . l . . . .
    ///    |s + c + l + b .
    ///    |. . . . . e . .
    ///    |. . . . . + . .
    ///    |. . . . . . . .
    public string[,] GenerateArray(int length, int depth, int depthChance, int depthChanceReduction, int loot, int enemy)
    {
        level = new string[(depth * 2 + length) * 2 + 3, (length + depth) * 2 + 5]; // TODO: check if correct size

        int currentRow = depth * 2 + length + 1;
        int currentColumn = 0;

        List<(int, int)> intersections = new List<(int, int)>();

        // add start segment
        level[currentRow, currentColumn] = "s";
        currentColumn++;
        level[currentRow, currentColumn] = "+";
        intersections.Add((currentRow, currentColumn));

        // generate main street
        int rowOffset = 0;
        int columnOffset = 1;
        while (length > 0)
        {
            if (columnOffset != 1)
            {
                rowOffset = 0;
                columnOffset = 1;
            }
            else
            {
                if (Randomize(50))
                {
                    if (Randomize(50))
                    {
                        rowOffset = 1;
                        columnOffset = 0;
                    }
                    else
                    {
                        rowOffset = -1;
                        columnOffset = 0;
                    }
                }
            }
            currentRow += rowOffset;
            currentColumn += columnOffset;
            level[currentRow, currentColumn] = "m";
            currentRow += rowOffset;
            currentColumn += columnOffset;
            level[currentRow, currentColumn] = "+";
            intersections.Add((currentRow, currentColumn));

            length--;
        }
        // add boss segment
        currentColumn++;
        level[currentRow, currentColumn] = "b";

        // add side streets
        foreach (var intersection in intersections)
        {
            generateSideStreet(intersection.Item1, intersection.Item2, depth, depthChance, depthChanceReduction);
        }

        // TODO: convert m to c and c to l/e
        return level;
    }

    private bool Randomize(int chance)
    {
        if (chance <= 0) return false;
        if (chance >= 100) return true;

        int roll = UnityEngine.Random.Range(1, 101);
        return roll <= chance;
    }

    private void generateSideStreet(int row, int column, int depth, int depthChance, int depthChanceReduction)
    {
        // dont generate if out of bounds
        if (row < 2 || column < 2) return;
        // generate side streets if max depth isn't reached and randomize with given depthChance
        if (level[row - 1, column] == null && Randomize(depthChance) && depth > 0) // check if up branch is used
        {
            level[row - 1, column] = "c";
            if (level[row - 2, column] == null)
            {
                level[row - 2, column] = "+";
                generateSideStreet(row - 2, column, depth - 1, depthChance / depthChanceReduction, depthChanceReduction);
            }
        }
        if (level[row, column + 1] == null && Randomize(depthChance) && depth > 0) // check if right branch is used
        {
            level[row, column + 1] = "c";
            if (level[row, column + 2] == null)
            {
                level[row, column + 2] = "+";
                generateSideStreet(row, column + 2, depth - 1, depthChance / depthChanceReduction, depthChanceReduction);
            }
        }
        if (level[row + 1, column] == null && Randomize(depthChance) && depth > 0) // check if down branch is used
        {
            level[row + 1, column] = "c";
            if (level[row + 2, column] == null)
            {
                level[row + 2, column] = "+";
                generateSideStreet(row + 2, column, depth - 1, depthChance / depthChanceReduction, depthChanceReduction);
            }
        }
        if (level[row, column - 1] == null && Randomize(depthChance) && depth > 0) // check if left branch is used
        {
            level[row, column - 1] = "c";
            if (level[row, column - 2] == null)
            {
                level[row, column - 2] = "+";
                generateSideStreet(row, column - 2, depth - 1, depthChance / depthChanceReduction, depthChanceReduction);
            }
        }
    }
}
