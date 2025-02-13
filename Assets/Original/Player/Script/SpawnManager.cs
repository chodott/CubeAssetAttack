using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance {  get; private set; }
    private SplineContainer _enemyPath;

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
            ScriptableEnemy spawnEnemyInfo = Database.Instance.GetEnemyInfo(curWave.enemyType);
            GameObject spawnEnemy = spawnEnemyInfo.EnemyPrefab;

            for (int i = 0; i < curWave.monsterCount; ++i)
            {
                GameObject spawnedEnemy = Instantiate(spawnEnemy);
                spawnedEnemy.GetComponent<Enemy>().setPath(_enemyPath);
                spawnedEnemy.GetComponent<Enemy>().SetData(spawnEnemyInfo);
                yield return new WaitForSeconds(curWave.spawnInterval);
            }

            _currentWaveIndex += 1;
            yield return new WaitForSeconds(curWave.waveInterval);
        }
    }
}
