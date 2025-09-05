using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class FishingNet : MonoBehaviour
{
    [SerializeField] private PlayerEnum _controllingPlayer;
    [SerializeField] private Transform _gfxTransform;

    private TweenerCore<Vector3, Vector3, VectorOptions> _oldTweenTo;
    private TweenerCore<Vector3, Vector3, VectorOptions> _oldTweenFrom;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IFish fish))
        {
            fish.CollectFish(_controllingPlayer);

            if(_oldTweenTo != null)
            {
                _oldTweenTo.Kill();
                _oldTweenTo = null;
            }
            if(_oldTweenFrom != null)
            {
                _oldTweenFrom.Kill();
                _oldTweenFrom = null;
            }
            
            _oldTweenTo = transform.DOScale(1.5f, 0.1f).OnComplete(() =>
            {
                _oldTweenFrom = transform.DOScale(1f, 0.1f);
            });
        }
    }
}
