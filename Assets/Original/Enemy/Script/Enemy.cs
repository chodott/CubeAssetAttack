using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    private SplineAnimate _splineAnimate;
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
        _hp = 100.0f;
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

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Bullet")) return;

        _hp -= collision.transform.GetComponent<Bullet>().Power;
        if (_hp < 0)
        {
            _hp = 0;
            Destroy(gameObject);
        }
    }
}
