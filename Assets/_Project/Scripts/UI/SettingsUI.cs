using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button closeButton;

    [Header("Audio Settings")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float decibelMultiplier = 40f;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxParam;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private string bgmParam;

    private readonly float minValue = .0001f;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        LoadAudioSettings();
        ToggleMainMenuButton();

        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        SaveAudioSettings();

        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
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

    private void ToggleMainMenuButton()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePlayingState())
            mainMenuButton.gameObject.SetActive(true);
        else
            mainMenuButton.gameObject.SetActive(false);
    }

    private void OnSFXVolumeChanged(float sliderValue) => SetMixerVolume(sfxParam, sliderValue);

    private void OnBGMVolumeChanged(float sliderValue) => SetMixerVolume(bgmParam, sliderValue);

    private void SetMixerVolume(string paramName, float sliderValue)
    {
        if (string.IsNullOrEmpty(paramName) || audioMixer == null)
            return;

        float clampValue = Mathf.Clamp(sliderValue, minValue, 1f);
        float db = Mathf.Log10(clampValue) * decibelMultiplier;
        audioMixer.SetFloat(paramName, db);
    }

    private void SaveAudioSettings()
    {
        if (sfxSlider == null || bgmSlider == null)
            return;

        SaveData.SaveSFXSetting(sfxSlider.value);
        SaveData.SaveBGMSetting(bgmSlider.value);
    }

    public void LoadAudioSettings()
    {
        if (sfxSlider == null || bgmSlider == null)
            return;

        float sfxLoadValue = SaveData.LoadSFXSetting();
        float bgmLoadValue = SaveData.LoadBGMSetting();

        OnSFXVolumeChanged(sfxLoadValue);
        sfxSlider.value = sfxLoadValue;

        OnBGMVolumeChanged(bgmLoadValue);
        bgmSlider.value = bgmLoadValue;
    }
}
