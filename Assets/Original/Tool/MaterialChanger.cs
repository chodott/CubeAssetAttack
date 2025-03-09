using UnityEngine;
using UnityEngine.UI;

public class MaterialChanger : MonoBehaviour
{
    public Material customMaterial;  // 적용할 커스텀 머테리얼

    void Start()
    {
        Graphic[] uiElements = GetComponentsInChildren<Graphic>(); // 모든 UI 요소 가져오기
        foreach (Graphic graphic in uiElements)
        {
            graphic.material = customMaterial; // 커스텀 머테리얼 적용
        }
    }
}
