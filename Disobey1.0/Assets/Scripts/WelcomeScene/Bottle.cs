using UnityEngine;

public class Bottle : MonoBehaviour
{
    public float damage = 30f;
    public GameObject firePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Police")) { 
            Instantiate(firePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
