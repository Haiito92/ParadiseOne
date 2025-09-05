using System;
using System.Collections.Generic;
using DG.Tweening;
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

    protected float _originalOpacity;
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

        _originalOpacity = seaDataSo.FishBaseOpacity;
        BeVisible(false);
    }
    
    public virtual void StartFishLife()
    {
        _fishLifeTimer.InitTimer(_fishLifeTime);
        _fishLifeTimer.SeaTimerElapsed += OnFishLifeTimerElapsed;
        
        
        _fishLifeTimer.StartTimer();
        
        Color newColor = _spriteRenderer.color;
        newColor.a = 0;
        _spriteRenderer.color = newColor;

        newColor.a = _originalOpacity;
        _spriteRenderer.DOColor(newColor, 0.2f);
    }

    public void KillFish()
    {
        FishDied?.Invoke(this);
        
        Color newColor = _spriteRenderer.color;
        newColor.a = 0;
        
        _spriteRenderer.DOColor(newColor, 0.2f).OnComplete(() =>
        {
            Destroy(this.gameObject);

        });
    }

    private void OnFishLifeTimerElapsed()
    {
        KillFish();
    }
    #endregion

    #region Fish Size

    public void BeBig(float scaleMultiplier, bool animate = true)
    {
        _scale = _originalScale * scaleMultiplier;
        Vector3 newScaleVector = new Vector3(_scale, _scale, _scale);

        if (animate)
        {
            transform.DOScale(newScaleVector, 0.4f);
        }
        else
        {
            transform.localScale = newScaleVector;
        }
    }

    public void BeSmall(bool animate = true)
    {
        _scale = _originalScale;
        Vector3 newScaleVector = new Vector3(_scale, _scale, _scale);
        if (animate)
        {
            transform.DOScale(newScaleVector, 0.4f);
        }
        else
        {
            transform.localScale = newScaleVector;
        }
    }
    #endregion

    #region Fish Speed

    public virtual void BeFast(float speedMultiplier, bool animate = true)
    {
        _speed = _originalSpeed * speedMultiplier;
    }

    public virtual void BeSlow(bool animate = true)
    {
        _speed = _originalSpeed;
    }
    #endregion

    #region Fish Visibility

    public void BeVisible(bool animate = true)
    {
        Color newColor = _spriteRenderer.color;
        newColor.a = _originalOpacity;

        if (animate)
        {
            _spriteRenderer.DOColor(newColor, 0.4f);
        }
        else
        {
            _spriteRenderer.color = newColor;
        }
    }

    public void BeNotVisible(float alpha, bool animate = true)
    {
        Color newColor = _spriteRenderer.color;
        newColor.a = alpha;
        
        if (animate)
        {
            _spriteRenderer.DOColor(newColor, 0.4f);
        }
        else
        {
            _spriteRenderer.color = newColor;
        }
    }
    #endregion
}
