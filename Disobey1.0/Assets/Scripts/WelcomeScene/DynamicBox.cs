using UnityEngine;

public class DynamicBox : MonoBehaviour
{
    public float health = 50f; // Initial health of the box

    private void Start()
    {
        Debug.Log("DynamicBox initialized with health: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            float damage = collision.gameObject.GetComponent<Brick>().damage;
            Debug.Log($"Collision detected with Cube! Damage: {damage}");
            TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Molli"))
        {
            float damage = collision.gameObject.GetComponent<Bottle>().damage;
            Debug.Log($"Collision detected with Cube! Damage: {damage}");
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Box Health: {health}");

        if (health <= 0)
        {
            DestroyBox();
        }
    }

    private void DestroyBox()
    {
        Debug.Log("Box destroyed!");
        Destroy(gameObject);
    }
}
