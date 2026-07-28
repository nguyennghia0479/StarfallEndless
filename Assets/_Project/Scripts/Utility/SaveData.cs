using UnityEngine;

public static class SaveData
{
    private const string LAST_SHIP_SELECTED = "lastShipSelected";
    private const string REWARD_POINTS = "RewardPoints";

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
}
