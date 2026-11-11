using UnityEngine;

public sealed class TriviaSafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    private void Update()
    {
        if (Screen.safeArea != lastSafeArea) ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        lastSafeArea = Screen.safeArea;
        if (rectTransform == null) return;
        var anchorMin = lastSafeArea.position;
        var anchorMax = lastSafeArea.position + lastSafeArea.size;
        anchorMin.x /= Screen.width; anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width; anchorMax.y /= Screen.height;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
