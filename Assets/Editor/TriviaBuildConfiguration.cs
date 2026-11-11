using System.Linq;
using UnityEditor;
using UnityEngine;

public static class TriviaBuildConfiguration
{
    public static void BuildAndValidate()
    {
        TriviaSceneObjectBuilder.CreateObjects();
        UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
        TriviaSceneValidator.Validate();
        TriviaQuestionValidator.Validate();
        Configure();
        ValidateBuildReadiness();
    }

    [MenuItem("Bible Trivia/Configure Portrait Build")]
    public static void Configure()
    {
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
        PlayerSettings.allowedAutorotateToPortrait = true;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
        PlayerSettings.allowedAutorotateToLandscapeLeft = false;
        PlayerSettings.allowedAutorotateToLandscapeRight = false;

        var scene = AssetDatabase.FindAssets("t:Scene")
            .Select(AssetDatabase.GUIDToAssetPath)
            .FirstOrDefault(path => path.EndsWith("SampleScene.unity"));
        if (!string.IsNullOrEmpty(scene))
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(scene, true) };
        Debug.Log("Bible Trivia portrait build configured.");
    }

    [MenuItem("Bible Trivia/Validate Build Readiness")]
    public static void ValidateBuildReadiness()
    {
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).ToArray();
        if (scenes.Length == 0) Debug.LogError("No enabled scenes are configured for build.");
        else Debug.Log("Build readiness: " + scenes.Length + " enabled scene(s).");
    }
}
