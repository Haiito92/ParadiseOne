using UnityEngine;

public class WrapAround : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _netTransform;
    [SerializeField] private float _marggin;
    [Range(0f, 1f)]
    [SerializeField] private float _cornerZoneRatioX;
    [Range(0f, 1f)]
    [SerializeField] private float _cornerZoneRatioY;

    private Vector2 _min;
    private Vector2 _max;

    private Vector2 center => (_min + _max) * 0.5f;

    private void Start()
    {
        UpdateBounds();
    }

    private void UpdateBounds()
    {
        if (_camera == null) return;

        float camHeight = _camera.orthographicSize * 2f;
        float camWidth = camHeight * _camera.aspect;

        Vector3 camPos = _camera.transform.position;

        _min = new Vector2(camPos.x - camWidth / 2f - _marggin, camPos.y - camHeight / 2f - _marggin);
        _max = new Vector2(camPos.x + camWidth / 2f + _marggin, camPos.y + camHeight / 2f + _marggin);
    }

    private void Update()
    {
        if (_netTransform == null || _camera == null) return;

        Vector3 pos = transform.position;
        Vector3 netPos = _netTransform.position;
        Vector2 c = center;

        float cornerWidth = (_max.x - _min.x) * _cornerZoneRatioX;
        float cornerHeight = (_max.y - _min.y) * _cornerZoneRatioY;

        bool isBeyondX = pos.x < _min.x && netPos.x < _min.x || pos.x > _max.x && netPos.x > _max.x;
        bool isBeyondY = pos.y < _min.y && netPos.y < _min.y || pos.y > _max.y && netPos.y > _max.y;

        bool isCornerX = (pos.x < _min.x + cornerWidth) || (pos.x > _max.x - cornerWidth);
        bool isCornerY = (pos.y < _min.y + cornerHeight) || (pos.y > _max.y - cornerHeight);

        if (isBeyondX || isBeyondY)
        {
            if (pos.x < _min.x) pos.x = _max.x;
            else if (pos.x > _max.x) pos.x = _min.x;

            if (pos.y < _min.y) pos.y = _max.y;
            else if (pos.y > _max.y) pos.y = _min.y;
        }
        else if (isCornerX && isCornerY && (isBeyondX || isBeyondY))
        {
            pos = new Vector2(2 * c.x - pos.x, 2 * c.y - pos.y);
        }

        transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        if (_camera == null) return;

        UpdateBounds();

        Gizmos.color = Color.red;
        Vector3 c = new Vector3(center.x, center.y, 0f);
        Vector3 size = new Vector3(_max.x - _min.x, _max.y - _min.y, 0f);
        Gizmos.DrawWireCube(c, size);

        float cornerWidth = (_max.x - _min.x) * _cornerZoneRatioX;
        float cornerHeight = (_max.y - _min.y) * _cornerZoneRatioY;
        Gizmos.color = Color.green;

        Vector3 bottomLeft = new Vector3(_min.x + cornerWidth * 0.5f, _min.y + cornerHeight * 0.5f, 0f);
        Vector3 bottomRight = new Vector3(_max.x - cornerWidth * 0.5f, _min.y + cornerHeight * 0.5f, 0f);
        Vector3 topLeft = new Vector3(_min.x + cornerWidth * 0.5f, _max.y - cornerHeight * 0.5f, 0f);
        Vector3 topRight = new Vector3(_max.x - cornerWidth * 0.5f, _max.y - cornerHeight * 0.5f, 0f);

        Vector3 cornerSize = new Vector3(cornerWidth, cornerHeight, 0f);

        Gizmos.DrawWireCube(bottomLeft, cornerSize);
        Gizmos.DrawWireCube(bottomRight, cornerSize);
        Gizmos.DrawWireCube(topLeft, cornerSize);
        Gizmos.DrawWireCube(topRight, cornerSize);
    }
}
