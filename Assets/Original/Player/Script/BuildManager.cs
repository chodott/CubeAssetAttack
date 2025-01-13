using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    public Transform buildTransform;

    protected void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void Build(GameObject targetPrefab)
    {
        GameObject buildTarget = Instantiate(targetPrefab,buildTransform);
        buildTarget.transform.position = buildTransform.position + Vector3.up * 0.5f;
    }

    public void Check()
    {
        Debug.Log("NoProb");
    }

}
