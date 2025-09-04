using UnityEngine;

public class WrapAround : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _netTransform;
    [SerializeField] private float _marggin;

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

        bool isBeyondX = pos.x < _min.x && netPos.x < _min.x || pos.x > _max.x && netPos.x > _max.x;
        bool isBeyondY = pos.y < _min.y && netPos.y < _min.y || pos.y > _max.y && netPos.y > _max.y;

        if (isBeyondX || isBeyondY)
        {
            if (pos.x < _min.x) pos.x = _max.x;
            else if (pos.x > _max.x) pos.x = _min.x;

            if (pos.y < _min.y) pos.y = _max.y;
            else if (pos.y > _max.y) pos.y = _min.y;
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
    }
}
