using UnityEngine;
using UnityEngine.InputSystem;

public class BoatControllerAbsolute : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private float _moveForce;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _turnSpeed;

    private Rigidbody2D _rb;
    private Vector2 _inputVector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _input.action.performed += OnInputEvent;
        _input.action.canceled += OnInputEvent;
    }

    private void FixedUpdate()
    {
        _rb.AddForce(transform.up * _moveForce);

        if (_rb.linearVelocity.magnitude > _maxSpeed)
            _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;

        if (_inputVector.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(_inputVector.y, _inputVector.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = Mathf.MoveTowardsAngle(_rb.rotation, targetAngle, _turnSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnInputEvent(InputAction.CallbackContext ctx)
    {
        _inputVector = ctx.ReadValue<Vector2>();
    }
}
