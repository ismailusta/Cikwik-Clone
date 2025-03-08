using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playeranimator;
    private StateController _stateController;
    private PlayerController _playerController;
    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        _playerController.OnPlayerJump += PlayerController_OnPlayerJump;
    }
    void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play &&
        GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }
        SetAnimationPlayer();
    }
    private void PlayerController_OnPlayerJump()
    {
        _playeranimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        Invoke(nameof(ResetJump), 0.5f);
    }
    private void ResetJump()
    {
        _playeranimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
    }

    private void SetAnimationPlayer()
    {
        var currentState = _stateController.GetCurrentState();
        switch (currentState)
        {
            case PlayerState.Idle:
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                break;
            case PlayerState.Move:
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                break;
            case PlayerState.Slide:
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                break;
            case PlayerState.SlideIdle:
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                _playeranimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                break;
        }
    }
}
