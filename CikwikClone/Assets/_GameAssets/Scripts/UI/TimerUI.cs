using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatableTransform;
    [SerializeField] private TMP_Text _timerText;

    [Header("Settings")]
    [SerializeField] private float _timerRotateDuration;
    [SerializeField] private Ease _rotationEase;

    private bool _isTimerActive;
    private Tween _timerRotaterAnimation;
    private float _elapsedTime;
    private string FinalTime;

    void Start()
    {
        TimerRotaterAnimation();
        StartTimer();
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged_Event;
    }

    private void OnGameStateChanged_Event(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Pause:
                PauseTimer();
                break;
            case GameState.Resume:
                ResumeTimer();
                break;
            case GameState.GameOver:
                FinishTimer();
                break;
        }
    }
    private void PauseTimer()
    {
        _isTimerActive = false;
        CancelInvoke(nameof(UpdaterTimer));
        _timerRotaterAnimation.Pause();
    }
    private void ResumeTimer()
    {
        if (!_isTimerActive)
        {
            _isTimerActive = true;
            InvokeRepeating(nameof(UpdaterTimer), 0f, 1f);
            _timerRotaterAnimation.Play();
        }
    }
    private void FinishTimer()
    {
        PauseTimer();
        FinalTime = FormatTime();
    }

    private string FormatTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        return _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    void TimerRotaterAnimation()
    {
        _timerRotaterAnimation = _timerRotatableTransform.DORotate(new Vector3(0f, 0f, -360f), _timerRotateDuration, RotateMode.FastBeyond360).
        SetLoops(-1, LoopType.Restart).SetEase(_rotationEase);
    }

    void StartTimer()
    {
        _isTimerActive = true;
        _elapsedTime = 0f;
        InvokeRepeating(nameof(UpdaterTimer), 0f, 1f);
    }
    void UpdaterTimer()
    {
        if (!_isTimerActive)
        {
            return;
        }
        _elapsedTime += 1f;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    public string GetFinalTime()
    {
        return FinalTime;
    }
}
