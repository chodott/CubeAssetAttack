using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Sprite _openedSprite;
    [SerializeField]
    private Sprite _lockedSprite;
    [SerializeField]
    private Image _lockedImage;
    [SerializeField]
    TextMeshProUGUI _stageNumTMP;
    private int _num = 0;


    public StageInfoUI StageInfoUI;

    public void SetImage(int num, bool bOpened)
    {
        _num = num;
        _stageNumTMP.text = num.ToString();
        Button button = transform.GetComponent<Button>();

        if (bOpened)
        {
            _image.sprite = _openedSprite;
            _stageNumTMP.gameObject.SetActive(true);
            _lockedImage.gameObject.SetActive(false);
            button.interactable = true;
        }
        else
        {
            _image.sprite = _lockedSprite;
            _stageNumTMP.gameObject.SetActive(false);
            _lockedImage.gameObject.SetActive(true);
            button.interactable = false;
        }
    }

    public void ShowStageInfo()
    {
        StageInfoUI.StageIndex = _num;
        StageInfoUI.gameObject.SetActive(true);
        StageInfoUI.transform.position = transform.position;
        StageInfoUI.transform.SetAsLastSibling();
    }



}
