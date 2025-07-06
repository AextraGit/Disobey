using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public static LevelController Instance;

    public static int levelToLoad = 1;

    public ArrayLevelGenerator arrayLevelGenerator;
    public LevelBuilder levelBuilder;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (Application.isPlaying)

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Level")
        {
            switch (LevelController.levelToLoad)
            {
                case 1:
                    Level1();
                    break;

                case 2:
                    Level2();
                    break;

                default:
                    SceneManager.LoadScene("Tutorial");
                    break;
            }
        }
    }

    private void Level1()
    {
        // generate basic small level
        string[,] basicLevel = arrayLevelGenerator.GenerateArray(4, 1, 50, 2, 2, 2);
        levelBuilder.BuildLevel(basicLevel);
    }

    private void Level2()
    {
        // generate bigger level
        string[,] basicLevel = arrayLevelGenerator.GenerateArray(8, 2, 50, 2, 2, 2);
        levelBuilder.BuildLevel(basicLevel);
    }
}
