using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 50f;

    private void Start()
    {
        Debug.Log("Player initialized with health: " + health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player Health: {health}");

        if (health <= 0)
        {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        Debug.Log("Player destroyed!");
        Destroy(gameObject);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float damage = collision.gameObject.GetComponent<Brick>().damage;
            Debug.Log($"Collision detected with Enemy! Damage: {damage}");
            TakeDamage(damage);
        }
    }
    */
}

