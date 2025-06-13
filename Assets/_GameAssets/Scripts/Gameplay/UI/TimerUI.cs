using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatableTransform;
    [SerializeField] private TMP_Text _timerText;
    
    [Header("Settings")]
    [SerializeField] private float _rotateDuration;
    [SerializeField] private Ease _easeMode;

    private float _elapsedTime;
    private void Start()
    {
        RotateTimer();
        InvokeRepeating(nameof(UpdateTimerTextUI),0f,1f);
    }

    private void RotateTimer()
    {
        _timerRotatableTransform.DORotate(new Vector3(0, 0, -360f), _rotateDuration, RotateMode.FastBeyond360)
            .SetEase(_easeMode).SetLoops(-1, LoopType.Restart);
    }

    public void StartTimer()
    {
        _elapsedTime = 0f;
        UpdateTimerTextUI();
    }

    public void UpdateTimerTextUI()
    {
        _elapsedTime += 1;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
