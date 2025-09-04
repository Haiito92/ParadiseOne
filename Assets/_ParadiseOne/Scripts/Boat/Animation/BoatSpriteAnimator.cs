using UnityEngine;

public class BoatSpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _upRight;
    [SerializeField] private Sprite _up;
    [SerializeField] private Sprite _upLeft;
    [SerializeField] private Sprite _left;
    [SerializeField] private Sprite _downLeft;
    [SerializeField] private Sprite _down;
    [SerializeField] private Sprite _downRight;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    public void SetBoatSprite(Vector2 velocity)
    {
        if (velocity.sqrMagnitude < 0.001f)
            return;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        int index = Mathf.RoundToInt(angle / 45f) % 8;

        switch (index)
        {
            case 0: _spriteRenderer.sprite = _right; break;
            case 1: _spriteRenderer.sprite = _upRight; break;
            case 2: _spriteRenderer.sprite = _up; break;
            case 3: _spriteRenderer.sprite = _upLeft; break;
            case 4: _spriteRenderer.sprite = _left; break;
            case 5: _spriteRenderer.sprite = _downLeft; break;
            case 6: _spriteRenderer.sprite = _down; break;
            case 7: _spriteRenderer.sprite = _downRight; break;
        }
    }
}
