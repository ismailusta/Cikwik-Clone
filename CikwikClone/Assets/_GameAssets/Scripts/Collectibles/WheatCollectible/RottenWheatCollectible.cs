using UnityEngine;
using UnityEngine.UI;

public class RottenWheatCollectible : MonoBehaviour, ICollectible
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private WheatDesignSO _wheatDesignSO;

    [SerializeField] private PlayerStateUI _playerStateUI;
    private RectTransform _boosterSlowTransform;
    private Image _rottenWheatImage;

    void Awake()
    {
        _boosterSlowTransform = _playerStateUI.GetBoosterSlowTransform;
        _rottenWheatImage = _boosterSlowTransform.GetComponent<Image>();
    }

    public void CoinCollect()
    {
        _playerController.SetMovementSpeed(_wheatDesignSO.IncreaseDecreaseAmount, _wheatDesignSO.EffectDuration);
        _playerStateUI.PlayerBoosterUIAnimation(_boosterSlowTransform, _rottenWheatImage, _playerStateUI.GetRottenWheatImage,
            _wheatDesignSO.ActiveSprite, _wheatDesignSO.InactiveSprite,
            _wheatDesignSO.ActiveWheatSprite, _wheatDesignSO.InactiveWheatSprite,
            _wheatDesignSO.EffectDuration);
        Destroy(gameObject);
    }
}
