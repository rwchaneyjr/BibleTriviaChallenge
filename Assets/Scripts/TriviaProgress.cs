using UnityEngine;

public static class TriviaProgress
{
    private const string ScoreKey = "bible_trivia_score";
    private const string BestScoreKey = "bible_trivia_best_score";
    private const string CompletedKey = "bible_trivia_completed";

    public static int Score => PlayerPrefs.GetInt(ScoreKey, 0);
    public static int BestScore => PlayerPrefs.GetInt(BestScoreKey, 0);
    public static int CompletedQuestions => PlayerPrefs.GetInt(CompletedKey, 0);

    public static void RecordAnswer(bool correct)
    {
        var score = Score + (correct ? 100 : 0);
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.SetInt(BestScoreKey, Mathf.Max(BestScore, score));
        PlayerPrefs.SetInt(CompletedKey, CompletedQuestions + 1);
        PlayerPrefs.Save();
    }

    public static void ResetCurrentRun()
    {
        PlayerPrefs.SetInt(ScoreKey, 0);
        PlayerPrefs.Save();
    }

    public static void ResetAll()
    {
        foreach (var key in new[]
        {
            ScoreKey, BestScoreKey, CompletedKey,
            "bible_trivia_unlocked_level", "achievement_first_correct",
            "achievement_perfect_round", "round_correct", "round_answered"
        }) PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }
}
