using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuildListUI : MonoBehaviour
{
    [SerializeField]
    private List<TowerData> _towerDataList;
    [SerializeField]
    private GameObject _towerPanelPrefab;
    
    protected void Start()
    {
        foreach(TowerData data in _towerDataList)
        {
            GameObject newPanel = Instantiate(_towerPanelPrefab);
            newPanel.transform.SetParent(transform,false);
            newPanel.GetComponent<TowerSelectUI>().Data = data;
        }
    }

    protected void Update()
    {
        
    }
}
