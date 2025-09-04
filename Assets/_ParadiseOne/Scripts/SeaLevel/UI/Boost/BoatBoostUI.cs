using UnityEngine;
using UnityEngine.UI;

public class BoatBoostUI : MonoBehaviour
{
    [SerializeField] private BoatBoostController _boostController;
    [SerializeField] private Color _blockedBoostColor;

    private Image _boostGauge;

    private void Awake()
    {
        _boostGauge = GetComponent<Image>();
    }

    private void Update()
    {
        if (_boostController == null || _boostGauge == null)
            return;

        float ratio = _boostController.CurrentBoostRatio;
        _boostGauge.fillAmount = ratio;

        if (ratio < _boostController.MinGaugeToBoost)
            _boostGauge.color = _blockedBoostColor;
        else
            _boostGauge.color = Color.white;
    }
}
