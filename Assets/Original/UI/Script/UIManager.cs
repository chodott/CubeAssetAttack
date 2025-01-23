using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private LoseResultUI _loseResultUI;
    protected void Start()
    {
        GameManager.Instance.LoseStage += ShowLoseResult;
    }

    private void ShowLoseResult()
    {
        _loseResultUI.UpdateUI();
    }
}
