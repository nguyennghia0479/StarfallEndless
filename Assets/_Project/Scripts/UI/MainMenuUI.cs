using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayPlayButton);
        settingButton.onClick.AddListener(PlaySettingButton);
        quitButton.onClick.AddListener(PlayQuitButton);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayPlayButton);
        settingButton.onClick.RemoveListener(PlaySettingButton);
        quitButton.onClick.RemoveListener(PlayQuitButton);
    }

    private void PlayPlayButton()
    {
        UIEvents.RaiseGamePlayed();
    }

    private void PlaySettingButton()
    {
        UIManager.Instance.SwitchToSettingUI();
    }

    private void PlayQuitButton()
    {
        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
}
