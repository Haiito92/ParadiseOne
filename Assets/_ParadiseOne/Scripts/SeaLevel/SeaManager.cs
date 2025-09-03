using System;
using UnityEngine;

public class SeaManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private SeaDataSO _seaData;
    [SerializeField] private SeaTimer _seaTimer;
    #endregion

    #region Actions

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

    #region React To SeaTimer Events

    private void OnSeaTimerElapsed()
    {
        SeaGameEnded?.Invoke();
    }

    #endregion
}
