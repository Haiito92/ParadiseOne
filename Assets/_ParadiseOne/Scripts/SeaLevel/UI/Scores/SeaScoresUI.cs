using System;
using UnityEngine;

public class SeaScoresUI : MonoBehaviour
{
    [SerializeField] private PlayerScoreUI _playerOneScoreUI;
    [SerializeField] private PlayerScoreUI _playerTwoScoreUI;

    public void InitSeaScoresUI(SeaScores seaScores)
    {
        Debug.Log(seaScores == null ? "SeaScore Null" : "SeaScore Not Null"); 
        _playerOneScoreUI.UpdateScoreText(seaScores.GetScore(PlayerEnum.PlayerOne));
        _playerTwoScoreUI.UpdateScoreText(seaScores.GetScore(PlayerEnum.PlayerTwo));

        seaScores.ScoreUpdated += OnScoreUpdated;
    }

    private void OnScoreUpdated(PlayerEnum player, int score)
    {
        UpdatePlayerScore(player, score);
    }

    private void UpdatePlayerScore(PlayerEnum player, int scoreToShow)
    {
        switch (player)
        {
            case PlayerEnum.None:
                return;
            case PlayerEnum.PlayerOne:
                _playerOneScoreUI.UpdateScoreText(scoreToShow);
                return;
            case PlayerEnum.PlayerTwo:
                _playerTwoScoreUI.UpdateScoreText(scoreToShow);
                return;
            default:
                return;
        }
    }
}
