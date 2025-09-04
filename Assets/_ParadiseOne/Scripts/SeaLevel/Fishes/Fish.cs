using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fish : MonoBehaviour, IFish
{
    #region Properties

    [Header("Fish References")] 
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SeaTimer _fishLifeTimer;
    
    public int Score { get; protected set; } = 10;
    protected float _fishLifeTime = 10;

    protected float _originalSpeed;
    protected float _speed;
    
    protected float _originalScale;
    protected float _scale;
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

    #region Fish LifeCycle
    public virtual void InitFish(SeaDataSO seaDataSo)
    {
        _originalScale = transform.localScale.x;
        _scale = _originalScale;
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
    #endregion

    #region Fish Size

    public void BeBig(float scaleMultiplier)
    {
        _scale = _originalScale * scaleMultiplier;
        Vector3 newScaleVector = new Vector3(_scale, _scale, _scale);
        transform.localScale = newScaleVector;
    }

    public void BeSmall()
    {
        _scale = _originalScale;
        Vector3 newScaleVector = new Vector3(_scale, _scale, _scale);
        transform.localScale = newScaleVector;
    }
    #endregion

    #region Fish Speed

    public virtual void BeFast(float speedMultiplier)
    {
        _speed = _originalSpeed * speedMultiplier;
    }

    public virtual void BeSlow()
    {
        _speed = _originalSpeed;
    }
    #endregion

    #region Fish Visibility

    public void BeVisible()
    {
        Color newColor = _spriteRenderer.color;
        newColor.a = 1f;
        _spriteRenderer.color = newColor;
    }

    public void BeNotVisible(float alpha)
    {
        Color newColor = _spriteRenderer.color;
        newColor.a = alpha;
        _spriteRenderer.color = newColor;
    }
    #endregion
}
