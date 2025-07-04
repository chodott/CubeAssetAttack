using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField]
    private Image[] _starImages; // 별 UI 이미지 배열\
    [SerializeField]
    private Sprite _filledStar; // 채워진 별 스프라이트
    [SerializeField]
    private Sprite _emptyStar;  // 비워진 별 스프라이트
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    [SerializeField]
    private string _winText;
    [SerializeField]
    private string _loseText;

    public void UpdateResultUI(int stageScore)
    {
        if (stageScore == 0)
        {
            SetLoseResult();
        }
        else
        {
            SetWinResult(stageScore);
        }

        gameObject.SetActive(true);
    }

    void SetLoseResult()
    {
        SetResultText(_loseText);
        return;
    }

    private void SetWinResult(int stageScore)
    {
        SetResultText(_winText);
        for (int i = 0; i < stageScore; ++i)
        {
            _starImages[i].sprite = _filledStar;
        }
    }

    private void SetResultText(string text)
    {
        _textMeshPro.text = text;
    }
}
