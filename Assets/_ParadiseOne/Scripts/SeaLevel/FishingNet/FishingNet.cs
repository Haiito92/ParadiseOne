using DG.Tweening;
using UnityEngine;

public class FishingNet : MonoBehaviour
{
    [SerializeField] private PlayerEnum _controllingPlayer; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IFish fish))
        {
            fish.CollectFish(_controllingPlayer);
        }
    }
}
