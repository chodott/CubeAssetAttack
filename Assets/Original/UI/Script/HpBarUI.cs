using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class HpBarUI : MonoBehaviour
{
    private Camera mainCamera;
    protected void Start()
    {
        mainCamera = Camera.main;
    }

    protected void LateUpdate()
    {
        Vector3 directionVector = mainCamera.transform.position - transform.position;
        directionVector.z = 0;
        transform.rotation = quaternion.LookRotation(directionVector, Vector3.up);
        transform.Rotate(-50.0f, 180.0f, 0, Space.Self);
    }
}
