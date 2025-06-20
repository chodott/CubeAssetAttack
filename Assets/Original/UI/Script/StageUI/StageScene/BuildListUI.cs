using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuildListUI : MonoBehaviour
{
    [SerializeField]
    private List<FriendTowerData> _friendDataList;
    [SerializeField]
    private GameObject _friendPanelPrefab;
    
    protected void Start()
    {
        foreach(FriendTowerData data in _friendDataList)
        {
            GameObject newPanel = Instantiate(_friendPanelPrefab);
            newPanel.transform.SetParent(transform,false);
            newPanel.GetComponent<FriendUI>().Data = data;
        }
    }

    protected void Update()
    {
        
    }
}
