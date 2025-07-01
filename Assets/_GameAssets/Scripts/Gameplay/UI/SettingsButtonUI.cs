using System;
using _GameAssets.Scripts.Gameplay.Helpers;
using DG.Tweening;
using MaskTransitions;
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

    [Header("Sprites")]
    [SerializeField] private Sprite _musicActiveSprite;
    [SerializeField] private Sprite _musicPassiveSprite;
    [SerializeField] private Sprite _soundActiveSprite;
    [SerializeField] private Sprite _soundPassiveSprite;
    
    private bool _isMusicActive = true;
    private bool _isSoundActive = true;
    
    private Image _backgroundImage;
    private void Awake()
    {
        _backgroundImage = _backgroundPanel.GetComponent<Image>();
        _settingsPanel.transform.localScale = Vector3.zero;
        _settingsButton.onClick.AddListener(OnClick_SettingsButton);
        _resumeButton.onClick.AddListener(OnClick_ResumeButton);
        _soundButton.onClick.AddListener(OnClick_SoundButton);
        _musicButton.onClick.AddListener(OnClick_MusicButton);
        _mainMenuButton.onClick.AddListener((() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.GameScenes.MENU_SCENE);
        }));
    }

    private void OnClick_MusicButton()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isMusicActive = !_isMusicActive;
        _musicButton.image.sprite = _isMusicActive ? _musicActiveSprite : _musicPassiveSprite;
        BackgroundMusic.Instance.SetMusicMute(!_isMusicActive);
    }

    private void OnClick_SoundButton()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isSoundActive =! _isSoundActive;
        _soundButton.image.sprite = _isSoundActive ? _soundActiveSprite : _soundPassiveSprite;
        AudioManager.Instance.SetSoundEffectsMute(!_isSoundActive);
    }

    private void OnClick_SettingsButton()
    {
        GameManager.Instance.ChangeGameState(GameState.Pause);
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _settingsPanel.SetActive(true);
        _backgroundImage.gameObject.SetActive(true);
        
        _backgroundImage.DOFade(0.8f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPanel.transform.DOScale(1.5f, _scaleDuration).SetEase(Ease.OutBack);
    }

    private void OnClick_ResumeButton()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _backgroundImage.DOFade(0f, _scaleDuration).SetEase(Ease.Linear);
        _settingsPanel.transform.DOScale(0f, _scaleDuration).SetEase(Ease.OutExpo).OnComplete(() =>
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);
            _backgroundPanel.SetActive(false);
            _settingsPanel.SetActive(false);
        });

    }
}
