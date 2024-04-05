using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // Damage dealt by the bullet

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet hits the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the Health component on the player
            Health playerHealth = collision.gameObject.GetComponent<Health>();

            // If the Health component is found, deal damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
