using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaPortraitLayout : MonoBehaviour
{
    private const float ReferenceWidth = 1080f;
    private const float ReferenceHeight = 1920f;

    private void Awake()
    {
        var scaler = FindObjectOfType<CanvasScaler>();
        if (scaler != null)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(ReferenceWidth, ReferenceHeight);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1f;
        }
        ApplyLayout();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (isActiveAndEnabled) ApplyLayout();
    }

    private void ApplyLayout()
    {
        Set("QuestionText", new Vector2(0, 436), new Vector2(820, 80));
        Set("ReferenceText", new Vector2(0, 360), new Vector2(820, 45));
        Set("ScoreText", new Vector2(-300, 455), new Vector2(260, 50));
        Set("ProgressText", new Vector2(300, 455), new Vector2(260, 50));
        Set("MuteButton", new Vector2(300, 525), new Vector2(300, 70));
        Set("AnswerButton1", new Vector2(0, 254), new Vector2(820, 82));
        Set("AnswerButton2", new Vector2(0, 148), new Vector2(820, 82));
        Set("AnswerButton3", new Vector2(0, 42), new Vector2(820, 82));
        Set("AnswerButton4", new Vector2(0, -64), new Vector2(820, 82));
        Set("FeedbackText", new Vector2(0, -146), new Vector2(820, 70));
        Set("HintButton", new Vector2(-220, -216), new Vector2(330, 70));
        Set("NextButton", new Vector2(220, -216), new Vector2(330, 70));
        foreach (var text in FindObjectsOfType<Text>(true))
        {
            if (text.transform.IsChildOf(transform)) continue;
            if (text.name == "QuestionText")
            {
                text.fontSize = 42;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 26;
                text.resizeTextMaxSize = 42;
            }
            if (text.GetComponentInParent<Button>() != null)
            {
                var labelRect = text.rectTransform;
                labelRect.anchorMin = Vector2.zero;
                labelRect.anchorMax = Vector2.one;
                labelRect.offsetMin = new Vector2(20, 10);
                labelRect.offsetMax = new Vector2(-20, -10);
                text.fontSize = 36;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 20;
                text.resizeTextMaxSize = 36;
                text.horizontalOverflow = HorizontalWrapMode.Overflow;
                text.verticalOverflow = VerticalWrapMode.Truncate;
            }
            else
            {
                text.fontSize = 32;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 18;
                text.resizeTextMaxSize = 32;
            }
        }
    }

    private static void Set(string objectName, Vector2 position, Vector2 size)
    {
        var target = GameObject.Find(objectName);
        if (target == null) return;
        var rect = target.GetComponent<RectTransform>();
        if (rect == null) return;
        rect.anchorMin = rect.anchorMax = new Vector2(.5f, .5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;
    }
}
