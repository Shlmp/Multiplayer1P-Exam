using UnityEngine;
using Mirror;

public class Collectible : NetworkBehaviour
{
    private void Start()
    {
        if (isServer)
        {
            SetRandomPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (collision.TryGetComponent(out PlayerScore playerScore))
        {
            playerScore.CmdIncreaseScore(); // Only this method will increase score
            CmdCollectItem(); // Move collectible
        }
    }

    [Command(requiresAuthority = false)]
    private void CmdCollectItem()
    {
        SetRandomPosition();
    }

    private void SetRandomPosition()
    {
        Vector3 newPosition = GetRandomPosition();
        transform.position = newPosition;
        RpcUpdatePosition(newPosition);
    }

    [ClientRpc]
    private void RpcUpdatePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0f);
    }
}
