using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Action<GameState> OnGameStateChanged;

    [SerializeField] private int _maxEggCount = 5;
    
    private GameState _currentGameState;
    private int _currentEggCount;
    private void Awake()
    {
        Instance = this;
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
        }
        EggCollectUI.Instance.UpdateEggCountText(_currentEggCount,_maxEggCount);
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
