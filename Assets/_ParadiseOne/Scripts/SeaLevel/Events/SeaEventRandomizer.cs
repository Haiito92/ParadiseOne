using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SeaEventRandomizer : MonoBehaviour
{
    #region Fields
    [SerializeField] private SeaTimer _eventTimer;
    private float _eventLength;

    private List<SeaEventsEnum> _allEvents = new List<SeaEventsEnum>();
    public SeaEventsEnum CurrentEvent { get; private set; }
    #endregion

    #region Actions

    public event Action<SeaEventsEnum> EventStarted;
    public event Action<SeaEventsEnum> EventStopped;

    #endregion    

    public void InitSeaEventRandomizer(SeaDataSO seaDataSo)
    {
        _eventLength = seaDataSo.EventLength;

        _allEvents = seaDataSo.Events;
        CurrentEvent = SeaEventsEnum.Undefined;
        
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
        EventStopped?.Invoke(CurrentEvent);

        int randomIndex = Random.Range(0, _allEvents.Count);

        CurrentEvent = _allEvents[randomIndex];

        EventStarted?.Invoke(CurrentEvent);
        
        Debug.LogWarning($"New event is : {CurrentEvent}");
    }
    #endregion
}
