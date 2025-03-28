using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjustable variable for player movement speed

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Read input from arrow keys or custom keys for movement
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Check for left and right movement
        if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1f;
        }

        // Check for up and down movement
        if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1f;
        }

        // Calculate movement direction
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Normalize movement vector to ensure consistent speed in all directions
        movement.Normalize();

        // Apply movement to the player's Rigidbody2D component
        rb.linearVelocity = movement * moveSpeed;
    }
}
