using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    public Transform BuildPlatformTransform;

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
        BuildPlatform buildPlatform = BuildPlatformTransform.GetComponent<BuildPlatform>();
        if (buildPlatform.bCanBuild == false) return;
        GameObject buildTarget = Instantiate(friendData._SpawnObject);
        buildTarget.transform.position = BuildPlatformTransform.position + Vector3.up * 0.25f;
        Friend builtFriend = buildTarget.GetComponent<Friend>();
        builtFriend.Data = friendData;
        buildPlatform.BuiltFriend = builtFriend;
    }
}
