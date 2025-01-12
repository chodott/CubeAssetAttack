using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuildListUI : MonoBehaviour
{
    [SerializeField]
    private List<ScriptableFriend> _friendDataList;
    [SerializeField]
    private GameObject _friendPanelPrefab;
    
    protected void Start()
    {
        foreach(ScriptableFriend data in _friendDataList)
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
