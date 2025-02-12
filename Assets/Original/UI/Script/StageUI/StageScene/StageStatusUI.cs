using TMPro;
using UnityEngine;

public class StageStatusUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _lifeTMP;
    [SerializeField]
    private TextMeshProUGUI _coinTMP;
    protected void Start()
    {
        GameManager.Instance.OnLifeChanged += UpdateLifeUI;
        GameManager.Instance.OnCoinChanged += UpdateCoinUI;
    }

    private void UpdateLifeUI(int lifeCnt)
    {
        _lifeTMP.text = lifeCnt.ToString();
    }

    private void UpdateCoinUI(int lifeCnt)
    {
        _coinTMP.text = lifeCnt.ToString();
    }
}
