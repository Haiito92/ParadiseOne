using System;
using NaughtyAttributes;
using UnityEngine;

public class SeaManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private SeaDataSO _seaData;
    [SerializeField] private SeaFishSpawner _seaFishSpawner;
    #endregion

    #region Properties
    [field:SerializeField] public SeaScores SeaScores { get; private set; }
    [field:SerializeField] public SeaTimer SeaTimer { get; private set; }
    #endregion

    #region Actions
    public event Action SeaGameStarted;
    public event Action SeaGameEnded;
    #endregion
    
    private void Awake()
    {
        SeaTimer.InitTimer(_seaData.SeaGameLength);
        
        _seaFishSpawner.InitSeaFishSpawner();
    }

    private void Start()
    {
        SeaTimer.SeaTimerElapsed += OnSeaTimerElapsed;
        _seaFishSpawner.SpawnedFishCollected += OnSeaFishCollected;
    }


    #region SeaGame

    public void StartSeaGame()
    {
        SeaScores.ResetScores();
        SeaGameStarted?.Invoke();
        SeaTimer.StartTimer();
        _seaFishSpawner.StartSpawner();
    }

    private void EndSeaGame()
    {
        _seaFishSpawner.StopSpawner();
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
        SeaScores.AddScore(collector, scoreToAdd);
    }
    #endregion

    //TODO REMOVE THIS SECTION
    #region Test Functions
    [Button]
    public void TestStartSeaGame() => StartSeaGame();
    #endregion
}
