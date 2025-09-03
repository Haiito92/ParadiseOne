using System;
using UnityEngine;

public class SeaManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private SeaDataSO _seaData;
    [SerializeField] private SeaTimer _seaTimer;
    [SerializeField] private SeaFishSpawner _seaFishSpawner;
    [SerializeField] private SeaScores _seaScores;
    #endregion

    #region Actions

    public event Action SeaGameStarted;
    public event Action SeaGameEnded;

    #endregion
    
    private void Awake()
    {
        _seaTimer.InitTimer(_seaData.SeaGameLength);
    }

    private void Start()
    {
        _seaTimer.SeaTimerElapsed += OnSeaTimerElapsed;
        _seaFishSpawner.SpawnedFishCollected += OnSeaFishCollected;
    }


    #region SeaGame

    public void StartSeaGame()
    {
        _seaScores.ResetScores();
        SeaGameStarted?.Invoke();
        _seaTimer.StartTimer();
    }

    private void EndSeaGame()
    {
        SeaGameEnded?.Invoke();
    }
    #endregion
    
    #region React To SeaTimer Events
    private void OnSeaTimerElapsed()
    {
        EndSeaGame();
    }
    #endregion
    
    #region React To SeaFishSpawner Events
    

    private void OnSeaFishCollected(PlayerEnum collector, int scoreToAdd)
    {
        _seaScores.AddScore(collector, scoreToAdd);
    }
    #endregion
}
