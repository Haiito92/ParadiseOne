using System;
using NaughtyAttributes;
using UnityEngine;

public class SeaTimer : MonoBehaviour
{
    #region Fields
    [SerializeField, Label("Game Length (in seconds)"), Tooltip("In seconds")] private float _gameLength;
    private float _seaTimer; 
    #endregion

    #region Actions

    public event Action SeaTimerElapsed; 

    #endregion
    
    private void Awake()
    {
        _seaTimer = _gameLength;
    }

    // Update is called once per frame
    void Update()
    {
        _seaTimer -= Time.deltaTime;

        if (_seaTimer <= 0)
        {
            SeaTimerElapsed?.Invoke();
        }
    }
}
