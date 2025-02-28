using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _IncreaseForceAmount;
    [SerializeField] private float _effectDuration;
    public void CoinCollect()
    {
        _playerController.SetJumpForce(_IncreaseForceAmount, _effectDuration);
        Destroy(gameObject);
    }
}
