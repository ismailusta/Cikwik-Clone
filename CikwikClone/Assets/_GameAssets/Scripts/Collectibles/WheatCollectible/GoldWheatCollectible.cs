using UnityEngine;
using UnityEngine.UI;

public class GoldWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerStateUI _playerStateUI;
    private RectTransform _boosterSpeedTransform;
    private Image _goldenWheatImage;

    void Awake()
    {
        _boosterSpeedTransform = _playerStateUI.GetBoosterSpeedTransform;
        _goldenWheatImage = _boosterSpeedTransform.GetComponent<Image>();
    }
    public void CoinCollect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseAmount, _wheatDesignSO.EffectDuration);
        _playerStateUI.PlayerBoosterUIAnimation(_boosterSpeedTransform, _goldenWheatImage, _playerStateUI.GetGoldenWheatImage,
            _wheatDesignSO.ActiveSprite, _wheatDesignSO.InactiveSprite,
            _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.InactiveWheatSprite,
            _wheatDesignSO.EffectDuration);
        Destroy(gameObject);
    }

}
