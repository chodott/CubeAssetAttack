using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private ScriptableWeapon _weaponInfo;
    [SerializeField]
    private Transform _muzzleTransform;
    private float _reloadSaveTime;
    private bool _bCanLaunch = true;

    protected void Update()
    {
        //Reload
        if(_bCanLaunch == false)
        {
            _reloadSaveTime += Time.deltaTime;
            if(_reloadSaveTime >= _weaponInfo.ReloadTime)
            {
                _bCanLaunch = true;
                _reloadSaveTime = 0.0f;
            }
        }
    }

    public void Launch(Transform targetTransform)
    {
        if (_bCanLaunch == false) return;

        //Vector3 directionVector = launchTransform.forward;
        //Vector3 spawnPos = launchTransform.position + directionVector * 2;


        //new type
        Vector3 directionVector = targetTransform.position - _muzzleTransform.position;
        Vector3 spawnPos = _muzzleTransform.position;


        GameObject bullet = Instantiate<GameObject>(_weaponInfo.Bullet, spawnPos, Quaternion.LookRotation(directionVector));
        bullet.GetComponent<Bullet>().Power = _weaponInfo.Power;
        _bCanLaunch = false;


    }
    public void Equipped(Transform parentTransform)
    {
        transform.position = _weaponInfo.EquipPosition;
        transform.localRotation = _weaponInfo.EquipRotation;
        transform.SetParent(parentTransform,false);
    }

    public int GetWeaponType()
    {
        return _weaponInfo.WeaponType;
    }
}
