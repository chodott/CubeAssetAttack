using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] 
    private InputActionAsset inputActions;
    private InputAction pointerPositionAction;
    [SerializeField]
    private GameObject _buildListUI;

    private int _buildLayerMask;
    private bool _OnMouseClick = false;

    public UnityEvent<Vector2> ClickUI;

    protected void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected void Start()
    {
        // Action Map과 Action을 가져옴
        var playerInputMap = inputActions.FindActionMap("Player");
        pointerPositionAction = playerInputMap.FindAction("Touch");
        _buildLayerMask = LayerMask.GetMask("BuildPlatform");

        // 입력 활성화
        pointerPositionAction.Enable();

        // 클릭 이벤트 구독
        pointerPositionAction.performed += OnPointerClick;
    }

    private void OnDisable()
    {
        // 입력 비활성화
        pointerPositionAction.Disable();

        pointerPositionAction.performed -= OnPointerClick;
    }

    protected void LateUpdate()
    {
        _OnMouseClick = false;
        Vector2 inputPosition = Vector2.zero;
        //First Check UI Click
        if (Input.GetMouseButtonDown(0))
        {
            inputPosition = Input.mousePosition;
            _OnMouseClick = true;
        }
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == UnityEngine.TouchPhase.Began)
            {
                inputPosition = touch.position;
                _OnMouseClick = true;
            }
        }

        if (_OnMouseClick == false) return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            ClickUI.Invoke(inputPosition);
            return;
        }

        else
        {
            //After Check BuildPlatform Click
            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _buildLayerMask))
            {
                GameObject clickedObject = hit.collider.gameObject;
                SpawnManager.Instance.BuildPlatformTransform = hit.transform;
                SpawnManager.Instance.Build();
            }
        }

    }

    private void OnPointerClick(InputAction.CallbackContext context)
    {
        _OnMouseClick = true;
    }
}
