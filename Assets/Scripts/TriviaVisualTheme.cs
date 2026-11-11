using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaVisualTheme : MonoBehaviour
{
    [SerializeField] private Color background = new Color(0.035f, 0.055f, 0.12f, 1f);
    [SerializeField] private Color buttonColor = new Color(0.12f, 0.32f, 0.55f, 1f);
    [SerializeField] private Color textColor = new Color(0.95f, 0.97f, 1f, 1f);

    private void Awake()
    {
        var canvas = FindObjectOfType<Canvas>();
        if (canvas == null) return;
        var backgroundObject = canvas.transform.Find("Background")?.gameObject;
        if (backgroundObject == null)
        {
            backgroundObject = new GameObject("Background", typeof(RectTransform), typeof(Image));
            backgroundObject.transform.SetParent(canvas.transform, false);
            backgroundObject.transform.SetAsFirstSibling();
            var rect = backgroundObject.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero; rect.anchorMax = Vector2.one; rect.offsetMin = Vector2.zero; rect.offsetMax = Vector2.zero;
        }
        backgroundObject.GetComponent<Image>().color = background;
        foreach (var image in canvas.GetComponentsInChildren<Image>(true))
            if (image.GetComponent<Button>() != null) image.color = buttonColor;
        foreach (var text in canvas.GetComponentsInChildren<Text>(true))
            text.color = textColor;
    }
}
