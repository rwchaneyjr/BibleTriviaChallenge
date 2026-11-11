using UnityEditor;
using UnityEngine;

public static class TriviaSceneValidator
{
    [MenuItem("Bible Trivia/Validate Scene Objects")]
    public static void Validate()
    {
        var required = new[] { "TriviaGameCanvas", "QuestionText", "ReferenceText", "FeedbackText", "ScoreText", "ProgressText", "NextButton", "TriviaQuestionDatabase", "TriviaGameController" };
        var missing = 0;
        foreach (var name in required)
            if (GameObject.Find(name) == null) { Debug.LogError("Missing scene object: " + name); missing++; }
        if (missing == 0) Debug.Log("Trivia scene objects validated successfully.");
    }
}
