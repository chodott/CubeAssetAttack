using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance {  get; private set; }
    private ObjectPool _objectPool;

    //UI
    [SerializeField]
    private Transform _canvas;
    [SerializeField]
    private GameObject _hpbarPrefab;
    [SerializeField]
    private GameObject _friendTowerPrefab;

    public Mesh shadowMesh;
    public Material shadowMaterial;
    private List<GameObject> _shadowSpawnTargets = new();

    private WaveInfo[] _waveInfos;
    private int _currentWaveIndex = 0;
    public int CurrentWave { get { return _currentWaveIndex; } }

    private void LateUpdate()
    {
        const int MAX_INSTANCES = 1023;
        List<Matrix4x4> matrices = new();

        for (int index = _shadowSpawnTargets.Count - 1; index >= 0; --index)
        {
            if (_shadowSpawnTargets[index].activeSelf == false)
            {
                _shadowSpawnTargets.RemoveAt(index);
                continue;
            }
            Transform targetTransform = _shadowSpawnTargets[index].transform;
            Vector3 pos = targetTransform.position + Vector3.up * 0.05f;
            Quaternion rot = Quaternion.Euler(0, 0, 0);
            Vector3 scale = Vector3.one * 1.5f;

            matrices.Add(Matrix4x4.TRS(pos, rot, scale));
        }

        for (int i = 0; i < matrices.Count; i += MAX_INSTANCES)
        {
            int count = Mathf.Min(MAX_INSTANCES, matrices.Count - i);
            Graphics.DrawMeshInstanced(shadowMesh, 0, shadowMaterial, matrices.GetRange(i, count));
        }
    }

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

        _objectPool = GetComponent<ObjectPool>();
        _objectPool.Initialize(_waveInfos);
        _objectPool.SpawnDefault(_hpbarPrefab);
        _objectPool.SpawnDefault(_friendTowerPrefab);

        StartCoroutine(SpawnWave());
    }

    public void SpawnTower(FriendTowerData towerData, Vector3 spawnPosition)
    {
        GameObject spawnedGameObject = _objectPool.GetObject(_friendTowerPrefab);
        spawnedGameObject.transform.position = spawnPosition;
    }

    IEnumerator SpawnWave()
    {
        while (_currentWaveIndex < _waveInfos.Length)
        {
            WaveInfo curWave = _waveInfos[_currentWaveIndex];
            ScriptableEnemy spawnEnemyInfo = Database.Instance.GetEnemyInfo(curWave.enemyType);
            GameObject spawnEnemy = spawnEnemyInfo.EnemyPrefab;
            yield return new WaitForSeconds(curWave.waveInterval);

            for (int i = 0; i < curWave.monsterCount; ++i)
            {
                GameObject spawnedGameObject = _objectPool.GetObject(spawnEnemy);
                Enemy spawnedEnemy = spawnedGameObject.GetComponent<Enemy>();
                HpBarUI spawnedHpbarUI = _objectPool.GetObject(_hpbarPrefab).GetComponent<HpBarUI>();
                spawnedHpbarUI.transform.SetParent(_canvas, false);
                spawnedEnemy.Initialize(spawnedHpbarUI, spawnEnemyInfo);
                yield return new WaitForSeconds(curWave.spawnInterval);
            }

            _currentWaveIndex += 1;
        }
    }

    public void GetBack(GameObject gameObject)
    {
        _objectPool.ReturnObject(gameObject);
    }
}
