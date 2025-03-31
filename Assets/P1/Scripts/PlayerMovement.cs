using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public float speed = 5f;

    [SyncVar(hook = nameof(OnColorChanged))]
    private Color playerColor; // Sync color across clients

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        playerColor = GetRandomColor(); // Assign a random color on the server
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        OnColorChanged(Color.white, playerColor); // Apply the color on the client
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

        rb.linearVelocity = moveInput * speed;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    private void OnColorChanged(Color oldColor, Color newColor)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.color = newColor;
    }
}
