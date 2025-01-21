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

    public void Build(ScriptableFriend friendData)
    {
        GameObject buildTarget = Instantiate(friendData._SpawnObject);
        buildTarget.transform.position = buildTransform.position + Vector3.up * 0.5f;
        buildTarget.GetComponent<Friend>().Data = friendData;
    }
}
