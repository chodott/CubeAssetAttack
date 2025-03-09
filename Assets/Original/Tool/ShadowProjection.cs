using UnityEngine;

public class ShadowProjection : MonoBehaviour
{
    private Light dirLight;

    void Start()
    {
        dirLight = GetComponent<Light>(); // Directional Light 가져오기
    }

    void Update()
    {
        if (dirLight != null)
        {
            Shader.SetGlobalVector("_GlobalLightDir", dirLight.transform.forward);
        }
    }
}
