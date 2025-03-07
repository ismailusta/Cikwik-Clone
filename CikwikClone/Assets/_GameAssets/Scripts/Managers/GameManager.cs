using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("References")]
    [SerializeField] private int _maxEgg = 5;
    [SerializeField] private EggCounterUI _eggCounterUI;
    private int _currentEgg;

    void Awake()
    {
        Instance = this;
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
}
