using Unity.VisualScripting;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }
    public Transform[] _wayPoints;

    protected void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        for(int i=0;i<_wayPoints.Length-1;++i)
        {
            _wayPoints[i].LookAt(_wayPoints[i + 1]);
        }
    }

    public Vector3 GetNextPoint(int level)
    {
        level++;
        if (_wayPoints.Length <= level) return Vector3.zero;
        else return _wayPoints[level].position;
    }

    public float GetGapBetweenPoints(int curPoint)
    {
        return Vector3.Distance(_wayPoints[curPoint].position, _wayPoints[curPoint - 1].position);
    }

    public Vector3 GetFirstPoint()
    {
        return _wayPoints[0].position;
    }
}
