using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private WeaponData _weaponData;
    [SerializeField]
    private Transform _muzzleTransform;
    private float _reloadSaveTime;
    private bool _bCanLaunch = true;

    protected void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _weaponData.ShootAudioClip;
    }

    protected void Update()
    {
        //Reload
        if(_bCanLaunch == false)
        {
            _reloadSaveTime += Time.deltaTime;
            if(_reloadSaveTime >= _weaponData.ReloadTime)
            {
                _bCanLaunch = true;
                _reloadSaveTime = 0.0f;
            }
        }
    }

    public void Launch(Transform targetTransform)
    {
        if (_bCanLaunch == false) return;

        Vector3 directionVector = targetTransform.position - _muzzleTransform.position;
        Vector3 spawnPos = _muzzleTransform.position;

        _audioSource.Play();
        GameObject bullet = Instantiate<GameObject>(_weaponData.Bullet, spawnPos, Quaternion.LookRotation(directionVector));
        bullet.GetComponent<Bullet>().Power = _weaponData.Power;
        _bCanLaunch = false;
        
    }

    public int GetWeaponType()
    {
        return _weaponData.WeaponType;
    }
}
