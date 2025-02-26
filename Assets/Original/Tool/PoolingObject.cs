using UnityEngine;

public class PoolingObject : MonoBehaviour
{
    [SerializeField]
    public GameObject Prefab;

    public void Activate(GameObject originalPrefab)
    {
        GetComponent<PoolingObject>().Prefab = originalPrefab;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

    }
}
