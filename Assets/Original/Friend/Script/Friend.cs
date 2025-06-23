using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Friend : PoolingObject
{
    private Weapon _equippedWeapon;
    private Transform _targetTransform;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _havingWeaponTransform;
    [SerializeField]
    private Transform _attackRangeTransform;

    [SerializeField]
    private AudioSource _audioSource;

    private FriendTowerData _data;
    public FriendTowerData Data {  get { return _data; } set { value = _data; } }
    [SerializeField]

    private Slider _hpUI;

    private const float RANGE_DISPLAY_SCALE = 2.0f;
    private float _hp;
    private float _attackRange = 5.0f;
    private float _rotateSpeed = 10.0f;
    protected void Start()
    {
        _animator.SetFloat("Angle", 0.0f);
        Vector3 firstPos = new Vector3(Camera.main.transform.position.x, transform.position.y,Camera.main.transform.position.z);
        transform.LookAt(firstPos);

        _attackRangeTransform.localScale *= _attackRange * RANGE_DISPLAY_SCALE;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        bool value = CheckTargetInRange();
        if (value == false)
        {
            SetTarget();
        }
        
        if (_targetTransform == null) return;
        TurnToTarget(Time.deltaTime);
        _equippedWeapon.Launch(_targetTransform);

    }

    private void SetTarget()
    {
        Vector3 pos = transform.position;
        Collider[] enemys = Physics.OverlapCapsule(pos, pos - transform.up, _attackRange);
        float maxValue = 0.0f;
        foreach (Collider collider in enemys)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            if (enemy == null) continue;
            //적 진행도 파악
            float value = enemy.GetProgress();
            if (maxValue > value) continue;
            maxValue = value;
            _targetTransform = enemy.HeadTransform;
        }
    }

    private void TurnToTarget(float deltaTime)
    {
        Vector3 directionVector = _targetTransform.position  - (transform.position + transform.up * 0.5f);
        Vector3 directionXZ = directionVector;
        directionXZ.y = 0;
        Quaternion curRotation =  Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionXZ), deltaTime * _rotateSpeed);
        transform.rotation = curRotation;


        float angle = Vector3.Angle(directionXZ, directionVector);
        _animator.SetFloat("Angle", angle/ 45.0f);

    }

    private bool CheckTargetInRange()
    {
        if (_targetTransform == null) return false;
        Vector3 position2D = transform.position;
        position2D.y = 0;
        Vector3 targetPosition2D = _targetTransform.position;
        targetPosition2D.y = 0;

        float distance2D = Vector3.Distance(position2D, targetPosition2D);
        if (distance2D < _attackRange) return true;

        _targetTransform = null;
        return false;
    }

    public void Initialize(FriendTowerData data)
    {
        SetMeshState(false);
        _data = data;
        SetMeshState(true);
        _animator.SetFloat("Angle", -1.0f);
        _animator.SetInteger("Type", _equippedWeapon.GetWeaponType());
        _audioSource.clip = data.SpawnAudioClip;
        _audioSource.Play();
    }

    public void SetMeshState(bool value)
    {
        if (_data == null) return;
        int type = (int)_data.Type;
        transform.GetChild(type).gameObject.SetActive(value);
        transform.GetChild(20 + type).gameObject.SetActive(value);

        GameObject weaponObject = _havingWeaponTransform.GetChild(type + 3).gameObject;
        weaponObject.SetActive(value);
        _equippedWeapon = weaponObject.GetComponent<Weapon>();

    }

    public void OnSelected()
    {
        _attackRangeTransform.gameObject.SetActive(true);
    }

    public void OnUnselected()
    {
        _attackRangeTransform.gameObject.SetActive(false);
    }
}
