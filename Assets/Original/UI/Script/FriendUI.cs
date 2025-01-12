using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendUI : MonoBehaviour
{
    private ScriptableFriend _data;
    public ScriptableFriend Data {
        get { return _data; } 
        set { 
            _nameTextUI.text = value.Name;
            _thumbnail.sprite = Sprite.Create(_data.Thumbnail, new Rect(0,0,100,100), new Vector2(0.5f, 0.5f));
        } }

    [SerializeField]
    private TextMeshProUGUI _nameTextUI;
    [SerializeField]
    private Image _thumbnail;

}
