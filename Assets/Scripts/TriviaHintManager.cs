using UnityEngine;
using UnityEngine.UI;
public sealed class TriviaHintManager : MonoBehaviour
{
    [SerializeField] private Button hintButton;
    [SerializeField] private Text hintText;
    [SerializeField] private TriviaGameController gameController;
    [SerializeField] private TriviaAudioManager audioManager;
    private void Start()
    {
        if (hintButton == null) hintButton = TriviaSceneLookup.FindIn("GamePanel", "HintButton")?.GetComponent<Button>();
        if (hintText == null) hintText = TriviaSceneLookup.FindIn("GamePanel", "HintText")?.GetComponent<Text>();
        if (gameController == null) gameController = FindObjectOfType<TriviaGameController>();
        if (hintButton != null) hintButton.onClick.AddListener(UseHint);
    }
    public void UseHint()
    {
        if (gameController == null || !gameController.UseHint()) return;
        if (audioManager == null) audioManager = FindObjectOfType<TriviaAudioManager>();
        if (audioManager != null) audioManager.PlayClick();
        if (hintText != null) hintText.text = "Hint used: one incorrect answer removed.";
        if (hintButton != null) hintButton.interactable = false;
        if (audioManager != null) audioManager.PlayHint();
    }
}
