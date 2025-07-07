using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CountBricks : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public float displayTimeMaxCountReached = 4f;

    public ThrowMolli throwMolli;
    public ThrowStone throwStone;

    public void UpdateCount(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks left: " + bricksLeft.ToString();
        // countText.text = "Bricks left: " + bricksLeft.ToString() + "\nBricks thrown: " + count.ToString() + "/" + max.ToString();
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
        // countText.text = "Bricks left: " + bricksLeft.ToString() + "\nMollies left: ";
        countText.text = "Bricks left: " + throwStone.GetBricksLeft() + "\nMollies left: " + throwMolli.GetMollisLeft();
    }

    public IEnumerator UpdateMaxCountReached(int count, int bricksLeft, int max)
    {
        // countText.text = "Bricks thrown: " + count.ToString() + "/" + max.ToString();
        countText.color = Color.red;
        yield return new WaitForSeconds(displayTimeMaxCountReached);
        countText.color = Color.white;
    }
}
