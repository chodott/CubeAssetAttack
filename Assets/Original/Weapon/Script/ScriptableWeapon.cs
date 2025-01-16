using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "ScriptableWeapon", menuName = "Scriptable Objects/ScriptableWeapon")]
public class ScriptableWeapon : ScriptableObject
{
    [SerializeField]
    private GameObject _bulletObject;
    public GameObject Bullet { get { return _bulletObject; } }
    [SerializeField]
    private Vector3 _equipPosition;
    public Vector3 EquipPosition { get { return _equipPosition; } }
    [SerializeField]
    private Quaternion _equipRotation;
    public Quaternion EquipRotation { get { return _equipRotation; } }
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
