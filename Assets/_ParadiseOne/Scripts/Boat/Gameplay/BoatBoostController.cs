using UnityEngine;
using UnityEngine.InputSystem;

public class BoatBoostController : MonoBehaviour
{
    [SerializeField] private InputActionReference _input;
    [SerializeField] private float _boostAcceleration;
    [SerializeField] private float _boostMaxSpeed;
    [SerializeField] private float _boostTurnSpeed;
    [SerializeField] private float _maxDurationBoost;
    [SerializeField] private float _rechargeRate;
    [SerializeField, Range(0f, 1f)] private float _minGaugeToBoost;

    private BoatControllerAbsolute _boatControl;
    private float _baseAcceleration;
    private float _baseMaxSpeed;
    private float _baseTurnSpeed;
    private float _currentBoostGauge;
    private bool _isBoosting;
    private bool _infiniteBoost;

    public float CurrentBoostRatio => _currentBoostGauge / _maxDurationBoost;
    public float MinGaugeToBoost => _minGaugeToBoost;

    private void Awake()
    {
        _boatControl = GetComponent<BoatControllerAbsolute>();
        _baseAcceleration = _boatControl.Acceleration;
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

        _boatControl.Acceleration = _isBoosting ? _boostAcceleration : _baseAcceleration;
        _boatControl.MaxSpeed = _isBoosting ? _boostMaxSpeed : _baseMaxSpeed;
        _boatControl.TurnSpeed = _isBoosting ? _boostTurnSpeed : _baseTurnSpeed;
    }

    private void HandleBoost()
    {
        if (_isBoosting)
        {
            if (!_infiniteBoost)
            {
                _currentBoostGauge -= Time.fixedDeltaTime;
                if (_currentBoostGauge <= 0f)
                {
                    _currentBoostGauge = 0f;
                    _isBoosting = false;
                }
            }
        }
        else
        {
            if (!_infiniteBoost)
            {
                if (_currentBoostGauge < _maxDurationBoost)
                    _currentBoostGauge += _rechargeRate * Time.fixedDeltaTime;

                if (_currentBoostGauge > _maxDurationBoost)
                    _currentBoostGauge = _maxDurationBoost;
            }
        }
    }

    private void OnInputPerformed(InputAction.CallbackContext ctx)
    {
        if (!_isBoosting && (_currentBoostGauge >= _maxDurationBoost * _minGaugeToBoost || _infiniteBoost))
        {
            _isBoosting = true;
        }
    }

    private void OnInputCanceled(InputAction.CallbackContext ctx)
    {
        if (!_infiniteBoost)
            _isBoosting = false;
    }

    public void SetInfiniteBoost(bool infinite)
    {
        _infiniteBoost = infinite;

        if (_infiniteBoost)
        {
            _currentBoostGauge = _maxDurationBoost;
            _isBoosting = true;
        }
    }
}
