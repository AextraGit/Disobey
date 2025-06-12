using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.UI.Image;

public class LevelStringGenerator : MonoBehaviour
{
    void Start()
    {
        LevelStringGenerator generator = new LevelStringGenerator();

        int mainStreetLength = 3;
        int sideStreetDepth = 2;
        int lootCount = 2;
        int enemyCount = 3;

        string generatedLevel = generator.GenerateString(mainStreetLength, sideStreetDepth, lootCount, enemyCount);

        Debug.Log("Generiertes Level:");
        Debug.Log(generatedLevel);
    }

    /// <summary>
    /// Generates a String that can be used to create a randomized level based on the given inputs.
    /// The String will always start with a boss component ("B") and end with a start component ("S")
    /// at the end of each component (except boss and wall component) is an intersection with 3 access points (W1)(W2)(W3)
    /// (),(),() is the left, middle, right access point and W1, W2, W3 are the rest of the level
    /// </summary>
    /// <param name="size"> length of the main street to the boss component</param>
    /// <param name="depth"> maximum depth (number of intersections) that side streets can have in a row</param>
    /// <param name="loot"> The total number of components that will contain loot</param>
    /// <param name="enemy"> The total number of components that contain basic enemys</param>
    /// <returns> String in which
    /// c = empty street combiner component,
    /// e = enemy component,
    /// i = item loot component,
    /// b = boss component,
    /// s = start component,
    /// w = wall component</returns>
    /// Example: size = 2, depth = 1, loot = 2, enemy = 1 can output a String like:
    /// s(w)(c(i(w)(w)(w))(i(w)(b)(e(w)(w)(w)))(w))(w) and generated will look like:

    /// 
    ///            |
    ///            i
    ///            |
    /// --s--+--c--+--i--+--b--
    ///                  |
    ///                  e
    ///                  |
    ///
    /// streets can generate into each other (needs to be handled by map generator)
    public string GenerateString(int size, int depth, int loot, int enemy)
    {
        string word = "s" + "(" + GenerateSideStreet(depth) + ")" + "(" + GenerateMainStreet(size, depth) + ")" + "(" + GenerateSideStreet(depth) + ")";

        // TODO: ersetzen einzelner Komponente mit Loot/Enemy

        return word;
    }

    private string GenerateMainStreet(int size, int depth)
    {
        if (size <= 0) return "b";

        string component = "c";

        int nextMainStreetDirection = IntersectionStreetRandomizer(70);
        switch (nextMainStreetDirection)
        {
            case 0:
                return component + "(" + GenerateMainStreet(size - 1, depth) + ")" + "(" + GenerateSideStreet(depth) + ")" + "(" + GenerateSideStreet(depth) + ")";
            case 1:
                return component + "(" + GenerateSideStreet(depth) + ")" + "(" + GenerateMainStreet(size - 1, depth) + ")" + "(" + GenerateSideStreet(depth) + ")";
            case 2:
                return component + "(" + GenerateSideStreet(depth) + ")" + "(" + GenerateSideStreet(depth) + ")" + "(" + GenerateMainStreet(size - 1, depth) + ")";
        }

        return "b";
    }

    private string GenerateSideStreet(int depth)
    {
        if (depth <= 0 || IsWallRandomizer(60)) return "w";

        string component = "c";

        int numberOfStreets = UnityEngine.Random.Range(1, 3);
        int nextStreetDirection = IntersectionStreetRandomizer(50);

        if (numberOfStreets == 1)
        {
            switch (nextStreetDirection)
            {
                case 0:
                    return component + "(" + GenerateSideStreet(depth - 1) + ")" + "(w)" + "(w)";
                case 1:
                    return component + "(w)" + "(" + GenerateSideStreet(depth - 1) + ")" + "(w)";
                case 2:
                    return component + "(w)" + "(w)" + "(" + GenerateSideStreet(depth - 1) + ")";
            }
        }

        switch (nextStreetDirection)
        {
            case 0:
                return component + "(w)" + "(" + GenerateSideStreet(depth - 1) + ")" + "(" + GenerateSideStreet(depth - 1) + ")";
            case 1:
                return component + "(" + GenerateSideStreet(depth - 1) + ")" + "(w)" + "(" + GenerateSideStreet(depth - 1) + ")";
            case 2:
                return component + "(" + GenerateSideStreet(depth - 1) + ")" + "(" + GenerateSideStreet(depth - 1) + ")" + "(w)";
        }

        return "w";
    }

    /// <summary>
    /// randomizes in which direction of an intersection the Main street continues
    /// </summary>
    /// <param name="middleChance"> The chance that the main street continues on the middle street. the chance that the main street continues on the left/right side is (100-middleChance)/2</param>
    /// <returns>0 for left street, 1 for middle street and 2 for right street</returns>
    private int IntersectionStreetRandomizer(int middleChance)
    {
        int leftOrRightChance = 0;
        if (middleChance < 0 || middleChance > 100) return 1;

        if (middleChance % 2 == 0)
        {
            leftOrRightChance = (100 - middleChance) / 2;
        }
        else
        {
            leftOrRightChance = (101 - middleChance) / 2;
        }

        int randomNumber = UnityEngine.Random.Range(1, 101);
        if (randomNumber <= leftOrRightChance) return 0;
        if (randomNumber <= leftOrRightChance + middleChance) return 1;
        return 2;
    }

    /// <summary>
    /// randomizes if an access point of an intersection will be a wall or a street combiner
    /// </summary>
    /// <param name="wallChance"> chance for the access point to be a wall in % (0-100)</param>
    /// <returns> true if its a wall, false if the street continues. true if streetChance is not between 0 and 100</returns>
    private bool IsWallRandomizer(int wallChance)
    {
        if (wallChance < 0 || wallChance > 100) return true;

        int randomNumber = UnityEngine.Random.Range(1, 101);
        return randomNumber <= wallChance;
    }


    private string ReplaceFirst(string word, string oldString, string replacement)
    {
        int index = word.IndexOf(oldString);
        if (index == -1)
        {
            return word;
        }

        return word.Substring(0, index) + replacement + word.Substring(index + oldString.Length);
    }
}
