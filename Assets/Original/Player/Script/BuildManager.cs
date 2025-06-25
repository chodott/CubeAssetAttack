using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    public TowerData BuildTowerData;

    private Dictionary<BuildPlatform, bool> _platformHasTower = new Dictionary<BuildPlatform, bool>();
    private Dictionary<Tower, BuildPlatform> _towerPlatformMap = new Dictionary<Tower, BuildPlatform>();

    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private UIManager _uiManager;
    [SerializeField]
    private SpawnManager _spawnManager;

    private Tower _selectedTower;

    protected void Start()
    {
        SelectionEvents.OnSelected += HandleSelection;
        Tower.OnTowerSold += ClearPlatform;

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

        TrySelectTower(selectableTarget);
        TrySelectTowerUI(selectableTarget);
    }

    private void TrySelectTower(ISelectable selectableTarget)
    {
        if (selectableTarget is Tower tower)
        {
            _selectedTower = tower;
            _uiManager.ActivateTowerControlUI(tower);
        }
        else
        {
            _uiManager.DeactivateTowerControlUI();
        }
    }

    private void TrySelectTowerUI(ISelectable selectableTarget)
    {
        if (selectableTarget is TowerSelectUI towerSelectUI)
        {
            BuildTowerData = towerSelectUI.Data;
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

        Tower spawnedTower = _spawnManager.SpawnTower(BuildTowerData, platform.SpawnPosition);
        _towerPlatformMap.Add(spawnedTower, platform);
        _platformHasTower[platform] = false;
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

    private void ClearPlatform(Tower tower)
    {
        _platformHasTower[_towerPlatformMap[tower]] = true;
    }
}
