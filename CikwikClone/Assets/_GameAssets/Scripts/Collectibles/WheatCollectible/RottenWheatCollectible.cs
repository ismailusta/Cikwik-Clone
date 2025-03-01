using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    public void CoinCollect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseAmount, _wheatDesignSO.EffectDuration);
        Destroy(gameObject);
    }
}
