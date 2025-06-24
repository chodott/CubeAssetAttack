using UnityEngine;

public class TowerControlUI : MonoBehaviour
{
    private Friend _selectedTower { get; set; }

    protected void Start()
    {
        Friend.OnTowerSold += HandleTowerSold;
    }
    public void ShowForTower(Friend selectedTower)
    {
        _selectedTower = selectedTower;
        Vector3 worldPos = selectedTower.transform.position + (Vector3.up * 1.0f);
        transform.position = Camera.main.WorldToScreenPoint(worldPos);
        gameObject.SetActive(true);
    }

    public void SellFriend()
    {
        _selectedTower.Sell();
    }

    public void UpgradeTower()
    {

    }
    public void Deactivate()
    {
        _selectedTower = null;
        gameObject.SetActive(false);
    }
    private void HandleTowerSold(Friend soldTower)
    {
        if (_selectedTower != soldTower) return;
        Deactivate();
    }
}
