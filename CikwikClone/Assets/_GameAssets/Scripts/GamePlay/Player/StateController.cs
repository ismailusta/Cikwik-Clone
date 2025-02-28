using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentState = PlayerState.Idle;

    void Start()
    {
        ChangeState(PlayerState.Idle);
    }
    public void ChangeState(PlayerState newState)
    {
        if (_currentState == newState)
        {
            return;
        }
        _currentState = newState;
    }
    public PlayerState GetCurrentState()
    {
        return _currentState;
    }
}
