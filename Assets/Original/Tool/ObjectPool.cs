using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<GameObject, Queue<GameObject>> _dictionaryPool = new Dictionary<GameObject ,Queue<GameObject>>();
    private int _initSpawnCnt = 100;

    public void Initialize(WaveInfo[] waveInfos)
    {
        foreach(WaveInfo waveInfo  in waveInfos)
        {
            GameObject enemyPrefab =  Database.Instance.GetEnemyInfo(waveInfo.enemyType).EnemyPrefab;
            SpawnDefault(enemyPrefab);
        }
    }

    public void SpawnDefault(GameObject prefab)
    {
        _dictionaryPool.Add(prefab, new Queue<GameObject>());
        for (int i=0;i< _initSpawnCnt;++i)
        {
            GameObject newObject = Instantiate(prefab);
            newObject.SetActive(false);
            _dictionaryPool[prefab].Enqueue(newObject);
        }
    }


    public GameObject GetObject(GameObject prefab)
    {
        if(!_dictionaryPool.ContainsKey(prefab)) _dictionaryPool.Add(prefab, new Queue<GameObject>());
        GameObject newObject = null;
        if(_dictionaryPool[prefab].Count >0) newObject =  _dictionaryPool[prefab].Dequeue();
        else newObject =  Instantiate(prefab);

        newObject.GetComponent<PoolingObject>().Activate(prefab);
        return newObject;
    }

    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        GameObject prefab = gameObject.GetComponent<PoolingObject>().Prefab;
        _dictionaryPool[prefab].Enqueue(gameObject);
    }
}
