using UnityEngine;

public static class TriviaSceneLookup
{
    public static GameObject FindIn(string parentName, string objectName)
    {
        var parent = Find(parentName);
        if (parent != null)
            foreach (var transform in parent.GetComponentsInChildren<Transform>(true))
                if (transform.name == objectName) return transform.gameObject;
        return Find(objectName);
    }

    public static GameObject Find(string objectName)
    {
        var active = GameObject.Find(objectName);
        if (active != null) return active;
        foreach (var transform in Object.FindObjectsOfType<Transform>(true))
            if (transform.name == objectName) return transform.gameObject;
        return null;
    }
}
