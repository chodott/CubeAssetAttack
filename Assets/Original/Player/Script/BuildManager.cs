using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    public FriendTowerData BuildTowerData;

    private Dictionary<BuildPlatform, bool> _platformHasTower = new Dictionary<BuildPlatform, bool>();
    private Dictionary<Friend, BuildPlatform> _towerPlatformMap = new Dictionary<Friend, BuildPlatform>();

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
        Friend.OnTowerSold += ClearPlatform;

        foreach(var obj in FindObjectsByType<BuildPlatform>(FindObjectsSortMode.None))
        {
            _platformHasTower.Add(obj, true);
        }
    }

    private void HandleSelection(ISelectable selectableTarget)
    {
        if(selectableTarget is BuildPlatform buildPlatform)
        {
            TryBuildTower(buildPlatform);
        }

        TrySelectFriendTower(selectableTarget);
        TrySelectFriendTowerUI(selectableTarget);
    }

    private void TrySelectFriendTower(ISelectable selectableTarget)
    {
        if (selectableTarget is Friend friendTower)
        {
            _selectedFriendTower = friendTower;
            _uiManager.ActivateTowerControlUI(friendTower);
        }
        else
        {
            _uiManager.DeactivateTowerControlUI();
        }
    }

    private void TrySelectFriendTowerUI(ISelectable selectableTarget)
    {
        if (selectableTarget is FriendUI friendUI)
        {
            BuildTowerData = friendUI.Data;
            SetBuildEffects(true);
        }
        else
        {
            BuildTowerData = null;
            SetBuildEffects(false);
        }
    }

    private void TryBuildTower(BuildPlatform platform)
    {
        if (_platformHasTower[platform] == false) return;
        if (BuildTowerData == null) return;
        if (_gameManager.PayCoin(BuildTowerData.COST) == false) return;

        Friend spawnedFriendTower = _spawnManager.SpawnTower(BuildTowerData, platform.SpawnPosition);
        _towerPlatformMap.Add(spawnedFriendTower, platform);
        SetBuildEffects(false);
    }

    private void SetBuildEffects(bool value)
    {
        foreach (var platformInfo in _platformHasTower)
        {
            if (platformInfo.Value == false) return;
            platformInfo.Key.SetBuildEffect(value);
        }
    }

    private void ClearPlatform(Friend friendTower)
    {
        _platformHasTower[_towerPlatformMap[friendTower]] = true;
    }
}
