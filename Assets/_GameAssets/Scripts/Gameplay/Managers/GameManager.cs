using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private int _maxEggCount = 5;
    [SerializeField] private HealthManager _healthManager;
    [Header("Settings")]
    [SerializeField] private float _delay = 1f;
    
    private GameState _currentGameState;
    private int _currentEggCount;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _healthManager.OnGameOver += HealthManager_OnGameOver;
    }

    private void HealthManager_OnGameOver()
    {
        OnGameOver();
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }

    public void ChangeGameState(GameState newGameState)
    {
        OnGameStateChanged?.Invoke(newGameState);
        _currentGameState = newGameState;
        Debug.Log("Game state changed to: " + _currentGameState);
    }
    public void OnEggCollected()
    {
        _currentEggCount++;
        
        if (_currentEggCount == _maxEggCount)
        {
            //Todo: Win the game
            EggCollectUI.Instance.UpdateEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
        EggCollectUI.Instance.UpdateEggCountText(_currentEggCount,_maxEggCount);
    }

    IEnumerator OnGameLose()
    {
        yield return new WaitForSeconds(_delay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }

    public void OnGameOver()
    {
        StartCoroutine(OnGameLose());
    }
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
