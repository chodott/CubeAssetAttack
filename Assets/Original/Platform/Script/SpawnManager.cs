using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance {  get; private set; }
    private SplineContainer _enemyPath;

    [SerializeField]
    private GameObject[] _spawnEnemys; 

    private WaveInfo[] _waveInfos;
    private int _currentWaveIndex = 0;
    public int CurrentWave { get { return _currentWaveIndex; } }


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
        _waveInfos = Database.Instance.GetWaveInfos();
        _enemyPath = GetComponent<SplineContainer>();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        while (_currentWaveIndex < _waveInfos.Length)
        {
            WaveInfo curWave = _waveInfos[_currentWaveIndex];
            GameObject spawnEnemy = _spawnEnemys[curWave.enemyType];
            for (int i = 0; i < curWave.monsterCount; ++i)
            {
                GameObject spawnedEnemy = Instantiate(spawnEnemy);
                spawnedEnemy.GetComponent<Enemy>().setPath(_enemyPath);
                yield return new WaitForSeconds(curWave.spawnInterval);
            }

            _currentWaveIndex += 1;
            yield return new WaitForSeconds(curWave.waveInterval);
        }
    }
}
