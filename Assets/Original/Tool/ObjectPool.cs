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
            _dictionaryPool.Add(enemyPrefab, new Queue<GameObject>());
            for (int i = 0; i < _initSpawnCnt; i++)
            {
                GameObject newObject = Instantiate(enemyPrefab);
                newObject.SetActive(false);
                _dictionaryPool[enemyPrefab].Enqueue(newObject);
            }
        }
    }


    public GameObject GetObject(GameObject prefab)
    {
        if(!_dictionaryPool.ContainsKey(prefab)) _dictionaryPool.Add(prefab, new Queue<GameObject>()); 
        if(_dictionaryPool[prefab].Count >0) return _dictionaryPool[prefab].Dequeue();
        else return Instantiate(prefab);
    }

    public void ReturnObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        GameObject prefab = gameObject.GetComponent<PoolingObject>().Prefab;
        _dictionaryPool[prefab].Enqueue(gameObject);
    }
}
