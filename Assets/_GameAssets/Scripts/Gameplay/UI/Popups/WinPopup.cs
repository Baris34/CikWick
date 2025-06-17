using System;
using _GameAssets.Scripts.Gameplay.Helpers;
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
        _timerText.text = _timerUI.GetFinalTime();
        _oneMoreButton.onClick.AddListener(OneMoreButton_OnClick);
    }

    private void OneMoreButton_OnClick()
    {
        SceneManager.LoadScene(Consts.GameScenes.GAME_SCENE);
    }
}
