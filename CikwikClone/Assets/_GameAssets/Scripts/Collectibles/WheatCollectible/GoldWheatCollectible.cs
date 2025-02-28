using UnityEngine;

public class GoldWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _IncreaseSpeedAmount;
    [SerializeField] private float _effectDuration;
    public void CoinCollect()
    {
        _playerController.SetMovementSpeed(_IncreaseSpeedAmount, _effectDuration);
        Destroy(gameObject);
    }

}
