using UnityEngine;

public class Friend : MonoBehaviour
{
    [SerializeField]
    private Weapon _equippedWeapon;
    private Transform _targetTransform;
    private float _attackRange = 5.0f;
    private float _rotateSpeed = 10.0f;
    void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        CheckTargetInRange();
        if (_targetTransform == null)
        {
            SetTarget();
        }
        else
        {
            TurnToTarget(Time.deltaTime);
            _equippedWeapon.Launch(_targetTransform);
        }
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
            float value = collider.transform.GetComponent<Enemy>().GetProgress();
            if (maxValue > value) continue;
            maxValue = value;
            _targetTransform = collider.transform;
        }
    }

    private void TurnToTarget(float deltaTime)
    {
        Vector3 directionVector = _targetTransform.position - transform.position;
        directionVector.y = 0;
        Quaternion curRotation =  Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), deltaTime * _rotateSpeed);
        transform.rotation = curRotation;
    }

    private bool CheckTargetInRange()
    {
        if (_targetTransform == null) return false;
        Vector3 position2D = transform.position;
        position2D.y = 0;
        Vector3 targetPosition2D = _targetTransform.position;
        targetPosition2D.y = 0;

        float distance2D = (position2D - targetPosition2D).magnitude;
        if (distance2D < _attackRange) return true;

        _targetTransform = null;
        return false;
    }
}
