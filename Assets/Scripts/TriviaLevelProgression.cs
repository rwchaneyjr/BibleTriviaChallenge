using UnityEngine;

public enum TriviaLevelDifficulty { Easy, Medium, Hard }

public static class TriviaLevelProgression
{
    private const string UnlockedKey = "bible_trivia_unlocked_level";

    public static int HighestUnlockedLevel => Mathf.Max(1, PlayerPrefs.GetInt(UnlockedKey, 1));

    public static TriviaLevelDifficulty DifficultyFor(int level)
    {
        if (level >= 7) return TriviaLevelDifficulty.Hard;
        if (level >= 4) return TriviaLevelDifficulty.Medium;
        return TriviaLevelDifficulty.Easy;
    }

    public static bool IsUnlocked(int level) => level <= HighestUnlockedLevel;

    public static void RecordCompletedLevel(int level)
    {
        if (level < HighestUnlockedLevel) return;
        PlayerPrefs.SetInt(UnlockedKey, level + 1);
        PlayerPrefs.Save();
    }
}
