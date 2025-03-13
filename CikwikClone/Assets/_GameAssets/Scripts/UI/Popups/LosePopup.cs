using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private TMP_Text _timerText;

    void OnEnable()
    {
        _timerText.text = _timerUI.GetFinalTime();
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }
    private void OnTryAgainButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.GAME_SCENE);
    }
    private void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(Consts.SceneNames.MENU_SCENE);
    }
}
