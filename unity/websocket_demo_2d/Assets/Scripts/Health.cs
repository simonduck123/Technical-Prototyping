using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    private int currentHealth; // Current health

    void Start()
    {
        // Initialize current health to max health at the start
        currentHealth = maxHealth;
    }

    // Method to reduce health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if health drops below 0
        if (currentHealth <= 0)
        {
            // Call any function you like here, e.g., to handle player death
            Debug.Log("Player is dead!");
            // Optionally, destroy the player object or trigger death animation
             Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Player health: {currentHealth}");
        }
    }
}
