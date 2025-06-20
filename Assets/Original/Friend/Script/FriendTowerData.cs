using UnityEngine;

[CreateAssetMenu(fileName = "FriendTowerData", menuName = "Scriptable Objects/FriendTowerData")]
public class FriendTowerData : ScriptableObject
{
    public enum EFriendType
    { 
        Boy =0,
        Ailen =1,
        Girl = 2,
        Bear = 3,
        Soldier = 4,
        Chicken = 5,
        Jocker = 6,
        Box = 7,
        Cowboy = 9,
        Scientist = 13,
        Astronaut = 19
    }

    [SerializeField]
    private AudioClip _spawnAudioClip;
    public AudioClip SpawnAudioClip { get { return _spawnAudioClip; } }

    private float volume = 1.0f;
    public float Volume { get { return volume; } }

    [SerializeField]
    private Sprite _thumbnail;
    public Sprite Thumbnail {get{return _thumbnail;}}

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
