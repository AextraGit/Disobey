using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Feedback : MonoBehaviour
{
    public AnimationWelcomeText animationWelcomeText;
    public GameObject welcomeText;
    public TextMeshProUGUI countText;
    public float displayTimeWelcome = 5f;
    public float displayTimeMessages = 3f;
    public float displayMaxCountReached = 4f;

    public InputActionReference doContinue;

    private string[] tutorialSteps;
    private int currentStep;
    private string welcomeMessage = "Welcome to Disobey!";
    private string continueButtonText = "Space";
    private string throwBrickButtonText = "left mouse button";

    void Start()
    {
        currentStep = 0;
        tutorialSteps = new string[]
        {
            welcomeMessage,
            "Use W, A, S, D to move to the front, left, back and right. Press " + continueButtonText + " to continue.",
            "Use the mouse to look around. Press Space to continue.",
            "Press the " + throwBrickButtonText + " to throw a brick and right mouse button to throw molotows. Try out the controls and hit the boxes or the police men. Press Space to continue.",
            "Throw bricks at the targets to damage them. Press Space to continue.",
            "Now you know how to play the game, have fun! Press Space to end the tutorial."
        };

        doContinue.action.Disable();
        StartCoroutine(ShowFirstStep());
    }

    IEnumerator ShowFirstStep()
    {
        welcomeText.GetComponent<TextMeshProUGUI>().text = tutorialSteps[currentStep];
        yield return new WaitForSeconds(displayTimeWelcome);
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
            animationWelcomeText.StopWobble(); // Stop the wobble effect
            welcomeText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    IEnumerator WaitForInput()
    {
        // Disable input while waiting for the display time
        doContinue.action.Disable();
        yield return new WaitForSeconds(displayTimeMessages);
        // Re-enable input after the display time
        doContinue.action.Enable();
    }

    public void UpdateCount(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks left: " + bricksLeft.ToString();
        countText.text = "Items left: " + bricksLeft.ToString() + "\nItems thrown: " + count.ToString() + "/" + max.ToString();
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
    }

    public IEnumerator UpdateMaxCountReached(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
        countText.color = Color.red; // Change text color to red
        yield return new WaitForSeconds(displayMaxCountReached);
        countText.color = Color.white; // Change text color back to white
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
}
