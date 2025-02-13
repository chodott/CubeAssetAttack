using UnityEngine;
using UnityEngine.SceneManagement;

public class Database : MonoBehaviour
{
    public static Database Instance;
    private WaveDataLoader _dataLoader = new WaveDataLoader();

    [SerializeField]
    private ScriptableEnemy[] EnemyInfos;
    public StageInfo[] StageInfos;

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _dataLoader.LoadWaveData(this,"WaveData.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public ScriptableEnemy GetEnemyInfo(int index)
    {
        return EnemyInfos[index];
    }

    public WaveInfo[] GetWaveInfos()
    {
        int index = SceneManager.GetActiveScene().buildIndex-1;
        return StageInfos[index].waveInfos;
    }
}
