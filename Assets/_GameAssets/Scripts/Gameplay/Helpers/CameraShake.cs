using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
    private float _shakeTimer;
    private float _totalShakeDuration;
    private float _startingIntensity;
    
    private void Awake()
    {
        Instance = this;
        _cinemachineBasicMultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime;
            if (_shakeTimer <= 0f)
            {
                _cinemachineBasicMultiChannelPerlin.AmplitudeGain = Mathf.Lerp(_startingIntensity,0f, 1 - (_shakeTimer / _totalShakeDuration));
            }
        }
    }
    IEnumerator ShakeCameraCouroutine(float intensity, float duration,float delay)
    {
        yield return new WaitForSeconds(delay);
        _cinemachineBasicMultiChannelPerlin.AmplitudeGain = intensity;
        _shakeTimer = duration;
        _totalShakeDuration = duration;
        _startingIntensity = intensity;
    }

    public void ShakeCamera(float intensity, float duration, float delay = 0f)
    {
        StartCoroutine(ShakeCameraCouroutine(intensity, duration, delay));
    }

}
