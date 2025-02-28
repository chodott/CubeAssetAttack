using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance {  get; private set; }
    private ObjectPool _objectPool;

    //Build
    public Transform BuildPlatformTransform;

    //UI
    [SerializeField]
    private Transform _canvas;
    [SerializeField]
    private GameObject _hpbarPrefab;
    [SerializeField]
    private GameObject _friendPrefab;

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

        _objectPool = GetComponent<ObjectPool>();
        _objectPool.Initialize(_waveInfos);
        _objectPool.SpawnDefault(_hpbarPrefab);
        _objectPool.SpawnDefault(_friendPrefab);

        StartCoroutine(SpawnWave());
    }

    public void Build(ScriptableFriend friendData)
    {
        BuildPlatform buildPlatform = BuildPlatformTransform.GetComponent<BuildPlatform>();
        if (buildPlatform.bCanBuild == false) return;
        if (GameManager.Instance.PayCoin(friendData.COST) == false) return;

        GameObject buildTarget = _objectPool.GetObject(_friendPrefab);
        buildTarget.transform.position = BuildPlatformTransform.position + Vector3.up * 0.25f;
        Friend builtFriend = buildTarget.GetComponent<Friend>();
        builtFriend.Initialize(friendData);
        buildPlatform.BuildOnPlatform(builtFriend);
    }

    public void Sell()
    {
        BuildPlatform buildPlatform = BuildPlatformTransform.GetComponent<BuildPlatform>();
        GameManager.Instance.GetCoin(buildPlatform.SellOnPlatform());
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
                GameObject spawnedGameObject = _objectPool.GetObject(spawnEnemy);
                Enemy spawnedEnemy = spawnedGameObject.GetComponent<Enemy>();
                HpBarUI spawnedHpbarUI = _objectPool.GetObject(_hpbarPrefab).GetComponent<HpBarUI>();
                spawnedHpbarUI.transform.SetParent(_canvas, false);
                spawnedEnemy.Initialize(spawnedHpbarUI, spawnEnemyInfo);
                yield return new WaitForSeconds(curWave.spawnInterval);
            }

            _currentWaveIndex += 1;
            yield return new WaitForSeconds(curWave.waveInterval);
        }
    }

    public void GetBack(GameObject gameObject)
    {
        _objectPool.ReturnObject(gameObject);
    }
}
