using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaDatabaseStatus : MonoBehaviour
{
    [SerializeField] private Text statusText;
    [SerializeField] private TriviaQuestionDatabase database;

    private void Start()
    {
        if (statusText == null) statusText = GameObject.Find("DatabaseStatusText")?.GetComponent<Text>();
        if (database == null) database = FindObjectOfType<TriviaQuestionDatabase>();
        if (statusText == null || database == null) return;
        var categories = string.Join(", ", database.Categories());
        var difficulties = string.Join(", ", database.Difficulties());
        statusText.text = "Questions: " + database.Questions.Count +
            "\nCategories: " + categories + "\nDifficulty: " + difficulties;
    }
}
