using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<GameState> OnGameStateChanged;

    [Header("References")]
    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealthUI;

    [Header("Settings")]
    [SerializeField] private float _gameOverDelay;
    [SerializeField] private int _maxEgg = 5;
    private GameState _currentGameState;
    private int _currentEgg;

    void Awake()
    {
        Instance = this;
        _catController.OnCatCatched += OnCatCatched_GameOver;
    }

    private void OnCatCatched_GameOver()
    {
        _playerHealthUI.AllAnimationDamage();
        StartCoroutine(GameOver());
    }

    private void OnEnable()
    {
        ChangeGameState(GameState.Play);
    }
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState = gameState;
        Debug.Log("Game State: " + _currentGameState);
    }
    public void OnEggCollected()
    {
        _currentEgg++;
        _eggCounterUI.SetEggCount(_currentEgg, _maxEgg);
        if (_currentEgg == _maxEgg)
        {
            // Win
            _eggCounterUI.SettEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();
        }
        Debug.Log("Egg Count: " + _currentEgg);
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(_gameOverDelay);
        ChangeGameState(GameState.GameOver);
        _winLoseUI.OnGameLose();
    }
    public void GetGameOver()
    {
        StartCoroutine(GameOver());
    }
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
