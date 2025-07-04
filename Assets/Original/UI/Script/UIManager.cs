using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ResultUI _loseResultUI;
    [SerializeField]
    private TowerControlUI towerControlUI; 
    protected void Start()
    {
        GameManager.Instance.LoseStage += ShowResultUI;
    }

    private void ShowResultUI(int stageScore)
    {
        _loseResultUI.UpdateResultUI(stageScore);
    }

    public void ActivateTowerControlUI(Tower selectedTower)
    {
        towerControlUI.ShowForTower(selectedTower);
    }
    public void DeactivateTowerControlUI()
    {
        towerControlUI.Deactivate();
    }
}
