using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        Time.timeScale = 0f;

        mainMenuButton.onClick.AddListener(PlayMainMenuButton);
        closeButton.onClick.AddListener(PlayCloseButton);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        mainMenuButton.onClick.RemoveListener(PlayMainMenuButton);
        closeButton.onClick.AddListener(PlayCloseButton);
    }

    private void PlayMainMenuButton()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            GameEvents.RaiseGameRetried(false);
        }


    }

    private void PlayCloseButton()
    {
        if (GameManager.Instance.IsMainMenuState())
        {
            UIManager.Instance.SwitchToMainMenuUI();
        }
        else if (GameManager.Instance.IsGamePlayingState())
        {
            UIManager.Instance.SwitchToMainGameUI();
        }
    }
}
