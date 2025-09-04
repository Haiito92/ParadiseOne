using System;
using TMPro.Examples;
using Unity.Mathematics;
using UnityEngine;

public class SeaTimer : MonoBehaviour
{
    #region Fields

    private float _originalSeaTimerValue = 30;
    private float _seaTimer = 30;
    private bool _seaTimerOn = false;
    private bool _isLooping = false;
    #endregion

    #region Actions

    public event Action<float> SeaTimerTicked;
    public event Action SeaTimerElapsed; 

    #endregion

    #region Timer

    public void InitTimer(float timerValue, bool isLooping = false)
    {
        if (timerValue > 0)
        {
            _originalSeaTimerValue = timerValue;
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
        
        _seaTimer = Math.Max(_seaTimer - deltaTime, 0f);
        SeaTimerTicked?.Invoke(_seaTimer);

        if (_seaTimer <= 0)
        {
            SeaTimerElapsed?.Invoke();

            if (_isLooping)
            {
                RefreshTimer();
            }
            else
            {
                StopTimer();
            }
        }
    }

    public void RefreshTimer()
    {
        _seaTimer = _originalSeaTimerValue;
    }
    #endregion
    
    #region Unity Lifecycle

    void Update()
    {
        TickTimer(Time.deltaTime);
    }

    #endregion
    
    
}
