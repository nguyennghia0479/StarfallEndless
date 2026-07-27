using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button closeButton;

    private void OnEnable()
    {
        Time.timeScale = 0f;

        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        ToggleMainMenuButton();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;

        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void ToggleMainMenuButton()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePlayingState())
            mainMenuButton.gameObject.SetActive(true);
        else
            mainMenuButton.gameObject.SetActive(false);
    }

    private void OnMainMenuButtonClicked()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            UIEvents.RaiseMainMenuButtonClicked();
        }
    }

    private void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
