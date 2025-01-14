using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    private SplineAnimate _splineAnimate;

    protected void Awake()
    {
        _splineAnimate = GetComponent<SplineAnimate>();
    }
    public void setPath(SplineContainer path)
    {
        _splineAnimate.Container = path;
        _splineAnimate.Play();

    }
}
