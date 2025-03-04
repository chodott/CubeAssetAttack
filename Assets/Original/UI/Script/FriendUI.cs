using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendUI : MonoBehaviour
{
    [SerializeField]
    private Image _thumbnail;
    private Animator _animator;
    [SerializeField]
    private RectTransform _animationRectTransform;
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


    protected void Start()
    {
        _animator = GetComponent<Animator>();

        InputManager.Instance.ClickUI.AddListener(SelectFriend);
    }
    public void SelectFriend(Vector2 vec2)
    {
        bool bClicked = RectTransformUtility.RectangleContainsScreenPoint(_animationRectTransform, vec2);
        if(bClicked)
        {
            _animator.SetBool("Active", true);
            SpawnManager.Instance.SetBuildEffects(true);
            SpawnManager.Instance.BuildData = _data;
        }
        else
        {
            _animator.SetBool("Active", false);
            SpawnManager.Instance.SetBuildEffects(false);
            SpawnManager.Instance.BuildData = null;
        }
  
    }

}
