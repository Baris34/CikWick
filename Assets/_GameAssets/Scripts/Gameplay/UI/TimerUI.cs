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

    private bool _isTimerRunning;
    private Tween _rotateTween;

    private string _finalTime;
    private void Start()
    {
        RotateTimer();
        StartTimer();

        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Pause:
                StopTimer();
                break;
            case GameState.Resume:
                ResumeTimer();
                break;
            case GameState.GameOver:
                FinishTimer();
                break;
        }
    }

    private void RotateTimer()
    {
        _rotateTween = _timerRotatableTransform.DORotate(new Vector3(0, 0, -360f), _rotateDuration, RotateMode.FastBeyond360)
            .SetEase(_easeMode).SetLoops(-1, LoopType.Restart);
    }

    public void StartTimer()
    {
        _isTimerRunning = true;
        _elapsedTime = 0f;
        InvokeRepeating(nameof(UpdateTimerTextUI), 0f, 1f);
    }

    private void StopTimer()
    {
        _isTimerRunning = false;
        CancelInvoke(nameof(UpdateTimerTextUI));
        _rotateTween.Pause();

    }

    private void ResumeTimer()
    {
        _isTimerRunning = true;
        InvokeRepeating(nameof(UpdateTimerTextUI), 0f, 1f);
        _rotateTween.Play();
    }

    private void FinishTimer()
    {
        StopTimer();
        _finalTime = GetFormattedTime();
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void UpdateTimerTextUI()
    {
        if (!_isTimerRunning) return;
        _elapsedTime += 1;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public string GetFinalTime()
    {
        return _finalTime;
    }
}
