using System;
using UnityEngine;

public abstract class Fish : MonoBehaviour, IFish
{
    #region Properties
    [field: SerializeField] public int Score { get; private set; } = 10;
    #endregion

    #region Actions
    public event Action<PlayerEnum,int> FishCollected;
    #endregion
    
    public virtual void CollectFish(PlayerEnum collector)
    {
        FishCollected?.Invoke(collector, Score);
    }
}
