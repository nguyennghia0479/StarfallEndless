using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private ReviveUI reviveUI;
    [SerializeField] private CountingUI countingUI;

    private void OnEnable()
    {
        GameEvents.OnPlayerDestroyed += EnableReviveUI;
        UIEvents.OnPlayerRevived += HandlePlayerRevived;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerDestroyed -= EnableReviveUI;
        UIEvents.OnPlayerRevived -= HandlePlayerRevived;
    }

    private void Start()
    {
        DisableSetingUI();
        DisableReviveUI();
    }

    private void HandlePlayerRevived()
    {
        DisableReviveUI();
        countingUI.gameObject.SetActive(true);
        countingUI.SetToCountdown();
    }

    public void EnableSettingUI() => settingsUI.gameObject.SetActive(true);
    public void DisableSetingUI() => settingsUI.gameObject.SetActive(false);
    private void EnableReviveUI() => reviveUI.gameObject.SetActive(true);
    private void DisableReviveUI() => reviveUI.gameObject.SetActive(false);
}
