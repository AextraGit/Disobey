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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
