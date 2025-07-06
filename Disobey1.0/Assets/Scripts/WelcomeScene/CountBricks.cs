using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CountBricks : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public float displayMaxCountReached = 4f;

    public void UpdateCount(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks left: " + bricksLeft.ToString();
        countText.text = "Bricks left: " + bricksLeft.ToString() + "\nBricks thrown: " + count.ToString() + "/" + max.ToString();
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
    }

    public IEnumerator UpdateMaxCountReached(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
        countText.color = Color.red; // Change text color to red
        yield return new WaitForSeconds(displayMaxCountReached);
        countText.color = Color.white; // Change text color back to white
    }
}
