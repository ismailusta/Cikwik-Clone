using UnityEngine;

public class RottenWheatCollectible : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _DecreaseSpeedAmount;
    [SerializeField] private float _effectDuration;
    public void CoinCollect()
    {
        _playerController.SetMovementSpeed(_DecreaseSpeedAmount, _effectDuration);
        Destroy(gameObject);
    }
}
