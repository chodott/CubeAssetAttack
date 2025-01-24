using UnityEngine;
using UnityEngine.UI;

public class StageSelectList : MonoBehaviour
{
    [SerializeField]
    GameObject _stageSelectButtonPrefab;
    [SerializeField]
    StageInfoUI _stageInfoUI;
    protected void Start()
    {
        for(int i=0;i< Database.Instance.StageInfos.Length;++i)
        {
            StageInfo stageInfo = Database.Instance.StageInfos[i];
            GameObject newButton = Instantiate(_stageSelectButtonPrefab);
            StageSelectButton selectButton = newButton.GetComponent<StageSelectButton>();
            selectButton.transform.SetParent(transform, false);
            selectButton.SetImage(stageInfo.stageNum, stageInfo.bClear);

            //Set Position
            RectTransform rectTransform = newButton.GetComponent<RectTransform>();
            rectTransform.position = new Vector2(200.0f * (i+1), rectTransform.position.y);

            //Set InfoUI
            selectButton.StageInfoUI = _stageInfoUI;
        }
    }


}
