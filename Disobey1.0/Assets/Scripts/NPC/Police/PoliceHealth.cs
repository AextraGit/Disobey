using System.IO.Pipes;
using UnityEngine;

public class PoliceHealth : MonoBehaviour
{
    public float health = 50f;
    public Animator animator;

    private void Start()
    {
        Debug.Log("Police Man initialized with health: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            float damage = collision.gameObject.GetComponent<Brick>().damage;
            Debug.Log($"Collision detected with Brick! Damage: {damage}");
            TakeDamage(damage);
        }
        if (collision.gameObject.CompareTag("Molli")) {
            animator.SetBool("IsMolotowHit", true);
            Debug.Log(animator.GetBool("IsMolotowHit") + " is the value of IsMolliHit");
            float damage = collision.gameObject.GetComponent<Bottle>().damage;
            Debug.Log($"Collision detected with Molli! Damage: {damage}");
            TakeDamage(damage);
        }
        //animator.SetBool("IsMolotowHit", false);
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
