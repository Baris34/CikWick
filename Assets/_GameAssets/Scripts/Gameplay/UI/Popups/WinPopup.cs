using System;
using _GameAssets.Scripts.Gameplay.Helpers;
using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;
    
    private void OnEnable()
    {
        BackgroundMusic.Instance.PlayBackgroundMusic(false);
        AudioManager.Instance.Play(SoundType.WinSound);

        _timerText.text = _timerUI.GetFinalTime();
        _oneMoreButton.onClick.AddListener(OneMoreButton_OnClick);
        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);
            TransitionManager.Instance.LoadLevel(Consts.GameScenes.MENU_SCENE);
        });
    }

    private void OneMoreButton_OnClick()
    {
        AudioManager.Instance.Play(SoundType.TransitionSound);
        TransitionManager.Instance.LoadLevel(Consts.GameScenes.GAME_SCENE);
    }
}
