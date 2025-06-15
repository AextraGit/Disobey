using UnityEngine;
using UnityEngine.SceneManagement;

public class MapButtons : MonoBehaviour

{
    public ArrayLevelGenerator arrayLevelGenerator;
    public LevelBuilder levelBuilder;

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Level1()
    {
        LevelController.levelToLoad = 1;
        SceneManager.LoadScene("Level");
    }

    public void Level2()
    {
        LevelController.levelToLoad = 2;
        SceneManager.LoadScene("Level");
    }
}
