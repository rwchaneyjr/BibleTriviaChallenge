using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaAudioManager : MonoBehaviour
{
    private const string MuteKey = "bible_trivia_muted";
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip answerClip;
    [SerializeField] private AudioClip correctClip;
    [SerializeField] private AudioClip incorrectClip;
    [SerializeField] private AudioClip hintClip;
    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip levelCompleteClip;
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private Button muteButton;
    [SerializeField] private Text muteLabel;

    public bool IsMuted { get; private set; }

    private void Awake()
    {
        if (muteButton == null) muteButton = TriviaSceneLookup.Find("MuteButton")?.GetComponent<Button>();
        if (muteButton != null && muteLabel == null) muteLabel = muteButton.GetComponentInChildren<Text>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        if (musicSource == null) musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.clip = musicClip;
        musicSource.playOnAwake = false;
        IsMuted = PlayerPrefs.GetInt(MuteKey, 0) == 1;
        ApplyMute();
        if (musicClip != null && !musicSource.isPlaying) musicSource.Play();
        if (muteButton != null) muteButton.onClick.AddListener(ToggleMute);
    }

    public void PlayAnswer(bool correct)
    {
        if (correct && correctClip != null) audioSource.PlayOneShot(correctClip);
        else if (!correct && incorrectClip != null) audioSource.PlayOneShot(incorrectClip);
        else if (answerClip != null) audioSource.PlayOneShot(answerClip);
    }

    public void PlayHint()
    {
        if (hintClip != null) audioSource.PlayOneShot(hintClip);
    }

    public void PlayClick()
    {
        if (clickClip != null) audioSource.PlayOneShot(clickClip);
    }

    public void PlayLevelComplete()
    {
        if (levelCompleteClip != null) audioSource.PlayOneShot(levelCompleteClip);
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
        if (musicSource != null) musicSource.mute = IsMuted;
        if (muteLabel != null) muteLabel.text = IsMuted ? "Sound: Off" : "Sound: On";
    }
}
