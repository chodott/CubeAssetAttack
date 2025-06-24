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

    public void ActivateTowerControlUI(Friend selectedTower)
    {
        towerControlUI.ShowForTower(selectedTower);
    }
    public void DeactivateTowerControlUI()
    {
        towerControlUI.Deactivate();
    }
}
