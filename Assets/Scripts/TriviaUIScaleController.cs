using UnityEngine;
using UnityEngine.UI;

public sealed class TriviaUIScaleController : MonoBehaviour
{
    [SerializeField] private Slider scaleSlider;
    [SerializeField] private Text scaleLabel;
    [SerializeField] private Canvas canvas;

    private const float MinimumScale = 0.85f;
    private const float MaximumScale = 1.10f;

    private void Awake()
    {
        if (canvas == null) canvas = FindObjectOfType<Canvas>();
        if (scaleSlider == null) scaleSlider = GameObject.Find("UIScaleSlider")?.GetComponent<Slider>();
        if (scaleLabel == null) scaleLabel = GameObject.Find("UIScaleText")?.GetComponent<Text>();
        if (scaleSlider == null) return;

        scaleSlider.minValue = MinimumScale;
        scaleSlider.maxValue = MaximumScale;
        scaleSlider.wholeNumbers = false;
        scaleSlider.onValueChanged.RemoveListener(ApplyScale);
        scaleSlider.onValueChanged.AddListener(ApplyScale);
        ApplyScale(scaleSlider.value);
    }

    private void ApplyScale(float value)
    {
        if (canvas != null) canvas.transform.localScale = Vector3.one * value;
        if (scaleLabel != null) scaleLabel.text = "UI Size: " + Mathf.RoundToInt(value * 100f) + "%";
    }
}
