using UnityEngine;

public static class SaveData
{
    private const string LAST_SHIP_SELECTED = "lastShipSelected";
    private const string REWARD_POINTS = "RewardPoints";
    private const string SFX_PARAM = "sfxParam";
    private const string BGM_PARAM = "bgmParam";
    private const float DEFAULT_AUDIO_VAL = .5f;

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
}
