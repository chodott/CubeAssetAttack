using UnityEngine;
using UnityEngine.UI;

public class LoseResultUI : MonoBehaviour
{
    [SerializeField]
    private Image[] starImages; // �� UI �̹��� �迭\
    [SerializeField]
    private Sprite filledStar; // ä���� �� ��������Ʈ
    [SerializeField]
    private Sprite emptyStar;  // ����� �� ��������Ʈ

    public void UpdateUI()
    {
        int result = SpawnManager.Instance.CurrentWave;
        for(int i=0;i<result;++i)
        {
            starImages[i].sprite = filledStar;
        }
        gameObject.SetActive(true);
    }
}
