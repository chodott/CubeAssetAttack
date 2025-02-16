using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private Dictionary<int, Queue<GameObject>> _dictionaryPool = new Dictionary<int ,Queue<GameObject>>();
    private int _initSpawnCnt = 0;

    public void Initialize(WaveInfo[] waveInfos)
    {
        foreach(WaveInfo waveInfo  in waveInfos)
        {
            int enemyType = waveInfo.enemyType;
            GameObject enemyPrefab =  Database.Instance.GetEnemyInfo(enemyType).EnemyPrefab;
            _dictionaryPool.Add(enemyType, new Queue<GameObject>());
            for (int i = 0; i < _initSpawnCnt; i++)
            {
                GameObject newObject = Instantiate(enemyPrefab);
                _dictionaryPool[enemyType].Enqueue(newObject);
            }
        }
    }


    public GameObject GetObject(int type)
    {
        if(_dictionaryPool[type].Count >0) return _dictionaryPool[type].Dequeue();
        else return Instantiate(Database.Instance.GetEnemyInfo(type).EnemyPrefab);
    }

    public void ReturnObject(Enemy enemy)
    {
        _dictionaryPool[enemy.EnemyType].Enqueue(enemy.gameObject);
    }
}
