using UnityEngine;

public class Vivano : Fish
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public override void CollectFish(PlayerEnum collector)
    {
        Debug.Log("Collected Vivano");
        base.CollectFish(collector);
        
    }
}
