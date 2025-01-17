using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendUI : MonoBehaviour
{
    private ScriptableFriend _data;
    public ScriptableFriend Data {
        get { return _data; } 
        set {
            _data = value;
            _nameTextUI.text = value.Name;
        } }

    [SerializeField]
    private TextMeshProUGUI _nameTextUI;
    [SerializeField]
    private Image _thumbnail;

    public void BuildFriend()
    {
        BuildManager.Instance.Build(Data);
    }

}
