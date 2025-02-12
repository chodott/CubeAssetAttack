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
            _nameTMP.text = value.Name;
            _costTMP.text = value.COST.ToString();
        } }

    [SerializeField]
    private TextMeshProUGUI _nameTMP;
    [SerializeField]
    private TextMeshProUGUI _costTMP;
    [SerializeField]
    private Image _thumbnail;

    public void BuildFriend()
    {
        BuildManager.Instance.Build(Data);
    }

}
