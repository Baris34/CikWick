using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _backgroundPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private float _scaleDuration;
    
    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    private Image _backgroundImage;
    private void Awake()
    {
        _backgroundImage = _backgroundPanel.GetComponent<Image>();
        _settingsPanel.transform.localScale = Vector3.zero;
        _settingsButton.onClick.AddListener(OnClick_SettingsButton);
        _resumeButton.onClick.AddListener(OnClick_ResumeButton);
    }
    
    private void OnClick_SettingsButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        _settingsPanel.SetActive(true);
        _backgroundImage.gameObject.SetActive(true);
        
        _backgroundImage.DOFade(0.8f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPanel.transform.DOScale(1.5f, _scaleDuration).SetEase(Ease.OutBack);
    }

    private void OnClick_ResumeButton()
    {

        _backgroundImage.DOFade(0f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPanel.transform.DOScale(0f, _scaleDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            _backgroundPanel.SetActive(false);
            _settingsPanel.SetActive(false);
        });

    }
}
