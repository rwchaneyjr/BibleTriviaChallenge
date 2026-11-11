using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaAudioManager : MonoBehaviour
{
    private const string MuteKey = "bible_trivia_muted";
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip answerClip;
    [SerializeField] private AudioClip correctClip;
    [SerializeField] private AudioClip incorrectClip;
    [SerializeField] private Button muteButton;
    [SerializeField] private Text muteLabel;

    public bool IsMuted { get; private set; }

    private void Awake()
    {
        if (muteButton == null) muteButton = TriviaSceneLookup.Find("MuteButton")?.GetComponent<Button>();
        if (muteButton != null && muteLabel == null) muteLabel = muteButton.GetComponentInChildren<Text>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        IsMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        ApplyMute();
        if (muteButton != null) muteButton.onClick.AddListener(ToggleMute);
    }

    public void PlayAnswer(bool correct)
    {
        if (correct && correctClip != null) audioSource.PlayOneShot(correctClip);
        else if (!correct && incorrectClip != null) audioSource.PlayOneShot(incorrectClip);
        else if (answerClip != null) audioSource.PlayOneShot(answerClip);
    }

    public void ToggleMute()
    {
        IsMuted = !IsMuted;
        PlayerPrefs.SetInt(MuteKey, IsMuted ? 1 : 0);
        PlayerPrefs.Save();
        ApplyMute();
    }

    private void ApplyMute()
    {
        if (audioSource != null) audioSource.mute = IsMuted;
        if (muteLabel != null) muteLabel.text = IsMuted ? "Sound: Off" : "Sound: On";
    }
}
