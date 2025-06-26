using UnityEngine;

public interface IBlobshadowTarget
{
    public float GetShadowSize();
    public bool GetActive();
    public Transform GetTransform();
}
