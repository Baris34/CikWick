using _GameAssets.Scripts.Gameplay.Helpers;
using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _QuitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.GameScenes.GAME_SCENE);
        });
        
        _QuitButton.onClick.AddListener(() =>
        {
            Debug.Log("Quit Game");
            Application.Quit();
        });
    }
}
