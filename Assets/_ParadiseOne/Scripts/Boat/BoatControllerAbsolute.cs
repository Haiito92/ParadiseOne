using UnityEngine;
using UnityEngine.InputSystem;

public class BoatControllerAbsolute : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _maxSpeed;

    private Rigidbody2D _rb;
    private Vector2 _inputVector;
    private Vector2 _currentVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input.action.performed += OnInputEvent;
        _input.action.canceled += OnInputEvent;
    }

    private void FixedUpdate()
    {
        _currentVelocity += (Vector2)transform.up * (_moveSpeed * Time.fixedDeltaTime);

        if (_currentVelocity.magnitude > _maxSpeed)
            _currentVelocity = _currentVelocity.normalized * _maxSpeed;

        _rb.MovePosition(_rb.position + _currentVelocity * Time.fixedDeltaTime);

        if (_inputVector.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(_inputVector.y, _inputVector.x) * Mathf.Rad2Deg - 90f;
            float newAngle = Mathf.MoveTowardsAngle(_rb.rotation, targetAngle, _turnSpeed * Time.fixedDeltaTime);
            _rb.MoveRotation(newAngle);
        }
    }

    private void OnInputEvent(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();
    }
}
