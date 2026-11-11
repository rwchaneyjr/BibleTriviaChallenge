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
        Set("QuestionText", new Vector2(0, 620), new Vector2(900, 150));
        Set("ReferenceText", new Vector2(0, 500), new Vector2(900, 60));
        Set("ScoreText", new Vector2(-350, 770), new Vector2(300, 60));
        Set("ProgressText", new Vector2(350, 770), new Vector2(300, 60));
        Set("AnswerButton1", new Vector2(0, 300), new Vector2(900, 105));
        Set("AnswerButton2", new Vector2(0, 170), new Vector2(900, 105));
        Set("AnswerButton3", new Vector2(0, 40), new Vector2(900, 105));
        Set("AnswerButton4", new Vector2(0, -90), new Vector2(900, 105));
        Set("FeedbackText", new Vector2(0, -230), new Vector2(900, 100));
        Set("HintButton", new Vector2(-250, -360), new Vector2(360, 85));
        Set("NextButton", new Vector2(250, -360), new Vector2(360, 85));
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
