using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private LoseResultUI _loseResultUI;
    [SerializeField]
    private TowerControlUI towerControlUI; 
    protected void Start()
    {
        GameManager.Instance.LoseStage += ShowLoseResult;
    }

    private void ShowLoseResult()
    {
        _loseResultUI.UpdateUI();
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
