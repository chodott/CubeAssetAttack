using UnityEngine;

public class DirectionalLightManager : MonoBehaviour
{
    void Update()
    {
        // Directional Light의 방향 가져오기
        Vector3 lightDirection = -transform.forward; // 라이트의 앞방향이 빛의 방향과 반대이므로 음수 처리

        // 글로벌 셰이더 변수에 설정
        Shader.SetGlobalVector("_GlobalLightDir", new Vector4(lightDirection.x, lightDirection.y, lightDirection.z, 0));
    }
}
