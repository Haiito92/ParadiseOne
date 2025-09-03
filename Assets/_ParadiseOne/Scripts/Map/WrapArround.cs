using UnityEngine;

public class WrapAround : MonoBehaviour
{
    private Camera _camera;
    private float _minX, _maxX, _minY, _maxY;

    void Start()
    {
        _camera = Camera.main;
        UpdateBounds();
    }

    void Update()
    {
        UpdateBounds();
        CheckWrap();
    }

    void UpdateBounds()
    {
        Vector3 bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
        Vector3 topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, _camera.nearClipPlane));

        _minX = bottomLeft.x;
        _minY = bottomLeft.y;
        _maxX = topRight.x;
        _maxY = topRight.y;
    }

    void CheckWrap()
    {
        Vector3 pos = transform.position;

        if (pos.x < _minX || pos.x > _maxX || pos.y < _minY || pos.y > _maxY)
        {
            Vector3 camCenter = _camera.transform.position;
            Vector3 offset = pos - camCenter;
            transform.position = camCenter - offset;
        }
    }
}
