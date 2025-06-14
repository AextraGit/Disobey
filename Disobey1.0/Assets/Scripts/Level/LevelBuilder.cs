using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.Rendering.HableCurve;

public class LevelBuilder : MonoBehaviour
{
    private string[,] levelArray;

    public GameObject streetPrefab;
    public GameObject intersectionPrefab;
    public float streetOffset = 20f;
    public float intersectionOffset = 17.5f;

    public void BuildLevel(string[,] array)
    {
        levelArray = array;

        float currentWorldPositionX = 0f;
        float currentWorldPositionZ = 0f;

        int currentRow = 0;
        int currentColumn = 0;
        // get the starting entry
        for (int i = 0; i < levelArray.GetLength(0); i++)
        {
            if (levelArray[i, 0] != null)
            {
                currentRow = i;
                break;
            }
        }
        // instantiate start section (and intersection section)
        Instantiate(streetPrefab, new Vector3(currentWorldPositionX, 0, currentWorldPositionZ), Quaternion.identity);
        levelArray[currentRow, currentColumn] = null; // delete instantiated sections from array
        currentWorldPositionX = currentWorldPositionX + (streetOffset + intersectionOffset);
        Instantiate(intersectionPrefab, new Vector3(currentWorldPositionX, 0, currentWorldPositionZ), Quaternion.identity);
        currentColumn++;
        levelArray[currentRow, currentColumn] = null;

        // go through street path with recursion and instantiate all sections
        GenerateSections(currentRow, currentColumn, currentWorldPositionX, currentWorldPositionZ);

    }

    private void GenerateSections(int row, int column, float worldPositionX, float WorldPositionZ)
    {
        // TODO: if branch isnt used, generate wall. if no branch is used, remove intersection (or just place 3 walls)
        if (levelArray[row - 1, column] != null) // check if up branch is used
        {
            Instantiate(streetPrefab, new Vector3(worldPositionX, 0, WorldPositionZ + (streetOffset + intersectionOffset)), Quaternion.Euler(0, -90, 0));
            levelArray[row - 1, column] = null;
            if (levelArray[row - 2, column] != null)
            {
                Instantiate(intersectionPrefab, new Vector3(worldPositionX, 0, WorldPositionZ + (streetOffset + intersectionOffset) * 2), Quaternion.Euler(0, -90, 0));
                levelArray[row - 2, column] = null;
                GenerateSections(row - 2, column, worldPositionX, WorldPositionZ + (streetOffset + intersectionOffset) * 2);
            }
        }
        if (levelArray[row, column + 1] != null) // check if right branch is used
        {
            Instantiate(streetPrefab, new Vector3(worldPositionX + (streetOffset + intersectionOffset), 0, WorldPositionZ), Quaternion.identity);
            levelArray[row, column + 1] = null;
            if (levelArray[row, column + 2] != null)
            {
                Instantiate(intersectionPrefab, new Vector3(worldPositionX + (streetOffset + intersectionOffset) * 2, 0, WorldPositionZ), Quaternion.identity);
                levelArray[row, column + 2] = null;
                GenerateSections(row, column + 2, worldPositionX + (streetOffset + intersectionOffset) * 2, WorldPositionZ);
            }
        }
        if (levelArray[row + 1, column] != null) // check if down branch is used
        {
            Instantiate(streetPrefab, new Vector3(worldPositionX, 0, WorldPositionZ - (streetOffset + intersectionOffset)), Quaternion.Euler(0, 90, 0));
            levelArray[row + 1, column] = null;
            if (levelArray[row + 2, column] != null)
            {
                Instantiate(intersectionPrefab, new Vector3(worldPositionX, 0, WorldPositionZ - (streetOffset + intersectionOffset) * 2), Quaternion.Euler(0, 90, 0));
                levelArray[row + 2, column] = null;
                GenerateSections(row + 2, column, worldPositionX, WorldPositionZ - (streetOffset + intersectionOffset) * 2);
            }
        }
        if (levelArray[row, column - 1] != null) // check if left branch is used
        {
            Instantiate(streetPrefab, new Vector3(worldPositionX - (streetOffset + intersectionOffset), 0, WorldPositionZ), Quaternion.Euler(0, -180, 0));
            levelArray[row, column - 1] = null;
            if (levelArray[row, column - 2] != null)
            {
                Instantiate(intersectionPrefab, new Vector3(worldPositionX - (streetOffset + intersectionOffset) * 2, 0, WorldPositionZ), Quaternion.Euler(0, -180, 0));
                levelArray[row, column - 2] = null;
                GenerateSections(row, column - 2, worldPositionX - (streetOffset + intersectionOffset) * 2, WorldPositionZ);
            }
        }
    }
}
