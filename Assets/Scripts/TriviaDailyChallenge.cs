using System;
public static class TriviaDailyChallenge
{
    public static int QuestionIndex(int questionCount)
    {
        if (questionCount <= 0) return 0;
        var date = DateTime.Now.Date;
        var stableValue = date.Year * 10000 + date.Month * 100 + date.Day;
        return stableValue % questionCount;
    }
    public static string DisplayDate => DateTime.Now.ToString("MMMM d, yyyy");
}
