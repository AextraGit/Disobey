using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Play()
    {
        SceneManager.LoadScene("Map");
    }

    public void Testing()
    {
        SceneManager.LoadScene("Testing");
    }
}
