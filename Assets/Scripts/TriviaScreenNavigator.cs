using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaScreenNavigator : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button levelsButton;
    [SerializeField] private Button backButton;

    private void Start()
    {
        mainMenuPanel = mainMenuPanel ?? TriviaSceneLookup.Find("MainMenuPanel");
        levelPanel = levelPanel ?? TriviaSceneLookup.Find("LevelPanel");
        gamePanel = gamePanel ?? TriviaSceneLookup.Find("GamePanel");
        playButton = FindButtonIn("MainMenuPanel", "PlayButton") ?? playButton;
        levelsButton = FindButtonIn("MainMenuPanel", "LevelsButton") ?? levelsButton;
        backButton = FindButtonIn("LevelPanel", "BackButton") ?? backButton;
        if (playButton != null) { playButton.onClick.RemoveListener(ShowLevels); playButton.onClick.AddListener(ShowLevels); }
        if (levelsButton != null) { levelsButton.onClick.RemoveListener(ShowLevels); levelsButton.onClick.AddListener(ShowLevels); }
        if (backButton != null) { backButton.onClick.RemoveListener(ShowMenu); backButton.onClick.AddListener(ShowMenu); }
        Debug.Log("Trivia navigation ready: Play=" + (playButton != null) + ", Levels=" + (levelsButton != null) + ", Back=" + (backButton != null));
        ShowMenu();
    }

    private static Button FindButtonIn(string panelName, string buttonName)
    {
        var panel = TriviaSceneLookup.Find(panelName);
        if (panel == null) return null;
        foreach (var button in panel.GetComponentsInChildren<Button>(true))
            if (button.name == buttonName) return button;
        return null;
    }

    public void ShowMenu()
    {
        SetVisibleByName("MainMenuPanel", true); SetVisibleByName("LevelPanel", false); SetVisibleByName("GamePanel", false);
    }

    public void ShowLevels()
    {
        Debug.Log("Trivia navigation: showing levels");
        SetVisibleByName("MainMenuPanel", false); SetVisibleByName("LevelPanel", true); SetVisibleByName("GamePanel", false);
    }

    public void ShowGame()
    {
        SetVisibleByName("MainMenuPanel", false); SetVisibleByName("LevelPanel", false); SetVisibleByName("GamePanel", true);
    }

    private static void SetVisibleByName(string objectName, bool visible)
    {
        SetVisible(TriviaSceneLookup.Find(objectName), visible);
    }

    private static void SetVisible(GameObject target, bool visible)
    {
        if (target != null) target.SetActive(visible);
    }
}
