using UnityEngine;

public class Friend : MonoBehaviour
{
    [SerializeField]
    private Weapon _equippedWeapon;
    private Transform _targetTransform;
    private float attackRange = 5.0f;
    void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_targetTransform == null)
        {
            SetTarget();
        }
        else
        {
            _equippedWeapon.Launch(_targetTransform);
        }
    }

    private void SetTarget()
    {
        Vector3 pos = transform.position;
        Collider[] enemys = Physics.OverlapCapsule(pos, pos - transform.up, attackRange);
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
}
