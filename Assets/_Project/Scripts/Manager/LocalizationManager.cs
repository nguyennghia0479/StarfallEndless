using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        UIEvents.OnLocaleChanaged += ChangeLanguage;
    }

    private void OnDisable()
    {
        UIEvents.OnLocaleChanaged -= ChangeLanguage;
    }

    private void ChangeLanguage(string localeCode)
    {
        if (LocalizationSettings.InitializationOperation.IsDone)
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        else
            StartCoroutine(ChangeLanguageRoutine(localeCode));
    }

    private IEnumerator ChangeLanguageRoutine(string localeCode)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        yield return new WaitForSeconds(.1f);
        UIEvents.RaiseLocaleChangeDone();
    }

    public void ChangeDynamicLocalizedText(string tableName, string entryRef, TMP_Text textToLocalized)
    {
        LocalizedString localizedEndText = new(tableName, entryRef);
        localizedEndText.StringChanged += (translatedValue) =>
        {
            if (textToLocalized != null)
                textToLocalized.text = translatedValue;
        };

        localizedEndText.RefreshString();
    }
}
