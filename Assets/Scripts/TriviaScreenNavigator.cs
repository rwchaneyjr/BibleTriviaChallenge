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
    [SerializeField] private Button resetButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button gameBackButton;
    private TriviaAudioManager audioManager;

    private void Start()
    {
        mainMenuPanel = mainMenuPanel ?? TriviaSceneLookup.Find("MainMenuPanel");
        levelPanel = levelPanel ?? TriviaSceneLookup.Find("LevelPanel");
        gamePanel = gamePanel ?? TriviaSceneLookup.Find("GamePanel");
        playButton = FindButtonIn("MainMenuPanel", "PlayButton") ?? playButton;
        levelsButton = FindButtonIn("MainMenuPanel", "LevelsButton") ?? levelsButton;
        backButton = FindButtonIn("LevelPanel", "BackButton") ?? backButton;
        resetButton = FindButtonIn("MainMenuPanel", "ResetButton") ?? resetButton;
        exitGameButton = FindButtonIn("MainMenuPanel", "ExitGameButton") ?? exitGameButton;
        gameBackButton = FindButtonIn("GamePanel", "GameBackButton") ?? gameBackButton;
        audioManager = FindObjectOfType<TriviaAudioManager>();
        if (playButton != null) { playButton.onClick.RemoveListener(ShowLevels); playButton.onClick.AddListener(ShowLevels); playButton.onClick.AddListener(PlayClick); }
        if (levelsButton != null) { levelsButton.onClick.RemoveListener(ShowLevels); levelsButton.onClick.AddListener(ShowLevels); levelsButton.onClick.AddListener(PlayClick); }
        if (backButton != null) { backButton.onClick.RemoveListener(ShowMenu); backButton.onClick.AddListener(ShowMenu); backButton.onClick.AddListener(PlayClick); }
        if (resetButton != null) { resetButton.onClick.AddListener(ResetGame); resetButton.onClick.AddListener(PlayClick); }
        if (exitGameButton != null) { exitGameButton.onClick.AddListener(ExitGame); exitGameButton.onClick.AddListener(PlayClick); }
        if (gameBackButton != null) { gameBackButton.onClick.AddListener(ShowLevels); gameBackButton.onClick.AddListener(PlayClick); }
        Debug.Log("Trivia navigation ready: Play=" + (playButton != null) + ", Levels=" + (levelsButton != null) + ", Back=" + (backButton != null));
        ShowMenu();
    }

    private void PlayClick()
    {
        if (audioManager != null) audioManager.PlayClick();
    }

    private void ResetGame()
    {
        TriviaProgress.ResetAll();
        ShowMenu();
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game requested.");
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
