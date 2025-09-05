using System;
using UnityEngine;

public class SeaScores : MonoBehaviour
{
    #region Fields
    private int _playerOneScore = 0;
    private int _playerTwoScore = 0;
    #endregion

    #region Action

    public event Action<PlayerEnum, int> ScoreUpdated;

    #endregion
    
    
    public void ResetScores()
    {
        _playerOneScore = 0;
        _playerTwoScore = 0;
    }

    public void AddScore(PlayerEnum player, int scoreToAdd)
    {
        switch (player)
        {
            case PlayerEnum.None:
                return;
            case PlayerEnum.PlayerOne:
                _playerOneScore += scoreToAdd;
                ScoreUpdated?.Invoke(player, _playerOneScore);
                return;
            case PlayerEnum.PlayerTwo:
                _playerTwoScore += scoreToAdd;
                ScoreUpdated?.Invoke(player, _playerTwoScore);
                return;
            default:
                return;
        }
    }

    public int GetScore(PlayerEnum player)
    {
        return player switch
        {
            PlayerEnum.None => -1,
            PlayerEnum.PlayerOne => _playerOneScore,
            PlayerEnum.PlayerTwo => _playerTwoScore,
            _ => -1
        };
    }
}
