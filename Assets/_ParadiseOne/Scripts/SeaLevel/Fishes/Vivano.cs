using System;
using UnityEngine;

public class Vivano : Fish
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _speed;
    private Vector2 _swimmingDirection;


    private void Start()
    {
        _swimmingDirection = _rigidbody.transform.right;
        _rigidbody.linearVelocity = _swimmingDirection * _speed * Time.fixedDeltaTime;
    }

    #region Fish/IFish Implementation
    public override void CollectFish(PlayerEnum collector)
    {
        Debug.Log("Collected Vivano");
        base.CollectFish(collector);
    }
    #endregion
    
}
