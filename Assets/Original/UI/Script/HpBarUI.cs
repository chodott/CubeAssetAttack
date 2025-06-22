using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : PoolingObject
{
    [SerializeField]
    private Slider _slider;
    private Camera mainCamera;
    [SerializeField]
    private float _yGap = 1.5f;
    protected void Start()
    {
        mainCamera = Camera.main;
    }

    public void UpdateHP(float curHp)
    {
        _slider.value = curHp;
    }

    public void SetPosition(Transform parentTransform)
    {
        transform.position = parentTransform.position + parentTransform.up * _yGap;
    }

    protected void LateUpdate()
    {
        Vector3 directionVector = mainCamera.transform.position - transform.position;
        directionVector.z = 0;
        transform.rotation = quaternion.LookRotation(directionVector, Vector3.up);
        transform.Rotate(-50.0f, 180.0f, 0, Space.Self);
    }
}
