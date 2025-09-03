using System;
using UnityEngine;

public class Fish : MonoBehaviour, IFish
{
    #region Properties
    [field: SerializeField] public int Score { get; private set; } = 10;
    #endregion

    #region Actions
    public event Action<PlayerEnum,int> FishCollected;
    #endregion
    
    public void CollectFish(PlayerEnum collector)
    {
        FishCollected?.Invoke(collector, Score);
    }
}
