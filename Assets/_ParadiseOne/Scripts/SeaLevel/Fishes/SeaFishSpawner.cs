using System;
using System.Collections.Generic;
using UnityEngine;

public class SeaFishSpawner : MonoBehaviour
{
    #region Fields
    [SerializeField] private List<Transform> _fishSpawnPoints;
    [SerializeField] private List<Fish> _fishes;
    #endregion

    private void Start()
    {
        foreach (Fish fish in _fishes)
        {
            fish.FishCollected += OnFishCollected;
        }
    }

    #region Actions

    public event Action<PlayerEnum, int> SpawnedFishCollected;

    #endregion

    #region React To Fish Event

    private void OnFishCollected(PlayerEnum collector, int scoreToAdd)
    {
        SpawnedFishCollected?.Invoke(collector, scoreToAdd);
    }
    
    #endregion
}
