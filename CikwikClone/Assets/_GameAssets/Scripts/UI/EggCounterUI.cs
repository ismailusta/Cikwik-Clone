using DG.Tweening;
using TMPro;
using UnityEngine;

public class EggCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _eggCounterText;

    [Header("Settings")]
    [SerializeField] private Color _eggCompletedColor;
    [SerializeField] private float _eggCompletedColorDuration;
    [SerializeField] private float _eggCompletedScaleDuration;

    private RectTransform _eggCounterRectTransform;

    void Awake()
    {
        _eggCounterRectTransform = _eggCounterText.gameObject.GetComponent<RectTransform>();
    }
    public void SetEggCount(int eggCount, int maxEggCount)
    {
        _eggCounterText.text = $"{eggCount}/{maxEggCount}";
    }
    public void SettEggCompleted()
    {
        _eggCounterText.DOColor(_eggCompletedColor, _eggCompletedColorDuration);
        _eggCounterRectTransform.DOScale(1.2f, _eggCompletedScaleDuration).SetEase(Ease.OutBack);
    }
}
