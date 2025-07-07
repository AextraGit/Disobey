using UnityEngine;

public class PoliceHealth : MonoBehaviour
{
    public float health = 50f; // Initial health of the police

    private void Start()
    {
        Debug.Log("Police Man initialized with health: " + health);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            float damage = collision.gameObject.GetComponent<Brick>().damage;
            Debug.Log($"Collision detected with Cube! Damage: {damage}");
            TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Molli")) {
            float damage = collision.gameObject.GetComponent<Brick>().damage;
            Debug.Log($"Collision detected with Cube! Damage: {damage}");
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Police Health: {health}");

        if (health <= 0)
        {
            DestroyPolice();
        }
    }

    private void DestroyPolice()
    {
        Debug.Log("Police destroyed!");
        Destroy(gameObject);
    }
}
