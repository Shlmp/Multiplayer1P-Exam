using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D reference
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        // Get movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical).normalized; // Normalize to prevent diagonal speed boost
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;

        // Apply velocity directly for better movement
        rb.linearVelocity = moveInput * speed;
    }
}
