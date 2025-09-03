using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void UpdateScoreText(int score)
    {
        _scoreText.text = $"Score : {score}";
    }
}
