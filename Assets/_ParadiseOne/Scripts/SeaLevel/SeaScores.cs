using UnityEngine;

public class SeaScores : MonoBehaviour
{
    public int PlayerOneScore { get; private set; }
    public int PlayerTwoScore { get; private set; }

    public void ResetScores()
    {
        PlayerOneScore = 0;
        PlayerTwoScore = 0;
    }

    public void AddScore(PlayerEnum player, int scoreToAdd)
    {
        switch (player)
        {
            case PlayerEnum.None:
                return;
            case PlayerEnum.PlayerOne:
                PlayerOneScore += scoreToAdd;
                return;
            case PlayerEnum.PlayerTwo:
                PlayerTwoScore += scoreToAdd;
                return;
            default:
                return;
        }
    }
}
