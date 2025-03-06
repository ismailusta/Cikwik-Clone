using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatableTransform;
    [SerializeField] private TMP_Text _timerText;

    [Header("Settings")]
    [SerializeField] private float _timerRotateDuration;
    [SerializeField] private Ease _rotationEase;

    private float _elapsedTime;

    void Start()
    {
        TimerRotaterAnimation();
        StartTimer();
    }

    void TimerRotaterAnimation()
    {
        _timerRotatableTransform.DORotate(new Vector3(0f, 0f, -360f), _timerRotateDuration, RotateMode.FastBeyond360).
        SetLoops(-1, LoopType.Restart).SetEase(_rotationEase);
    }

    void StartTimer()
    {
        _elapsedTime = 0f;
        InvokeRepeating(nameof(UpdaterTimer), 0f, 1f);
    }
    void UpdaterTimer()
    {
        _elapsedTime += 1f;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
