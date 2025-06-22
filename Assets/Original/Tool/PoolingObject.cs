using UnityEngine;

public abstract class PoolingObject : MonoBehaviour
{
    [SerializeField]
    public GameObject Prefab;

    public void Activate(GameObject originalPrefab)
    {
        Prefab = originalPrefab;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

    }
}
