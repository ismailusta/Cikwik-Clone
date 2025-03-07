using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private int _maxEgg = 5;
    [SerializeField] private EggCounterUI _eggCounterUI;
    private GameState _currentGameState;
    private int _currentEgg;

    void Awake()
    {
        Instance = this;

    }
    void OnEnable()
    {
        _currentGameState = GameState.Play;
        Debug.Log("Game State: " + _currentGameState);
    }
    public void OnEggCollected()
    {
        _currentEgg++;
        _eggCounterUI.SetEggCount(_currentEgg, _maxEgg);
        if (_currentEgg == _maxEgg)
        {
            _eggCounterUI.SettEggCompleted();
        }
        Debug.Log("Egg Count: " + _currentEgg);
    }
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
    }
}
