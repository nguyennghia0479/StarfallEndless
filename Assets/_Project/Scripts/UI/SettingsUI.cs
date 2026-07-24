using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    private void OnEnable()
    {
        Time.timeScale = 0;

        mainMenuButton.onClick.AddListener(PlayMainMenuButton);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void PlayMainMenuButton()
    {
        Debug.Log("Go Main menu");
    }
}
