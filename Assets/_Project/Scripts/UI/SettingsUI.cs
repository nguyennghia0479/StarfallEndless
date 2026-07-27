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
        ToggleMainMenuButton();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        mainMenuButton.onClick.RemoveListener(PlayMainMenuButton);
        closeButton.onClick.AddListener(PlayCloseButton);
    }

    private void ToggleMainMenuButton()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePlayingState())
            mainMenuButton.gameObject.SetActive(true);
        else
            mainMenuButton.gameObject.SetActive(false);
    }

    private void PlayMainMenuButton()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            UIEvents.RaiseMainMenuButtonClicked();
        }
    }

    private void PlayCloseButton()
    {
        gameObject.SetActive(false);
    }
}
