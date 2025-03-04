using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;

    [Header("Images")]
    [SerializeField] private Image _goldenWheatImage;
    public Image GetGoldenWheatImage => _goldenWheatImage;
    [SerializeField] private Image _holyWheatImage;
    public Image GetHolyWheatImage => _holyWheatImage;
    [SerializeField] private Image _rottenWheatImage;
    public Image GetRottenWheatImage => _rottenWheatImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingInactiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingInactiveSprite;


    [Header("Settings")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;

    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }
    void Start()
    {
        _playerController.OnPlayerStateChange += OnPlayerStateChange_Event;
        SetPlayerStateUI(_playerWalkingActiveSprite, _playerSlidingInactiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void OnPlayerStateChange_Event(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                SetPlayerStateUI(_playerWalkingActiveSprite, _playerSlidingInactiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;
            case PlayerState.Slide:
            case PlayerState.SlideIdle:
                SetPlayerStateUI(_playerWalkingInactiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;
        }
    }
    private void SetPlayerStateUI(Sprite playerWalkSprite, Sprite playerSlideSprite,
    RectTransform activeTransform, RectTransform inactiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkSprite;
        _playerSlidingImage.sprite = playerSlideSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        inactiveTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }
    private IEnumerator SetBoosterUI(RectTransform activeTransform, Image boosterImage, Image wheatImage,
    Sprite activeSprite, Sprite inactiveSprite, Sprite activeWheatSprite,
    Sprite inactiveWheatSprite, float Duration)
    {
        boosterImage.sprite = activeSprite;
        wheatImage.sprite = activeWheatSprite;
        activeTransform.DOAnchorPosX(25f, Duration).SetEase(_moveEase);

        yield return new WaitForSeconds(Duration);

        boosterImage.sprite = inactiveSprite;
        wheatImage.sprite = inactiveWheatSprite;
        activeTransform.DOAnchorPosX(90f, Duration).SetEase(_moveEase);
    }
    public void PlayerBoosterUIAnimation(RectTransform activeTransform, Image boosterImage, Image wheatImage,
    Sprite activeSprite, Sprite inactiveSprite, Sprite activeWheatSprite,
    Sprite inactiveWheatSprite, float Duration)
    {
        StartCoroutine(SetBoosterUI(activeTransform, boosterImage, wheatImage,
        activeSprite, inactiveSprite, activeWheatSprite,
        inactiveWheatSprite, Duration));
    }
}
