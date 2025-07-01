using _GameAssets.Scripts.Gameplay.Helpers;
using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private TMP_Text _timerText; 
    [SerializeField] private Button _TryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    
    private void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _TryAgainButton.onClick.AddListener(TryAgainButton_OnClick);
        _mainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.GameScenes.MENU_SCENE);
        });
    }

    private void TryAgainButton_OnClick()
    {
        TransitionManager.Instance.LoadLevel(Consts.GameScenes.GAME_SCENE);
    }
}
