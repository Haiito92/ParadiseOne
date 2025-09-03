using UnityEngine;

public class Vivano : Fish
{
    [Header("Vivano References")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SeaTimer _changeDirectionTimer;
    
    [Header("Vivano Stats")]
    [SerializeField] private float _speed;

    [SerializeField, Tooltip("In seconds")] private float _changeDirectionTime;
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

    public override void StartFishLife()
    {
        base.StartFishLife();
        
        SetRandomDirection();
        
        _changeDirectionTimer.InitTimer(_changeDirectionTime, true);
        _changeDirectionTimer.SeaTimerElapsed += OnChangeDirectionTimerElapsed;
        
        _changeDirectionTimer.StartTimer();
    }

    #endregion
    
}
