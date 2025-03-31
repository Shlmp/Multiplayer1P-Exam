using UnityEngine;
using Mirror;
using TMPro;

public class PlayerScore : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnScoreChanged))]
    private int score = 0;

    public static event System.Action UpdateScoreboard;

    public void IncreaseScore()
    {
        if (!isServer) return;

        score++;
        UpdateScoreboard?.Invoke();
    }

    private void OnScoreChanged(int oldScore, int newScore)
    {
        UpdateScoreboard?.Invoke();
    }

    public int GetScore() => score;
}
