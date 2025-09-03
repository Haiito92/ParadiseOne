using System;
using System.Collections.Generic;
using UnityEngine;

public class SeaFishSpawner : MonoBehaviour
{
    #region Fields
    
    private List<Fish> _fishes;

    //Spawning
    private Camera _seaLevelCamera;
    private Rect _spawningZone;

    [SerializeField] private SeaTimer _spawningTimer;
    [SerializeField] private float _spawningInterval = 5;
    [SerializeField] private int _maxNumberOfFish;
    
    #endregion

    #region Actions

    public event Action<PlayerEnum, int> SpawnedFishCollected;

    #endregion

    #region Spawner

    private void SetupSpawningZone()
    {
        float height = _seaLevelCamera.orthographicSize * 2;
        float width = height * _seaLevelCamera.aspect;
        Vector2 spawningZoneSize = new Vector2(width, height);
        
        Vector2 spawningZonePosition = new Vector2(_seaLevelCamera.transform.position.x - width / 2,
            _seaLevelCamera.transform.position.y - height / 2);
        
        _spawningZone = new Rect(spawningZonePosition, spawningZoneSize);
    }

    public void InitSeaFishSpawner()
    {
        // Setup of spawning zone;

        _seaLevelCamera = Camera.main;

        if (_seaLevelCamera == null)
        {
            Debug.LogError("NO MAIN CAMERA");
        }
        
        SetupSpawningZone();
        
        _spawningTimer.InitTimer(_spawningInterval, true);

        _spawningTimer.SeaTimerElapsed += OnSpawningTimerElasped;
    }

    public void StartSpawner()
    {
        _spawningTimer.StartTimer();
    }

    public void StopSpawner()
    {
        //TODO
    }
    #endregion

    #region React To SpawningTimer Events


    private void OnSpawningTimerElasped()
    {
        //TODO SPAWN FISH
        Debug.LogWarning("Spawn Fish");
    }

    #endregion
    
    #region React To Fish Event

    private void OnFishCollected(PlayerEnum collector, int scoreToAdd)
    {
        SpawnedFishCollected?.Invoke(collector, scoreToAdd);
    }
    
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        
        Gizmos.DrawWireCube(_spawningZone.center, _spawningZone.size);
    }
}
