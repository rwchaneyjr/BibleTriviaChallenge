using UnityEngine;

public static class TriviaAchievements
{
    private const string FirstCorrectKey = "achievement_first_correct";
    private const string PerfectRoundKey = "achievement_perfect_round";
    private const string CorrectKey = "round_correct";
    private const string AnsweredKey = "round_answered";

    public static bool FirstCorrect => PlayerPrefs.GetInt(FirstCorrectKey, 0) == 1;
    public static bool PerfectRound => PlayerPrefs.GetInt(PerfectRoundKey, 0) == 1;

    public static string RecordAnswer(bool correct)
    {
        if (correct) PlayerPrefs.SetInt(FirstCorrectKey, 1);
        PlayerPrefs.SetInt(AnsweredKey, PlayerPrefs.GetInt(AnsweredKey, 0) + 1);
        if (correct) PlayerPrefs.SetInt(CorrectKey, PlayerPrefs.GetInt(CorrectKey, 0) + 1);
        PlayerPrefs.Save();
        return correct ? "Correct answer!" : "Keep studying!";
    }

    public static int CurrentStars()
    {
        var answered = PlayerPrefs.GetInt(AnsweredKey, 0);
        if (answered == 0) return 0;
        var accuracy = (float)PlayerPrefs.GetInt(CorrectKey, 0) / answered;
        return accuracy >= .9f ? 3 : accuracy >= .7f ? 2 : 1;
    }

    public static void MarkPerfectRound()
    {
        PlayerPrefs.SetInt(PerfectRoundKey, 1);
        PlayerPrefs.Save();
    }
}
