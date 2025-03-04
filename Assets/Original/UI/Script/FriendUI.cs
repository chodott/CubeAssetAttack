using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FriendUI : MonoBehaviour
{
    [SerializeField]
    private Image _thumbnail;
    private ScriptableFriend _data;
    public ScriptableFriend Data {
        get { return _data; } 
        set {
            _data = value;
            _nameTMP.text = value.Name;
            _costTMP.text = value.COST.ToString();
            if(Data.Thumbnail != null)
            {
                _thumbnail.sprite = Sprite.Create(Data.Thumbnail, new Rect(0,0,Data.Thumbnail.width,Data.Thumbnail.height), new Vector2(0.5f, 0.5f));
                _thumbnail.preserveAspect = true;
            }
        } 
    }

    [SerializeField]
    private TextMeshProUGUI _nameTMP;
    [SerializeField]
    private TextMeshProUGUI _costTMP;

    public void BuildFriend()
    {
        SpawnManager.Instance.SetBuildEffects(true);
        SpawnManager.Instance.BuildData = _data;
    }

}
