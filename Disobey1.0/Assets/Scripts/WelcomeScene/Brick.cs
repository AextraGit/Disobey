using UnityEngine;

public class Brick : MonoBehaviour
{
    public float damage = 10f;
    
    private void Start()
    {
        Debug.Log("Brick initialized with damage: " + damage);
    }
}
