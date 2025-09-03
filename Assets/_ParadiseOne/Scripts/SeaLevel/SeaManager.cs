using System;
using UnityEngine;

public class SeaManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private SeaDataSO _seaData;
    [SerializeField] private SeaTimer _seaTimer;
    #endregion

    #region Actions

    public event Action SeaGameStarted;
    public event Action SeaGameEnded;

    #endregion
    
    private void Awake()
    {
        _seaTimer.InitTimer(_seaData.SeaGameLength);
    }

    private void Start()
    {
        _seaTimer.SeaTimerElapsed += OnSeaTimerElapsed;
    }


    #region SeaGame

    public void StartSeaGame()
    {
        SeaGameStarted?.Invoke();
        _seaTimer.StartTimer();
    }

    private void EndSeaGame()
    {
        SeaGameEnded?.Invoke();
    }
    #endregion
    
    #region React To SeaTimer Events

    private void OnSeaTimerElapsed()
    {
        EndSeaGame();
    }

    #endregion
}
