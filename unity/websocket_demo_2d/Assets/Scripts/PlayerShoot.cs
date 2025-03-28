using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint; // Point from which the projectile is spawned
    public float projectileSpeed = 10f; // Speed of the projectile

    // Update is called once per frame
    void Update()
    {
        // Check for shooting input (example: pressing spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate a new projectile at the fire point position and rotation
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

        // Check if the Rigidbody2D component exists
        if (rb != null)
        {
            // Set the velocity of the projectile to move in the direction the ship is facing
            rb.linearVelocity = transform.up * projectileSpeed; // Change here from transform.right to transform.up
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on the projectile prefab!");
        }
    }

}
