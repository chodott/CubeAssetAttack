using UnityEngine;

public class BuildPlatform : MonoBehaviour
{
    private Friend _builtFriend;
    public bool bCanBuild
    {
        get
        {
            return _builtFriend == null ? true : false;
        }
    }

    public void BuildOnPlatform(Friend friend)
    {
        _builtFriend = friend;
    }

    public int SellOnPlatform()
    {
        if (bCanBuild) return 0;

        int value = _builtFriend.Data.COST;
        SpawnManager.Instance.GetBack(_builtFriend.gameObject);
        _builtFriend = null;
        return value;
    }
}
