using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class TriviaQuestionDatabase : MonoBehaviour
{
    [SerializeField] private TextAsset questionAsset;
    private readonly List<TriviaQuestion> questions = new();

    public IReadOnlyList<TriviaQuestion> Questions => questions;

    private void Awake()
    {
        if (questionAsset == null)
            questionAsset = Resources.Load<TextAsset>("Trivia/questions");

        if (questionAsset == null)
        {
            Debug.LogWarning("No trivia question database found at Resources/Trivia/questions.json.");
            return;
        }

        var collection = JsonUtility.FromJson<TriviaQuestionCollection>(questionAsset.text);
        if (collection?.questions != null)
            questions.AddRange(collection.questions.Where(IsUsable));
    }

    public IEnumerable<TriviaQuestion> Filter(string category, string difficulty)
    {
        return questions.Where(q =>
            (string.IsNullOrEmpty(category) || q.category == category) &&
            (string.IsNullOrEmpty(difficulty) || q.difficulty == difficulty));
    }

    public IEnumerable<string> Categories()
    {
        return questions.Select(q => q.category).Where(c => !string.IsNullOrWhiteSpace(c)).Distinct().OrderBy(c => c);
    }

    public IEnumerable<string> Difficulties()
    {
        return questions.Select(q => q.difficulty).Where(d => !string.IsNullOrWhiteSpace(d)).Distinct().OrderBy(d => d);
    }

    private static bool IsUsable(TriviaQuestion q) => q != null &&
        !string.IsNullOrWhiteSpace(q.id) && !string.IsNullOrWhiteSpace(q.question) &&
        q.answers != null && q.answers.Length == 4 &&
        q.correctAnswerIndex >= 0 && q.correctAnswerIndex < q.answers.Length;
}
