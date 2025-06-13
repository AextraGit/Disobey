using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    string[,] level;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="length"> Number of segments forming the main street </param>
    /// <param name="depth"> Maximum number of consecutive sections forming a side street</param>
    /// <param name="depthChance"> Chance for a intersection branch to be a street (instead of a wall)</param>
    /// <param name="depthChanceReduction"> DepthChance will be divided by depthChanceReduction for each consecutive section in a side street. maps won't get as big</param>
    /// <param name="loot"> Number of loot segments in a map</param>
    /// <param name="enemy"> Number of enemy segments in a map</param>
    /// <returns> 2D string array in which
    /// c = street combiner sections,
    /// e = enemy sections,
    /// l = loot sections,
    /// b = boss sections,
    /// s = start sections,
    /// + = intersection sections</returns>
    /// 
    ///    i
    /// s+c+i+b
    ///      e
    private string[,] GenerateArray(int length, int depth, int depthChance, int depthChanceReduction, int loot, int enemy)
    {
        level = new string[(depth * 2 + length) * 2 + 3, length * 2 + 5]; // TODO: check if correct size

        int currentRow = depth * 2 + length + 1;
        int currentColumn = 0;

        List<(int, int)> intersections = new List<(int, int)>();

        // add start segment
        level[currentRow, currentColumn] = "s";
        currentColumn++;
        level[currentRow, currentColumn] = "+";
        intersections.Add((currentRow, currentColumn));

        // add main street
        int rowOffset = 0;
        int columnOffset = 1;
        while (length > 0)
        {
            if(columnOffset != 1)
            {
                rowOffset = 0;
                columnOffset = 1;
            } else
            {
                if (Randomize(50))
                {
                    if (Randomize(50))
                    {
                        rowOffset = 1;
                        columnOffset = 0;
                    } else
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
            generateSideStreet(intersection.Item1, intersection.Item1, depth, depthChance, depthChanceReduction);
        }

            // TODO: convert m to c and c to l e
            return level;
    }
    private void GenerateLevel(string[,] array)
    {

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
        if (level[row - 1, column] == null) return;
        if (level[row, column + 1] == null) return;
        if (level[row + 1, column] == null) return;
        if (level[row, column - 1] == null) return;
    }
}
