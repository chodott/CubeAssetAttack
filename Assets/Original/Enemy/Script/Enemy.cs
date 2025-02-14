using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Splines;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform _headTransform;
    private ScriptableEnemy _enemyData;
    public Transform HeadTransform { get { return _headTransform; } }
    [SerializeField]
    private Slider _hpUI;
    private SplineAnimate _splineAnimate;
    private float _curHp;
    public float Hp { get { return _curHp; } }
    private float _runningTime = 0.0f;
   

    protected void Awake()
    {
        _splineAnimate = GetComponent<SplineAnimate>();
        GetComponent<Collider>().isTrigger = false;
        _splineAnimate.Loop = SplineAnimate.LoopMode.Once;
    }

    protected void Update()
    {
        if(_splineAnimate.NormalizedTime >= 1.0f)
        {
            GameManager.Instance.OnDamaged(1);
            Destroy(gameObject);
        }
    }

    public void SetData(ScriptableEnemy enemyData)
    {
        _enemyData = enemyData;
        _curHp = _enemyData.HP;
        _hpUI.value = _curHp / _enemyData.HP;
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
        _curHp -= damage;
        _hpUI.value = _curHp / _enemyData.HP;
        if (_curHp <= 0)
        {
            _curHp = 0;
            GameManager.Instance.GetCoin(_enemyData.Reward);
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
