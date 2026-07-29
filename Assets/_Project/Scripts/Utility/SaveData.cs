using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class SaveData
{
    private const string LAST_SHIP_SELECTED = "lastShipSelected";
    private const string REWARD_POINTS = "RewardPoints";
    private const string SFX_PARAM = "sfxParam";
    private const string BGM_PARAM = "bgmParam";
    private const float DEFAULT_AUDIO_VAL = .5f;
    private const string LANG_EN = "langEN";
    private const string LANG_VI = "langVI";
    private const int DEFAULT_LANG = 1;
    private const string LOCALE_SELECTED = "localeSelected";

    public static void SaveSelectedShip(int saveIndex)
    {
        PlayerPrefs.SetInt(LAST_SHIP_SELECTED, saveIndex);
    }

    public static int LoadLastShipSelected()
    {
        return PlayerPrefs.GetInt(LAST_SHIP_SELECTED);
    }

    public static void SaveUnlockShip(string shipIndex)
    {
        PlayerPrefs.SetInt(shipIndex, 1); // 1: unlocked, 0: locked
    }

    public static int LoadShipIsUnlocked(string shipIndex, int unlockedByDefault)
    {
        return PlayerPrefs.GetInt(shipIndex, unlockedByDefault);
    }

    public static void SaveRewardPoints(int currentRewardPoints)
    {
        PlayerPrefs.SetInt(REWARD_POINTS, currentRewardPoints);
    }

    public static int LoadRewardPoints()
    {
        return PlayerPrefs.GetInt(REWARD_POINTS, 0);
    }

    public static void SaveSFXSetting(float value) => PlayerPrefs.SetFloat(SFX_PARAM, value);

    public static float LoadSFXSetting() => PlayerPrefs.GetFloat(SFX_PARAM, DEFAULT_AUDIO_VAL);

    public static void SaveBGMSetting(float value) => PlayerPrefs.SetFloat(BGM_PARAM, value);

    public static float LoadBGMSetting() => PlayerPrefs.GetFloat(BGM_PARAM, DEFAULT_AUDIO_VAL);

    public static void SaveLanguageSettings(bool enToggle, bool viToggle)
    {
        int enToggleVal = enToggle ? 1 : 0;
        int viToggleVal = viToggle ? 1 : 0;

        if (enToggle)
            PlayerPrefs.SetString(LOCALE_SELECTED, GameIdentifiers.Locales.LOCALES_EN);
        else if (viToggle)
            PlayerPrefs.SetString(LOCALE_SELECTED, GameIdentifiers.Locales.LOCALES_VI);

        PlayerPrefs.SetInt(LANG_EN, enToggleVal);
        PlayerPrefs.SetInt(LANG_VI, viToggleVal);
    }

    public static int LoadLangEnglishSetting() => PlayerPrefs.GetInt(LANG_EN, DEFAULT_LANG);
    public static int LoadLangVietnameseSetting() => PlayerPrefs.GetInt(LANG_VI, 0);
}
