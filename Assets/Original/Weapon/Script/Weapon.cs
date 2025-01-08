using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ScriptableWeapon _weaponInfo;
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void Launch()
    {
        Vector3 directionVector = transform.forward;
        GameObject bullet = Instantiate<GameObject>(_weaponInfo.Bullet);
        bullet.transform.position = transform.position + directionVector;
        bullet.transform.forward = directionVector;
    }

    public void Equipped(Transform parentTransform)
    {
        transform.position = _weaponInfo.EquipPosition;
        transform.localRotation = _weaponInfo.EquipRotation;
        transform.SetParent(parentTransform,false);
    }
}
