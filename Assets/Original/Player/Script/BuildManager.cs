using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    public FriendTowerData BuildTowerData;

    private Dictionary<BuildPlatform, bool> _buildPlatforms;

    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private SpawnManager _spawnManager;

    private Friend _selectedFriendTower;

    protected void Start()
    {
        SelectionEvents.OnSelected += HandleSelection;

        foreach(var obj in FindObjectsByType<BuildPlatform>(FindObjectsSortMode.None))
        {
            _buildPlatforms.Add(obj, false);
        }
    }

    private void HandleSelection(ISelectable selectableTarget)
    {
        if(selectableTarget is BuildPlatform buildPlatform)
        {
            TryBuildTower(buildPlatform);
        }

        else if(selectableTarget is Friend friendTower)
        {
            _selectedFriendTower = friendTower;
            _uiManager.ActivateTowerControlUI(friendTower);
        }

        else if(selectableTarget is FriendUI friendUI)
        {
            BuildTowerData = friendUI.Data;
        }
    }

    private void TryBuildTower(BuildPlatform platform)
    {
        if (_gameManager.PayCoin(BuildTowerData.COST) == false) return;

        _spawnManager.SpawnTower(BuildTowerData, platform.transform.position);
        SetBuildEffects(false);
    }

    private void SetBuildEffects(bool value)
    {
        foreach (BuildPlatform platform in _buildPlatforms.Keys)
        {
            platform.SetBuildEffect(value);
        }
    }
}
