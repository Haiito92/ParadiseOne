using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeaEventRandomizer : MonoBehaviour
{
    #region Fields
    [SerializeField] private SeaTimer _eventTimer;
    private float _eventLength;

    private List<SeaEventsEnum> _allEvents = new List<SeaEventsEnum>();
    private SeaEventsEnum _currentEvent;
    #endregion

    #region Actions

    public event Action<SeaEventsEnum> EventStarted;
    public event Action<SeaEventsEnum> EventStopped;

    #endregion    

    public void InitSeaEventRandomizer(SeaDataSO seaDataSo)
    {
        _eventLength = seaDataSo.EventLength;

        _allEvents = seaDataSo.Events;
        _currentEvent = SeaEventsEnum.Undefined;
        
        _eventTimer.InitTimer(_eventLength, true);
        _eventTimer.SeaTimerElapsed += OnEventTimerElapsed;
    }

    public void StartSeaEventRandomizer()
    {
        _eventTimer.StartTimer();
    }
    
    public void StopSeaEventRandomizer()
    {
        _eventTimer.StopTimer();
    }

    #region React to Event Timer
    private void OnEventTimerElapsed()
    {
        EventStopped?.Invoke(_currentEvent);

        int randomIndex = Random.Range(0, _allEvents.Count);

        _currentEvent = _allEvents[randomIndex];

        EventStarted?.Invoke(_currentEvent);
        
        //Debug.LogWarning($"New event is : {_currentEvent}");
    }
    #endregion
}
