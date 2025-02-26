using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableFriend", menuName = "Scriptable Objects/ScriptableFriend")]
public class ScriptableFriend : ScriptableObject
{
    public enum EFriendType
    { 
      Girl = 0,
      Ailen =1
    }

    [SerializeField]
    private Texture2D _thumbnail;
    public Texture2D Thumbnail {get{return _thumbnail;}}

    [SerializeField]
    private string _name;
    public string Name { get { return _name; } }

    [SerializeField]
    private float _attackRange;
    public float Range { get { return _attackRange; } }

    [SerializeField]
    private EFriendType _type;
    public EFriendType Type { get { return _type; } }

    [SerializeField]
    private int _cost;
    public int COST { get { return _cost; } }

    [SerializeField]
    private float _maxHP;
    public float MaxHP { get { return _maxHP; } }

}
