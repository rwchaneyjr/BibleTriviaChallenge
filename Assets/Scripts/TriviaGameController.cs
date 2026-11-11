using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public sealed class TriviaGameController : MonoBehaviour
{
    [Header("Scene objects")]
    [SerializeField] private Text questionText;
    [SerializeField] private Text referenceText;
    [SerializeField] private Text feedbackText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text progressText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private Button nextButton;
    [SerializeField] private TriviaAudioManager audioManager;
    [SerializeField] private bool dailyChallenge;
    [SerializeField] private Text achievementText;

    [Header("Data")]
    [SerializeField] private TriviaQuestionDatabase database;
    [SerializeField] private string categoryFilter;
    [SerializeField] private string difficultyFilter;
    private readonly List<TriviaQuestion> roundQuestions = new();
    private TriviaQuestion currentQuestion;
    private int questionIndex;
    private int score;
    private bool answered;
    private bool hintUsed;

    public void BeginLevel(string difficulty)
    {
        if (database == null || database.Questions.Count == 0) return;
        difficultyFilter = difficulty;
        questionIndex = 0;
        roundQuestions.Clear();
        roundQuestions.AddRange(database.Filter(categoryFilter, difficultyFilter));
        ShuffleQuestions(roundQuestions);
        if (roundQuestions.Count == 0)
        {
            roundQuestions.AddRange(database.Questions);
            ShuffleQuestions(roundQuestions);
        }
        if (dailyChallenge) questionIndex = TriviaDailyChallenge.QuestionIndex(roundQuestions.Count);
        ShowQuestion(roundQuestions[questionIndex]);
    }

    public void BeginCategory(string category)
    {
        if (database == null || database.Questions.Count == 0) return;
        categoryFilter = category;
        difficultyFilter = string.Empty;
        questionIndex = 0;
        roundQuestions.Clear();
        roundQuestions.AddRange(database.Filter(categoryFilter, difficultyFilter));
        ShuffleQuestions(roundQuestions);
        if (roundQuestions.Count > 0) ShowQuestion(roundQuestions[0]);
    }

    private void Start()
    {
        EnsureEventSystem();
        score = TriviaProgress.Score;
        if (database == null) database = FindObjectOfType<TriviaQuestionDatabase>();
        if (audioManager == null) audioManager = FindObjectOfType<TriviaAudioManager>();
        if (achievementText == null) achievementText = GameObject.Find("AchievementText")?.GetComponent<Text>();
        questionText = TriviaSceneLookup.FindIn("GamePanel", "QuestionText")?.GetComponent<Text>() ?? questionText;
        referenceText = TriviaSceneLookup.FindIn("GamePanel", "ReferenceText")?.GetComponent<Text>() ?? referenceText;
        feedbackText = TriviaSceneLookup.FindIn("GamePanel", "FeedbackText")?.GetComponent<Text>() ?? feedbackText;
        scoreText = TriviaSceneLookup.FindIn("GamePanel", "ScoreText")?.GetComponent<Text>() ?? scoreText;
        progressText = TriviaSceneLookup.FindIn("GamePanel", "ProgressText")?.GetComponent<Text>() ?? progressText;
        nextButton = TriviaSceneLookup.FindIn("GamePanel", "NextButton")?.GetComponent<Button>() ?? nextButton;
        {
            answerButtons = new Button[4];
            var gamePanel = TriviaSceneLookup.Find("GamePanel");
            for (var i = 0; i < 4; i++)
            {
                var name = "AnswerButton" + (i + 1);
                if (gamePanel != null)
                    foreach (var button in gamePanel.GetComponentsInChildren<Button>(true))
                        if (button.name == name) { answerButtons[i] = button; break; }
                if (answerButtons[i] == null) answerButtons[i] = TriviaSceneLookup.FindIn("GamePanel", name)?.GetComponent<Button>();
            }
        }
        if (nextButton != null) nextButton.onClick.AddListener(NextQuestion);
        if (database == null || database.Questions.Count == 0) return;
        roundQuestions.AddRange(database.Filter(categoryFilter, difficultyFilter));
        ShuffleQuestions(roundQuestions);
        if (roundQuestions.Count == 0)
        {
            roundQuestions.AddRange(database.Questions);
            ShuffleQuestions(roundQuestions);
        }
        ShowQuestion(roundQuestions[0]);
    }

    // Fisher-Yates gives every verse an equal chance and changes the order each time a round starts.
    private static void ShuffleQuestions(List<TriviaQuestion> items)
    {
        for (var i = items.Count - 1; i > 0; i--)
        {
            var j = Random.Range(0, i + 1);
            (items[i], items[j]) = (items[j], items[i]);
        }
    }

    private static void EnsureEventSystem()
    {
        if (EventSystem.current != null) return;
        var eventSystem = new GameObject("EventSystem", typeof(EventSystem));
        eventSystem.AddComponent<StandaloneInputModule>();
    }

    public void ShowQuestion(TriviaQuestion question)
    {
        currentQuestion = question;
        answered = false;
        hintUsed = false;
        // Show the verse first; the player must identify its reference from the buttons.
        questionText.text = question.explanation;
        referenceText.text = string.IsNullOrWhiteSpace(question.question)
            ? "Choose the Bible reference:"
            : question.question;
        feedbackText.text = string.Empty;
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (progressText != null) progressText.text = "Question " + (questionIndex + 1) + " of " + roundQuestions.Count;
        if (nextButton != null) nextButton.gameObject.SetActive(false);
        for (var i = 0; i < answerButtons.Length; i++)
        {
            var index = i;
            answerButtons[i].GetComponentInChildren<Text>().text = question.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => Answer(index));
        }
    }

    public bool UseHint()
    {
        if (hintUsed || answered || currentQuestion == null) return false;
        hintUsed = true;
        for (var i = answerButtons.Length - 1; i >= 0; i--)
            if (i != currentQuestion.correctAnswerIndex && answerButtons[i] != null)
            {
                answerButtons[i].interactable = false;
                return true;
            }
        return false;
    }

    private void Answer(int index)
    {
        if (answered) return;
        answered = true;
        if (audioManager != null) audioManager.PlayClick();
        var correct = index == currentQuestion.correctAnswerIndex;
        TriviaProgress.RecordAnswer(correct);
        var achievementMessage = TriviaAchievements.RecordAnswer(correct);
        if (audioManager != null) audioManager.PlayAnswer(correct);
        score = TriviaProgress.Score;
        feedbackText.text = correct
            ? "Correct! " + currentQuestion.explanation
            : "Not quite. " + currentQuestion.explanation;
        if (achievementText != null) achievementText.text = achievementMessage + "  Stars: " + TriviaAchievements.CurrentStars();
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (nextButton != null) nextButton.gameObject.SetActive(true);
    }

    public void NextQuestion()
    {
        if (roundQuestions.Count == 0) return;
        if (questionIndex == roundQuestions.Count - 1 && audioManager != null) audioManager.PlayLevelComplete();
        questionIndex = (questionIndex + 1) % roundQuestions.Count;
        if (audioManager != null) audioManager.PlayClick();
        ShowQuestion(roundQuestions[questionIndex]);
    }
}
