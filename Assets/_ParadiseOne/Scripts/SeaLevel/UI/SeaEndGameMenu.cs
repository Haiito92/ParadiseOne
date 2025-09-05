using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class SeaEndGameMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreOneText;
    [SerializeField] private TextMeshProUGUI _scoreTwoText;
    [SerializeField] private TextMeshProUGUI _winnerText;

    [SerializeField] private float _addingRate = 20f;
    public void ResetUI()
    {
        _scoreOneText.text = "0";
        _scoreTwoText.text = "0";
        _winnerText.text = "";
        _winnerText.gameObject.SetActive(false);
    }

    public void ShowScores(SeaScores seaScores)
    {
        StartCoroutine(ScoreAnim(seaScores));
    }

    private IEnumerator ScoreAnim(SeaScores seaScores)
    {
        
        float finalScorePlayerOne = seaScores.GetScore(PlayerEnum.PlayerOne), finalScorePlayerTwo = seaScores.GetScore(PlayerEnum.PlayerTwo);

        float showedScoreOne = 0, showedScoreTwo = 0;
        
        while (showedScoreOne < finalScorePlayerOne || showedScoreTwo < finalScorePlayerTwo)
        {
            if (showedScoreOne < finalScorePlayerOne)
            {
                showedScoreOne = Mathf.Min(showedScoreOne + _addingRate * Time.deltaTime, finalScorePlayerOne);
                _scoreOneText.text = $"{showedScoreOne}";
            }
            
            if (showedScoreTwo < finalScorePlayerTwo)
            {
                showedScoreTwo = Mathf.Min(showedScoreTwo + _addingRate * Time.deltaTime, finalScorePlayerTwo);
                _scoreTwoText.text = $"{showedScoreTwo}";
            }
            yield return null;
        }

        if (finalScorePlayerOne > finalScorePlayerTwo)
        {
            _winnerText.text = "Player One Wins !";
        }
        else if (finalScorePlayerOne < finalScorePlayerTwo)
        {
            _winnerText.text = "Player Two Wins !";
        }
        else
        {
            _winnerText.text = "Draw";
        }
        
        _winnerText.gameObject.SetActive(true);
    }
}
