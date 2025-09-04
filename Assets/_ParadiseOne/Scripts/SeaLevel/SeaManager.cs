using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class SeaManager : MonoBehaviour
{
    #region Fields
    [Header("Sea Level Manager Setup")]
    [SerializeField] private SeaDataSO _seaDataSO;
    [SerializeField] private SeaFishSpawner _seaFishSpawner;
    #endregion

    #region Properties
    [field:SerializeField] public SeaScores SeaScores { get; private set; }
    [field:SerializeField] public SeaTimer SeaTimer { get; private set; }

    [SerializeField] private BoatControllerAbsolute _playerOneBoat;
    [SerializeField] private BoatControllerAbsolute _playerTwoBoat;
    #endregion

    #region Actions
    public event Action SeaGameStarted;
    public event Action SeaGameEnded;
    #endregion
    
    private void Awake()
    {
        SeaTimer.InitTimer(_seaDataSO.SeaGameLength);
        
        _seaFishSpawner.InitSeaFishSpawner(_seaDataSO);
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
        
        if(!_playerOneBoat) Debug.LogError("Missing Player One Boat");
        _playerOneBoat.StartBoat();
        
        if(!_playerTwoBoat) Debug.LogError("Missing Player Two Boat");
        _playerTwoBoat.StartBoat();
    }

    private void EndSeaGame()
    {
        _seaFishSpawner.StopSpawner();
        
        _playerOneBoat?.StopBoat();
        _playerTwoBoat?.StopBoat();
        
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
