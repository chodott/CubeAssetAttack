using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform _headTransform;
    private ScriptableEnemy _enemyInfo;
    public Transform HeadTransform { get { return _headTransform; } }
    private HpBarUI _hpUI;

    private float _curHp;
    public float Hp { get { return _curHp; } }
    public int EnemyType { get { return _enemyInfo.Type; } }

    private Vector3 pointToGo;
    private int curPathLevel = 1;

    private float _rotateSpeed = 10.0f;

    protected void Awake()
    {
        GetComponent<Collider>().isTrigger = false;
    }

    protected void Update()
    {
        float progress = Mathf.Abs(GetProgress());
        if (progress >= 0.99)
        {
            pointToGo =  PathManager.Instance.GetNextPoint(curPathLevel++);

            //Success Destination
            if(pointToGo == Vector3.zero)
            {
                GameManager.Instance.OnDamaged(1);
                SpawnManager.Instance.GetBack(_hpUI.gameObject);
                SpawnManager.Instance.GetBack(gameObject);
                return;
            }
        }

        Vector3 position = transform.position;
        Vector3 directionVector = pointToGo - transform.position;
        directionVector.Normalize();
        transform.position += directionVector * _enemyInfo.Speed;
        RotateToRoad(Time.deltaTime);

        _hpUI.SetPosition(transform);
    }

    public float GetProgress()
    {
        float distance = Vector3.Distance(pointToGo, transform.position);
        float totalDistance = PathManager.Instance.GetGapBetweenPoints(curPathLevel);

        return (totalDistance - distance) / totalDistance;
    }

    public void RotateToRoad(float deltaTime)
    {
        Vector3 directionVector = pointToGo - (transform.position + transform.up * 0.5f);
        Vector3 directionXZ = directionVector;

        directionXZ.y = 0;

        Quaternion curRotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionXZ), deltaTime * _rotateSpeed);
        transform.rotation = curRotation;
    }

    private void TakeDamage(float damage)
    {
        _curHp -= damage;
        _hpUI.UpdateHP( _curHp / _enemyInfo.HP);
        if (_curHp <= 0)
        {
            Die();  
        }
    }

    private void Die()
    {
        _curHp = 0;
        GameManager.Instance.GetCoin(_enemyInfo.Reward);
        SpawnManager.Instance.GetBack(_hpUI.gameObject);
        SpawnManager.Instance.GetBack(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Bullet")) return;

        float damage = collision.transform.GetComponent<Bullet>().Power;
        TakeDamage(damage);
    }
    public void Initialize(HpBarUI hpbar, ScriptableEnemy info)
    {
        transform.position = PathManager.Instance.GetFirstPoint();
        curPathLevel = 1;
        pointToGo = PathManager.Instance._wayPoints[1].position;

        _hpUI = hpbar;

        _enemyInfo = info;
        _curHp = _enemyInfo.HP;
        _hpUI.UpdateHP(_curHp / _enemyInfo.HP);
    }

    public void Acitvate(GameObject originalPrefab)
    {
        GetComponent<PoolingObject>().Prefab = originalPrefab;
        gameObject.SetActive(true);
    }
}
