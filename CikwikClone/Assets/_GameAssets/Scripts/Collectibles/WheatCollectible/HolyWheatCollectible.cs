using UnityEngine;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    public void CoinCollect()
    {
        _playerController.SetJumpForce(_wheatDesignSO.IncreaseDecreaseAmount, _wheatDesignSO.EffectDuration);
        Destroy(gameObject);
    }
}
