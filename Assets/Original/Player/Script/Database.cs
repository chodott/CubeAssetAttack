using UnityEngine;

public class Database : MonoBehaviour
{
    public static Database Instance;
    private WaveDataLoader _dataLoader = new WaveDataLoader();

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


}
