using UnityEngine;
using UnityEngine.UI;

public class LoseResultUI : MonoBehaviour
{
    [SerializeField]
    private Image[] starImages; // 별 UI 이미지 배열\
    [SerializeField]
    private Sprite filledStar; // 채워진 별 스프라이트
    [SerializeField]
    private Sprite emptyStar;  // 비워진 별 스프라이트

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
