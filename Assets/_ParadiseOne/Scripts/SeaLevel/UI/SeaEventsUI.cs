using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

public class SeaEventsUI : MonoBehaviour
{
    [SerializeField] private Image _eventImage;
    [SerializeField] private SerializedDictionary<SeaEventsEnum, Sprite> _eventSprites;
    
    public void InitSeaEventsUI(SeaEventRandomizer seaEventRandomizer)
    {
        _eventImage.gameObject.SetActive(false);
        seaEventRandomizer.EventStarted += OnEventStarted;
    }

    private void OnEventStarted(SeaEventsEnum eventType)
    {
        if (eventType == SeaEventsEnum.Undefined)
        {
            _eventImage.gameObject.SetActive(false);
            return;
        }
        
        _eventImage.gameObject.SetActive(true);

        _eventImage.sprite = _eventSprites[eventType];
    }
}
