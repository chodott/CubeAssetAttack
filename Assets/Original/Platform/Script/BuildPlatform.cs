using UnityEngine;

public class BuildPlatform : MonoBehaviour
{
    private Friend _builtFriend;
    public Friend BuiltFriend { get; set; }
    public bool bCanBuild
    {
        get
        {
            return _builtFriend == null ? true : false;
        }
    }

}
