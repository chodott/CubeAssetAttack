using System.Security.Cryptography;
using UnityEngine;

public class LODShadowController : MonoBehaviour
{

    private LODGroup lodGroup;
    public GameObject shadowCube; // Only Shadow 큐브
    private void Start()
    {
        lodGroup = GetComponent<LODGroup>();
        int lodIndex = GetCurrentLODIndex();

        if (lodIndex == 2) // LOD2일 경우
        {
            shadowCube.SetActive(false); // 그림자 출력 비활성화
        }
        else
        {
            shadowCube.SetActive(true); // 다른 LOD에서는 그림자 활성화
        }
    }

    private int GetCurrentLODIndex()
    {
        float relativeHeight = Camera.main.WorldToViewportPoint(transform.position).z;
        LOD[] lods = lodGroup.GetLODs();

        for (int i = 0; i < lods.Length; i++)
        {
            if (relativeHeight >= lods[i].screenRelativeTransitionHeight)
                return i;
        }

        return lods.Length - 1;
    }
}
