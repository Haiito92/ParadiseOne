using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatControllerAbsolute : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private BoatSpriteAnimator _animator;

    private Rigidbody2D _rb;
    private Vector2 _inputVector;
    private Vector2 _currentDirrection;
    private bool _isInputInverted;

    public bool IsActive { get; set; }
    public float Acceleration;
    public float MaxSpeed;
    public float TurnSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input.action.performed += InputPerformed;
        _input.action.canceled += InputCanceled;
    }

    private void OnDestroy()
    {
        _input.action.performed -= InputPerformed;
        _input.action.canceled -= InputCanceled;
    }

    private void FixedUpdate()
    {
        if (!IsActive)
            return;

        _currentDirrection += (Vector2)transform.up * (Acceleration * Time.fixedDeltaTime);

        if (_currentDirrection.magnitude > MaxSpeed)
            _currentDirrection = _currentDirrection.normalized * MaxSpeed;

        _rb.MovePosition(_rb.position + _currentDirrection * Time.fixedDeltaTime);

        if (_inputVector.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(_inputVector.y, _inputVector.x) * Mathf.Rad2Deg - 90f;
            float newAngle = Mathf.MoveTowardsAngle(_rb.rotation, targetAngle, TurnSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(newAngle);
        }
    }

    private void Update()
    {
        _animator.SetBoatSprite(_currentDirrection);
    }

    private void InputPerformed(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();

        if (_isInputInverted)
            _inputVector = -_inputVector;
    }

    private void InputCanceled(InputAction.CallbackContext ctx)
    {
        _inputVector = Vector2.zero;
    }

    public void StartBoat()
    {
        IsActive = true;
    }

    public void StopBoat()
    {
        IsActive = false;
    }

    public void InvertedInputsOn()
    {
        _isInputInverted = true;
    }

    public void InvertedInputsOff()
    {
        _isInputInverted = false;
    }
}
