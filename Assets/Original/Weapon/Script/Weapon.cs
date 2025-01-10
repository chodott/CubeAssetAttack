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

    public void Launch(Transform launchTransform)
    {
        Vector3 directionVector = launchTransform.forward;
        GameObject bullet = Instantiate<GameObject>(_weaponInfo.Bullet);
        bullet.transform.position = launchTransform.position + directionVector;
        bullet.transform.forward = directionVector;
    }

    public void Equipped(Transform parentTransform)
    {
        transform.position = _weaponInfo.EquipPosition;
        transform.localRotation = _weaponInfo.EquipRotation;
        transform.SetParent(parentTransform,false);
    }
}
