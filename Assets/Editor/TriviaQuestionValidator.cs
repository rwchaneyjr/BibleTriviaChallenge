using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class TriviaQuestionValidator
{
    [MenuItem("Bible Trivia/Validate Question Database")]
    public static void Validate()
    {
        var asset = Resources.Load<TextAsset>("Trivia/questions");
        if (asset == null) { Debug.LogError("Missing Resources/Trivia/questions.json"); return; }
        var data = JsonUtility.FromJson<TriviaQuestionCollection>(asset.text);
        var errors = new List<string>();
        var seen = new HashSet<string>();
        foreach (var q in data?.questions ?? new TriviaQuestion[0])
        {
            if (q == null || string.IsNullOrWhiteSpace(q.id)) errors.Add("Question has no ID.");
            else if (!seen.Add(q.id)) errors.Add($"Duplicate ID: {q.id}");
            if (q?.answers == null || q.answers.Length != 4) errors.Add($"{q?.id}: must have exactly four answers.");
            if (q?.answers != null && q.answers.Length == 4)
            {
                if (q.answers.Any(string.IsNullOrWhiteSpace)) errors.Add($"{q.id}: answer choices cannot be empty.");
                if (q.answers.Distinct().Count() != 4) errors.Add($"{q.id}: answer choices must be unique.");
            }
            if (q != null && string.IsNullOrWhiteSpace(q.question)) errors.Add($"{q.id}: missing question text.");
            if (q != null && (q.correctAnswerIndex < 0 || q.correctAnswerIndex > 3)) errors.Add($"{q.id}: invalid correct answer index.");
            if (q != null && string.IsNullOrWhiteSpace(q.reference)) errors.Add($"{q.id}: missing reference.");
            if (q != null && string.IsNullOrWhiteSpace(q.explanation)) errors.Add($"{q.id}: missing explanation.");
            else if (q != null && q.explanation.Trim().Length < 20) errors.Add($"{q.id}: explanation is too short.");
        }
        if (errors.Count == 0) Debug.Log($"Trivia database valid: {data?.questions?.Length ?? 0} questions.");
        else Debug.LogError(string.Join("\n", errors));
    }
}
