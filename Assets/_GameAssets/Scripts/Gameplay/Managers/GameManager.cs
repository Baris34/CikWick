using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int _maxEggCount = 5;
    
    private int _currentEggCount;
    public int MaxEggCount => _maxEggCount;
    private void Awake()
    {
        Instance = this;
    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        
        if (_currentEggCount == _maxEggCount)
        {
            //Todo: Win the game
            EggCollectUI.Instance.UpdateEggCompleted();
        }
        EggCollectUI.Instance.UpdateEggCountText(_currentEggCount,_maxEggCount);
    }
}
