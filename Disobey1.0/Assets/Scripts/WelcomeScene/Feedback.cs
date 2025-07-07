using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Feedback : MonoBehaviour
{
    public GameObject welcomeText;
    public AnimationWelcomeText animationWelcomeText;

    public TextMeshProUGUI countText;
    
    public float displayTimeWelcome = 5f;
    public float displayTimeMessages = 3f;
    public float displayMaxCountReached = 4f;
    
    public ThrowMolli throwMolli;
    public ThrowStone throwStone;

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
        currentStep++;
        ShowTutorialStep();
    }

    void ShowTutorialStep()
    {
        if (currentStep < tutorialSteps.Length)
        {
            welcomeText.GetComponent<TextMeshProUGUI>().text = tutorialSteps[currentStep];
            currentStep++;
            StartCoroutine(WaitForInput());
        }
        else
        {
            animationWelcomeText.StopWobble();
            welcomeText.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    IEnumerator WaitForInput()
    {
        doContinue.action.Disable();
        yield return new WaitForSeconds(displayTimeMessages);
        doContinue.action.Enable();
    }

    public void UpdateCount(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks left: " + bricksLeft.ToString();
        // countText.text = "Items left: " + bricksLeft.ToString() + "\nItems thrown: " + count.ToString() + "/" + max.ToString();
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
        countText.text = "Bricks left: " + throwStone.GetBricksLeft() + "\nMollies left: " + throwMolli.GetMollisLeft();
    }

    public IEnumerator UpdateMaxCountReached(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
        countText.color = Color.red;
        yield return new WaitForSeconds(displayMaxCountReached);
        countText.color = Color.white;
    }

    void OnEnable()
    {
        doContinue.action.started += DoContinue;
        doContinue.action.Enable();
    }

    void OnDisable()
    {
        doContinue.action.started -= DoContinue;
        doContinue.action.Disable();
    }

    private void DoContinue(InputAction.CallbackContext obj)
    {
        ShowTutorialStep();
    }
}
