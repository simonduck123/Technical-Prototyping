using extOSC;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjustable variable for player movement speed
    private string input;
    private Rigidbody2D rb;
    public GameObject projectilePrefab; // Prefab for the projectile
    public Transform firePoint; // Point from which the projectile is spawned
    public float projectileSpeed = 10f; // Speed of the projectile

    public string Address = "/game";

    [Header("OSC Settings")]
    public OSCReceiver Receiver;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Receiver.Bind(Address, ReceiveString);
    }

    void Update()
    {
        // Read input from arrow keys or custom keys for movement
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (input == "j")
        {
            moveHorizontal = -1f;
        }
        if (input == "l")
        {
            moveHorizontal = 1f;
        }
        if (input == "i")
        {
            moveVertical = 1f;
        }
        if (input == "k")
        {
            moveVertical = -1f;
        }
        if (input == "p")
        {
            Shoot();
        }

        // Calculate movement direction
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Normalize movement vector to ensure consistent speed in all directions
        movement.Normalize();

        // Apply movement to the player's Rigidbody2D component
        rb.linearVelocity = movement * moveSpeed;
    }

    public void ReceiveString(OSCMessage message)
    {
        if (message.ToString(out var value))
        {
            input = value;
            Debug.Log(input);
        }
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Set the velocity of the projectile to move in the direction the ship is facing
            rb.linearVelocity = -transform.up * projectileSpeed; // Change here from transform.right to transform.up
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on the projectile prefab!");
        }
    }
}
