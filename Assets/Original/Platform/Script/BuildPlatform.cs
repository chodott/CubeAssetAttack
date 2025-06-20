using UnityEngine;

public class BuildPlatform : MonoBehaviour
{
    private Friend _builtFriend;
    [SerializeField]
    private GameObject _buildEffectObject;
    public bool CanBuild
    {
        get
        {
            return _builtFriend == null ? true : false;
        }
    }

    public void SetBuildEffect(bool value)
    {
        if (!CanBuild) value = false;
        _buildEffectObject.SetActive(value);
    }

    public void BuildOnPlatform(Friend friend)
    {
        _builtFriend = friend;
    }

    public int SellOnPlatform()
    {
        if (CanBuild) return 0;

        int value = _builtFriend.Data.COST;
        SpawnManager.Instance.GetBack(_builtFriend.gameObject);
        _builtFriend = null;
        return value;
    }
}
