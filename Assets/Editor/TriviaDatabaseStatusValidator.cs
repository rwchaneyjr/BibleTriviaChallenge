using UnityEditor;
using UnityEngine;

public static class TriviaDatabaseStatusValidator
{
    [MenuItem("Bible Trivia/Print Database Status")]
    public static void PrintStatus()
    {
        var asset = Resources.Load<TextAsset>("Trivia/questions");
        if (asset == null) { Debug.LogError("Missing Trivia/questions.json"); return; }
        var data = JsonUtility.FromJson<TriviaQuestionCollection>(asset.text);
        Debug.Log("Trivia database contains " + (data?.questions?.Length ?? 0) + " questions.");
    }
}
