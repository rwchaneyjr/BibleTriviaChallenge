using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaLevelSelector : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Text statusText;
    [SerializeField] private TriviaGameController gameController;
    [SerializeField] private TriviaScreenNavigator navigator;

    private void Start()
    {
        if (levelButtons == null || levelButtons.Length == 0)
        {
            levelButtons = new Button[10];
            for (var i = 0; i < levelButtons.Length; i++)
                levelButtons[i] = TriviaSceneLookup.Find("LevelButton" + (i + 1))?.GetComponent<Button>();
        }
        if (statusText == null) statusText = TriviaSceneLookup.Find("LevelStatusText")?.GetComponent<Text>();
        if (gameController == null) gameController = FindObjectOfType<TriviaGameController>();
        if (navigator == null) navigator = FindObjectOfType<TriviaScreenNavigator>();
        for (var i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] == null) continue;
            var level = i + 1;
            var unlocked = TriviaLevelProgression.IsUnlocked(level);
            levelButtons[i].interactable = unlocked;
            var label = levelButtons[i].GetComponentInChildren<Text>();
            label.text = "Level " + level + (unlocked ? "" : " (Locked)");
            label.fontSize = 28;
            label.resizeTextForBestFit = true;
            label.resizeTextMinSize = 14;
            label.resizeTextMaxSize = 28;
            label.horizontalOverflow = HorizontalWrapMode.Wrap;
            label.verticalOverflow = VerticalWrapMode.Truncate;
            var capturedLevel = level;
            levelButtons[i].onClick.AddListener(() => SelectLevel(capturedLevel));
        }
    }

    private void SelectLevel(int level)
    {
        if (!TriviaLevelProgression.IsUnlocked(level)) return;
        var difficulty = TriviaLevelProgression.DifficultyFor(level).ToString();
        if (statusText != null) statusText.text = "Level " + level + " • " + difficulty;
        if (navigator != null) navigator.ShowGame();
        if (gameController != null) gameController.BeginLevel(difficulty);
    }
}
