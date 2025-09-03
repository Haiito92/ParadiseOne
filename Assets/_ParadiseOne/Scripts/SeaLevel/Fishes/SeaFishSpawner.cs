using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SeaFishSpawner : MonoBehaviour
{
    #region Fields
    
    private List<Fish> _fishes = new List<Fish>();

    //Spawning
    private Camera _seaLevelCamera;
    private Rect _spawningZone;

    [SerializeField] private SeaTimer _spawningTimer;
    [SerializeField] private float _spawningInterval = 5;
    [SerializeField] private int _maxNumberOfFish;

    [SerializeField] private GameObject _oursinPrefab;
    #endregion

    #region Actions

    public event Action<PlayerEnum, int> SpawnedFishCollected;

    #endregion

    #region Spawner LifeCycle

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
        _spawningTimer.StopTimer();
        
        for (int i = 0; i < _fishes.Count; )
        {
            _fishes[i].KillFish();
        }
    }
    #endregion

    #region SpawnManagement

    

    private void SpawnFish()
    {
        if(_fishes.Count >= _maxNumberOfFish) return;
        
        //Debug.LogWarning("SpawnFish");
        
        //Find random point on map
        float minX = _spawningZone.center.x - _spawningZone.size.x / 2;
        float maxX = _spawningZone.center.x + _spawningZone.size.x / 2;
        float spawningX = Random.Range(minX, maxX);
        
        float minY = _spawningZone.center.y - _spawningZone.size.y / 2;
        float maxY = _spawningZone.center.y + _spawningZone.size.y / 2;
        float spawningY = Random.Range(minY, maxY);

        Vector2 spawningPosition = new Vector2(spawningX, spawningY);
        
        //Spawn Fish (first with prefab)

        GameObject fishGO = GameObject.Instantiate(_oursinPrefab, spawningPosition, this.gameObject.transform.rotation, this.transform);
        Fish fish = fishGO.GetComponent<Fish>();

        if (fish == null)
        {
            Debug.LogError("Fish object spawned doesn't have a Fish component");
        }

        AddFish(fish);
    }

    private void AddFish(Fish fish)
    {
        _fishes.Add(fish);
        fish.FishDied += OnFishDied;
    }

    private void RemoveFish(Fish fish)
    {
        fish.FishDied -= OnFishDied;
        _fishes.Remove(fish);
    }
    #endregion

    
    #region React To SpawningTimer Events


    private void OnSpawningTimerElasped()
    {
        SpawnFish();
    }
    #endregion
    
    #region React To Fish Event

    private void OnFishCollected(PlayerEnum collector, int scoreToAdd)
    {
        SpawnedFishCollected?.Invoke(collector, scoreToAdd);
    }

    private void OnFishDied(Fish deadFish)
    {
        RemoveFish(deadFish);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        
        Gizmos.DrawWireCube(_spawningZone.center, _spawningZone.size);
    }
}
