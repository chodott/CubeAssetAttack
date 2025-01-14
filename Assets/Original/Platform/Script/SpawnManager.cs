using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance {  get; private set; }
    private SplineContainer _enemyPath;
    public GameObject tempSpawnEnemy;


    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        _enemyPath = GetComponent<SplineContainer>();

        GameObject spawnedObject = Instantiate(tempSpawnEnemy);
        spawnedObject.GetComponent<Enemy>().setPath(_enemyPath);
    }


}
