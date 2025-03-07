using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _settingsPopupObject;
    [SerializeField] private GameObject _blackBackgroundObject;

    [Header("Buttons")]
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;

    [Header("Settings")]
    [SerializeField] private float _animationDuration;
    private Image _blackBackgroundImage;

    void Awake()
    {
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();
        _settingsPopupObject.transform.localScale = Vector3.zero;
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    private void OnSettingsButtonClicked()
    {
        _gameManager.ChangeGameState(GameState.Pause);
        _settingsPopupObject.SetActive(true);
        _blackBackgroundObject.SetActive(true);

        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);
        _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
}
