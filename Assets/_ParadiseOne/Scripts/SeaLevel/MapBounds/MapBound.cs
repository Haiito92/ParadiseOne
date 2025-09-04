using System;
using UnityEngine;

public class MapBound : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private void OnDrawGizmos()
    {
        if(_collider==null)return;
        
        Gizmos.color = Color.yellow;
        
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.bounds.size);
    }
}
