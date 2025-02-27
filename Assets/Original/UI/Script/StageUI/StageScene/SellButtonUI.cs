using UnityEngine;

public class SellButtonUI : MonoBehaviour
{
    public void SellFriend()
    {
        SpawnManager.Instance.Sell();
    }
}
