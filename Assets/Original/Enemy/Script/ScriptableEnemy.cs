using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEnemy", menuName = "Scriptable Objects/ScriptableEnemy")]
public class ScriptableEnemy : ScriptableObject
{
    [SerializeField]
    private GameObject _enemyPrefab;
    public GameObject EnemyPrefab { get { return _enemyPrefab; } }

    [SerializeField]
    private Sprite _thumbnail;
    public Sprite Thumbnail { get { return _thumbnail; } }

    [SerializeField]
    private string _name;
    public string Name { get { return _name; } }

    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } }

    [SerializeField]
    private float _power;
    public float Power { get { return _power; } }

    [SerializeField]
    private int _reward;
    public int Reward { get { return _reward; } }

    [SerializeField]
    private int _hp;
    public int HP { get { return _hp; } }
}
