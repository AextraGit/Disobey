using UnityEngine;

public class Bottle : MonoBehaviour
{
    public float damage = 30f;
    public GameObject firePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Police")) {

            ContactPoint contact = collision.contacts[0];
            Vector3 awayFromCollision = (contact.normal);

            GameObject fire = Instantiate(firePrefab, transform.position + awayFromCollision*0.5f, Quaternion.identity);

            Rigidbody rb = fire.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(awayFromCollision * 1f, ForceMode.Impulse);
            }
            Destroy(gameObject);
        }
    }
}
