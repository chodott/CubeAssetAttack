using UnityEngine;

public class TowerControlUI : MonoBehaviour
{
    private Tower _selectedTower { get; set; }

    public void ShowForTower(Tower selectedTower)
    {
        _selectedTower = selectedTower;
        Vector3 worldPos = selectedTower.transform.position + (Vector3.up * 1.0f);
        transform.position = Camera.main.WorldToScreenPoint(worldPos);
        gameObject.SetActive(true);
    }

    public void SellTower()
    {
        _selectedTower.Sell();
        Deactivate();
    }

    public void UpgradeTower()
    {

    }
    public void Deactivate()
    {
        _selectedTower = null;
        gameObject.SetActive(false);
    }
}
