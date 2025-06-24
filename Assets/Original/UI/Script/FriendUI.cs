using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendUI : MonoBehaviour, ISelectable
{
    [SerializeField]
    private Image _thumbnail;
    private Animator _animator;
    [SerializeField]
    private RectTransform _animationRectTransform;
    private FriendTowerData _data;
    public FriendTowerData Data {
        get { return _data; } 
        set {
            _data = value;
            _nameTMP.text = value.Name;
            _costTMP.text = value.COST.ToString();
            if(Data.Thumbnail != null)
            {
                _thumbnail.sprite =Data.Thumbnail;
                //_thumbnail.preserveAspect = true;
            }
        } 
    }

    [SerializeField]
    private TextMeshProUGUI _nameTMP;
    [SerializeField]
    private TextMeshProUGUI _costTMP;


    protected void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnSelected()
    {
        _animator.SetBool("Active", true);
        SelectionEvents.NotifySelected(this);
    }

    public void OnDeselected()
    {
        _animator.SetBool("Active", false);
    }
}
