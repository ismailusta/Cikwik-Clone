using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _healthImages;

    [Header("Sprites")]
    [SerializeField] private Sprite _liveSprite;
    [SerializeField] private Sprite _deadSprite;

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;

    private RectTransform[] _healthTransforms;

    void Awake()
    {
        _healthTransforms = new RectTransform[_healthImages.Length];
        for (int i = 0; i < _healthImages.Length; i++)
        {
            _healthTransforms[i] = _healthImages[i].GetComponent<RectTransform>();
        }

    }
    public void AnimationDamage()
    {
        for (int i = 0; i < _healthImages.Length; i++)
        {
            if (_healthImages[i].sprite == _liveSprite)
            {
                AnimateDamageSprite(_healthImages[i], _healthTransforms[i]);
                break;
            }
        }
    }
    public void AllAnimationDamage()
    {
        for (int i = 0; i < _healthImages.Length; i++)
        {
            if (_healthImages[i].sprite == _liveSprite)
            {
                AnimateDamageSprite(_healthImages[i], _healthTransforms[i]);
            }
        }
    }
    void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            activeImage.sprite = _deadSprite;
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }

}
