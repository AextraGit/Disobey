using Unity.VisualScripting;
using UnityEngine;

public class StringToMapGenerator : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    /// <summary>
    /// s(w)(c(i(w)(w)(w))(i(w)(b)(e(w)(w)(w)))(w))(w)
    /// </summary>
    /// <param name="levelString"></param>
    void GenerateMapFromString(string level)
    {
        string levelString = level;

        while (levelString.Length > 0)
        {
            string levelStringFirst = levelString[0].ToString();
            string levelStringRest = levelString.Substring(1);

            if (levelStringFirst == "(" || levelStringFirst == ")")
            {
                levelString = levelStringRest;
                break;
            }

        }
    }
}
