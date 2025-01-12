using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableFriend", menuName = "Scriptable Objects/ScriptableFriend")]
public class ScriptableFriend : ScriptableObject
{
    [SerializeField]
    private GameObject _spawnObject;
    public GameObject _SpawnObject { get { return _spawnObject; } }

    [SerializeField]
    private Texture2D _thumbnail;
    public Texture2D Thumbnail {get{return _thumbnail;}}

    [SerializeField]
    private string _name;
    public string Name { get { return _name; } }

    [SerializeField]
    private int _cost;
    public int COST { get { return _cost; } }
    
}
