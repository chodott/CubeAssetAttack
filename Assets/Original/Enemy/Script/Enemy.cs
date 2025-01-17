using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Slider _hpUI;
    private SplineAnimate _splineAnimate;
    private float _maxHp;
    private float _hp;
    public float Hp { get { return _hp; } }
    private float _runningTime = 0.0f;
   

    protected void Awake()
    {
        _splineAnimate = GetComponent<SplineAnimate>();
        GetComponent<Collider>().isTrigger = false;
    }

    protected void Start()
    {
        _maxHp = 100.0f;
        _hp = _maxHp;
        _hpUI.value = _hp/_maxHp;
    }

    protected void Update()
    {
        if(_splineAnimate.NormalizedTime >= 1.0f)
        {
            GameManager.Instance.OnDamaged(1);
            Destroy(gameObject);
        }
    }



    public void setPath(SplineContainer path)
    {
        _splineAnimate.Container = path;
        _splineAnimate.Play();
    }

    public float GetProgress()
    {
        _runningTime += Time.deltaTime;
        return _runningTime / _splineAnimate.Duration;
    }

    private void TakeDamage(float damage)
    {
        _hp -= damage;
        _hpUI.value = _hp / _maxHp;
        if (_hp < 0)
        {
            _hp = 0;
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Bullet")) return;

        float damage = collision.transform.GetComponent<Bullet>().Power;
        TakeDamage(damage);
    }
}
