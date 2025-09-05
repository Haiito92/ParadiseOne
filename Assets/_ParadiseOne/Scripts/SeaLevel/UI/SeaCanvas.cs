using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SeaCanvas : MonoBehaviour
{
    [SerializeField] private SeaManager _seaManager;

    [SerializeField] private SeaScoresUI _seaScoresUI;
    [SerializeField] private SeaTimerUI _seaTimerUI;
    [SerializeField] private SeaEventsUI _seaEventsUI;

    [SerializeField] private GameObject _seaGameStatsUIObject;
    [SerializeField] private GameObject _seaStartGameMenuObject;
    [SerializeField] private GameObject _seaEndGameMenuObject;

    private void Start()
    {
        _seaManager.SeaGameStarted += OnSeaGameStarted;
        _seaManager.SeaGameEnded += OnSeaGameEnded;
        
        _seaScoresUI.InitSeaScoresUI(_seaManager.SeaScores);
        _seaTimerUI.InitSeaTimerUI(_seaManager.SeaTimer);
        _seaEventsUI.InitSeaEventsUI(_seaManager.SeaEventRandomizer);
    }

    private void OnSeaGameStarted()
    {
        _seaGameStatsUIObject.SetActive(true);
        _seaStartGameMenuObject.SetActive(false);
    }

    #region React To SeaManager Events

    private void OnSeaGameEnded()
    {
        _seaEndGameMenuObject.SetActive(true);
        _seaGameStatsUIObject.SetActive(false);
    }

    #endregion
    
}
