using UnityEngine;
using Mirror;
using TMPro;

public class ScoreboardManager : NetworkBehaviour
{
    public TMP_Text scoreText;  // Assign in Unity Inspector

    private void OnEnable()
    {
        PlayerScore.UpdateScoreboard += UpdateScoreDisplay;
    }

    private void OnDisable()
    {
        PlayerScore.UpdateScoreboard -= UpdateScoreDisplay;
    }

    private void UpdateScoreDisplay()
    {
        PlayerScore[] players = FindObjectsOfType<PlayerScore>();

        string scoreboard = "Scores:\n";
        foreach (var player in players)
        {
            scoreboard += $"Player {player.netId}: {player.GetScore()}\n";
        }

        scoreText.text = scoreboard;
    }
}
