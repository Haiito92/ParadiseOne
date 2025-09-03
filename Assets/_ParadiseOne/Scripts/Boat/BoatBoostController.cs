using UnityEngine;
using UnityEngine.InputSystem;

public class BoatBoostController : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private float _boostMaxSpeed;
    [SerializeField] private float _boostTurnSpeed;
    [SerializeField] private float _maxDurationBoost;
    [SerializeField] private float _rechargeRate;
    [SerializeField, Range(0f, 1f)] private float _minGaugeToBoost;

    private BoatControllerAbsolute _boatControl;
    private float _baseMaxSpeed;
    private float _baseTurnSpeed;
    private float _currentBoostGauge;
    private bool _isBoosting;

    private void Awake()
    {
        _boatControl = GetComponent<BoatControllerAbsolute>();
        _baseMaxSpeed = _boatControl.MaxSpeed;
        _baseTurnSpeed = _boatControl.TurnSpeed;

        _currentBoostGauge = _maxDurationBoost;

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
        HandleBoost();

        _boatControl.MaxSpeed = _isBoosting ? _boostMaxSpeed : _baseMaxSpeed;
        _boatControl.TurnSpeed = _isBoosting ? _boostTurnSpeed : _baseTurnSpeed;
    }

    private void HandleBoost()
    {
        if (_isBoosting && _currentBoostGauge > 0f)
        {
            _currentBoostGauge -= Time.fixedDeltaTime;
            if (_currentBoostGauge <= 0f)
            {
                _currentBoostGauge = 0f;
                _isBoosting = false;
            }
        }
        else
        {
            if (_currentBoostGauge < _maxDurationBoost)
                _currentBoostGauge += _rechargeRate * Time.fixedDeltaTime;

            if (_currentBoostGauge > _maxDurationBoost)
                _currentBoostGauge = _maxDurationBoost;
        }
    }

    private void OnInputPerformed(InputAction.CallbackContext ctx)
    {
        if (!_isBoosting && _currentBoostGauge >= _maxDurationBoost * _minGaugeToBoost)
        {
            _isBoosting = true;
        }
    }

    private void OnInputCanceled(InputAction.CallbackContext ctx)
    {
        _isBoosting = false;
    }
}
