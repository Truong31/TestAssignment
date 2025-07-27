using UnityEngine;
public static class ProgressManager
{
    private const string Key_LevelUnlocked = "UnlockedLevel";

    // Mặc định Level 1 được mở
    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(Key_LevelUnlocked, 1);
    }

    public static void UnlockLevel(int level)
    {
        int current = GetUnlockedLevel();
        if (level > current)
        {
            PlayerPrefs.SetInt(Key_LevelUnlocked, level);
            PlayerPrefs.Save();
        }
    }
}
