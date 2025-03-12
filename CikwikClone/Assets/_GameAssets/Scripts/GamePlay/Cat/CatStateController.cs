using UnityEngine;

public class CatStateController : MonoBehaviour
{
    [SerializeField] private CatState currentState = CatState.Walking;
    void Start()
    {
        ChangeState(CatState.Walking);
    }
    public void ChangeState(CatState newState)
    {
        if (currentState == newState)
        {
            return;
        }
        currentState = newState;
    }
    public CatState GetCurrentState()
    {
        return currentState;
    }
}
