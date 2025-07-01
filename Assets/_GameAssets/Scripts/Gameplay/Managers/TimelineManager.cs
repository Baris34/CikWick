using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    private PlayableDirector _playableDirector;

    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        _playableDirector.Play();
        _playableDirector.stopped += OnTimelineStopped;
    }

    private void OnTimelineStopped(PlayableDirector obj)
    {
        _gameManager.ChangeGameState(GameState.Play);
    }
}
