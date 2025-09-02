using System;
using UnityEngine;

public class SeaTimer : MonoBehaviour
{
    #region Fields
    private float _seaTimer = 30; 
    #endregion

    #region Actions

    public event Action SeaTimerElapsed; 

    #endregion

    #region Init

    public void InitTimer(float seaGameLength)
    {
        if (seaGameLength > 0)
        {
            _seaTimer = seaGameLength;
        }
    }

    #endregion
    
    #region Unity Lifecycle

    void Update()
    {
        _seaTimer -= Time.deltaTime;

        if (_seaTimer <= 0)
        {
            SeaTimerElapsed?.Invoke();
        }
    }

    #endregion
    
    
}
