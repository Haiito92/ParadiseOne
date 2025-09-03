using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatControllerAbsolute : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;

    private Rigidbody2D _rb;
    private Vector2 _inputVector;
    private Vector2 _currentVelocity;

    public bool IsActive { get; set; }
    public float MaxSpeed;

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

        _currentVelocity += (Vector2)transform.up * (_moveSpeed * Time.fixedDeltaTime);

        if (_currentVelocity.magnitude > MaxSpeed)
            _currentVelocity = _currentVelocity.normalized * MaxSpeed;

        _rb.MovePosition(_rb.position + _currentVelocity * Time.fixedDeltaTime);

        if (_inputVector.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(_inputVector.y, _inputVector.x) * Mathf.Rad2Deg - 90f;
            float newAngle = Mathf.MoveTowardsAngle(_rb.rotation, targetAngle, _turnSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(newAngle);
        }
    }

    private void OnInputPerformed(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();
    }

    private void OnInputCanceled(InputAction.CallbackContext ctx)
    {
        _inputVector = Vector2.zero;
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
