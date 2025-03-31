using UnityEngine;
using Mirror;

public class Collectible : NetworkBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        if (isServer)
        {
            mainCamera = Camera.main;
            Reposition();
        }
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScore player))
        {
            player.IncreaseScore();
            Reposition();
        }
    }

    [Server]
    private void Reposition()
    {
        if (mainCamera == null) return;

        // Get camera bounds in world space
        float camHeight = mainCamera.orthographicSize * 2f;
        float camWidth = camHeight * mainCamera.aspect;

        // Random position within the camera's view
        float x = Random.Range(mainCamera.transform.position.x - camWidth / 2, mainCamera.transform.position.x + camWidth / 2);
        float y = Random.Range(mainCamera.transform.position.y - camHeight / 2, mainCamera.transform.position.y + camHeight / 2);

        transform.position = new Vector2(x, y);
    }
}
