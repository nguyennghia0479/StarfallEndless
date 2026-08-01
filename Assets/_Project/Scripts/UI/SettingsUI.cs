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

    [Header("Localization")]
    [SerializeField] private Toggle enToggle;
    [SerializeField] private Toggle viToggle;

    private readonly float minValue = .0001f;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        LoadLanguageSettings();
        ToggleMainMenuButton();

        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        enToggle.onValueChanged.AddListener(OnEnglishToggle);
        viToggle.onValueChanged.AddListener(OnVietnameseToggle);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        SaveSettings();

        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        sfxSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
        bgmSlider.onValueChanged.RemoveListener(OnBGMVolumeChanged);
        enToggle.onValueChanged.RemoveListener(OnEnglishToggle);
        viToggle.onValueChanged.RemoveListener(OnVietnameseToggle);
    }

    private void OnMainMenuButtonClicked()
    {
        if (GameManager.Instance.IsGamePlayingState())
        {
            gameObject.SetActive(false);
            UIEvents.RaiseSettingQuitButtonClicked();
            UIEvents.RaiseButtonClicked();
        }
    }

    private void OnCloseButtonClicked()
    {
        UIEvents.RaiseButtonClicked();
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

    private void LoadAudioSettings()
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

    private void OnEnglishToggle(bool isCheck)
    {
        if (isCheck)
        {
            viToggle.isOn = false;
            UIEvents.RaiseLocaleChanged(GameIdentifiers.Locales.LOCALES_EN);
        }

        enToggle.interactable = !enToggle.isOn;
    }

    private void OnVietnameseToggle(bool isCheck)
    {
        if (isCheck)
        {
            enToggle.isOn = false;
            UIEvents.RaiseLocaleChanged(GameIdentifiers.Locales.LOCALES_VI);
        }

        viToggle.interactable = !viToggle.isOn;
    }

    private void SaveLanguageSettings()
    {
        if (enToggle == null || viToggle == null)
            return;

        SaveData.SaveLanguageSettings(enToggle.isOn, viToggle.isOn);
    }

    private void LoadLanguageSettings()
    {
        if (enToggle == null || viToggle == null)
            return;

        bool enToggleLoadValue = SaveData.LoadLangEnglishSetting() == 1;
        bool viToggleLoadValue = SaveData.LoadLangVietnameseSetting() == 1;

        OnEnglishToggle(enToggleLoadValue);
        enToggle.isOn = enToggleLoadValue;

        OnVietnameseToggle(viToggleLoadValue);
        viToggle.isOn = viToggleLoadValue;
    }

    private void SaveSettings()
    {
        SaveAudioSettings();
        SaveLanguageSettings();
    }

    public void LoadSettings()
    {
        LoadAudioSettings();
        LoadLanguageSettings();
    }
}
