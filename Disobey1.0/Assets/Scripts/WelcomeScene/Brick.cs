using UnityEngine;

public class Brick : MonoBehaviour
{
    public float damage = 10f; // Initial damage of the brick
    
    private void Start()
    {
        // Optionally, you can initialize or set up anything else here
        Debug.Log("Brick initialized with damage: " + damage);
    }
}
