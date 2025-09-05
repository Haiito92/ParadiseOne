using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

public class VignetteAnimator : MonoBehaviour
{
    [SerializeField] private Volume _vignetteVolume;
    [FormerlySerializedAs("_animationSpeed")] [SerializeField] private float _animationLength = 1f;
    
    private Vignette _vignette;

    private Coroutine _vignetteAnimCoroutine;
    
    void Start()
    {
        if (_vignetteVolume.profile.TryGet(out _vignette))
        {
            Debug.LogWarning("Found Vignette");
        }
    }

    public void StartVignetteAnim()
    {
        if(_vignette == null ) return;

        StopVignetteAnim();
        
        _vignetteAnimCoroutine = StartCoroutine(VignetteAnim());
    }

    private IEnumerator VignetteAnim()
    {
        float halfLength = _animationLength/2f;
        
        while (_vignette.intensity.value < 0.25f)
        {
            _vignette.intensity.value = Math.Min(_vignette.intensity.value + (halfLength * Time.deltaTime), 0.25f);
            yield return null;
        }

        _vignette.intensity.value = 0.25f;
        
        while (_vignette.intensity.value > 0.0f)
        {
            _vignette.intensity.value = Math.Max(_vignette.intensity.value - (halfLength * Time.deltaTime), 0.0f);
            yield return null;
        }
    }
    
    private void StopVignetteAnim()
    {
        if (_vignetteAnimCoroutine == null) return;
        
        StopCoroutine(_vignetteAnimCoroutine);
        _vignetteAnimCoroutine = null;
    }
}
