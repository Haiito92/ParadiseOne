using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SeaCanvas : MonoBehaviour
{
    [SerializeField] private SeaManager _seaManager;

    [SerializeField] private SeaScoresUI _seaScoresUI;
        
    [SerializeField] private GameObject _seaStartGameMenuObject;
    [SerializeField] private GameObject _seaEndGameMenuObject;

    private void Start()
    {
        _seaManager.SeaGameStarted += OnSeaGameStarted;
        _seaManager.SeaGameEnded += OnSeaGameEnded;
        
        _seaScoresUI.InitSeaScoresUI(_seaManager.SeaScores);
    }

    private void OnSeaGameStarted()
    {
        _seaStartGameMenuObject.SetActive(false);
    }

    #region React To SeaManager Events

    private void OnSeaGameEnded()
    {
        _seaEndGameMenuObject.SetActive(true);
    }

    #endregion
    
}
