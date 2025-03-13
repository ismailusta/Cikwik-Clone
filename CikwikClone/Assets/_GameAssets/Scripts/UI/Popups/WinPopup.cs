using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private Button _oneMoreButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;


    void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _oneMoreButton.onClick.AddListener(OneMoreButtonClicked);
        _mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }
    private void OneMoreButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
    private void MainMenuButtonClicked()
    {
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);
    }
}
