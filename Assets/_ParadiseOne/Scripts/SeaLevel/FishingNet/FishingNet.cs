using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class FishingNet : MonoBehaviour
{
    [SerializeField] private PlayerEnum _controllingPlayer;
    [SerializeField] private Transform _gfxTransform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IFish fish))
        {
            fish.CollectFish(_controllingPlayer);

            transform.DOScale(1.5f, 0.1f).OnComplete(() =>
            {
                transform.DOScale(1f, 0.1f);
            });
        }
    }
}
