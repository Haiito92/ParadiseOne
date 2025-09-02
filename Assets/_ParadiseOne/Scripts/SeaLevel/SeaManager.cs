using UnityEngine;

public class SeaLevelManager : MonoBehaviour
{
    [SerializeField] private SeaTimer _seaTimer;

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
