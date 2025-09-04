using UnityEngine;

public class BoatSpriteAnimator : MonoBehaviour
{
    [SerializeField] private Sprite _up;
    [SerializeField] private Sprite _down;
    [SerializeField] private Sprite _right;
    [SerializeField] private Sprite _left;
    [SerializeField] private Sprite _upRight;
    [SerializeField] private Sprite _upLeft;
    [SerializeField] private Sprite _downRight;
    [SerializeField] private Sprite _downLeft;

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetBoatSprite(Vector2 facedDirection)
    {
        facedDirection.Normalize();

        //if (facedDirection.x > 0)
        //    _spriteRenderer.sprite = _up;
        //else if (facedDirection.x < 0)
        //    _spriteRenderer.sprite = _down;
        //else if (facedDirection.y > 0)
        //    _spriteRenderer.sprite = _right;
        //else if (facedDirection.y < 0)
        //    _spriteRenderer.sprite = _left;
        //else if (facedDirection.x > 0 && facedDirection.y > 0)
        //    _spriteRenderer.sprite = _upRight;
        //else if (facedDirection.x > 0 && facedDirection.y < 0)
        //    _spriteRenderer.sprite = _upLeft;
        //else if (facedDirection.x < 0 && facedDirection.y > 0)
        //    _spriteRenderer.sprite = _downRight;
        //else if (facedDirection.x < 0 && facedDirection.y < 0)
        //    _spriteRenderer.sprite = _downLeft;


        if (facedDirection.x > 0)
        {
            if (facedDirection.y < 0)
            {
                _spriteRenderer.sprite = _downRight;

            }
            else if (facedDirection.y > 0)
            {
                _spriteRenderer.sprite = _upRight;
            } 
            else if (facedDirection.y == 0)
            {
                _spriteRenderer.sprite = _right;
            }
        }
        else if (facedDirection.x < 0)
        {
            if(facedDirection.y < 0)
            {
                _spriteRenderer.sprite = _downLeft;

            }
            else if(facedDirection.y > 0)
            {
                _spriteRenderer.sprite = _upLeft;
            }
            else if (facedDirection.y == 0)
            {
                _spriteRenderer.sprite = _left;
            }
        }
        else if (facedDirection.x == 0)
        {
            if (facedDirection.y < 0)
            {
                _spriteRenderer.sprite = _down;

            }
            else if (facedDirection.y > 0)
            {
                _spriteRenderer.sprite = _up;
            }
            else if (facedDirection.y == 0)
            {
                _spriteRenderer.sprite = _up;
            }
        }
    }
}
