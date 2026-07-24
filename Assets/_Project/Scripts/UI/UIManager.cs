using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SettingsUI settingsUI;

    private void Start()
    {
        DisableSetingUI();
    }

    public void EnableSettingUI() => settingsUI.gameObject.SetActive(true);

    public void DisableSetingUI() => settingsUI.gameObject.SetActive(false);
}
