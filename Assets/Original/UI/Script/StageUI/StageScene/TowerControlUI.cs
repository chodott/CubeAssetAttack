using UnityEngine;

public class TowerControlUI : MonoBehaviour
{
    private Friend _selectedTower { get; set; }

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
}
