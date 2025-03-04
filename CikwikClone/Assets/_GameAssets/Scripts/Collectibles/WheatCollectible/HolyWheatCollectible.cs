using UnityEngine;
using UnityEngine.UI;

public class HolyWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private WheatDesignSO _wheatDesignSO;
    [SerializeField] private PlayerStateUI _playerStateUI;
    private RectTransform _boosterJumpTransform;
    private Image _holyWheatImage;
    void Awake()
    {
        _boosterJumpTransform = _playerStateUI.GetBoosterJumpTransform;
        _holyWheatImage = _boosterJumpTransform.GetComponent<Image>();
    }
    public void CoinCollect()
    {
        _playerController.SetJumpForce(_wheatDesignSO.IncreaseDecreaseAmount, _wheatDesignSO.EffectDuration);
        _playerStateUI.PlayerBoosterUIAnimation(_boosterJumpTransform, _holyWheatImage, _playerStateUI.GetHolyWheatImage,
            _wheatDesignSO.ActiveSprite, _wheatDesignSO.InactiveSprite,
            _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.InactiveWheatSprite,
            _wheatDesignSO.EffectDuration);
        Destroy(gameObject);
    }
}
