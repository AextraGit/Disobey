using UnityEngine;

public class LevelController : MonoBehaviour
{
    public ArrayLevelGenerator arrayLevelGenerator;
    public LevelBuilder levelBuilder;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // generate basic small level
        string[,] basicLevel = arrayLevelGenerator.GenerateArray(5, 1, 50, 2, 2, 2);
        levelBuilder.BuildLevel(basicLevel);
    }
}
