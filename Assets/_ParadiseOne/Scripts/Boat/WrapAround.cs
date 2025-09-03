using UnityEngine;

public class WrapAround : MonoBehaviour
{
    [SerializeField] private float _minX, _maxX, _minY, _maxY;
    [Range(0f, 1f)]
    [SerializeField] private float _wrapTowardsCenter;

    private Vector2 center => new Vector2((_minX + _maxX) * 0.5f, (_minY + _maxY) * 0.5f);

    private void Update()
    {
        Vector3 pos = transform.position;
        Vector2 c = center;

        if (pos.x < _minX || pos.x > _maxX || pos.y < _minY || pos.y > _maxY)
        {
            Vector2 symPos = new Vector2(2 * c.x - pos.x, 2 * c.y - pos.y);

            pos.x = Mathf.Lerp(symPos.x, c.x, _wrapTowardsCenter);
            pos.y = Mathf.Lerp(symPos.y, c.y, _wrapTowardsCenter);

            transform.position = pos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 c = new Vector3((_minX + _maxX) * 0.5f, (_minY + _maxY) * 0.5f, 0f);
        Vector3 size = new Vector3(_maxX - _minX, _maxY - _minY, 0f);
        Gizmos.DrawWireCube(c, size);
    }
}
