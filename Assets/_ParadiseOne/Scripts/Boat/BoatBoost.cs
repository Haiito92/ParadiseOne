using UnityEngine;
using UnityEngine.InputSystem;

public class BoatBoost : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private float _boostMaxSpeed;

    private BoatControllerAbsolute _boatControl;
    private float _baseMaxSpeed;
    private bool _isBoosting;

    private void Awake()
    {
        _boatControl = GetComponent<BoatControllerAbsolute>();
        _baseMaxSpeed = _boatControl.MaxSpeed;

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
        _boatControl.MaxSpeed = _isBoosting ? _boostMaxSpeed : _baseMaxSpeed;
    }

    private void OnInputPerformed(InputAction.CallbackContext ctx)
    {
        _isBoosting = true;
    }

    private void OnInputCanceled(InputAction.CallbackContext ctx)
    {
        _isBoosting = false;
    }
}
