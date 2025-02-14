using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyListUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPanelPrefab;

    public void SetData(int stageIndex)
    {
        Database database = Database.Instance;
        StageInfo stageInfo = database.StageInfos[stageIndex];
        for(int index = 0; index<stageInfo.waveInfos.Length; ++index)
        {
            WaveInfo waveInfo = stageInfo.waveInfos[index];
            ScriptableEnemy enemyData =  database.GetEnemyInfo(waveInfo.enemyType);
            GameObject newPanel = Instantiate(_enemyPanelPrefab);
            newPanel.transform.SetParent(transform, false);
            newPanel.GetComponent<RectTransform>().position += Vector3.right * (100.0f * index);
            newPanel.GetComponent<Image>().sprite = enemyData.Thumbnail;
        }
    }
}
