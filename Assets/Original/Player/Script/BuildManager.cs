using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private Friend _builtFriend;
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
        if (_builtFriend != null) return;
        GameObject buildTarget = Instantiate(friendData._SpawnObject);
        buildTarget.transform.position = buildTransform.position + Vector3.up * 0.25f;
        _builtFriend = buildTarget.GetComponent<Friend>();
        _builtFriend.Data = friendData;
    }
}
