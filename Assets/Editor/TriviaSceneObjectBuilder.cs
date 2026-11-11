using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class TriviaSceneObjectBuilder
{
    [MenuItem("Bible Trivia/Rebuild Scene Game Objects")]
    public static void RebuildObjects()
    {
        var names = new[] { "TriviaGameCanvas", "TriviaLevelSelector", "TriviaScreenNavigator", "TriviaVisualTheme", "TriviaPortraitLayout", "TriviaAudioManager", "TriviaHintManager", "TriviaDatabaseStatus", "TriviaCategorySelector", "TriviaQuestionDatabase", "TriviaGameController" };
        foreach (var name in names)
        {
            var objects = Object.FindObjectsOfType<GameObject>(true);
            foreach (var target in objects)
                if (target.name == name) Undo.DestroyObjectImmediate(target);
        }
        CreateObjects();
    }

    [MenuItem("Bible Trivia/Create Scene Game Objects")]
    public static void CreateObjects()
    {
        EnsureEventSystem();
        var canvas = Object.FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            var root = new GameObject("TriviaGameCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = root.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            root.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            root.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 1920);
            root.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            root.GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
        }

        CreatePanel(canvas.transform, "MainMenuPanel");
        CreatePanel(canvas.transform, "LevelPanel");
        CreatePanel(canvas.transform, "GamePanel");
        CreateButton(canvas.transform, "PlayButton", "Play", new Vector2(0, 500));
        CreateButton(canvas.transform, "LevelsButton", "Levels", new Vector2(0, 330));
        CreateButton(canvas.transform, "BackButton", "Back", new Vector2(-400, 700));
        CreateButton(canvas.transform, "MuteButton", "Sound: On", new Vector2(400, 700));

        CreateText(canvas.transform, "QuestionText", "Question", new Vector2(0, 450));
        CreateText(canvas.transform, "ReferenceText", "Reference", new Vector2(0, 300));
        CreateText(canvas.transform, "FeedbackText", "Feedback", new Vector2(0, -500));
        CreateText(canvas.transform, "ScoreText", "Score: 0", new Vector2(-350, 700));
        CreateText(canvas.transform, "ProgressText", "Question 1", new Vector2(350, 700));
        CreateText(canvas.transform, "AchievementText", "Stars: 0", new Vector2(0, -600));
        CreateText(canvas.transform, "HintText", "", new Vector2(0, -670));
        CreateButton(canvas.transform, "HintButton", "Use Hint", new Vector2(0, -760));
        CreateButton(canvas.transform, "OldTestamentButton", "Old Testament", new Vector2(-220, -900));
        CreateButton(canvas.transform, "NewTestamentButton", "New Testament", new Vector2(220, -900));
        CreateText(canvas.transform, "DatabaseStatusText", "Loading database...", new Vector2(0, -900));
        for (var i = 0; i < 4; i++) CreateButton(canvas.transform, "AnswerButton" + (i + 1), "Answer", new Vector2(0, 150 - i * 180));
        CreateButton(canvas.transform, "NextButton", "Next Question", new Vector2(0, -700));
        CreateText(canvas.transform, "LevelStatusText", "Select a level", new Vector2(0, -850));
        for (var i = 0; i < 10; i++) CreateButton(canvas.transform, "LevelButton" + (i + 1), "Level " + (i + 1), new Vector2(-420 + (i % 5) * 210, -1000 - (i / 5) * 140));
        var selectorObject = FindAny("TriviaLevelSelector") ?? new GameObject("TriviaLevelSelector");
        if (selectorObject.GetComponent<TriviaLevelSelector>() == null) selectorObject.AddComponent<TriviaLevelSelector>();
        var navigatorObject = FindAny("TriviaScreenNavigator") ?? new GameObject("TriviaScreenNavigator");
        if (navigatorObject.GetComponent<TriviaScreenNavigator>() == null) navigatorObject.AddComponent<TriviaScreenNavigator>();
        var themeObject = FindAny("TriviaVisualTheme") ?? new GameObject("TriviaVisualTheme");
        if (themeObject.GetComponent<TriviaVisualTheme>() == null) themeObject.AddComponent<TriviaVisualTheme>();
        var layoutObject = FindAny("TriviaPortraitLayout") ?? new GameObject("TriviaPortraitLayout");
        if (layoutObject.GetComponent<TriviaPortraitLayout>() == null) layoutObject.AddComponent<TriviaPortraitLayout>();
        if (canvas.GetComponent<TriviaSafeArea>() == null) canvas.gameObject.AddComponent<TriviaSafeArea>();
        var audioObject = FindAny("TriviaAudioManager") ?? new GameObject("TriviaAudioManager");
        if (audioObject.GetComponent<TriviaAudioManager>() == null) audioObject.AddComponent<TriviaAudioManager>();
        var hintObject = FindAny("TriviaHintManager") ?? new GameObject("TriviaHintManager");
        if (hintObject.GetComponent<TriviaHintManager>() == null) hintObject.AddComponent<TriviaHintManager>();
        var statusObject = FindAny("TriviaDatabaseStatus") ?? new GameObject("TriviaDatabaseStatus");
        if (statusObject.GetComponent<TriviaDatabaseStatus>() == null) statusObject.AddComponent<TriviaDatabaseStatus>();
        var categoryObject = FindAny("TriviaCategorySelector") ?? new GameObject("TriviaCategorySelector");
        if (categoryObject.GetComponent<TriviaCategorySelector>() == null) categoryObject.AddComponent<TriviaCategorySelector>();
        var databaseObject = FindAny("TriviaQuestionDatabase") ?? new GameObject("TriviaQuestionDatabase");
        if (databaseObject.GetComponent<TriviaQuestionDatabase>() == null) databaseObject.AddComponent<TriviaQuestionDatabase>();
        var controllerObject = FindAny("TriviaGameController") ?? new GameObject("TriviaGameController");
        if (controllerObject.GetComponent<TriviaGameController>() == null) controllerObject.AddComponent<TriviaGameController>();
        Parent("PlayButton", "MainMenuPanel"); Parent("LevelsButton", "MainMenuPanel");
        Parent("LevelStatusText", "LevelPanel"); Parent("BackButton", "LevelPanel");
        for (var i = 1; i <= 10; i++) Parent("LevelButton" + i, "LevelPanel");
        Parent("QuestionText", "GamePanel"); Parent("ReferenceText", "GamePanel"); Parent("ScoreText", "GamePanel"); Parent("ProgressText", "GamePanel");
        Parent("FeedbackText", "GamePanel"); Parent("AchievementText", "GamePanel"); Parent("HintText", "GamePanel"); Parent("HintButton", "GamePanel"); Parent("NextButton", "GamePanel");
        Parent("MuteButton", "GamePanel"); Parent("DatabaseStatusText", "MainMenuPanel");
        Parent("OldTestamentButton", "LevelPanel"); Parent("NewTestamentButton", "LevelPanel");
        for (var i = 1; i <= 4; i++) Parent("AnswerButton" + i, "GamePanel");
        // Apply layout after parenting so positions are local to their actual screen panels.
        SetRect("BackButton", "LevelPanel", new Vector2(0, 700), new Vector2(420, 90));
        SetRect("OldTestamentButton", "LevelPanel", new Vector2(-220, -650), new Vector2(360, 90));
        SetRect("NewTestamentButton", "LevelPanel", new Vector2(220, -650), new Vector2(360, 90));
        for (var i = 1; i <= 10; i++)
            SetRect("LevelButton" + i, "LevelPanel", new Vector2((((i - 1) % 5) - 2) * 180, 180 - ((i - 1) / 5) * 120), new Vector2(160, 80));
        RemoveMisparented("PlayButton", "MainMenuPanel");
        RemoveMisparented("LevelsButton", "MainMenuPanel");
        RemoveMisparented("DatabaseStatusText", "MainMenuPanel");
        RemoveMisparented("LevelStatusText", "LevelPanel");
        RemoveMisparented("BackButton", "LevelPanel");
        for (var i = 1; i <= 10; i++) RemoveMisparented("LevelButton" + i, "LevelPanel");
        RemoveMisparented("OldTestamentButton", "LevelPanel");
        RemoveMisparented("NewTestamentButton", "LevelPanel");
        foreach (var name in new[] { "QuestionText", "ReferenceText", "ScoreText", "ProgressText", "FeedbackText", "AchievementText", "HintText", "HintButton", "NextButton", "MuteButton" })
            RemoveMisparented(name, "GamePanel");
        for (var i = 1; i <= 4; i++) RemoveMisparented("AnswerButton" + i, "GamePanel");
        // Keep the edit-time scene readable; the navigator enables the active screen at runtime.
        FindAny("MainMenuPanel")?.SetActive(true);
        FindAny("LevelPanel")?.SetActive(false);
        FindAny("GamePanel")?.SetActive(false);
        Debug.Log("Created persistent trivia scene objects. Save the scene to keep them.");
    }

    private static void EnsureEventSystem()
    {
        if (Object.FindObjectOfType<EventSystem>() != null) return;
        var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        Undo.RegisterCreatedObjectUndo(eventSystem, "Create UI EventSystem");
    }

    private static GameObject FindAny(string name)
    {
        foreach (var transform in Object.FindObjectsOfType<Transform>(true))
            if (transform.name == name) return transform.gameObject;
        return null;
    }

    private static void Parent(string childName, string parentName)
    {
        var child = FindAny(childName); var parent = FindAny(parentName);
        if (child != null && parent != null) child.transform.SetParent(parent.transform, false);
    }

    private static void SetRect(string objectName, string parentName, Vector2 position, Vector2 size)
    {
        var objectToPlace = FindAny(objectName);
        var expectedParent = FindAny(parentName);
        if (objectToPlace == null || expectedParent == null) return;
        var rect = objectToPlace.GetComponent<RectTransform>();
        if (rect == null) return;
        rect.SetParent(expectedParent.transform, false);
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;
    }

    private static void RemoveMisparented(string objectName, string expectedParentName)
    {
        var expectedParent = FindAny(expectedParentName);
        if (expectedParent == null) return;
        foreach (var transform in Object.FindObjectsOfType<Transform>(true))
            if (transform.name == objectName && !transform.IsChildOf(expectedParent.transform))
                Undo.DestroyObjectImmediate(transform.gameObject);
    }

    private static void CreatePanel(Transform parent, string name)
    {
        if (FindAny(name) != null) return;
        var panel = new GameObject(name, typeof(RectTransform), typeof(Image));
        panel.transform.SetParent(parent, false);
        var rect = panel.GetComponent<RectTransform>(); rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = Vector2.zero; rect.offsetMax = Vector2.zero;
        panel.GetComponent<Image>().color = new Color(0.05f, 0.08f, 0.15f, 0.92f);
    }

    private static void CreateText(Transform parent, string name, string value, Vector2 position)
    {
        var existing = FindAny(name);
        if (existing != null)
        {
            var existingText = existing.GetComponent<Text>();
            if (existingText != null) existingText.fontSize = name == "QuestionText" ? 42 : 32;
            return;
        }
        var go = new GameObject(name, typeof(RectTransform), typeof(Text));
        go.transform.SetParent(parent, false); var rect = go.GetComponent<RectTransform>(); rect.anchoredPosition = position; rect.sizeDelta = new Vector2(900, 120);
        var text = go.GetComponent<Text>(); text.text = value; text.alignment = TextAnchor.MiddleCenter; text.horizontalOverflow = HorizontalWrapMode.Wrap; text.verticalOverflow = VerticalWrapMode.Truncate; text.fontSize = name == "QuestionText" ? 42 : 32;
    }

    private static void CreateButton(Transform parent, string name, string value, Vector2 position)
    {
        var existing = FindAny(name);
        if (existing != null)
        {
            var existingLabel = existing.transform.Find("Label")?.GetComponent<Text>();
            if (existingLabel != null) existingLabel.fontSize = 36;
            return;
        }
        var go = new GameObject(name, typeof(RectTransform), typeof(Image), typeof(Button)); go.transform.SetParent(parent, false);
        var rect = go.GetComponent<RectTransform>(); rect.anchoredPosition = position; rect.sizeDelta = new Vector2(800, 120);
        var label = new GameObject("Label", typeof(RectTransform), typeof(Text)); label.transform.SetParent(go.transform, false); var labelRect = label.GetComponent<RectTransform>(); labelRect.anchorMin = Vector2.zero; labelRect.anchorMax = Vector2.one; labelRect.offsetMin = new Vector2(20, 10); labelRect.offsetMax = new Vector2(-20, -10); var t = label.GetComponent<Text>(); t.text = value; t.alignment = TextAnchor.MiddleCenter; t.horizontalOverflow = HorizontalWrapMode.Overflow; t.verticalOverflow = VerticalWrapMode.Truncate; t.fontSize = 36; t.color = Color.black;
    }
}
