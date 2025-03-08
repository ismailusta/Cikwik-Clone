using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _winPopupObject;
    [SerializeField] private GameObject _losePopupObject;
    [SerializeField] private GameObject _blackBackgroundObject;

    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.3f;

    private Image _blackBackgroundImage;
    private RectTransform _winPopupTransform, _losePopupTransform;
    void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _winPopupTransform = _winPopupObject.GetComponent<RectTransform>();
        _losePopupTransform = _losePopupObject.GetComponent<RectTransform>();
    }
    public void OnGameWin()
    {
        _blackBackgroundObject.SetActive(true);
        _winPopupObject.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
    public void OnGameLose()
    {
        _blackBackgroundObject.SetActive(true);
        _losePopupObject.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _losePopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
}
