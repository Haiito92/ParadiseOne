using System;
using TMPro.Examples;
using UnityEngine;

public class SeaTimer : MonoBehaviour
{
    #region Fields
    private float _seaTimer = 30;
    private bool _seaTimerOn = false;
    private bool _isLooping = false;
    #endregion

    #region Actions

    public event Action SeaTimerElapsed; 

    #endregion

    #region Timer

    public void InitTimer(float timerValue, bool isLooping = false)
    {
        if (timerValue > 0)
        {
            _seaTimer = timerValue;
        }

        _isLooping = isLooping;
    }

    public void StartTimer()
    {
        _seaTimerOn = true;
    }

    public void StopTimer()
    {
        _seaTimerOn = false;
    }

    private void TickTimer(float deltaTime)
    {
        if(_seaTimerOn == false) return;
        
        _seaTimer -= deltaTime;

        if (_seaTimer <= 0)
        {
            SeaTimerElapsed?.Invoke();
        }
    }

    #endregion
    
    #region Unity Lifecycle

    void Update()
    {
        TickTimer(Time.deltaTime);
    }

    #endregion
    
    
}
