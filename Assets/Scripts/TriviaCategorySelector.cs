using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaCategorySelector : MonoBehaviour
{
    [SerializeField] private Button oldTestamentButton;
    [SerializeField] private Button newTestamentButton;
    [SerializeField] private TriviaGameController gameController;
    [SerializeField] private TriviaScreenNavigator navigator;

    private void Start()
    {
        if (oldTestamentButton == null) oldTestamentButton = TriviaSceneLookup.Find("OldTestamentButton")?.GetComponent<Button>();
        if (newTestamentButton == null) newTestamentButton = TriviaSceneLookup.Find("NewTestamentButton")?.GetComponent<Button>();
        if (gameController == null) gameController = FindObjectOfType<TriviaGameController>();
        if (navigator == null) navigator = FindObjectOfType<TriviaScreenNavigator>();
        if (oldTestamentButton != null) oldTestamentButton.onClick.AddListener(() => Select("Old Testament"));
        if (newTestamentButton != null) newTestamentButton.onClick.AddListener(() => Select("New Testament"));
    }

    private void Select(string category)
    {
        if (navigator != null) navigator.ShowGame();
        if (gameController != null) gameController.BeginCategory(category);
    }
}
