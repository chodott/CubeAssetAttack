using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private TextMeshProUGUI _fpsTMP;
    private float deltaTime = 0.0f;

    protected void Start()
    {
        _fpsTMP = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        _fpsTMP.text = $"FPS: {Mathf.Ceil(fps)}";
    }
}
