using System;
using UnityEngine;

public class SeaLevelManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private SeaDataSO _seaData;
    [SerializeField] private SeaTimer _seaTimer;
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
        GameManager.Instance.LoadScene(0); // 0 is the index of the MainMenu scene in the the build scene list
    }

    #endregion
}
