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
    [SerializeField] private SeaEventRandomizer _seaEventRandomizer;
    #endregion

    #region Properties
    [field:SerializeField] public SeaScores SeaScores { get; private set; }
    [field:SerializeField] public SeaTimer SeaTimer { get; private set; }

    [SerializeField] private BoatControllerAbsolute _playerOneBoat;
    [SerializeField] private BoatControllerAbsolute _playerTwoBoat;
    [SerializeField] private BoatBoostController _playerOneBoostController;
    [SerializeField] private BoatBoostController _playerTwoBoostController;
    #endregion

    #region Actions
    public event Action SeaGameStarted;
    public event Action SeaGameEnded;
    #endregion
    
    private void Awake()
    {
        SeaTimer.InitTimer(_seaDataSO.SeaGameLength);
        
        _seaEventRandomizer.InitSeaEventRandomizer(_seaDataSO);
        _seaEventRandomizer.EventStarted += OnSeaEventStarted;
        _seaEventRandomizer.EventStopped += OnSeaEventStopped;
        
        _seaFishSpawner.InitSeaFishSpawner(_seaDataSO, _seaEventRandomizer);

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
        
        _seaEventRandomizer.StartSeaEventRandomizer();
    }

    private void EndSeaGame()
    {
        _seaEventRandomizer.StopSeaEventRandomizer();
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

    #region React To SeaEventRandomizer Events

    private void OnSeaEventStarted(SeaEventsEnum eventType)
    {
        switch (eventType)
        {
            case SeaEventsEnum.Undefined:
                //Debug.LogError("Received Undefined Event");
                break;
            case SeaEventsEnum.FastFish:
                _seaFishSpawner.AllFishFast();
                break;
            case SeaEventsEnum.BigFish:
                _seaFishSpawner.AllFishBig();
                break;
            case SeaEventsEnum.CloudyWater:
                _seaFishSpawner.AllFishNotVisible();
                break;
            case SeaEventsEnum.InvertedControls:
                _playerOneBoat.SetInvertedInputs(true);
                _playerTwoBoat.SetInvertedInputs(true);
                break;
            case SeaEventsEnum.UnlimitedBoost:
                _playerOneBoostController.InfiniteBoostOn();
                _playerTwoBoostController.InfiniteBoostOn();
                break;
            default:
                //Debug.LogError("Received default (undefined) Event");
                break;
        }
    }

    private void OnSeaEventStopped(SeaEventsEnum eventType)
    {
        switch (eventType)
        {
            case SeaEventsEnum.Undefined:
                //Debug.LogError("Received Undefined Event");
                break;
            case SeaEventsEnum.FastFish:
                _seaFishSpawner.AllFishSlow();
                break;
            case SeaEventsEnum.BigFish:
                _seaFishSpawner.AllFishSmall();
                break;
            case SeaEventsEnum.CloudyWater:
                _seaFishSpawner.AllFishVisible();
                break;
            case SeaEventsEnum.InvertedControls:
                _playerOneBoat.SetInvertedInputs(false);
                _playerTwoBoat.SetInvertedInputs(false);
                break;
            case SeaEventsEnum.UnlimitedBoost:
                _playerOneBoostController.InfiniteBoostOff();
                _playerTwoBoostController.InfiniteBoostOff();
                break;
            default:
                //Debug.LogError("Received default (undefined) Event");
                break;
        }
    }

    #endregion
}
