using TMPro;
using UnityEngine;

public class StageInfoUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    protected void Start()
    {
        GameManager.Instance.OnLifeChanged += UpdateLifeUI;
    }

    private void UpdateLifeUI(int lifeCnt)
    {
        _textMeshPro.text = lifeCnt.ToString();
    }
}
