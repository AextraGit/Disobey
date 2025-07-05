using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Feedback : MonoBehaviour
{
    public GameObject welcomeText;
    public TextMeshProUGUI countText;
    public float displayTime = 5f;

    public InputActionReference doContinue;

    private string[] tutorialSteps;
    private int currentStep;

    void Start()
    {
        currentStep = 0;
        tutorialSteps = new string[]
        {
            "Welcome to Disobey!",
            "Use W, A, S, D to move to the front, left, back and right. Press Space to continue.",
            "Use the mouse to look around. Press Space to continue.",
            "Press Q to throw a brick. Try out the controls and hit the boxes. Press Space to continue.",
            "Throw bricks at the targets to damage them. Press Space to continue.",
            "Now you know how to play the game, have fun! Press Space to end the tutorial."
        };

        doContinue.action.Disable();
        StartCoroutine(ShowFirstStep());
    }

    void OnEnable()
    {
        doContinue.action.started += DoContinue;
        doContinue.action.Enable(); // Ensure the action is enabled
    }

    void OnDisable()
    {
        doContinue.action.started -= DoContinue;
        doContinue.action.Disable(); // Disable the action when not in use
    }

    private void DoContinue(InputAction.CallbackContext obj)
    {
        // Only proceed if the current step is valid and the text has been displayed long enough
        ShowTutorialStep();
    }

    IEnumerator ShowFirstStep()
    {
        welcomeText.GetComponent<TextMeshProUGUI>().text = tutorialSteps[currentStep];
        yield return new WaitForSeconds(displayTime);
        currentStep++; // Move to the next step after displaying the first one
        ShowTutorialStep(); // Show the next step immediately
    }

    void ShowTutorialStep()
    {
        if (currentStep < tutorialSteps.Length)
        {
            welcomeText.GetComponent<TextMeshProUGUI>().text = tutorialSteps[currentStep];
            currentStep++; // Increment the step after showing the text
            StartCoroutine(WaitForInput());
        }
        else
        {
            welcomeText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    IEnumerator WaitForInput()
    {
        // Disable input while waiting for the display time
        doContinue.action.Disable();
        yield return new WaitForSeconds(displayTime);
        // Re-enable input after the display time
        doContinue.action.Enable();
    }

    public void UpdateCount(int count)
    {
        countText.text = "Bricks thrown: " + count.ToString();
    }
}
