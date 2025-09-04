using System;
using TMPro;
using UnityEngine;

public class SeaTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    
    public void InitSeaTimerUI(SeaTimer seaGameTimer)
    {
        seaGameTimer.SeaTimerTicked += OnSeaTimerTicked;
    }

    private void OnSeaTimerTicked(float timerValue)
    {
        int minutes = (int)Math.Floor(timerValue / 60f); //Timer is in seconds;
        int leftSeconds = (int)Math.Floor(timerValue % 60);
        int tens = (int)Math.Floor(leftSeconds / 10f);
        int units = (int)Math.Floor(leftSeconds % 10f);
        
        _timerText.text = $"{minutes} : {tens}{units}";
    }
}
