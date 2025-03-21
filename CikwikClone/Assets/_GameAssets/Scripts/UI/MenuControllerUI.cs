using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _howToPlayButton;

    void Awake()
    {
        _playButton.onClick.AddListener(
            () =>
            {
                TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
            });
        _quitButton.onClick.AddListener(
            () =>
            {
                Application.Quit();
            });
    }
}
