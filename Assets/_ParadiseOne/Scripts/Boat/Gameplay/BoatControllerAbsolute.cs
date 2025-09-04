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

        _input.action.performed += OnInputPerformed;
        _input.action.canceled += OnInputCanceled;
    }

    private void OnDestroy()
    {
        _input.action.performed -= OnInputPerformed;
        _input.action.canceled -= OnInputCanceled;
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

    private void OnInputPerformed(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();

        if (_isInputInverted)
            _inputVector = -_inputVector;
    }

    private void OnInputCanceled(InputAction.CallbackContext ctx)
    {
        _inputVector = Vector2.zero;
    }
    public void SetInvertedInputs(bool inverted)
    {
        _isInputInverted = inverted;
    }

    //// FOR TESTS PURPOSES ONLY. DO NOT CALL THOSE FUNCTIONS FROM OTHER SCRIPTS OR EVEN IN THIS ONE. ////

    [Button]
    public void StartBoat()
    {
        IsActive = true;
    }

    [Button]
    public void StopBoat()
    {
        IsActive = false;
    }
}
