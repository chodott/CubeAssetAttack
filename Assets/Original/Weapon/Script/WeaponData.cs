using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private AudioClip _shootAudioClip;
    public AudioClip ShootAudioClip { get { return _shootAudioClip; }}

    [SerializeField]
    private GameObject _bulletObject;
    public GameObject Bullet { get { return _bulletObject; } }
    [SerializeField]
    private string _name;
    public string Name { get { return _name; } }
    [SerializeField]
    private float reloadTime;
    public float ReloadTime { get { return reloadTime; } }
    [SerializeField]
    private int power;
    public int Power { get { return power; } }

    [SerializeField]
    private int _weaponType;
    public int WeaponType { get { return _weaponType; } }
    

}
