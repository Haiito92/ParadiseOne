using UnityEngine;
using UnityEngine.Serialization;

public class Vivano : Fish
{
    [Header("Vivano References")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SeaTimer _changeDirectionTimer;
    
    private float _speed;
    private float _changeDirectionInterval;
    private Vector2 _swimmingDirection;

    private void SetRandomDirection()
    {
        float randomX = Random.Range(-1, 1f);
        float randomY = Random.Range(-1, 1f);

        if (randomX == 0 && randomY == 0)
        {
            randomX = 1;
            randomY = 1;
        }

        Vector2 newDirection = new Vector2(randomX, randomY); 
        
        _swimmingDirection = newDirection.normalized;
        transform.up = _swimmingDirection;
        _rigidbody.linearVelocity = _swimmingDirection * _speed * Time.fixedDeltaTime;
    }
    
    private void OnChangeDirectionTimerElapsed()
    {
        SetRandomDirection();
    }

    #region Fish/IFish Implementation
    public override void CollectFish(PlayerEnum collector)
    {
        //Debug.Log("Collected Vivano");
        base.CollectFish(collector);
    }

    public override void InitFish(SeaDataSO seaDataSo)
    {
        base.InitFish(seaDataSo);

        _fishLifeTime = seaDataSo.VivanoLifeTime;
        Score = seaDataSo.VivanoScore;

        _speed = seaDataSo.VivanoSpeed;
        _changeDirectionInterval = seaDataSo.VivanoChangeDirectionInterval;
    }

    public override void StartFishLife()
    {
        base.StartFishLife();
        
        SetRandomDirection();
        
        _changeDirectionTimer.InitTimer(_changeDirectionInterval, true);
        _changeDirectionTimer.SeaTimerElapsed += OnChangeDirectionTimerElapsed;
        
        _changeDirectionTimer.StartTimer();
    }

    #endregion
    
}
