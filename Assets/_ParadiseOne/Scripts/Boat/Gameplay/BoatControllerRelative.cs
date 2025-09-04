using UnityEngine;
using UnityEngine.InputSystem;

public class BoatControllerRelative : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _turnSpeed;

    private Rigidbody2D _rb;
    private float _inputDirection;

    public bool IsActive { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input.action.performed += OnInputEvent;
        _input.action.canceled += OnInputEvent;
    }

    private void FixedUpdate()
    {
        if (!IsActive)
            return;

        _rb.AddForce(transform.up * _moveForce);

        if (_rb.linearVelocity.magnitude > _maxSpeed)
            _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;

        if (_inputDirection != 0f)
        {
            float rotation = -_inputDirection * _turnSpeed * Time.fixedDeltaTime;
            _rb.MoveRotation(_rb.rotation + rotation);
        }
    }

    private void OnInputEvent(InputAction.CallbackContext ctx)
    {
        Vector2 inputVector = ctx.ReadValue<Vector2>();
        _inputDirection = inputVector.x;
    }
}
