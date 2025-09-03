using System;
using UnityEngine;

public abstract class Fish : MonoBehaviour, IFish
{
    #region Properties
    [Header("Fish References")]
    [SerializeField] private SeaTimer _fishLifeTimer;
    
    [field:Header("Fish Stats")]

    [field: SerializeField] public int Score { get; private set; } = 10;
    [SerializeField] private float _fishLifeTime = 10;
    
    #endregion

    #region Actions
    public event Action<PlayerEnum,int> FishCollected;
    public event Action<Fish> FishDied; //Passes self reference (more easy, I don't want to do index or whatever)
    #endregion
    
    public virtual void CollectFish(PlayerEnum collector)
    {
        FishCollected?.Invoke(collector, Score);
        
        KillFish();
    }

    public virtual void StartFishLife()
    {
        _fishLifeTimer.InitTimer(_fishLifeTime);
        _fishLifeTimer.SeaTimerElapsed += OnFishLifeTimerElapsed;
        
        
        _fishLifeTimer.StartTimer();
    }

    public void KillFish()
    {
        FishDied?.Invoke(this);
        
        Destroy(this.gameObject);
    }

    private void OnFishLifeTimerElapsed()
    {
        KillFish();
    }
}
